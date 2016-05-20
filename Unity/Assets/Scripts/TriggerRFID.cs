using UnityEngine;
using System.Collections;

public class TriggerRFID : MonoBehaviour {

    public int taille;

   public class HitPoint
    {
        public GameObject colliderHit;
        public int zoneHit;
        public float distanceHit;

        public HitPoint(int number, GameObject collision)
        {
            zoneHit = number;
            colliderHit = collision;
            distanceHit = 0;
        }

        public HitPoint()
        {
            zoneHit = 0;
            colliderHit = null;
            distanceHit = 0;
        }
    }

    private HitPoint[] tableau;

	// Use this for initialization
	void Awake ()
    {
        tableau = new HitPoint[taille];

        for (int i = 0; i < taille; i++)
            tableau[i] = new HitPoint();
	}
	
	// Update is called once per frame
	void Update (){
	}

    public bool GotTrigger(int zoneNumber, GameObject collider, bool state)
    {
        int i;

        if (state == false) // Sort de la zone en question
        {
            for (i = 0; i < tableau.Length; i++)
            {
                if (tableau[i].colliderHit == collider)
                {
                        tableau[i].zoneHit = zoneNumber + 1;
                        tableau[i].distanceHit = Vector3.Distance(transform.position, collider.transform.position);
                        return true;
                    }
                }

            }

          else if (state == true) // Entre dans la zone en question
            {
                for (i = 0; i < tableau.Length; i++)
                {
                    if (tableau[i].colliderHit == collider)
                    {
                        if (zoneNumber <= tableau[i].zoneHit)
                        {
                            tableau[i].zoneHit = zoneNumber;
                            tableau[i].distanceHit = Vector3.Distance(transform.position, collider.transform.position);
                            return true;
                        }
                    }

                    else if (tableau[i].zoneHit == 0)
                    {
                        tableau[i] = new HitPoint(zoneNumber, collider);
                        return true;
                    }
                }  
            }
            return false;
        }
    }
