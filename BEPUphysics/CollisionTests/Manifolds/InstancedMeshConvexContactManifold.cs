﻿using System;
using BEPUphysics.Collidables;
using BEPUphysics.Collidables.MobileCollidables;
using SharpDX;
using BEPUphysics.DataStructures;
using BEPUphysics.MathExtensions;
using BEPUphysics.ResourceManagement;
using BEPUphysics.CollisionTests.CollisionAlgorithms;

namespace BEPUphysics.CollisionTests.Manifolds
{
    ///<summary>
    /// Manages persistent contacts between a convex and an instanced mesh.
    ///</summary>
    public class InstancedMeshConvexContactManifold : InstancedMeshContactManifold
    {

        UnsafeResourcePool<TriangleConvexPairTester> testerPool = new UnsafeResourcePool<TriangleConvexPairTester>();
        protected override void GiveBackTester(CollisionAlgorithms.TrianglePairTester tester)
        {
            testerPool.GiveBack((TriangleConvexPairTester)tester);
        }

        protected override CollisionAlgorithms.TrianglePairTester GetTester()
        {
            return testerPool.Take();
        }

    }
}
