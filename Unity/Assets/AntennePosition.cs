using UnityEngine;
using System.Collections;

public class AntennePosition : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GameObject myAntenna = GetComponent<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Vector3 PositionAntenne()
    {
        return GetComponent<Transform>().position;
    }

    public string NomAntenne()
    {
        return GetComponent<GameObject>().name;
    }
}
