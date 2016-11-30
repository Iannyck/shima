using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoToBehaviour : AbstractBehaviour {

	public GameObject smartHomeServer;

	/// <summary>
	/// The name of the room to go.
	/// </summary>
	public string roomToGo;

	/// <summary>
	/// The room point.
	/// </summary>
	private GameObject targetRoomPoint;

	/// <summary>
	/// Astar game object.
	/// </summary>
	public GameObject aStarGameObject;

	/// <summary>
	/// Indicate if the current targetRoomPoint is the final target room point.
	/// </summary>
	private bool isFinalTargetRoomPoint;

	/// <summary>
	/// Indicates if the path has been init.
	/// </summary>
	private bool isInit = false;

	/// <summary>
	/// The path to use to go to the room.
	/// </summary>
	private List<Node> path;

	/// <summary>
	/// The avatar rigidbody.
	/// </summary>
	public Rigidbody avatarRigidbody;

	/// <summary>
	/// Astar algorithms.
	/// </summary>
	private PathFinding aStar;

	/// <summary>
	/// The object detection script. It is used to detect doors.
	/// </summary>
	private DynamicObjectDetection objectDetection;

	/// <summary>
	/// Stores the time spent on the current node. This values is used to detect if the avatar rigidbody is blocked by something.
	/// </summary>
	private float currentNodeTimeSpent;

	/// <summary>
	/// The maximum time that the avatar can spent on a node. This values is used to detect if the avatar rigidbody is blocked by something. 
	/// </summary>
	public float maximumTimeSpentByNode = 2f;

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

	/// <summary>
	/// Init this instance.
	/// </summary>
	public override void Init (){
//		Debug.Log ("Init "+ BName);
		if (smartHomeServer != null) {
			SensorsGUI gui = smartHomeServer.GetComponent<SensorsGUI> ();
			gui.SetDebugText (8, BName);
		}
		objectDetection = GetComponent<DynamicObjectDetection> ();
		if (aStarGameObject == null) {
			aStarGameObject = GameObject.Find ("AStar");
			if (aStarGameObject != null) {
				aStar = aStarGameObject.GetComponent<PathFinding> ();
				previousNodes = new HashSet<Node> ();
			}
		} else if (aStar == null) {
			aStar = aStarGameObject.GetComponent<PathFinding> ();
			previousNodes = new HashSet<Node> ();
		}
		if (animator == null) {
			animator = GetComponent<Animator> ();
			speedFloat = Animator.StringToHash ("Speed");
			hFloat = Animator.StringToHash ("H");
			vFloat = Animator.StringToHash ("V");

			groundedBool = Animator.StringToHash ("Grounded");
		}
		if(animator != null && aStar != null)
			isInit = true;
		currentWaitingDelay = waitingDelay;
		wait = false;
	}

	public override State Execute ()
	{
		if (isInit) {
//			Debug.Log ("Moi "+ BName);
			if (IsTargetRoomReached ()) {
				if (isFinalTargetRoomPoint) {
					horizontal = 0;
					vertical = 0;
					animator.SetFloat (hFloat, horizontal);
					animator.SetFloat (vFloat, vertical);

					animator.SetBool (groundedBool, true);
					MovementManagement (horizontal, vertical, isRunning);
					return State.Suceeded;
				} else {
					GetPath ();
				}
			}
			else {
				if (!wait) {
//					Debug.Log ("Moi move " + BName);
					if (path == null)
						GetPath ();
					else {
						GetHV ();
						animator.SetFloat (hFloat, horizontal);
						animator.SetFloat (vFloat, vertical);

						animator.SetBool (groundedBool, true);
						MovementManagement (horizontal, vertical, isRunning);
					}
				} else {
					currentWaitingDelay -= Time.deltaTime;
					if(currentWaitingDelay >= 0.0f){
						wait = false;
						currentWaitingDelay = waitingDelay;
					}
				}
			}
		} else {
			GetPath();
		}
		return State.Running;
	}

	private GameObject GetTargetRoomPoint() {
		MindMap mindMap = GetComponent<MindMap> ();
		targetRoomPoint = mindMap.GetRoomPoint (roomToGo);
		return targetRoomPoint;
	}

	private void StoreNode(Node nodeToStore){
		previousNodes.Add(nodeToStore);
		previousNode = nodeToStore;
	}

	private void GetPath() {
		if (aStar == null) {
			Init ();
		} else {
			StoreNode(aStar.GetCurrentNode (transform.position));
			currentNodeTimeSpent = maximumTimeSpentByNode;
			MindMap mindMap = GetComponent<MindMap> ();
			targetRoomPoint = mindMap.GetRoomPoint (roomToGo);
			if (targetRoomPoint != null) {
				path = aStar.FindPath (transform.position, targetRoomPoint.transform.position);
				if (path != null) {
					isFinalTargetRoomPoint = true;
					aStar.SetPath (path);
//					Debug.Log ("Path OK");
				}
				else {
					Debug.Log ("Cannot move");
				}
			} else {
				Debug.Log ("The room for " + roomToGo +"does not exist");
			}
		}
	}

	/// <summary>
	/// Determines whether the target room is reached.
	/// </summary>
	/// <returns><c>true</c> if the target room is reached otherwise, <c>false</c>.</returns>
	private bool IsTargetRoomReached() {
		if (path != null && path.Count == 0)
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

	private OpeningClosingDoor doorNextToAvatar() {
		List<GameObject> uiObjects = objectDetection.ListeDetection;
		foreach(GameObject uiObject in uiObjects) {
			if (uiObject.name.Contains ("Door")) {
				return uiObject.GetComponent<OpeningClosingDoor> ();
			}
		}
		return null;
	}

	void GetHV() {
		Vector3 nextStep;
		if (path [0] != null) {
			if (aStar.IsOnThisNode (transform.position, path [0])) {
				StoreNode(path[0]);
				path.RemoveAt (0);
				currentNodeTimeSpent = maximumTimeSpentByNode;
				if (path.Count > 0) {
					nextStep = path [0].position;
					horizontal = nextStep.x - transform.position.x;
					vertical = nextStep.z - transform.position.z;
				} else {
					horizontal = 0;
					vertical = 0;
				}
			} else {
				if(aStar.IsOnThisNode(transform.position, previousNode))
					currentNodeTimeSpent -= Time.deltaTime;
				if (IsAvatarBeBlocked ()) {
					OpeningClosingDoor openingClosingDoorScript = doorNextToAvatar();
					if (openingClosingDoorScript == null) {
						Node newNode = aStar.GetReplacementNode (previousNode, path [0]);
						currentNodeTimeSpent = maximumTimeSpentByNode;
						path [0] = newNode;
						aStar.SetPath (path);
					} else {
						openingClosingDoorScript.PlayAnim ();
						wait = true;
						currentNodeTimeSpent = maximumTimeSpentByNode;
					}
				}
				nextStep = path [0].position;

				horizontal = nextStep.x - transform.position.x;
				vertical = nextStep.z - transform.position.z;
			}
			isRunning = false;
			isMoving = Mathf.Abs (horizontal) > 0.1 || Mathf.Abs (vertical) > 0.1;
		}
	}

	/// <summary>
	/// Movements the management.
	/// </summary>
	/// <param name="horizontal">Horizontal.</param>
	/// <param name="vertical">Vertical.</param>
	/// <param name="running">If set to <c>true</c> running.</param>
	void MovementManagement(float horizontal, float vertical, bool running)
	{
		Rotating(horizontal, vertical);

		if(isMoving)
		{
			if (running)
			{
				effectiveSpeed = runSpeed;
			}
			else
			{
				effectiveSpeed = walkSpeed;
			}

			animator.SetFloat(speedFloat, effectiveSpeed, speedDampTime, Time.deltaTime);
		}
		else
		{
			effectiveSpeed = 0f;
			animator.SetFloat(speedFloat, 0f);
		}
		GetComponent<Rigidbody>().AddForce(Vector3.forward * effectiveSpeed);
	}

	/// <summary>
	/// Rotating the specified horizontal and vertical.
	/// </summary>
	/// <param name="horizontal">Horizontal.</param>
	/// <param name="vertical">Vertical.</param>
	void Rotating(float horizontal, float vertical)
	{
		if((isMoving) )
		{
			Vector3 direction = path [0].position - transform.position;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation (direction), turnSmoothing * Time.deltaTime);
		}
	}	

	/// <summary>
	/// Repositioning this instance.
	/// </summary>
	private void Repositioning()
	{
		Vector3 repositioning = lastDirection;
		if(repositioning != Vector3.zero)
		{
			repositioning.y = 0;
			Quaternion targetRotation = Quaternion.LookRotation (repositioning, Vector3.up);
			Quaternion newRotation = Quaternion.Slerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);
			GetComponent<Rigidbody>().MoveRotation (newRotation);
		}
	}

}
