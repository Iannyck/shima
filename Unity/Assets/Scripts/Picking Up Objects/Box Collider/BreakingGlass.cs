using UnityEngine;
using System.Collections;

public class BreakingGlass : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Glass colided with" + other.gameObject);

        if (other.tag == "Floor")
        {
            Destroy(gameObject);
        }

    }
}
