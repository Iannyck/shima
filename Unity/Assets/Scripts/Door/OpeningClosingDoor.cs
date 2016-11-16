using UnityEngine;
using System.Collections;

public class OpeningClosingDoor : MonoBehaviour {

    // Il sera possible de retirer toutes les fonctions et parametres excepte Start, Animation et SelectAndPlay

    public bool isOpen;
    public Animation anim;

	void Start ()
    {
        anim = GetComponent<Animation>();
        isOpen = false;
	}
	
	void Update () {
	
	}

    public void PlayAnim()
    {
        if (isOpen)
        {
            isOpen = false;
            anim.Play("DoorClosing");
        }

        else
        {
            isOpen = true;
            anim.Play("DoorOpening");
        }
    }

    public bool GetStatus()
    {
        return isOpen;
    }

    public void SelectAndPlayAnimation(State myState)
    {
        switch(myState)
        {
            case State.Open:
                {
                    anim.Play("DoorClosing");
                    return;
                }

            case State.Close:
                {
                    anim.Play("DoorOpening");
                    return;
                }
        }
    }
}
