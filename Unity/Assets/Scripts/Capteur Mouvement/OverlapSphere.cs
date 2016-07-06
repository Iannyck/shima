using UnityEngine;
using System.Collections;

public class OverlapSphere : MonoBehaviour
{
    private Transform capteurTransform;
    private MovementDetection[] tableau;

    public int taille;
    public bool movement;

    int layerMask = 1 << 9; // Layer : RFID

    class MovementDetection
    {
        private GameObject collider;
        private float distance;

        public MovementDetection()
        {
            collider = null;
            distance = -1;
        }

        public MovementDetection(GameObject a, float b)
        {
            collider = a;
            distance = b;
        }

        public GameObject GetCollider()
        {
            return collider;
        }

        public void SetCollider(GameObject a)
        {
            collider = a;
        }

        public float GetDistance()
        {
            return distance;
        }

        public void SetDistance(float a)
        {
            distance = a;
        }
    }


    void Awake()
    {
        capteurTransform = GetComponent<Transform>();

        tableau = new MovementDetection[taille];

        for (int i = 0; i < taille; i++)
            tableau[i] = new MovementDetection();

    }

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

                            int position = CompareCollider(hitColliders[i].gameObject);
                            float newDistance = Vector3.Distance(hitColliders[i].transform.position, capteurTransform.position);

                            movement = CompareDistance(position, newDistance);
                            tableau[position].SetDistance(newDistance);

                            if (movement == true)
                                Debug.Log("Le personnage est en mouvement");

                            break;
                        }

                    case "PickUp":
                        {
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

    int CompareCollider(GameObject a)
    {
        for (int i= 0; i < taille; i++)
        {
            if (a == tableau[i].GetCollider())
                return i;

            if (tableau[i].GetDistance() == -1)
            {
                tableau[i] = new MovementDetection(a, 0);
                return i;
            }
        }

        return -1;
    }

    bool CompareDistance(int a,float b)
    {
        if (b == tableau[a].GetDistance())
            return false;

        return true;
    }
}
