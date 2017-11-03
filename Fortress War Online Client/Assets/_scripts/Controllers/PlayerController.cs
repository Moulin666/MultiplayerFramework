using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
	#region Public Variables

	//public float InterpolationSpeed = 0.001f;

	#endregion

	#region Private Variables

	// Player objects
	//private GameObject _playerModel;
	private Camera _camera;
	private Animator _animator;
	private NavMeshAgent _agent;

	private GameObject _playerTarget;
	private Vector3 _target;

	private float _playerSpeed = 3f;

	// Network
	private float _periodServerRPC = 0.02f;
	private float _lastTimeServerRPC = 0f;

	#endregion

	#region UnityMethods

	private void Start()
	{
		//if (_playerModel == null)
		//{
			//_playerModel = Network.Instantiate (Resources.Load ("PlayerPrefabs/Player"), transform.position, transform.rotation, 0) as GameObject;
			//_playerModel.transform.SetParent(transform);
			//_animator = _playerModel.GetComponent<Animator>();
			//if (_animator == null)
			//	Debug.LogError("Player model not exist Animator.");

		
			_camera = transform.Find("Main Camera").GetComponent<Camera>();
		if (!isLocalPlayer)
			_camera.gameObject.SetActive(false);
		//}

		transform.position = new Vector3(0, 4.5f, 0);
		_target = transform.position;
		_agent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		UpdateMoveLogic();
	}

	private void FixedUpdate()
	{
		if (isServer)
		{
			_agent.speed = _playerSpeed;
			_agent.destination = _target;
			
			if (_agent.remainingDistance > _agent.stoppingDistance & _agent.remainingDistance <= 100)
			{
				_agent.isStopped = false;
			}
			else
				_agent.isStopped = true;

			if (_lastTimeServerRPC + _periodServerRPC < Time.time)
			{
				RpcUpdatePlayerPosition(transform.position, transform.rotation);

				_lastTimeServerRPC = Time.time;
			}
		}
	}

	#endregion

	#region MyMethods

	private void UpdateMoveLogic()
	{
		if(isLocalPlayer)
		{
			Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Input.GetMouseButtonDown(0))
			{
				if (Physics.Raycast(ray, out hit))
				{
					GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = hit.point;
					_target = hit.point;
					CmdPlayerMove(_target);
				}
			}
		}
	}

	[ClientRpc(channel = 0)]
	private void RpcUpdatePlayerPosition(Vector3 newPos, Quaternion newRot)
	{
		if (isClient)
		{
			if (transform.position != newPos)
				//transform.position = Vector3.Lerp(transform.position, newPos, InterpolationSpeed * Time.deltaTime);
				transform.position = newPos;
			if (transform.rotation != newRot)
				transform.rotation = newRot;
		}
	}

	#endregion

	#region Network Commands

	[Command(channel = 0)]
	private void CmdPlayerMove(Vector3 target)
	{
		if (isServer)
		{
			_target = target;
		}
	}

	#endregion
}
