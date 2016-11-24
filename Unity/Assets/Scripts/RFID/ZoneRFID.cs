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
        if (script != null)
        {
            if (collider.layer == LayerMask.NameToLayer("RFID"))
                script.GotTrigger(numberZone, collider, collider.GetComponentInChildren<TagRFID>().nomTag,true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject collider = other.gameObject;
        if (script != null)
        {
            if (collider.layer == LayerMask.NameToLayer("RFID"))
                script.GotTrigger(numberZone, collider, collider.GetComponentInChildren<TagRFID>().nomTag,false);
        }
    }

    public float GetPuissanceZone()
    {
        return puissanceZone;
    }
  
}
