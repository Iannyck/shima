using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour {

    Rigidbody rgbd;

    private float massColliderEnter;
    private float massColliderExit;
    private float massTotale;

    private bool pressureOn;

	public bool PressureOn {
		get {
			return this.pressureOn;
		}
	}
    public float minimumValue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Floor")
        {
            rgbd = other.gameObject.GetComponent<Rigidbody>();
            massColliderEnter = rgbd.mass;

            massTotale = massTotale + massColliderEnter;

            if (massTotale != 0)
            {
                if (massTotale >= minimumValue)
                    pressureOn = true;
            }
        }
    }

    void OnCollisionExit(Collision other)
    {
        massColliderExit = other.gameObject.GetComponent<Rigidbody>().mass;
        massTotale = massTotale - massColliderExit;

        if (massTotale == 0)
        {
            pressureOn = false;
            Debug.Log(massTotale);
        }

    }
}
