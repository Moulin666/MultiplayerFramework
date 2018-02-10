using UnityEngine;

public class CameraController : MonoBehaviour
{
	#region Public vars

	[Header("Таргет")]
	public Transform Target;

	[Header("Макс./Мин. дистанция от таргета")]
	public float maxViewDestance = 25f;
	public float minViewDistance = 1f;

	[Header("Скорость камеры")]
	public int zoomRate = 30;
	public float cameraTargetHeight = 1f;

	#endregion

	#region Private vars

	private float x = 0.0f;
	private float y = 0.0f;

	private int mouseXSpeedMod = 5;
	private int mouseYSpeedMod = 3;

	private float distance = 10;

	private float desiredDistance;
	private float correctedDistance;
	private float currentDistance;

	#endregion

	#region Unity Methods

	private void Start()
	{
		Vector3 angles = transform.eulerAngles;
		x = angles.x;
		y = angles.y;

		currentDistance = distance;
		desiredDistance = distance;
		correctedDistance = distance;
	}

	private void LateUpdate()
	{
		if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftAlt))
		{
			x += Input.GetAxis("Mouse X") * mouseXSpeedMod;
			y += Input.GetAxis("Mouse Y") * mouseYSpeedMod;
		}

		y = ClampAngle(y, -50, 80);

		Quaternion rotation = Quaternion.Euler(y, x, 0);

		desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
		desiredDistance = Mathf.Clamp(desiredDistance, minViewDistance, maxViewDestance);
		correctedDistance = desiredDistance;
		
		Vector3 position = Target.position - (rotation * Vector3.forward * desiredDistance);
		
		RaycastHit collisionHit;
		Vector3 cameraTargetPosition = new Vector3(Target.position.x, Target.position.y + cameraTargetHeight, Target.position.z);

		bool isCorrected = false;
		if (Physics.Linecast(cameraTargetPosition, position, out collisionHit))
		{
			position = collisionHit.point;
			correctedDistance = Vector3.Distance(cameraTargetPosition, position);
			isCorrected = true;
		}
		
		currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * zoomRate) : correctedDistance;

		position = Target.position - (rotation * Vector3.forward * currentDistance + new Vector3(0, -cameraTargetHeight, 0));

		transform.rotation = rotation;
		transform.position = position;
	}

	#endregion

	#region Methods

	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360)
		{
			angle += 360;
		}
		if (angle > 360)
		{
			angle -= 360;
		}
		return Mathf.Clamp(angle, min, max);
	}

	#endregion
}