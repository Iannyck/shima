using UnityEngine;
using System.Collections;

public class UltrasoundCaptor : MonoBehaviour {

    public int numeroCapteur;
    private float temps;
    private float distance;

    public bool detection;

    public float GetTemps()
    {
        return temps;
    }

    public void SetTemps(float a)
    {
        temps = a;
    }

    public float GetDistance()
    {
        return distance;
    }

   public void SetDistance(float a)
    {
        distance = a;
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
            distance = hit.distance;

            Debug.Log(temps);
            detection = false;
        }

    }

    public void Reset()
    {
        temps = 0;
        distance = 0;
        detection = true;
    }
}
