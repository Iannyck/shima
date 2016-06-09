using UnityEngine;
using System.Collections;

public class RFIDController : MonoBehaviour {

    public GameObject capteur1;
    public GameObject capteur2;
    public GameObject capteur3;
    public GameObject capteur4;

	// Use this for initialization
	void Start ()
    {
	    capteur1.SetActive(true);
        capteur2.SetActive(false);
        capteur3.SetActive(false);
        capteur4.SetActive(false);

        InvokeRepeating("TokenRing", 1f, 1f);
	}

    void TokenRing()
    {
        if (capteur1.activeSelf == true)
        {
            capteur1.SetActive(false);
            capteur2.SetActive(true);
        }

        else if (capteur2.activeSelf == true)
        {
            capteur2.SetActive(false);
            capteur3.SetActive(true);
        }

        else if (capteur3.activeSelf == true)
        {
            capteur3.SetActive(false);
            capteur4.SetActive(true);
        }

        else if (capteur4.activeSelf == true)
        {
            capteur4.SetActive(false);
            capteur1.SetActive(true);
        }

    }

    IEnumerator WaitASec(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Waited " + waitTime + " sec");
     }
}
