using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAvatar : MonoBehaviour {


	public GameObject freezer;
	public GameObject shelf1;
	public GameObject shelf2;
	public GameObject mug;
	public GameObject frypan;
	public GameObject cooker;
	public GameObject coffeemachine;
	public GameObject egg;
	public GameObject bottle;
	public GameObject dish;
	// Item position
	public GameObject frypanPosCooker;
	public GameObject frypanPosShelf;

	public GameObject mugPosCoffee;
	public GameObject mugPosTable;
	public GameObject mugPosShelf;

	public GameObject dishEggPos;

	public GameObject dishPosCooker;
	public GameObject dishPosTable;
	public GameObject dishPosShelf;

	public GameObject bottlePosTable;
	public GameObject bottlePosShelf;

	private bool init;

	private AvatarGoTo avatarGoTo;
	private AvatarState avatarState;
	private AvatarMotion avatarMotion;

	private ArrayList actionsList;

	private int currentActionIndex;

	private float waitTime;

	private int reactivityTime = 1;
	private int reactivityMinDeltaTime = 0;
	private int reactivityMaxDeltaTime = 1;
		
	// Use this for initialization
	void Start () {
		init = false;
		avatarGoTo = GetComponent<AvatarGoTo> ();
		avatarState = GetComponent<AvatarState> ();
		avatarMotion = GetComponent<AvatarMotion> ();
		actionsList = new ArrayList ();
		Init ();
		currentActionIndex = 0;
		waitTime = -1;
	}
	
	// Update is called once per frame
	void Update () {
//		if (!init) {
//			if (avatarGoTo != null) {
//				avatarGoTo.GoTo ("Kitchen");
//				init = true;
//			}
//		}
		if (Wait ()) {
		} else {
			if (actionsList.Count > currentActionIndex) {
				if (((SimpleBTAction)actionsList [currentActionIndex]).State == SimpleBTAction.BTState.SUCCEEDED
				   || ((SimpleBTAction)actionsList [currentActionIndex]).State == SimpleBTAction.BTState.FAILED) {
					currentActionIndex++;
				} else {
					SimpleBTAction currentAction = ((SimpleBTAction)actionsList [currentActionIndex]);
					ManageActions (currentAction);
				}
			}
		}
	}

	private bool ManageGoTo() {
		return avatarGoTo.IsTargetRoomReached ();
	}

	private bool Put(Vector3 pos) {
		if (avatarState != null) {
			avatarState.releaseAt (pos);
			return true;
		}
		else
			avatarState = GetComponent<AvatarState> ();
		return false;
	}

	private bool Take(GameObject item) {
		if (avatarState != null) {
			avatarState.PickUp (item);
			avatarMotion.PickUp ();
			return true;
		}
		else
			avatarState = GetComponent<AvatarState> ();
		return false;
	}

	private void Translate(GameObject itemToMove, GameObject container, Vector3 positionToPut) {
//		if (container == null)
//			itemToMove.transform.SetParent (null);
//		else
		Debug.Log("Container parent "+container.transform.name);
		itemToMove.transform.SetParent (container.transform);
		itemToMove.transform.position = positionToPut;
	}

	private bool Wait(){
		if (waitTime < 0)
			return false;
		waitTime -= Time.deltaTime;
		return true;
	}

	private void SetWait(int time, int mindelta, int maxdelta) {
		if (mindelta == 0 && maxdelta == 0)
			waitTime = time;
		else
			waitTime = time + Random.Range (mindelta, maxdelta); 
	}

	private void ManageActions(SimpleBTAction currentAction){
		
		if (currentAction is SimpleGoTo) {
			if (currentAction.State == SimpleBTAction.BTState.UNKNOWN) {
				string posToGo = ((SimpleGoTo)currentAction).PosToGo;
				if (avatarGoTo != null) {
					avatarGoTo.GoTo (posToGo);
					currentAction.Start ();
				}
			} else {
				if (ManageGoTo ())
					currentAction.Succeeded ();
			}
		}
		else if(currentAction is SimpleBTActivate) {
			GameObject item = ((SimpleBTActivate)currentAction).ItemToActivate;
			Debug.Log ("Activate "+item.name);
			SimpleActivate oAct = item.GetComponent<SimpleActivate> ();
			oAct.Act ();
			currentAction.Succeeded ();
			SetWait (reactivityTime, reactivityMinDeltaTime, reactivityMaxDeltaTime);
		}
		else if(currentAction is SimpleBTOClose) {
			GameObject item = ((SimpleBTOClose)currentAction).ItemToOpen;
			Debug.Log ("Open or Close "+item.name);
			SimpleOpenClose oClose = item.GetComponent<SimpleOpenClose> ();
			oClose.Act ();
			currentAction.Succeeded ();
			SetWait (reactivityTime, reactivityMinDeltaTime, reactivityMaxDeltaTime);
		}
		else if(currentAction is SimpleBTPickUp) {
			GameObject item = ((SimpleBTPickUp)currentAction).ItemToPickUp;
			Debug.Log ("Pick up "+item.name);
			if (Take (item)) {
				currentAction.Succeeded ();
				SetWait (reactivityTime, reactivityMinDeltaTime, reactivityMaxDeltaTime);
			}
		}
		else if(currentAction is SimpleMoveTools) {
			Vector3 pos = ((SimpleMoveTools)currentAction).PositionToPut;
			GameObject item = ((SimpleMoveTools)currentAction).ToolToMove;
			Debug.Log ("Put "+item.name+" at "+pos);
			if (Put(pos)) {
				currentAction.Succeeded ();
				SetWait (reactivityTime, reactivityMinDeltaTime, reactivityMaxDeltaTime);
			}
		}
		else if(currentAction is SimpleBTSpawn) {
			GameObject item = ((SimpleBTSpawn)currentAction).ItemToSpawn;
			Debug.Log ("Spawn "+item.name);
			item.SetActive (true);
			currentAction.Succeeded ();
			SetWait (reactivityTime, reactivityMinDeltaTime, reactivityMaxDeltaTime);
		}
		else if(currentAction is SimpleBTDestroy) {
			GameObject item = ((SimpleBTDestroy)currentAction).ItemToDestroy;
			Debug.Log ("Destroy "+item.name);
			item.SetActive (false);
			currentAction.Succeeded ();
			SetWait (reactivityTime, reactivityMinDeltaTime, reactivityMaxDeltaTime);
		}
		else if(currentAction is SimpleBTWait) {
			int time = ((SimpleBTWait)currentAction).Time;
			Debug.Log ("Wait "+time);
			currentAction.Succeeded ();
			SetWait (time + reactivityTime, reactivityMinDeltaTime, reactivityMaxDeltaTime);
		}
		else if(currentAction is SimpleBTAsk) {
			int time = ((SimpleBTAsk)currentAction).Time;
			Debug.Log ("Ask for help "+time);
			currentAction.Succeeded ();
			avatarMotion.Poke ();
			SetWait (time + reactivityTime, reactivityMinDeltaTime, reactivityMaxDeltaTime);
		}
		else if(currentAction is SimpleBTTranslate) {
			GameObject item = ((SimpleBTTranslate)currentAction).ItemToMove;
			GameObject container = ((SimpleBTTranslate)currentAction).Container;
			GameObject objPos = ((SimpleBTTranslate)currentAction).PositionObjToPut;
			Debug.Log ("Translate "+item.name+ " to " + container.name);
			Translate(item, container, objPos.transform.position);
			currentAction.Succeeded ();
			SetWait (reactivityTime, reactivityMinDeltaTime, reactivityMaxDeltaTime);
		}
	}

	private void Init() {
		InitTypeA ();
//		InitTypeB ();
//		InitTypeTest();
	}

	private void InitVisit() {
		actionsList.Add (new SimpleGoTo("Bathroom"));
		actionsList.Add (new SimpleGoTo("Bedroom"));
//		actionsList.Add (new SimpleGoTo("Kitchen"));
		actionsList.Add (new SimpleGoTo("Living"));
	}

	private void InitTakeBottleA() {
		actionsList.Add (new SimpleGoTo("Bedroom"));
		actionsList.Add (new SimpleBTPickUp(0, bottle));
		actionsList.Add (new SimpleGoTo("Shelf"));
		actionsList.Add (new SimpleMoveTools(0, bottle, bottlePosTable.transform.position));
	}

	private void InitTakeBottleB() {
		actionsList.Add (new SimpleGoTo("Shelf"));
		actionsList.Add (new SimpleBTPickUp(0, bottle));
		actionsList.Add (new SimpleGoTo("Freezer"));
		actionsList.Add (new SimpleBTOClose(freezer));
		actionsList.Add (new SimpleMoveTools(0, bottle, bottlePosShelf.transform.position));
		actionsList.Add (new SimpleBTOClose(freezer));
	}

	private void InitTakeBottleC() {
		actionsList.Add (new SimpleGoTo("Bedroom"));
		actionsList.Add (new SimpleBTPickUp(0, bottle));
		actionsList.Add (new SimpleGoTo("Freezer"));
		actionsList.Add (new SimpleBTOClose(freezer));
		actionsList.Add (new SimpleMoveTools(0, bottle, bottlePosShelf.transform.position));
		actionsList.Add (new SimpleBTOClose(freezer));
	}

	private void InitMugCoffee() {
		actionsList.Add (new SimpleGoTo("Shelf"));
		actionsList.Add (new SimpleBTOClose(shelf1));
		actionsList.Add (new SimpleBTPickUp(0,mug));
		actionsList.Add (new SimpleBTOClose(shelf1));
		actionsList.Add (new SimpleGoTo("Cooker"));
		actionsList.Add (new SimpleMoveTools(0, mug, mugPosCoffee.transform.position));
		actionsList.Add (new SimpleBTActivate(0,coffeemachine));
	}

	private void RefreshMemoringShelfs() {
		actionsList.Add (new SimpleGoTo("Shelf"));
		actionsList.Add (new SimpleBTOClose(shelf1));
		actionsList.Add (new SimpleBTWait(2));
		actionsList.Add (new SimpleBTOClose(shelf1));
		actionsList.Add (new SimpleBTOClose(shelf2));
		actionsList.Add (new SimpleBTWait(2));
		actionsList.Add (new SimpleBTOClose(shelf2));
		actionsList.Add (new SimpleGoTo("Freezer"));
		actionsList.Add (new SimpleBTOClose(freezer));
		actionsList.Add (new SimpleBTWait(2));
		actionsList.Add (new SimpleBTOClose(freezer));
	}

	private void InitTypeTest() {
		reactivityTime = 3;
		reactivityMinDeltaTime = 1;
		reactivityMaxDeltaTime = 3;

		int cookTime = 20;
		int dinningWaitTime = 4;
		int freezerWaitTime = 3;

		actionsList.Add (new SimpleGoTo("Kitchen"));
		InitMugCoffee ();

	}

	private void InitTypeB() {
		reactivityTime = 3;
		reactivityMinDeltaTime = 1;
		reactivityMaxDeltaTime = 3;

		int cookTime = 20;
		int dinningWaitTime = 4;
		int freezerWaitTime = 3;

		actionsList.Add (new SimpleBTWait(2));

		actionsList.Add (new SimpleGoTo("Kitchen"));

		InitMugCoffee ();
//		InitTakeBottleC ();

		actionsList.Add (new SimpleBTAsk(3));


		actionsList.Add (new SimpleGoTo("Shelf"));
		actionsList.Add (new SimpleBTOClose(shelf2));
		actionsList.Add (new SimpleBTPickUp(0,frypan));
		actionsList.Add (new SimpleBTOClose(shelf2));
		actionsList.Add (new SimpleMoveTools(0, frypan, frypanPosCooker.transform.position));

		actionsList.Add (new SimpleBTAsk(3));
		RefreshMemoringShelfs ();

		actionsList.Add (new SimpleBTOClose(shelf1));
		actionsList.Add (new SimpleBTPickUp(0,dish));
		actionsList.Add (new SimpleBTOClose(shelf1));

		actionsList.Add (new SimpleGoTo("Cooker"));
		actionsList.Add (new SimpleMoveTools(0, dish, dishPosCooker.transform.position));

		InitTakeBottleA ();

		actionsList.Add (new SimpleGoTo("Cooker"));
		actionsList.Add (new SimpleBTPickUp(0,mug));

		actionsList.Add (new SimpleGoTo("Dinning"));
		actionsList.Add (new SimpleMoveTools(0, mug, mugPosTable.transform.position));

		actionsList.Add (new SimpleGoTo("Cooker"));
		actionsList.Add (new SimpleBTOClose(freezer));
		actionsList.Add (new SimpleBTWait(freezerWaitTime));
		actionsList.Add (new SimpleBTOClose(freezer));

		actionsList.Add (new SimpleBTActivate(0, cooker));
		actionsList.Add (new SimpleBTSpawn(0, egg));
		actionsList.Add (new SimpleBTWait(cookTime));
		actionsList.Add (new SimpleBTActivate(0,cooker));

		actionsList.Add (new SimpleBTPickUp(0,frypan));
		actionsList.Add (new SimpleBTTranslate(egg, dish, dishEggPos));
		actionsList.Add (new SimpleMoveTools(0, frypan, frypanPosCooker.transform.position));

		InitVisit ();
		actionsList.Add (new SimpleBTAsk(3));

		actionsList.Add (new SimpleGoTo("Cooker"));
		actionsList.Add (new SimpleBTPickUp(0,dish));

//		actionsList.Add (new SimpleBTWait(dinningWaitTime));
//
//		actionsList.Add (new SimpleBTPickUp(0,mug));
//		actionsList.Add (new SimpleGoTo("Shelf"));
//		actionsList.Add (new SimpleMoveTools(0, mug, bottlePosTable.transform.position));

//		actionsList.Add (new SimpleBTOClose(shelf1));
//		actionsList.Add (new SimpleBTWait(2));
//		actionsList.Add (new SimpleBTOClose(shelf1));
//
//		actionsList.Add (new SimpleBTOClose(shelf2));
//		actionsList.Add (new SimpleBTWait(2));
//		actionsList.Add (new SimpleBTOClose(shelf2));


		actionsList.Add (new SimpleGoTo("Dinning"));
		actionsList.Add (new SimpleMoveTools(0, dish, dishPosTable.transform.position));
		actionsList.Add (new SimpleBTWait(dinningWaitTime));

		actionsList.Add (new SimpleBTAsk(3));

		actionsList.Add (new SimpleBTPickUp(0,dish));
		actionsList.Add (new SimpleGoTo("Lavabo"));
		// Destroy here
		actionsList.Add (new SimpleBTDestroy(egg));
		actionsList.Add (new SimpleBTWait(5));
		actionsList.Add (new SimpleGoTo("Shelf"));
		actionsList.Add (new SimpleBTOClose(shelf1));
		actionsList.Add (new SimpleMoveTools(0, dish, dishPosShelf.transform.position));
		actionsList.Add (new SimpleBTOClose(shelf1));


		actionsList.Add (new SimpleGoTo("Dinning"));
		actionsList.Add (new SimpleBTPickUp(0,mug));
		actionsList.Add (new SimpleGoTo("Lavabo"));
		actionsList.Add (new SimpleBTWait(5));
		actionsList.Add (new SimpleGoTo("Shelf"));
		actionsList.Add (new SimpleBTOClose(shelf1));
		actionsList.Add (new SimpleMoveTools(0, mug, mugPosShelf.transform.position));
		actionsList.Add (new SimpleBTOClose(shelf1));



//		InitVisit ();
//		RefreshMemoringShelfs ();
//		actionsList.Add (new SimpleBTAsk(3));
//		InitTakeBottleC ();

//		actionsList.Add (new SimpleGoTo("Cooker"));
//		actionsList.Add (new SimpleGoTo("Cooker"));
//		actionsList.Add (new SimpleBTPickUp(0,frypan));
//		actionsList.Add (new SimpleGoTo("Lavabo"));
//		actionsList.Add (new SimpleBTWait(5));
//		actionsList.Add (new SimpleGoTo("Shelf"));
//		actionsList.Add (new SimpleBTOClose(shelf2));
//		actionsList.Add (new SimpleMoveTools(0, frypan, frypanPosShelf.transform.position));
//		actionsList.Add (new SimpleBTOClose(shelf2));
//		actionsList.Add (new SimpleBTOClose(shelf1));
//		actionsList.Add (new SimpleMoveTools(0, frypan, dishPosShelf.transform.position));
//		actionsList.Add (new SimpleBTOClose (shelf1));

//		actionsList.Add (new SimpleGoTo("Dinning"));
//		actionsList.Add (new SimpleBTPickUp(0,dish));
//		actionsList.Add (new SimpleGoTo("Lavabo"));
		// Destroy here
//		actionsList.Add (new SimpleBTDestroy(egg));

//		InitTakeBottle ();

		actionsList.Add (new SimpleGoTo("Entrance"));
		actionsList.Add (new SimpleBTAsk(1));
	}

	private void InitTypeA() {
		reactivityTime = 1;
		reactivityMinDeltaTime = 0;
		reactivityMaxDeltaTime = 1;

		int cookTime = 20;
		int dinningWaitTime = 5;

		actionsList.Add (new SimpleBTWait(2));
		actionsList.Add (new SimpleGoTo("Kitchen"));
		actionsList.Add (new SimpleGoTo("Shelf"));
		actionsList.Add (new SimpleBTOClose(shelf1));
		actionsList.Add (new SimpleBTPickUp(0,mug));
		actionsList.Add (new SimpleBTOClose(shelf1));
		actionsList.Add (new SimpleGoTo("Cooker"));
		actionsList.Add (new SimpleMoveTools(0, mug, mugPosCoffee.transform.position));

		actionsList.Add (new SimpleGoTo("Shelf"));
		actionsList.Add (new SimpleBTOClose(shelf2));
		actionsList.Add (new SimpleBTPickUp(0,frypan));
		actionsList.Add (new SimpleBTOClose(shelf2));
		actionsList.Add (new SimpleGoTo("Cooker"));

		actionsList.Add (new SimpleMoveTools(0, frypan, frypanPosCooker.transform.position));
		actionsList.Add (new SimpleBTOClose(freezer));
		actionsList.Add (new SimpleBTWait(5));
		actionsList.Add (new SimpleBTOClose(freezer));
		actionsList.Add (new SimpleBTActivate(0,coffeemachine)); // Activate with auto end
		actionsList.Add (new SimpleBTActivate(0,cooker));
		actionsList.Add (new SimpleBTSpawn(0, egg));
		actionsList.Add (new SimpleBTWait(cookTime));
		actionsList.Add (new SimpleBTActivate(0,cooker));

		actionsList.Add (new SimpleGoTo("Shelf"));
		actionsList.Add (new SimpleBTOClose(shelf1));
		actionsList.Add (new SimpleBTPickUp(0,dish));
		actionsList.Add (new SimpleBTOClose(shelf1));
		actionsList.Add (new SimpleGoTo("Cooker"));
		actionsList.Add (new SimpleMoveTools (0, dish, dishPosCooker.transform.position));
		actionsList.Add (new SimpleBTPickUp(0,frypan));
		actionsList.Add (new SimpleBTTranslate(egg, dish, dishEggPos));
		actionsList.Add (new SimpleMoveTools(0, frypan, frypanPosCooker.transform.position));
		actionsList.Add (new SimpleBTPickUp(0,dish));

		actionsList.Add (new SimpleGoTo("Dinning"));
		actionsList.Add (new SimpleMoveTools(0, dish, dishPosTable.transform.position));
		actionsList.Add (new SimpleGoTo("Freezer"));
		actionsList.Add (new SimpleBTPickUp(0,mug));
		actionsList.Add (new SimpleGoTo("Dinning"));
		actionsList.Add (new SimpleMoveTools(0, mug, mugPosTable.transform.position));
		actionsList.Add (new SimpleBTWait(dinningWaitTime));
		//		actionsList.Add (new SimpleBTAsk(2));

		actionsList.Add (new SimpleBTPickUp(0,mug));
		actionsList.Add (new SimpleGoTo("Lavabo"));
		actionsList.Add (new SimpleBTWait(5));
		actionsList.Add (new SimpleGoTo("Shelf"));
		actionsList.Add (new SimpleBTOClose(shelf1));
		actionsList.Add (new SimpleMoveTools(0, mug, mugPosShelf.transform.position));
		actionsList.Add (new SimpleBTOClose(shelf1));

		actionsList.Add (new SimpleGoTo("Dinning"));
		actionsList.Add (new SimpleBTPickUp(0,dish));
		actionsList.Add (new SimpleGoTo("Lavabo"));
		// Destroy here
		actionsList.Add (new SimpleBTDestroy(egg));
		actionsList.Add (new SimpleBTWait(5));
		actionsList.Add (new SimpleGoTo("Shelf"));
		actionsList.Add (new SimpleBTOClose(shelf1));
		actionsList.Add (new SimpleMoveTools(0, dish, dishPosShelf.transform.position));
		actionsList.Add (new SimpleBTOClose(shelf1));

		actionsList.Add (new SimpleGoTo("Cooker"));
		actionsList.Add (new SimpleBTPickUp(0,frypan));
		actionsList.Add (new SimpleGoTo("Lavabo"));
		actionsList.Add (new SimpleBTWait(5));
		actionsList.Add (new SimpleGoTo("Shelf"));
		actionsList.Add (new SimpleBTOClose(shelf2));
		actionsList.Add (new SimpleMoveTools(0, frypan, frypanPosShelf.transform.position));
		actionsList.Add (new SimpleBTOClose(shelf2));

		actionsList.Add (new SimpleGoTo("Bedroom"));
		actionsList.Add (new SimpleBTPickUp(0, bottle));
		actionsList.Add (new SimpleGoTo("Freezer"));
		actionsList.Add (new SimpleBTOClose(freezer));
		actionsList.Add (new SimpleMoveTools(0, bottle, bottlePosShelf.transform.position));
		actionsList.Add (new SimpleBTOClose(freezer));

		actionsList.Add (new SimpleGoTo("Entrance"));
		actionsList.Add (new SimpleBTAsk(1));
	}

}
