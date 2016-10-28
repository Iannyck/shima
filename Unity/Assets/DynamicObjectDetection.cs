using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicObjectDetection : MonoBehaviour {

    private List<GameObject> listeDetection;

    void Start()
    {
        listeDetection = new List<GameObject>();
    }

    public List<GameObject> ListeDetection
    {
        get
        {
            return listeDetection;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "UI_Object")
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
