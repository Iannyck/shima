using UnityEngine;
using System.Collections;

public class PickUpObject : MonoBehaviour {

    private bool guiShow = false;
    private bool isPick = false;

    public GameObject pickUp;
    // public Animation anim;
    public int rayLenght = 10;


    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.left);

        //Debug.DrawRay(transform.position, fwd * rayLenght, Color.red);
        //Debug.Log(fwd + "-" + transform.position);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLenght))
        {
            if (hit.collider.gameObject.tag == "PickUp")
            {
                guiShow = true;

                if (Input.GetKeyDown("e") && isPick == false)
                {
                    // anim.Play("");
                    


                    isPick = true;
                    guiShow = false;
                }

                else if (Input.GetKeyDown("e") && isPick == true)
                {
                    // anim.Play("");
                    isPick = false;
                    guiShow = true;
                }
            }
        }

        else
            guiShow = false;
    }

    void OnGUI()
    {
        if (guiShow == true && isPick == false)
        {
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 100, 25), "Pick up object");
        }

    }

}
