using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIRMotion : IESensor {

	public string SensorType = "PIRMotion";
	public float range = 100f;
	public float cooldown = 1f;
	public float tolerance = 0f;
	private float currentCooldown;
	private bool canDetect;

	private Hashtable detectedEntity;

	private Transform capteurTransform;
	private int layerMask = 1 << 9; // Layer : RFID

	protected override void IESensorInit ()
	{
		base.IESensorInit ();
		currentCooldown = cooldown;
		canDetect = true;

		capteurTransform = GetComponent<Transform>();
		detectedEntity = null;
	}

	protected override void IESensorUpdate ()
	{
		base.IESensorUpdate ();
		if (canDetect) {
			Hashtable detected = new Hashtable ();
			Collider[] hitColliders = Physics.OverlapSphere (capteurTransform.position, range);//, layerMask);
			if (hitColliders.Length != 0) {
				for (int i = 0; i < hitColliders.Length; i++) {
					if (detectedEntity != null) {
						if (canDetect && CheckMove (hitColliders [i])) {
							canDetect = false;
							SmartHomeServer.InsertBinarySensorData (name, SensorType, true);
						} else {
						}
					}
					if (!detected.Contains (hitColliders [i].gameObject)) {
						Vector3 position = new Vector3(hitColliders [i].gameObject.transform.position.x,
							hitColliders [i].gameObject.transform.position.y
							,hitColliders [i].gameObject.transform.position.z);
						detected.Add (hitColliders [i].gameObject, position);
					}
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

	protected override void IESensorStop ()
	{
		base.IESensorStop ();
	}

	private bool CheckMove(Collider hit) {
		if ((hit.tag == "Player") || (hit.tag == "PickUp")) {
			if (!detectedEntity.Contains (hit.gameObject))
				return true;
			else {
				return HasMoved(hit.gameObject);
			}
		}
		return false;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, range);
	}

	private bool HasMoved(GameObject entityNewPosition) {
		Vector3 entityOldPosition = ((Vector3)detectedEntity[entityNewPosition]);
		float delta = Mathf.Abs(entityOldPosition.x - entityNewPosition.transform.position.x);
		delta += Mathf.Abs(entityOldPosition.y - entityNewPosition.transform.position.y);
		delta += Mathf.Abs(entityOldPosition.z - entityNewPosition.transform.position.z);
		if (delta > tolerance)
			return true;
		return false;
	}
}
