using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicObjectDetection : MonoBehaviour {

    private List<GameObject> listeDetection = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door" || other.gameObject.tag == "PickUp" || other.gameObject.tag == "Light" || other.gameObject.tag == "Electronic" || other.gameObject.tag == "Drawer")
        {
            if (!listeDetection.Contains(other.gameObject))
            {
                listeDetection.Add(other.gameObject);
                other.gameObject.GetComponent<UI_Object>().AddUI();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (listeDetection.Contains(other.gameObject))
        {
            other.GetComponent<UI_Object>().RemoveUI();
            listeDetection.Remove(other.gameObject);
        }
    }

}
