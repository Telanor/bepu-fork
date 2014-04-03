using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.DataStructures;
using SlimDX;

namespace BEPUphysics.BroadPhaseSystems.SortAndSweep.Testing
{
    internal class SortAndSweep3D : BroadPhase
    {
        RawList<BroadPhaseEntry> entriesX = new RawList<BroadPhaseEntry>();
        RawList<BroadPhaseEntry> entriesY = new RawList<BroadPhaseEntry>();
        RawList<BroadPhaseEntry> entriesZ = new RawList<BroadPhaseEntry>();

        public override void Add(BroadPhaseEntry entry)
        {
            base.Add(entry);
            //binary search for the approximately correct location.  This helps prevent large first-frame sort times.
            //X Axis:
            int minIndex = 0; //inclusive
            int maxIndex = entriesX.count; //exclusive
            int index = 0;
            while (maxIndex - minIndex > 0)
            {
                index = (maxIndex + minIndex) / 2;
                if (entriesX.Elements[index].boundingBox.Minimum.X > entry.boundingBox.Minimum.X)
                    maxIndex = index;
                else if (entriesX.Elements[index].boundingBox.Minimum.X < entry.boundingBox.Minimum.X)
                    minIndex = ++index;
                else
                    break; //Found an equal value!
            }
            entriesX.Insert(index, entry);

            //Y Axis:
            minIndex = 0; //inclusive
            maxIndex = entriesY.count; //exclusive
            while (maxIndex - minIndex > 0)
            {
                index = (maxIndex + minIndex) / 2;
                if (entriesY.Elements[index].boundingBox.Minimum.Y > entry.boundingBox.Minimum.Y)
                    maxIndex = index;
                else if (entriesY.Elements[index].boundingBox.Minimum.Y < entry.boundingBox.Minimum.Y)
                    minIndex = ++index;
                else
                    break; //Found an equal value!
            }
            entriesY.Insert(index, entry);

            //Z Axis:
            minIndex = 0; //inclusive
            maxIndex = entriesZ.count; //exclusive
            while (maxIndex - minIndex > 0)
            {
                index = (maxIndex + minIndex) / 2;
                if (entriesZ.Elements[index].boundingBox.Minimum.Z > entry.boundingBox.Minimum.Z)
                    maxIndex = index;
                else if (entriesZ.Elements[index].boundingBox.Minimum.Z < entry.boundingBox.Minimum.Z)
                    minIndex = ++index;
                else
                    break; //Found an equal value!
            }
            entriesZ.Insert(index, entry);
        }

        public override void Remove(BroadPhaseEntry entry)
        {
            base.Remove(entry);
            entriesX.Remove(entry);
            entriesY.Remove(entry);
            entriesZ.Remove(entry);
        }

        protected override void UpdateMultithreaded()
        {
            UpdateSingleThreaded();
        }

        HashSet<BroadPhaseOverlap> overlapCandidatesX = new HashSet<BroadPhaseOverlap>();
        HashSet<BroadPhaseOverlap> overlapCandidatesY = new HashSet<BroadPhaseOverlap>();

        protected override void UpdateSingleThreaded()
        {
            overlapCandidatesX.Clear();
            overlapCandidatesY.Clear();
            Overlaps.Clear();

            //Sort along x axis using insertion sort; the list will be nearly sorted, so very few swaps are necessary.
            for (int i = 1; i < entriesX.count; i++)
            {
                var entry = entriesX.Elements[i];
                for (int j = i - 1; j >= 0; j--)
                {
                    if (entry.boundingBox.Minimum.X < entriesX.Elements[j].boundingBox.Minimum.X)
                    {
                        entriesX.Elements[j + 1] = entriesX.Elements[j];
                        entriesX.Elements[j] = entry;
                    }
                    else
                        break;
                }

            }
            //Sort along y axis using insertion sort; the list will be nearly sorted, so very few swaps are necessary.
            for (int i = 1; i < entriesY.count; i++)
            {
                var entry = entriesY.Elements[i];
                for (int j = i - 1; j >= 0; j--)
                {
                    if (entry.boundingBox.Minimum.Y < entriesY.Elements[j].boundingBox.Minimum.Y)
                    {
                        entriesY.Elements[j + 1] = entriesY.Elements[j];
                        entriesY.Elements[j] = entry;
                    }
                    else
                        break;
                }

            }
            //Sort along z axis using insertion sort; the list will be nearly sorted, so very few swaps are necessary.
            for (int i = 1; i < entriesZ.count; i++)
            {
                var entry = entriesZ.Elements[i];
                for (int j = i - 1; j >= 0; j--)
                {
                    if (entry.boundingBox.Minimum.Z < entriesZ.Elements[j].boundingBox.Minimum.Z)
                    {
                        entriesZ.Elements[j + 1] = entriesZ.Elements[j];
                        entriesZ.Elements[j] = entry;
                    }
                    else
                        break;
                }

            }

            //Hash-set based sweeping is way too slow.  3D sap is really best suited to an incremental approach.

            //Sweep the list looking for overlaps.
            //Sweep the X axis first; in this phase, add overlaps to the hash set if they exist.
            for (int i = 0; i < entriesX.count; i++)
            {
                BoundingBox a = entriesX.Elements[i].boundingBox;
                for (int j = i + 1; j < entriesX.count && a.Maximum.X > entriesX.Elements[j].boundingBox.Minimum.X; j++)
                {
                    overlapCandidatesX.Add(new BroadPhaseOverlap(entriesX.Elements[i], entriesX.Elements[j]));
                }
            }
            //Sweep the Y axis second; same thing
            for (int i = 0; i < entriesY.count; i++)
            {
                BoundingBox a = entriesY.Elements[i].boundingBox;
                for (int j = i + 1; j < entriesY.count && a.Maximum.Y > entriesY.Elements[j].boundingBox.Minimum.Y; j++)
                {
                    overlapCandidatesY.Add(new BroadPhaseOverlap(entriesY.Elements[i], entriesY.Elements[j]));
                }
            }
            //Sweep the Z axis last
            for (int i = 0; i < entriesZ.count; i++)
            {
                BoundingBox a = entriesZ.Elements[i].boundingBox;
                for (int j = i + 1; j < entriesZ.count && a.Maximum.Z > entriesZ.Elements[j].boundingBox.Minimum.Z; j++)
                {
                    var overlap = new BroadPhaseOverlap(entriesZ.Elements[i], entriesZ.Elements[j]);
                    if (overlapCandidatesX.Contains(overlap) && overlapCandidatesY.Contains(overlap))
                        TryToAddOverlap(entriesZ.Elements[i], entriesZ.Elements[j]);
                }
            }
        }

    }
}
