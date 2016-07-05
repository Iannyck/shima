using UnityEngine;
using System.Collections;

public class UltrasoundCaptor : MonoBehaviour {

    private float temps;
    public bool detection;

    public float GetTemps()
    {
        return temps;
    }

    public void SetTemps(float a)
    {
        temps = a;
    }

	void Start ()
    {
        detection = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (detection == true)
            Raycasting();
	}

    void Raycasting()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, 50);

        if (hit.collider.tag == "Player")
        {
            SetTemps(Time.time);
            detection = false;
        }

    }
}
