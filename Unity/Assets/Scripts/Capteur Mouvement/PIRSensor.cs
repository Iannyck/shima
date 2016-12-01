using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PIRSensor : MonoBehaviour {

	public float range = 100f;
	public float cooldown = 1f;
	private float currentCooldown;
	private bool canDetect;

	private Hashtable detectedEntity;

	private SmartHomeServer smartHomeServerScript;
//	private List<MovementDetection> newDetectedItem;

	private Transform capteurTransform;
	private int layerMask = 1 << 9; // Layer : RFID

	// Use this for initialization
	void Start () {
		currentCooldown = cooldown;
		canDetect = true;
		GameObject smartHomeServer = GameObject.Find ("smarthomeserver");
		smartHomeServerScript = smartHomeServer.GetComponent<SmartHomeServer> ();
	}
	
	void Awake()
	{
		capteurTransform = GetComponent<Transform>();

		detectedEntity = null;
//		newDetectedItem = new List<MovementDetection> ();

	}

	void Update()
	{
		if (canDetect) {
			Hashtable detected = new Hashtable ();
			Collider[] hitColliders = Physics.OverlapSphere (capteurTransform.position, range);//, layerMask);
			if (hitColliders.Length != 0) {
				for (int i = 0; i < hitColliders.Length; i++) {
//					Debug.Log ("Detected "+hitColliders [i].name);
					if (detectedEntity != null) {
						if (canDetect && CheckMove (hitColliders [i])) {
							canDetect = false;
							string timestamp = System.DateTime.Now.ToLongTimeString ();
							smartHomeServerScript.InsertBinarySensorData (timestamp, name, "PIRSensor", true);
//							Debug.Log ("Detected "+hitColliders [i].name);
						} else {
						}
					}
					if(!detected.Contains(hitColliders [i].gameObject))
						detected.Add (hitColliders [i].gameObject, new MovementDetection(hitColliders [i].gameObject));
				}
				if (detectedEntity != null)
					detectedEntity.Clear ();
				detectedEntity = detected;
			}
		} else {
			currentCooldown -= Time.deltaTime;
			if (currentCooldown <= 0) {
				currentCooldown = cooldown;
				canDetect = true;
			}
		}
	}

	private bool CheckMove(Collider hit) {
		if ((hit.tag == "Player") || (hit.tag == "PickUp")) {
			if (!detectedEntity.Contains (hit.gameObject))
				return true;
			else {
				return ((MovementDetection)detectedEntity [hit.gameObject]).HasMoved (hit.gameObject);
			}
		}
		return false;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, range);
	}

	private class MovementDetection
	{
		private float x,y,z;

		public MovementDetection (GameObject entity)
		{
			this.x = entity.transform.position.x;
			this.y = entity.transform.position.y;
			this.z = entity.transform.position.z;
		}

		public bool HasMoved(GameObject entityNewPosition) {
			if ((x == entityNewPosition.transform.position.x)
			   && (y == entityNewPosition.transform.position.y)
			   && (z == entityNewPosition.transform.position.z))
				return false;
			return true;
		}
		
		
	}



}
