using UnityEngine;
using System.Collections;

public class TriggerCollider : MonoBehaviour
{

    //    Transform playerH;
    //    Rigidbody rgbd;

    //    private bool isPick = false;

    //    void Start ()
    //    {
    //        rgbd = GetComponent<Rigidbody>();
    //	}

    //    void Update()
    //    {

    //    }

    //    void OnTriggerEnter(Collider other)
    //    {
    //        Debug.Log("Collision detecte");

    //        if (other.gameObject.tag == "Player")
    //        {
    //            if (Input.GetKeyDown("e"))
    //            {
    //                Debug.Log("Touche E appuye");
    //                transform.parent = playerH.transform;
    //                transform.localPosition = new Vector3(0, 0, 0);
    //                rgbd.isKinematic = true;

    //                isPick = true;
    //            }
    //        }
    //    }

    //}

    RaycastHit hit;
    GameObject pickedUpObject;
    Transform characterTransform;
    Transform objectTransform;

    void Start()
    {
        characterTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKey("e"))
        {

            if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
            {
               
                if (hit.collider.gameObject.tag == "PickUp")
                {
                    pickedUpObject = hit.collider.gameObject;
                    pickedUpObject.transform.position = new Vector3(characterTransform.position.x, characterTransform.position.y, characterTransform.position.z);

                    Debug.Log(pickedUpObject.transform.position);
                }
            }
        }

        else
        { 
            pickedUpObject = null;
        }
    }
}
