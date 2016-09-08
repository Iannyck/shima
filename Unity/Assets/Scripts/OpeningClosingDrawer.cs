using UnityEngine;
using System.Collections;

public class OpeningClosingDrawer : MonoBehaviour {

    public bool isOpen;
    public Animation anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animation>();
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAnim(string name)
    {

        string tempName = null;
        tempName = "D" + name.Substring(1,6);

        if (isOpen)
        {

            isOpen = false;
            anim.Play(tempName+"_close");
        }

        else
        {
            isOpen = true;
            anim.Play(tempName + "_open");
        }
    }

    public bool GetStatus()
    {
        return isOpen;
    }
}
