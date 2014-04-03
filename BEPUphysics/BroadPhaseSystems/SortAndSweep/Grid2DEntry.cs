using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEPUphysics.BroadPhaseEntries;

namespace BEPUphysics.BroadPhaseSystems.SortAndSweep
{
    class Grid2DEntry
    {
        internal void Initialize(BroadPhaseEntry entry)
        {
            this.item = entry;
            Grid2DSortAndSweep.ComputeCell(ref entry.boundingBox.Minimum, out previousMin);
            Grid2DSortAndSweep.ComputeCell(ref entry.boundingBox.Maximum, out previousMax);
        }


        internal BroadPhaseEntry item;
        internal Int2 previousMin;
        internal Int2 previousMax;
    }
}
