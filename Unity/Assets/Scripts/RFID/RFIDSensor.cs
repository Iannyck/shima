using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFIDSensor : IESensor {

	public float thresold = -71f; // -71 DB

	public GameObject nextRFIDAntena;
	public float activationTime = 0.25f;
	private float remainingTime = 0f;

	private GameObject detectionZone;
	public bool isEnable = true;



	public void RFIDActivate(bool value) {
		if(value)
			remainingTime = activationTime;
		detectionZone.SetActive (value);
		isEnable = value;
	}

	void Awake() {
		RFIDDetectionZone rfidDetection = GetComponentInChildren<RFIDDetectionZone> ();
		detectionZone = rfidDetection.gameObject;
		remainingTime = activationTime;
	}

	protected override void Sense (SmartHomeServer smartHomeServer)
	{
		base.Sense (smartHomeServer);
		if (isEnable) {
			remainingTime -= Time.deltaTime;
			if (remainingTime <= 0f) {
				if (nextRFIDAntena != null) {
					RFIDSensor rfidSensor = nextRFIDAntena.GetComponent<RFIDSensor> ();
					rfidSensor.RFIDActivate (true);
					RFIDActivate (false);
				}
			}
		}
	}

	public void Trigger(GameObject collider) {
		float rssi = RSSI (collider);
		if (rssi >= thresold)
			SmartHomeServer.InsertRFIDData(name, rssi, collider.name);
	}

	private float RSSI(GameObject collider) {
		float distance = Vector3.Distance(this.transform.position, collider.transform.position);
		float angle = Vector3.Angle(this.transform.position, collider.transform.position);
		float angleLose = 1 - Mathf.Cos (angle);
		float ambiantNoise = AmbiantNoise();
		float obstacleLose = ObstacleNoise(collider);

		float rssi = ((-9.1333f * Mathf.Log(distance)) - 10.726f) * angleLose;


		return rssi;
	}

	public float AmbiantNoise() {
		return 0f;
	}

	private float ObstacleNoise(GameObject collider) {
		GameObject obstacle = GetObstacle (collider);
		if (obstacle != null) {
			return 0f;
		}
		return 0f;
	}

	public GameObject GetObstacle(GameObject collider)                                  
	{
		Transform sensorPosition = GetComponent<Transform>();
		RaycastHit hitInfo;

		Debug.DrawLine(sensorPosition.position, collider.transform.position, Color.red, 10f);

		Physics.Linecast(sensorPosition.position, collider.transform.position, out hitInfo);
		{
			if (hitInfo.collider != null && (hitInfo.collider.CompareTag("Fixed") || hitInfo.collider.CompareTag("Door")))                             
				return hitInfo.collider.gameObject;

			else
				return null;
		}
	}

}
