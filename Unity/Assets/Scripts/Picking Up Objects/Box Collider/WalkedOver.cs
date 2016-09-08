using UnityEngine;
using System.Collections;

public class WalkedOver : MonoBehaviour {

    public PickUp pickUpScript = null;
    private GameObject walkedOver;

    void Start() {
        initPickUpScript();
    }

    void OnTriggerEnter(Collider other)
    {
        //        if (other.gameObject.tag == "Door")
        //            Debug.Log("TriggeEnter with" + other.gameObject);
        //
        //        if (other.gameObject.tag == "PickUp")
        //            Debug.Log("TriggeEnter with" + other.gameObject);
        //
        //        if (other.gameObject.tag == "Light")
        //            Debug.Log("TriggeEnter with" + other.gameObject);


        if (other.gameObject.tag == "Door" || other.gameObject.tag == "PickUp" || other.gameObject.tag == "Light" || other.gameObject.tag == "Electronic" || other.gameObject.tag == "Drawer")
        {
            if (walkedOver == null)
            {
                if (pickUpScript != null)
                {
                    pickUpScript.AddWalkedOverObject(other.gameObject, other.tag);
                    walkedOver = other.gameObject;
                }

                else
                    initPickUpScript();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
//        if (other.gameObject.tag == "Door")
//            Debug.Log("TriggerExit with" + other.gameObject);

        if (other.gameObject == walkedOver)
        {
//            Debug.Log("TriggerExit effectue");
            pickUpScript.AddWalkedOverObject(null, null);
            walkedOver = null;
        }
    }

    void initPickUpScript() {
        pickUpScript = GetComponentInParent<PickUp>();
    }
}
