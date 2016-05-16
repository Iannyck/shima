using UnityEngine;
using System.Collections;

public class WalkedOver : MonoBehaviour {
    
    public PickUp pickUpScript = null;
    private GameObject walkedOver;

	void Start () {
        initPickUpScript();
    }

    void OnTriggerEnter(Collider other)
    {
        if (pickUpScript != null)
        {
            pickUpScript.AddWalkedOverObject(other.gameObject, other.tag);
        }

        else
            initPickUpScript();
    }

    void OnTriggerExit(Collider other)
    {
        pickUpScript.AddWalkedOverObject(null,null);
    }

    void initPickUpScript() {
        pickUpScript = GetComponentInParent<PickUp>();
    }
}
