using UnityEngine;
using System.Collections;

public class OverlapSphere : MonoBehaviour
{
    private Transform capteurTransform;

    int layerMask = 1 << 9; // Layer : RFID

    // Use this for initialization
    void Awake()
    {
        capteurTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(capteurTransform.position, 45, layerMask);

        if (hitColliders.Length != 0)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                switch (hitColliders[i].tag)
                {
                    default:
                        break;

                    case "Player":
                        {
                            Debug.Log("Le capteur a detecte le personnage");
                            break;
                        }

                    case "PickUp":
                        {
                            Debug.Log("Le capteur a detecte un objet de type PickUp");
                            break;
                        }
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 45);
    }
}
