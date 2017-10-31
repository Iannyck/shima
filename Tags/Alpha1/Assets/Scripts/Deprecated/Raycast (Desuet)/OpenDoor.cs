using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

    /* La porte ouvre seulement si le joueur cliquait sur le bouton gauche de sa souris
       lorsque qu'il est entre en collision avec la porte. Il faut modifier la fonction
       probablement avec un RayCast pour calculer la distance et savoir si le joueur peut
       ouvrir ou non la porte.

       Sinon, l'animation semble fonctionner, mais cest difficile a dire puisque l'on doit
       maintenant enfonce le clic gauche pour la voir. On voit alors l'ouverture et la fermeture
       plutot que distinguer deux phases dinstinctes.         
       
        Sinon le cube recoit la porte en plein visage lorsqu'il sort de la chambre, il faudrait
        peut etre modifier la position du caractere ou une porte qui s'ouvre des deux cotes       */

    private Animation anim;

    public bool isOpen = false;

    public GameObject playerObject;

	void Awake() {

        anim = GetComponent<Animation>();
	}

    void Update() {
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == playerObject && Input.GetButton("Fire1"))
        {
  
            if (isOpen == false)
            {
                anim.Play("OpenDoor");
                isOpen = true;
            }

            else if (isOpen == true)
            {
                anim.Play("CloseDoor");
                isOpen = false;
            }
        }
    }
}
