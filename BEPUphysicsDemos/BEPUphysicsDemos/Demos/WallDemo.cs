using System;
using System.IO;
using BEPUphysics;
using BEPUphysics.Collidables;
using BEPUphysics.Entities.Prefabs;
using BEPUphysics.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Vector3 = SharpDX.Vector3;

namespace BEPUphysicsDemos.Demos
{
	/// <summary>
	/// Demo showing a wall of blocks stacked up.
	/// </summary>
	public class WallDemo : StandardDemo
	{
		/// <summary>
		/// Constructs a new demo.
		/// </summary>
		/// <param name="game">Game owning this demo.</param>
		public WallDemo(DemosGame game)
			: base(game)
		{
			foreach(var file in Directory.EnumerateFiles("Mesh"))
			{
				using(var reader = new BinaryReader(File.OpenRead(file)))
				{
					//if(file == @"Mesh\-10-10.bin")
					//	Console.WriteLine();
					var vertCount = reader.ReadInt32();
					var vertexBytes = reader.ReadBytes(vertCount);
					var indexCount = reader.ReadInt32();
					var indexBytes = reader.ReadBytes(indexCount);
					var vertexFloats = new float[vertCount / sizeof(float)];
					var vertices = new Vector3[vertexFloats.Length / 3];
					var indices = new int[indexCount / sizeof(int)];

					Buffer.BlockCopy(vertexBytes, 0, vertexFloats, 0, vertCount);
					Buffer.BlockCopy(indexBytes, 0, indices, 0, indexCount);

					for(var i = 0; i < vertexFloats.Length; i += 3)
						vertices[i / 3] = new Vector3(vertexFloats[i], vertexFloats[i + 1], vertexFloats[i + 2]);

					var mesh = new StaticMesh(vertices, indices);
					Space.Add(mesh);
					game.ModelDrawer.Add(mesh);
				}
			}

			var box = new Box(new Vector3(0, 1250, 0), 100, 5, 100);
			Space.Add(box);

			Space.Remove(kapow);
			//kapow = new Box(new Vector3(11000, 0, 0), 10, 10, 10, 50);
			//Space.Add(kapow);

			Space.ForceUpdater.Gravity = new Vector3(0, -9.81f, 0f) * 10 * 2.5f;
			Space.ForceUpdater.TimeStepSettings.TimeStepDuration = 1 / 33f;
			Space.ForceUpdater.TimeStepSettings.MaximumTimeStepsPerFrame = 3;

			ConfigurationHelper.ApplyScale(Space, 25f);
			CollisionResponseSettings.PenetrationRecoveryStiffness = 0.4f;

			game.Camera.Position = new Microsoft.Xna.Framework.Vector3(0, 1300, 0);
			game.Camera.Speed = 100;
		}

		private MouseState prevMouseState;
		public override void Update(float dt)
		{
			if(Game.MouseInput.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
			{
				RayCastResult hit;
				var pos = Game.Camera.Position;
				var forward = Game.Camera.WorldMatrix.Forward;
				if(Space.RayCast(new SharpDX.Ray(new Vector3(pos.X, pos.Y, pos.Z), new Vector3(forward.X, forward.Y, forward.Z)), 100, out hit))
				{
					var box = new Box(hit.HitData.Location + Vector3.UnitY, 10, 10, 10, 50);
					Space.Add(box);
					Game.ModelDrawer.Add(box);
				}
			}

			base.Update(dt);
			prevMouseState = Game.MouseInput;
		}

		/// <summary>
		/// Gets the name of the simulation.
		/// </summary>
		public override string Name
		{
			get { return "Wall"; }
		}
	}
}