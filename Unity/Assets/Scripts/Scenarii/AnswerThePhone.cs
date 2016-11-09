﻿using UnityEngine;
using System.Collections;

public class AnswerThePhone : Sequence {

//	public AudioClip RingPhone;
	public GameObject phone;
	public GameObject aStar;
	public Rigidbody playerRigidbody;

	public override void Init ()
	{
//		if (RingPhone != null)
//			AudioSource.PlayClipAtPoint (RingPhone,phone.transform.position);
		PhoneDevice phoneDevice = phone.GetComponent<PhoneDevice>();
		phoneDevice.Ring ();

		this.BName = "AnswerThePhone";

		AbstractBehaviour[] behaviours = new AbstractBehaviour[2];
		MoveToBehaviour behaviour = gameObject.AddComponent(typeof(MoveToBehaviour)) as MoveToBehaviour;
		behaviour.playerRigidbody = playerRigidbody;
		behaviour.roomToGo = "Phone";
		behaviour.aStarGameObject = aStar;
		behaviour.PathInit ();
		behaviour.BName = "Move";
		behaviours [0] = behaviour;

		UseDeviceBehaviour uDBehaviour = gameObject.AddComponent(typeof(UseDeviceBehaviour)) as UseDeviceBehaviour;
		uDBehaviour.DeviceToUse = phone;
		uDBehaviour.BName = "Answer";
		behaviours [1] = uDBehaviour;

		this.Behaviours = behaviours;

	}

}
