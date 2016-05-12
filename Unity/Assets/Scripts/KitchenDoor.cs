using UnityEngine;
using System.Collections;

public class KitchenDoor : MonoBehaviour {

    private bool guiShow = false;
    private bool isOpen = false;

    public GameObject door;
    public Animation anim;
    public int rayLenght = 10;

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLenght))
        {
            if (hit.collider.gameObject.tag == "Door")
            {
                guiShow = true;

                if (Input.GetKeyDown("e") && isOpen == false)
                {
                    anim.Play("KitchenDoorOpen");
                    isOpen = true;
                    guiShow = false;
                }

                else if (Input.GetKeyDown("e") && isOpen == true)
                {
                    anim.Play("KitchenDoorClose");
                    isOpen = false;
                    guiShow = true;
                }
            }
        }

        else
            guiShow = false;
    }

    void OnGUI()
    {
        if (guiShow == true && isOpen == false)
        {
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 100, 25), "Open Door");
        }

    }


}


