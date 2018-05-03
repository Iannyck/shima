using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListTagRFID : MonoBehaviour
{
    private List<GameObject> listeTags;

    public List<GameObject> ListeTags
    {
        get { return listeTags; }
    }

    void Awake()
    {
        listeTags = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<TagRFID>() != null)
            {
                listeTags.Add(transform.GetChild(i).gameObject);
            }
        }
    }
}
