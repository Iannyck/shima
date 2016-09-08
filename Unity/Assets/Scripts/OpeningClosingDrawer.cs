using UnityEngine;
using System.Collections;

public class OpeningClosingDrawer : MonoBehaviour {

    public static int nbDrawer = 10;
    private bool[] myList = new bool[nbDrawer];

    public Animation anim;
   

    void Start()
    {
        anim = GetComponent<Animation>();

        for (int i = 0; i < nbDrawer; i++)
            myList[i] = false;

    }

    void Update(){}

    public void PlayAnim(string name)
    {
        int position = GetPosition(name);

        if (myList[position])
        {
            myList[position] = false;
            anim.Play(name + "_closing");
        }

        else
        {
            myList[position]= true;
            anim.Play(name + "_opening");
        }
    }

    public bool GetStatus(string name)
    {
        return (myList[GetPosition(name)]);
    }

    private int GetPosition (string name)
    {
        string tempName = null;
        tempName = name.Substring(6);

        return System.Int32.Parse(tempName);
    }
}
