using UnityEngine;
using System.Collections;

public class ZoneRFID : MonoBehaviour {

    TriggerRFID script;
    public float Puissance;
    public int numberZone;
    public int taille;

    public puissanceObject[] tableauPuissance;

    public class puissanceObject                                      // Classe doit etre dans le parent pour etre modifiable par toutes les fonctions
    {
        public GameObject objet;
        public float puissance;

        public puissanceObject (float number, GameObject collision)
        {
            puissance = number;
            objet = collision;
        }

        public puissanceObject()
        {
            puissance = 0;
            objet = null;
        }
    }

    // Use this for initialization
    void Start () {
        tableauPuissance = new puissanceObject[taille];

        for (int i = 0; i < taille; i++)
            tableauPuissance[i] = new puissanceObject();

        script = GetComponentInParent<TriggerRFID>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider other)
    {
        GameObject collider = other.gameObject;

        if (script.GotTrigger(numberZone, collider,true))
        {
            for (int i = 0; i < taille; i++)
            {
                if (tableauPuissance[i].puissance != 0)
                {
                    if (tableauPuissance[i].objet == collider)
                    {
                        tableauPuissance[i].puissance = Puissance;
                        Debug.Log(tableauPuissance[i].puissance);
                        return;
                    }
                }

                else
                {
                    tableauPuissance[i] = new puissanceObject(Puissance,collider);
                    Debug.Log(tableauPuissance[i].puissance);
                    return;
                }

              }
          }

        else
            Debug.Log(0);
    }

    void OnTriggerExit(Collider other)
    {
        GameObject collider = other.gameObject;

        script.GotTrigger(numberZone, collider, false);

        for (int i = 0; i < taille; i++)
        {
            if (tableauPuissance[i].objet == collider)
            {
                tableauPuissance[i].puissance = Puissance/2; // Doit etre remplacer par une fonction qui calcule la difference de puissance entre deux zones

                if (tableauPuissance[i].puissance < 25)
                    tableauPuissance[i].puissance = 0;

                Debug.Log(tableauPuissance[i].puissance);
                return;
            }
        }
    }
}
