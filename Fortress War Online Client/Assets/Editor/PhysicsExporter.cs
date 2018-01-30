using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using GameCommon.SerializedPhysicsObjects;

using Vector3 = BEPUutilities.Vector3;

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
						Center = new Vector3(center.x, center.y, center.z),
						HalfExtents = new Vector3(box.size.x / 2f, box.size.y / 2f, box.size.z / 2f),
						Rotation = new Vector3(box.transform.rotation.eulerAngles.x,
							box.transform.rotation.eulerAngles.y, box.transform.rotation.eulerAngles.z),
						LocalScale = new Vector3(box.transform.localScale.y, box.transform.localScale.y,
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
						Center = new Vector3(center.x, center.y, center.z),
						Rotation = new Vector3(capsule.transform.rotation.eulerAngles.x,
							capsule.transform.rotation.eulerAngles.y, capsule.transform.rotation.eulerAngles.z),
						LocalScale = new Vector3(capsule.transform.localScale.y, capsule.transform.localScale.y,
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
						Center = new Vector3(center.x, center.y, center.z),
						Rotation = new Vector3(sphere.transform.rotation.eulerAngles.x,
							sphere.transform.rotation.eulerAngles.y, sphere.transform.rotation.eulerAngles.z),
						LocalScale = new Vector3(sphere.transform.localScale.y, sphere.transform.localScale.y,
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

					List<Vector3> vertArray = new List<Vector3>(mesh.sharedMesh.vertexCount);

					foreach(var vec in mesh.sharedMesh.vertices)
					{
						vertArray.Add(new Vector3(vec.x, vec.y, vec.z));
					}

					BPMesh bpMesh = new BPMesh()
					{
						Center = new Vector3(center.x, center.y, center.z),
						Rotation = new Vector3(mesh.transform.rotation.eulerAngles.x,
							mesh.transform.rotation.eulerAngles.y, mesh.transform.rotation.eulerAngles.z),
						LocalScale = new Vector3(mesh.transform.localScale.y, mesh.transform.localScale.y,
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
