﻿using System;
using GameCommon.MessageObjects;

namespace GameCommon.SerializedPhysicsObjects
{
	[Serializable]
	public class BPSphere
	{
		public PositionData Center { get; set; }
		public PositionData Rotation { get; set; }
		public PositionData LocalScale { get; set; }

		public float Radius { get; set; }

		public BPSphere()
		{
			Center = new PositionData();
			Rotation = new PositionData();
			LocalScale = new PositionData();
		}
	}
}
