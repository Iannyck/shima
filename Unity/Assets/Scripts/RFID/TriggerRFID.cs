using UnityEngine;
using System.Collections;

public class TriggerRFID : MonoBehaviour {

    public int taille;                                                                 // Correspond a la taille maximale du tableau du capteur RFID
    public float noise;

    public GameObject zone1;                                                           // Correspond aux différentes zones du capteur RFID
    public GameObject zone2;
    public GameObject zone3;

    private HitPoint[] tableau;                                                        // Correspond au tableau contenant les objets de classe HitPoint
    private Transform capteurPosition;

    private DatabaseService databaseService;

    public class HitPoint
    {
        private GameObject colliderHit;                                 // Correspond a l'objet qui est dans le champs de détection du capteur RFID

        private int zoneHit;                                            // Correspond au numero de la zone du capteur RFID
        private float puissanceZone;                                    // Correspond à la puissance relative à la zone en question

        public HitPoint(int number, GameObject collision)               // Constructeur qui prend la zone et l'objet en collision en arguments
        {
            zoneHit = number;
            colliderHit = collision;
            puissanceZone = -1;                                          // Prendre note que la puissance est toujours initialisé a -1
        }

        public HitPoint()                                               // Constructeur par défault de la classe
        {
            zoneHit = 0;
            colliderHit = null;
            puissanceZone = -1;                                          // Prendre note que la puissance est également initialisé à -1
        }

        public GameObject GetCollider()                                 // Retourne le GameObject de l'objet
        {
            return colliderHit;
        }

        public int GetZone()                                            // Retourne le numero de la zone de l'objet
        {
            return zoneHit;
        }

        public float GetPuissance()                                     // Retourne la puissance de l'objet
        {
            return puissanceZone;
        }

        public void ModifyCollider(GameObject a)                        // Permet de modifier le collider
        {
            colliderHit = a;
        }

        public void ModifyZone(int a)                                   // Permet de modifier le numero de zone
        {
            zoneHit = a;
        }

        public void ModifyPuissance(int a, bool b)                      // Permet de modifier la puissance ou int correspond a la zone
        {
            if (a > 3)
                puissanceZone = 0;

            else
            {
                ZoneRFID script = GameObject.Find("Zone" + a).GetComponentInChildren<ZoneRFID>();
                puissanceZone = script.GetPuissanceZone();

                if (b == true)
                    puissanceZone = puissanceZone - 10;                 //         /!\ Modifier la valeur 10 par une equation mathematique ou un % /!\
            }
        }

        public bool CompareZone(int a)                                  // Compare l'entier a et le numero de la zone, retourne vrai si la zone est plus
        {                                                               // proche du capteur que la precedente (Donc, la puissance sera plus élevée) 
            if (a < GetZone())
                return true;

            else
                return false;
        }

    }                                                           // Classe HitPoint contenant un GameObject, un numero de zone et une puissance

	void Awake ()
    {    
        tableau = new HitPoint[taille];                                                // Initialise le tableau par default selon le parametre taille
                                                                                       // Taille peut etre modifie directement sur Unity (Public Watch)
        for (int i = 0; i < taille; i++)
            tableau[i] = new HitPoint();

        noise = 0;

        databaseService = GetComponentInParent<RFIDController>().DatabaseService;
    }
	
	void Update ()
    {
        string timestamp = System.DateTime.Now.ToLongTimeString();

        for (int i = 0; i < 10; i++)
            if (tableau[i].GetPuissance() != -1)
                {

                if (databaseService == null)
                {
                    Debug.Log("Erreur Database");

                    if (GetComponentInParent<RFIDController>().DatabaseService == null)
                        Debug.Log("Erreur Database 2");

                    databaseService = GetComponentInParent<RFIDController>().DatabaseService;
                }

                else
                    StartCoroutine(databaseService.InsertRFIDData(timestamp, transform.parent.name + " - " + gameObject.name, tableau[i].GetPuissance(), tableau[i].GetCollider().name));
            }
    }

    public void GotTrigger(int zoneNumber, GameObject collider, bool state)             // Fonction qui est utilisee par les differentes zones du capteur RFID
    {
        int i;
        bool obstacle;

        if (state == false)                                                             // Si le parametre state est faux, cela indique que l'on sort de la zone en question
        {
            for (i = 0; i < tableau.Length; i++)                                        // On compare le tableau de HitPoint afin de trouver la position du collider
            { 
                if (tableau[i].GetCollider() == collider)
                {
                    tableau[i].ModifyZone(zoneNumber + 1);                              // On ajoute 1 au numero de zone afin d'indiquer que l'on se trouve maintenant dans une zone plus loin du capteur

                    obstacle = CheckObstacle(collider);
                    tableau[i].ModifyPuissance(zoneNumber + 1, obstacle);

                    Debug.Log(tableau[i].GetPuissance(), tableau[i].GetCollider());

                    return; 
                }
            }
         }

          else if (state == true && collider.layer == LayerMask.NameToLayer("RFID"))    // Si le parametre state est vrai, cela indique que l'on rentre dans la zone en question
            {
                for (i = 0; i < tableau.Length; i++)                                    // On parcourt le tableau afin de déterminer si l'objet en collision occupe déjè une case du tableau
                {
                    if (tableau[i].GetCollider() == collider)                           
                    {
                        if (tableau[i].CompareZone(zoneNumber))                         // Si oui, on compare le numéro de la zone afin de déterminer si le nouvelle zone est plus proche du capteur
                        {
                            tableau[i].ModifyZone(zoneNumber);                          // Si la zone est plus proche, on doit modifier la puissance ainsi que le numero de la zone

                            obstacle = CheckObstacle(collider);
                            tableau[i].ModifyPuissance(zoneNumber, obstacle);

                            Debug.Log(tableau[i].GetPuissance(), tableau[i].GetCollider());

                            return;
                        }
                    }

                    else if (tableau[i].GetPuissance() == -1)                           // Si la puissance correspond à -1, la case est vide
                    {
                        tableau[i] = new HitPoint(zoneNumber, collider);                // On occupe alors la case avec l'objet qui vient d'entrer en collision

                        obstacle = CheckObstacle(collider);
                        tableau[i].ModifyPuissance(zoneNumber, obstacle);

                        Debug.Log(tableau[i].GetPuissance(), tableau[i].GetCollider());

                        return;
                    }
                }  
            }

            else if (state == true && collider.tag == "Noise") // Indique que le collider produit du bruit
        {
            float noisePuissance = collider.GetComponent<ZoneRFID>().GetPuissanceZone();
            noise = noise + noisePuissance * 1;                                                         // Remplacer 1 par une equation

            // Permet de calculer la distance entre deux sources de bruits
            // Vector3 noisePosition = collider.gameObject.GetComponent<Transform>().position;
            // float noiseDistance = Vector3.Distance(noisePosition, GetComponent<Transform>().position);
            //noise = noise + (noiseDistance * 1);                                                            

        }

            return;
        }

    public bool CheckObstacle(GameObject collider)                                      // Fonction qui permet de determiner si un mur ou un autre collider se trouve entre le capteur et l'objet détecté
    {
        capteurPosition = GetComponent<Transform>();
        RaycastHit hitInfo;

        Debug.DrawLine(capteurPosition.position, collider.transform.position, Color.white, 10f);

        Physics.Linecast(capteurPosition.position, collider.transform.position, out hitInfo);
        {
            if (hitInfo.collider.CompareTag("Fixed") || hitInfo.collider.CompareTag("Door"))                                   // Si le collider est un objet de type Fixed, la puissance doit etre diminue
                return true;

            else
                return false;
        }
    }
}
