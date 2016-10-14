using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_Choice : MonoBehaviour
{
    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 90), "Choice Menu");

        if (GUI.Button(new Rect(20, 40, 80, 20), "Choix 1"))
        {
            Debug.Log("Choix 1");
        }

        if (GUI.Button(new Rect(20, 70, 80, 20), "Choix 2"))
        {
            Debug.Log("Choix 2");
        }
    }
}
