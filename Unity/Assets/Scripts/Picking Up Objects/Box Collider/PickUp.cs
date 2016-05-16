using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

    private GameObject walkedOverObject;
    private Rigidbody rb;
    private Transform characterTransform;

    private bool isPick;

    private bool guiShow;

    public OpeningClosingDoor doorInteraction;

    void Start ()
    {
        characterTransform = GetComponent<Transform>();

        isPick = false;
        guiShow = false;
	}

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (walkedOverObject != null)
            {
                switch (walkedOverObject.tag)
                {
                    case "PickUp":
                        {
                            if (isPick)
                            {
                                walkedOverObject.transform.parent = null;
                                AddWalkedOverObject(null, null);
                                rb.useGravity = true;
                                rb = null;
                                isPick = false;
                            }

                            else
                            {
                                walkedOverObject.transform.parent = characterTransform;
                                rb = walkedOverObject.GetComponent<Rigidbody>();
                                rb.useGravity = false;
                                isPick = true;
                                guiShow = false;

                            }

                            break;
                        }

                    case "Door":
                        {
                            doorInteraction.PlayAnim();
                            break;
                        }

                    default:
                        break;
                }
            }
        }
    }

    public void AddWalkedOverObject(GameObject walkedOverObject, string tag)
    {
        if (walkedOverObject != null)
        {
            guiShow = true;

            switch (walkedOverObject.tag)
            {
                case "PickUp":
                    {
                        this.walkedOverObject = walkedOverObject;
                        break;
                    }

                case "Door":
                    {
                        this.walkedOverObject = walkedOverObject;
                        doorInteraction = walkedOverObject.GetComponent<OpeningClosingDoor>();
                        break;
                    }
            }
        }

        else
        {
            Debug.Log("Detachement effectue");

            guiShow = false;
            walkedOverObject = null;
        }
    }

    public GameObject GetWalkedOverObject()
    {
        return walkedOverObject;
    }

    void OnGUI()
    {
        if (walkedOverObject != null)
        {
            switch (walkedOverObject.tag)
            {
                case "PickUp":
                    {
                        if (isPick == false && guiShow == true)
                            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 100, 25), "Pick up object");

                        break;
                    }

                case "Door":
                    {
                        if (doorInteraction.GetStatus() == false && guiShow == true)
                            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 100, 25), "Open Door");

                        if (doorInteraction.GetStatus() == true && guiShow == true)
                            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 100, 25), "Close Door");

                        break;
                    }

                default:
                    break;
            }
        }
    }      
}
