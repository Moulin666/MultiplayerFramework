using System;
using System.Collections.Generic;

namespace GameCommon.SerializedPhysicsObjects
{
	[Serializable]
	public class BPColliders
	{
		public List<BPBox> Boxes { get; set; }
		public List<BPSphere> Spheres { get; set; }
		public List<BPCapsule> Capsules { get; set; }
		public List<BPMesh> Meshes { get; set; }

		public BPColliders()
		{
			Boxes = new List<BPBox>();
			Spheres = new List<BPSphere>();
			Capsules = new List<BPCapsule>();
			Meshes = new List<BPMesh>();
		}
	}
}
