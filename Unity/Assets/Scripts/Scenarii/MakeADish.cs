using UnityEngine;
using System.Collections;

public class MakeADish : AbstractScript {

	public GameObject smartHomeServer;
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
	public GameObject fork;
	public GameObject foodToCook;

	protected override AbstractBehaviour InitScript ()
	{
		this.BName = "Make a dish";

		if (smartHomeServer != null) {
			SensorsGUI gui = smartHomeServer.GetComponent<SensorsGUI> ();
			gui.SetDebugText (9, BName);
			gui.BehaviourTreeToShow = this;
		}

		AbstractBehaviour[] behaviours = new AbstractBehaviour[64];
		behaviours[0] = CreateGoToBehaviour("Kitchen");

		behaviours[1] = CreateGoToBehaviour("KitchenDishes");

		behaviours[2] = CreateTakeItemBehaviour(mug);

		behaviours[3] = CreateGoToBehaviour("KitchenDevices");

		behaviours[4] = CreatePutItemAtBehaviour(mug, 10.31f, 10.54f, 84.84f);
		 
		behaviours[5] = CreateUseDeviceBehaviour(coffeeMachine);

		behaviours[6] = CreateGoToBehaviour("KitchenTools");

		behaviours[7] = CreateTakeItemBehaviour(frypan);
		 
		behaviours[8] = CreateGoToBehaviour("Cooker");

		behaviours[9] = CreatePutItemAtBehaviour(frypan, 38.94f, 16.01f, 105.15f);

		behaviours[10] = CreateUseDeviceBehaviour(cooker);

		behaviours[11] = CreateGoToBehaviour("Refrigerator");

		behaviours[12] = CreateAtomicActivityBehaviour("Take Eggs", 5f);

		behaviours[13] = CreateGoToBehaviour("Cooker");

		behaviours[14] = CreateAtomicActivityBehaviour("Cook Eggs", 5f);

		behaviours[15] = CreateUseDeviceBehaviour(cooker);

		behaviours[16] = CreateGoToBehaviour("KitchenDishes");

		behaviours[17] = CreateTakeItemBehaviour(dish);

		behaviours[18] = CreateGoToBehaviour("Cooker");

		behaviours[19] = CreatePutItemAtBehaviour(dish, 35.476f, 9.867f, 111.679f);

		behaviours[20] = CreateTakeItemBehaviour(frypan);

		behaviours[21] = CreateAtomicActivityBehaviour("Put Eggs into dish", 4f);

		behaviours[22] = CreateGoToBehaviour("KitchenSink");

		behaviours[23] = CreatePutItemAtBehaviour(frypan, 40.369f, 7.46f, 93.18f);

		behaviours[24] = CreateGoToBehaviour("Cooker");

		behaviours[25] = CreateTakeItemBehaviour(dish);

		behaviours[26] = CreateGoToBehaviour("KitchenDishes");

		behaviours[27] = CreateTakeItemBehaviour(fork);

		behaviours[28] = CreateGoToBehaviour("KitchenDevices");

		behaviours[29] = CreateAtomicActivityBehaviour("Take toast", 4f);

		behaviours[30] = CreateGoToBehaviour("DiningTable");

		behaviours[31] = CreatePutItemAtBehaviour(dish, 25.1f, 11.41f, 40.4f);

		behaviours[32] = CreatePutItemAtBehaviour(fork, 21.32f, 11.41f, 40.4f);

		behaviours[33] = CreateGoToBehaviour("KitchenDevices");

		behaviours[34] = CreateTakeItemBehaviour(mug);

		behaviours[35] = CreateAtomicActivityBehaviour("Take sugar", 4f);

		behaviours[36] = CreateGoToBehaviour("DiningTable");

		behaviours[37] = CreatePutItemAtBehaviour(mug, 24.89f, 11.41f, 34.7f);

		behaviours[38] = CreateAtomicActivityBehaviour("Watch food", 4f);

		behaviours[39] = CreateTakeItemBehaviour(fork);

		behaviours[40] = CreateTakeItemBehaviour(dish);

		behaviours[41] = CreateGoToBehaviour("KitchenSink");

		behaviours[42] = CreatePutItemAtBehaviour(dish, 40.369f, 7.46f, 93.18f);

		behaviours[43] = CreatePutItemAtBehaviour(fork, 40.369f, 7.46f, 93.18f);

		behaviours[44] = CreateAtomicActivityBehaviour("Do the washing up", 10f);

		behaviours[45] = CreateTakeItemBehaviour(fork);

		behaviours[46] = CreatePutItemAtBehaviour(fork, 38.41f, 10.54f, 97.75f);

		behaviours[47] = CreateTakeItemBehaviour(dish);

		behaviours[48] = CreatePutItemAtBehaviour(dish, 36.91f, 10.54f, 97.75f);

		behaviours[49] = CreateAtomicActivityBehaviour("Wash the frypan", 10f);

		behaviours[50] = CreateTakeItemBehaviour(frypan);

		behaviours[51] = CreateGoToBehaviour("KitchenTools");

		behaviours[52] = CreatePutItemAtBehaviour(frypan, 41.21f, 17.2f, 84.06f);

		behaviours[53] = CreateGoToBehaviour("KitchenSink");

		behaviours[54] = CreateGoToBehaviour("DiningTable");

		behaviours[55] = CreateTakeItemBehaviour(mug);

		behaviours[56] = CreateGoToBehaviour("KitchenSink");

		behaviours[57] = CreateAtomicActivityBehaviour("Wash the mug", 4f);

		behaviours[58] = CreateTakeItemBehaviour(dish);

		behaviours[59] = CreateTakeItemBehaviour(fork);

		behaviours[60] = CreateGoToBehaviour("KitchenDishes");

		behaviours[61] = CreatePutItemAtBehaviour(fork, 19.37021f, 8.851f, 120.51f);

		behaviours[62] = CreatePutItemAtBehaviour(dish, 22.46242f, 9.782906f, 120.6399f);

		behaviours[63] = CreatePutItemAtBehaviour(mug, 15.15f, 8.83f, 120.31f);

//		behaviours[31] = CreateGoToBehaviour("KitchenDishes");

//		behaviours[31] = CreatePutItemAtBehaviour(mug, 22.95f, 11.41f, 37.9f);
			
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
		behaviour.smartHomeServer = smartHomeServer;
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
		aABehaviour.smartHomeServer = smartHomeServer;
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
