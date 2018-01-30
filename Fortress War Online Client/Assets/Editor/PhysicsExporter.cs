using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using GameCommon.MessageObjects;
using GameCommon.SerializedPhysicsObjects;

public class PhysicsExporter : Editor
{
	[MenuItem("Assets/Photon Server/Export Colliders")]
	public static void ExportColliders()
	{
		try
		{
			var path = EditorUtility.SaveFilePanelInProject("Export Physics Data", "Physics.xml", "xml", "");

			if (path.Length <= 0)
			{
				Debug.Log("Path can't be null.");
				return;
			}

			FileStream file = null;

			if (File.Exists(path))
				File.Delete(path);

			file = File.Create(path);

			XmlSerializer serializer = new XmlSerializer(typeof(BPColliders));
			BPColliders colliders = new BPColliders();

			foreach(var collider in FindObjectsOfType(typeof(Collider)).Cast<Collider>())
			{
				// Box Collider
				var box = collider as BoxCollider;

				if (box != null)
				{
					var center = box.center + box.gameObject.transform.position;

					BPBox bpBox = new BPBox()
					{
						Center = new PositionData(center.x, center.y, center.z),
						HalfExtents = new PositionData(box.size.x / 2f, box.size.y / 2f, box.size.z / 2f),
						Rotation = new PositionData(box.transform.rotation.x,
							box.transform.rotation.y, box.transform.rotation.z, box.transform.rotation.w),
						LocalScale = new PositionData(box.transform.localScale.y, box.transform.localScale.y,
							box.transform.localScale.z)
					};

					colliders.Boxes.Add(bpBox);
				}

				// Capsule Collider
				var capsule = collider as CapsuleCollider;

				if (capsule != null)
				{
					var center = capsule.center + capsule.gameObject.transform.position;

					BPCapsule bpCapsule = new BPCapsule()
					{
						Center = new PositionData(center.x, center.y, center.z),
						Rotation = new PositionData(capsule.transform.rotation.x,
							capsule.transform.rotation.y, capsule.transform.rotation.z, capsule.transform.rotation.w),
						LocalScale = new PositionData(capsule.transform.localScale.y, capsule.transform.localScale.y,
							capsule.transform.localScale.z),
						Height = capsule.height,
						Radius = capsule.radius
					};

					colliders.Capsules.Add(bpCapsule);
				}

				// Sphere Collider
				var sphere = collider as SphereCollider;

				if (sphere != null)
				{
					var center = sphere.center + sphere.gameObject.transform.position;

					BPSphere bpSphere = new BPSphere()
					{
						Center = new PositionData(center.x, center.y, center.z),
						Rotation = new PositionData(sphere.transform.rotation.x,
							sphere.transform.rotation.y, sphere.transform.rotation.z, sphere.transform.rotation.w),
						LocalScale = new PositionData(sphere.transform.localScale.y, sphere.transform.localScale.y,
							sphere.transform.localScale.z),
						Radius = sphere.radius
					};

					colliders.Spheres.Add(bpSphere);
				}

				// Mesh Collider
				var mesh = collider as MeshCollider;

				if (mesh != null)
				{
					var center = mesh.gameObject.transform.position;

					List<PositionData> vertArray = new List<PositionData>(mesh.sharedMesh.vertexCount);

					foreach(var vec in mesh.sharedMesh.vertices)
					{
						vertArray.Add(new PositionData(vec.x, vec.y, vec.z));
					}

					BPMesh bpMesh = new BPMesh()
					{
						Center = new PositionData(center.x, center.y, center.z),
						Rotation = new PositionData(mesh.transform.rotation.x,
							mesh.transform.rotation.y, mesh.transform.rotation.z, mesh.transform.rotation.w),
						LocalScale = new PositionData(mesh.transform.localScale.y, mesh.transform.localScale.y,
							mesh.transform.localScale.z),
						NumTris = mesh.sharedMesh.triangles.GetLength(0),
						NumVert = mesh.sharedMesh.vertexCount,
						Triangles = new List<int>(mesh.sharedMesh.triangles),
						Vertexes = vertArray
					};

					colliders.Meshes.Add(bpMesh);
				}
			}

			serializer.Serialize(file, colliders);
			file.Close();

			Debug.LogFormat("Export colliders success. Path: {0}", path);
		}
		catch(Exception ex)
		{
			Debug.LogErrorFormat("Physics export error: {0}", ex.ToString());
		}
	}
}
