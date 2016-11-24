using UnityEngine;
using System.Collections;

public class MakeADish : AbstractScript {

	public GameObject aStar;
	public Rigidbody playerRigidbody;

	public float duration = 15.0f;
	public GameObject toaster;
	public GameObject dish;
	public GameObject cooker;
	public GameObject frypan;
	public GameObject mug;
	public GameObject coffeeMachine;
	public GameObject milk;
	public GameObject foodToCook;

	protected override AbstractBehaviour InitScript ()
	{
		this.BName = "Make a dish";

		AbstractBehaviour[] behaviours = new AbstractBehaviour[16];
		behaviours [0] = CreateGoToBehaviour("Kitchen");

		behaviours [1] = CreateGoToBehaviour("KitchenDevices");
		 
		behaviours [2] = CreateUseDeviceBehaviour(coffeeMachine);

		behaviours [3] = CreateGoToBehaviour("KitchenTools");

		behaviours [4] = CreateTakeItemBehaviour(frypan);
		 
		behaviours [5] = CreateGoToBehaviour("Cooker");

		behaviours [6] = CreatePutItemAtBehaviour(frypan, 38.94f, 16.01f, 105.15f);

		behaviours [7] = CreateUseDeviceBehaviour(cooker);

		behaviours [8] = CreateGoToBehaviour("Refrigerator");

		behaviours [9] = CreateAtomicActivityBehaviour("Take Eggs", 5f);

		behaviours [10] = CreateGoToBehaviour("Cooker");

		behaviours [11] = CreateAtomicActivityBehaviour("Cook Eggs", 5f);

		behaviours [12] = CreateUseDeviceBehaviour(cooker);

		behaviours [13] = CreateGoToBehaviour("LivingroomDishes");

		behaviours [14] = CreateTakeItemBehaviour(dish);

		behaviours [15] = CreateGoToBehaviour("DiningTable");
			
		Sequence sequence = gameObject.AddComponent (typeof(Sequence)) as Sequence;
		sequence.Behaviours = behaviours;

		return sequence;
	}

	private GoToBehaviour CreateGoToBehaviour(string roomToGo) {
		GoToBehaviour behaviour = gameObject.AddComponent(typeof(GoToBehaviour)) as GoToBehaviour;
		behaviour.avatarRigidbody = playerRigidbody;
		behaviour.roomToGo = roomToGo;
		behaviour.aStarGameObject = aStar;
		behaviour.BName = "Go to "+roomToGo;
		return behaviour;
	}

	private UseDeviceBehaviour CreateUseDeviceBehaviour(GameObject deviceToUse) {
		UseDeviceBehaviour uDBehaviour = gameObject.AddComponent(typeof(UseDeviceBehaviour)) as UseDeviceBehaviour;
		uDBehaviour.DeviceToUse = deviceToUse;
		uDBehaviour.BName = "Use "+deviceToUse;
		return uDBehaviour;
	}

	private AtomicActivityBehaviour CreateAtomicActivityBehaviour(string action, float duration) {
		AtomicActivityBehaviour aABehaviour = gameObject.AddComponent(typeof(AtomicActivityBehaviour)) as AtomicActivityBehaviour;
		aABehaviour.duration = duration;
		aABehaviour.BName = action;
		return aABehaviour;
	}

	private TakeItemBehaviour CreateTakeItemBehaviour(GameObject objectToTake) {
		TakeItemBehaviour tIBehaviour = gameObject.AddComponent (typeof(TakeItemBehaviour)) as TakeItemBehaviour;
		tIBehaviour.ItemToTake = objectToTake;
		tIBehaviour.BName = "Take " + objectToTake;
		return tIBehaviour;
	}

	private PutItemAtBehaviour CreatePutItemAtBehaviour(GameObject objectToRelease, float x, float y, float z) {
		PutItemAtBehaviour pIABehaviour = gameObject.AddComponent (typeof(PutItemAtBehaviour)) as PutItemAtBehaviour;
		pIABehaviour.ItemToRelease = objectToRelease;
		pIABehaviour.x = x;
		pIABehaviour.y = y;
		pIABehaviour.z = z;
		pIABehaviour.BName = "Put " + objectToRelease + " at ("+x+","+y+","+z+")";
		return pIABehaviour;
	}

}
