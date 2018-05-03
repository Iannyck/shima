using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarGoTo : Pathfinding {


	/// <summary>
	/// The name of the room to go.
	/// </summary>
	private string roomToGo;

	/// <summary>
	/// The room point.
	/// </summary>
	private GameObject targetRoomPoint;

	/// <summary>
	/// Indicate if the current targetRoomPoint is the final target room point.
	/// </summary>
	private bool isFinalTargetRoomPoint;

	/// <summary>
	/// Indicates if the path has been init.
	/// </summary>
	private bool isInit = false;

	/// <summary>
	/// Stores the time spent on the current node. This values is used to detect if the avatar rigidbody is blocked by something.
	/// </summary>
	private float currentNodeTimeSpent;

	/// <summary>
	/// The maximum time that the avatar can spent on a node. This values is used to detect if the avatar rigidbody is blocked by something. 
	/// </summary>
	public float maximumTimeSpentByNode = 0.75f;

	/// <summary>
	/// Store the previous nodes used by the avatar.
	/// </summary>
	private HashSet<Node> previousNodes;

	/// <summary>
	/// The previous node.
	/// </summary>
	private Node previousNode;

	/// <summary>
	/// The walk speed of the avatar to go to the room.
	/// </summary>
	public float walkSpeed = 0.15f;

	/// <summary>
	/// The run speed of the avatar to go to the room.
	/// </summary>
	public float runSpeed = 1.0f;

	/// <summary>
	/// The turn smoothing.
	/// </summary>
	public float turnSmoothing = 30.0f;

	/// <summary>
	/// The speed damp time.
	/// </summary>
	public float speedDampTime = 0.1f;

	/// <summary>
	/// The effective speed.
	/// </summary>
	private float effectiveSpeed;

	/// <summary>
	/// The last direction.
	/// </summary>
	private Vector3 lastDirection;

	/// <summary>
	/// The animation.
	/// </summary>
	private Animator animator;

	/// <summary>
	/// The speed float.
	/// </summary>
	private int speedFloat;

	/// <summary>
	/// The h float.
	/// </summary>
	private int hFloat;

	/// <summary>
	/// The v float.
	/// </summary>
	private int vFloat;

	/// <summary>
	/// The grounded bool.
	/// </summary>
	private int groundedBool;

	/// <summary>
	/// The horizontal.
	/// </summary>
	private float horizontal;

	/// <summary>
	/// The vertical.
	/// </summary>
	private float vertical;

	/// <summary>
	/// Indicates if the avatar is running or not.
	/// </summary>
	private bool isRunning;

	/// <summary>
	/// Indicates if the avatar is moving or not.
	/// </summary>
	private bool isMoving;

	/// <summary>
	/// The waiting delay for the avatar for a door or drawer.
	/// </summary>
	public float waitingDelay = 3.0f;

	/// <summary>
	/// The current waiting delay.
	/// </summary>
	private float currentWaitingDelay;

	/// <summary>
	/// The wait.
	/// </summary>
	private bool wait;

	private bool mustMove;

	private Collider unwalkableCollider;

	private AvatarMotion avatarMotion;

	// Use this for initialization
	void Start () {
		avatarMotion = GetComponent<AvatarMotion> ();
		previousNodes = new HashSet<Node> ();
		currentWaitingDelay = waitingDelay;
		wait = false;
		mustMove = false;
//		if (animator == null) {
//			animator = GetComponent<Animator> ();
//			speedFloat = Animator.StringToHash ("Speed");
//			hFloat = Animator.StringToHash ("H");
//			vFloat = Animator.StringToHash ("V");
//
//			groundedBool = Animator.StringToHash ("Grounded");
//		}
	}

	// Update is called once per frame
	void Update () {
		if(mustMove){
			if (IsTargetRoomReached ()) {
				if (isFinalTargetRoomPoint) {
					horizontal = 0;
					vertical = 0;
					mustMove = false;
					avatarMotion.DirectUpdate (vertical, horizontal);
//					Debug.Log ("end ");
				} else {
					GetPath ();
				}
			} else {
				if (!wait) {
					//					Debug.Log ("Moi move " + BName);
					if (Path == null)
						GetPath ();
					else {
						GetHV ();
//						animator.SetFloat (hFloat, vFloat);
//						Debug.Log ("Moi move "+horizontal+" , "+vertical);
						avatarMotion.DirectUpdate (vertical, horizontal);
					}
				} else {
					currentWaitingDelay -= Time.deltaTime;
					if(currentWaitingDelay >= 0.0f){
						wait = false;
						currentWaitingDelay = waitingDelay;
					}
				}
			}
		}
	}

	public bool MustMove {
		get {
			return this.mustMove;
		}
	}

	public void GoTo(string location) {
		roomToGo = location;
//		Debug.Log ("Looking for path");
		GetPath ();
//		Debug.Log ("Looking for path ended: "+Path.Count);
	}

	private GameObject GetTargetRoomPoint() {
		AvatarState avatarState = GetComponent<AvatarState> ();
		targetRoomPoint = avatarState.GetRoomPoint (roomToGo);
		return targetRoomPoint;
	}

	private void StoreNode(Node nodeToStore){
		previousNodes.Add(nodeToStore);
		previousNode = nodeToStore;
	}

	private void GetPath() {
		currentNodeTimeSpent = maximumTimeSpentByNode;
		AvatarState avatarState = GetComponent<AvatarState> ();
		targetRoomPoint = avatarState.GetRoomPoint (roomToGo);
//		Debug.Log ("a: "+targetRoomPoint);
		if (targetRoomPoint != null) {
//			Debug.Log (transform.position + " and go to " + targetRoomPoint.transform.position);
			FindPath (transform.position, targetRoomPoint.transform.position);
			this.mustMove = true;
			if (Path != null && Path.Count == 0) {
				isFinalTargetRoomPoint = true;
			}
//			else {
//				Debug.Log ("Cannot move");
//			}
		} else {
			Debug.Log ("The room " + roomToGo +" does not exist");
		}
	}

	/// <summary>
	/// Determines whether the target room is reached.
	/// </summary>
	/// <returns><c>true</c> if the target room is reached otherwise, <c>false</c>.</returns>
	public bool IsTargetRoomReached() {
		if (Path != null && Path.Count == 0)
			return true;
		return false;
	}

	/// <summary>
	/// Determines whether the is avatar be blocked.
	/// </summary>
	/// <returns><c>true</c> if the avatar is blocked; otherwise, <c>false</c>.</returns>
	private bool IsAvatarBeBlocked() {
		if (currentNodeTimeSpent <= 0f)
			return true;
		return false;
	}

//	private OpeningClosingDoor doorNextToAvatar() {
//		List<GameObject> uiObjects = objectDetection.ListeDetection;
//		foreach(GameObject uiObject in uiObjects) {
//			if (uiObject.name.Contains ("Door")) {
//				return uiObject.GetComponent<OpeningClosingDoor> ();
//			}
//		}
//		return null;
//	}

	void GetHV() {
		Vector3 nextStep;
		if (Path [0] != null) {
//			Debug.Log ("GetHV");
			if (Vector3.Distance(transform.position, Path[0]) < 0.1F) {
				Path.RemoveAt (0);
				currentNodeTimeSpent = maximumTimeSpentByNode;
				if (Path.Count > 0) {
//					Debug.Log ("GetHV > 0");
					nextStep = Path [0];
					horizontal = nextStep.x - transform.position.x;
					vertical = nextStep.z - transform.position.z;
				} else {
//					Debug.Log ("GetHV else");
					horizontal = 0;
					vertical = 0;
				}
			} else {
//				Debug.Log ("GetHV HV");
//				if(Vector3.Distance(transform.position, previousNode) < 0.2F)
//					currentNodeTimeSpent -= Time.deltaTime;
//				if (IsAvatarBeBlocked ()) {
//					Node newNode = aStar.GetReplacementNode (previousNode, path [0]);
//					currentNodeTimeSpent = maximumTimeSpentByNode;
//					Path [0] = newNode;
//				}
				nextStep = Path [0];

				horizontal = nextStep.x - transform.position.x;
				vertical = nextStep.z - transform.position.z;
			}
//			isRunning = false;
//			isMoving = Mathf.Abs (horizontal) > 0.1 || Mathf.Abs (vertical) > 0.1;
		}
	}

//	public Node GetReplacementNode(Node currentNode, Node nextNode) {
//		List<Node> currentNodeNeighbours = grid.GetNeighbours (currentNode);
//		List<Node> nextNodeNeighbours = grid.GetNeighbours (nextNode);
//		foreach(Node node in currentNodeNeighbours) {
//			if(node.walkable) {
//				if(IsNodeInNeighbours(node,nextNodeNeighbours)) {
//					return node;
//				}
//			}
//		}
//		return null;
//	}

	/// <summary>
	/// Movements the management.
	/// </summary>
	/// <param name="horizontal">Horizontal.</param>
	/// <param name="vertical">Vertical.</param>
	/// <param name="running">If set to <c>true</c> running.</param>
//	void MovementManagement(float horizontal, float vertical, bool running)
//	{
//		Rotating(horizontal, vertical);
//
//		if(isMoving)
//		{
//			if (running)
//			{
//				effectiveSpeed = runSpeed;
//			}
//			else
//			{
//				effectiveSpeed = walkSpeed;
//			}
//
//			animator.SetFloat(speedFloat, effectiveSpeed, speedDampTime, Time.deltaTime);
//		}
//		else
//		{
//			effectiveSpeed = 0f;
//			animator.SetFloat(speedFloat, 0f);
//		}
//		GetComponent<Rigidbody>().AddForce(Vector3.forward * effectiveSpeed);
//	}

	/// <summary>
	/// Rotating the specified horizontal and vertical.
	/// </summary>
	/// <param name="horizontal">Horizontal.</param>
	/// <param name="vertical">Vertical.</param>
//	void Rotating(float horizontal, float vertical)
//	{
//		if((isMoving) )
//		{
//			Vector3 direction = path [0].position - transform.position;
//			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation (direction), turnSmoothing * Time.deltaTime);
//		}
//	}	

	/// <summary>
	/// Repositioning this instance.
	/// </summary>
//	private void Repositioning()
//	{
//		Vector3 repositioning = lastDirection;
//		if(repositioning != Vector3.zero)
//		{
//			repositioning.y = 0;
//			Quaternion targetRotation = Quaternion.LookRotation (repositioning, Vector3.up);
//			Quaternion newRotation = Quaternion.Slerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);
//			GetComponent<Rigidbody>().MoveRotation (newRotation);
//		}
//	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == 0 || other.gameObject.layer == 11) {
			unwalkableCollider = other;
		}
	}

	//	void OnTriggerExit(Collider other)
	//	{
	//		if((unwalkableCollider != null) && (other.gameObject == unwalkableCollider.gameObject)) {
	//			unwalkableCollider = null;
	//		}
	//	}
}
