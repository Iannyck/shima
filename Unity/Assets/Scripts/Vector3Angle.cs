using UnityEngine;
using System.Collections;

public class Vector3Angle : MonoBehaviour {

    public Transform target;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 targetDirection = target.position - transform.position;

        Vector3 forward = transform.forward;

        float angle = Vector3.Angle(targetDirection, forward);
        Debug.Log(angle);
	}
}
