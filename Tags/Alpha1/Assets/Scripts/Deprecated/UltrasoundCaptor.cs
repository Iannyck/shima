using UnityEngine;
using System.Collections;

public class UltrasoundCaptor : MonoBehaviour {

//    public int numeroCapteur;
//    private float temps;
	public float range = 20f;
    private float distance;
	public float cooldownTime = 0.3f;
	private float cooldown;

    public bool detection = true;
	public GameObject smartHomeServer;

	private SmartHomeServer smartHomeServeScript;

//    public float GetTemps()
//    {
//        return temps;
//    }
//
//    public void SetTemps(float a)
//    {
//        temps = a;
//    }

//    public float GetDistance()
//    {
//        return distance;
//    }
//
//   public void SetDistance(float a)
//    {
//        distance = a;
//    }

	void Start ()
    {
        detection = true;
		cooldown = cooldownTime;
		smartHomeServeScript = smartHomeServer.GetComponent<SmartHomeServer> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (detection == true)
			Raycasting ();
		else {
			cooldown -= Time.deltaTime;
//			Debug.Log ("non detectable");
			if (cooldown <= 0f) {
				detection = true;
				cooldown = cooldownTime;
			}
		}
	}

    void Raycasting()
    {
        RaycastHit hit;
		Physics.Raycast(transform.position, transform.up, out hit, range);
//		Debug.DrawRay (transform.position, transform.up * range, Color.red);
		if (hit.collider != null) {
			if (hit.collider.tag == "Player") {
//            SetTemps(Time.time);
				distance = hit.distance;

//            Debug.Log(temps);
				detection = false;
				smartHomeServeScript.InsertUltrasoundData (name, distance);
//			Debug.Log (""+timestamp +" - "+ name +" - "+ distance);
			}
		}

    }

    public void Reset()
    {
//        temps = 0;
        distance = 0;
        detection = true;
    }
}
