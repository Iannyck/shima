using UnityEngine;
using System.Collections;

public class TriggerCollider : MonoBehaviour
{
    RaycastHit hit;
    GameObject pickedUpObject;
    Transform characterTransform;

    bool isPick = false;

    void Start()
    {
        characterTransform = GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.left);

        if (isPick == false)
        {

            if (Input.GetKeyDown("e"))
            {

                if (Physics.Raycast(transform.position, fwd, out hit, 5))
                {
                    Debug.Log(characterTransform.rotation.y);
                    if (hit.collider.gameObject.tag == "PickUp")
                    {
                        //Debug.Log(hit.distance);

                        if (hit.distance >= 2)
                        {
                            PickUp();
                        }

                        else
                        {
                           // Debug.Log(characterTransform.rotation.y);
                            if (characterTransform.rotation.y >= 0f && characterTransform.rotation.y < 20f)
                            {
                                characterTransform.position = new Vector3(characterTransform.position.x +2f, characterTransform.position.y, characterTransform.position.z);
                            }

                            else if (characterTransform.rotation.y >= 80f && characterTransform.rotation.y < 100f)
                            {
                                characterTransform.position = new Vector3(characterTransform.position.x, characterTransform.position.y, characterTransform.position.z-2f);
                            }

                            else if (characterTransform.rotation.y >= 170f && characterTransform.rotation.y < 190f)
                            {
                                characterTransform.position = new Vector3(characterTransform.position.x - 2f, characterTransform.position.y, characterTransform.position.z);
                            }

                            else if (characterTransform.rotation.y >= 260f && characterTransform.rotation.y < 280f)
                            {
                                characterTransform.position = new Vector3(characterTransform.position.x, characterTransform.position.y, characterTransform.position.z -2f);
                            }

                            PickUp();
                        }
                    }
                }

            }
        }

        else if (isPick == true)
        {

            if (Input.GetKeyDown("e"))
            {
                pickedUpObject.transform.parent = null;
                pickedUpObject = null;
                isPick = false;
            }
        }
    }

    void PickUp()
    {
       pickedUpObject = hit.collider.gameObject;
       pickedUpObject.transform.parent = characterTransform;

       isPick = true;
    }
}
