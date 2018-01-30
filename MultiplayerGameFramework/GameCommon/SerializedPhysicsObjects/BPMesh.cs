using System;
using System.Collections.Generic;
using BEPUutilities;

namespace GameCommon.SerializedPhysicsObjects
{
	[Serializable]
	public class BPMesh
	{
		public Vector3 Center { get; set; }
		public Vector3 Rotation { get; set; }
		public Vector3 LocalScale { get; set; }

		public int NumTris { get; set; }
		public int NumVert { get; set; }

		public List<int> Triangles { get; set; }
		public List<Vector3> Vertexes { get; set; }

		public BPMesh()
		{
			Center = new Vector3();
			Rotation = new Vector3();
			LocalScale = new Vector3();
			Triangles = new List<int>();
			Vertexes = new List<Vector3>();
		}
	}
}
