using UnityEngine;
using System.Collections;

public class ZoneRFID : MonoBehaviour {

    TriggerRFID script;

    public int numberZone;
    public float puissanceZone;

    void Start ()
    {
        script = GetComponentInParent<TriggerRFID>(); 
	}
	
	void Update () {
	}

    void OnTriggerEnter(Collider other)
    {
        GameObject collider = other.gameObject;
        script.GotTrigger(numberZone, collider, true);
    }

    void OnTriggerExit(Collider other)
    {
        GameObject collider = other.gameObject;
        script.GotTrigger(numberZone, collider, false);
    }

    public float GetPuissanceZone()
    {
        return puissanceZone;
    }
  
}
