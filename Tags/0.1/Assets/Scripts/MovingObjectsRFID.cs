using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingObjectsRFID : MonoBehaviour {

    public List<Vector3> listePositionFinal = new List<Vector3>();
    private List<Vector3> listePosition0 = new List<Vector3>();
    private List<Vector3> listePosition1= new List<Vector3>();
    private List<Vector3> listePosition2 = new List<Vector3>();
    private List<Vector3> listePosition3 = new List<Vector3>();

    private int actualPosition;
    Transform myTransform;

    Vector3 position0 = new Vector3(19.01f, 8.83f, 120.13f);
    Vector3 position1 = new Vector3(5.66f,11.19f,32.61f);
    Vector3 position2 = new Vector3(-145.98f, 9.03f, 35.02f);
    Vector3 position3 = new Vector3(-110.3f,9.6f,-14.4f);

    Vector3 position1_1 = new Vector3(19.2f, 8.99f, 99.96f);
    Vector3 position1_2 = new Vector3(17.64f, 9.6f, 84.72f);
    Vector3 position1_3 = new Vector3(-24.1f, 8.99f, 99.66f);
    Vector3 position1_4 = new Vector3(-24.1f, 8.9f, 70.7f);
    Vector3 position1_5 = new Vector3(-9.2f, 8.9f, 53.3f);

    Vector3 position2_1 = new Vector3(-14.1f, 11f, 34.7f);
    Vector3 position2_2 = new Vector3(-37f,11f,53.7f);
    Vector3 position2_3 = new Vector3(-57.8f, 11f, 79.3f);
    Vector3 position2_4 = new Vector3(-102.2f, 11f,79.3f);
    Vector3 position2_5 = new Vector3(-133.2f, 11f, 79.3f);
    Vector3 position2_6 = new Vector3(-133.2f, 11f, 28.9f);

    Vector3 position3_1 = new Vector3(-113.5f, 11f, 28.9f);
    Vector3 position3_2 = new Vector3(-113.5f, 11f, 0f);

    // Use this for initialization
    void Start ()
    {
        actualPosition = 0;
        myTransform = GetComponent<Transform>();
          
        listePositionFinal.Insert(0, position0);
        listePositionFinal.Insert(1, position1);
        listePositionFinal.Insert(2, position2);
        listePositionFinal.Insert(3, position3);

        listePosition1.Insert(0, position1_1);
        listePosition1.Insert(1, position1_2);
        listePosition1.Insert(2, position1_3);
        listePosition1.Insert(3, position1_4);
        listePosition1.Insert(4, position1_5);

        listePosition2.Insert(0, position2_1);
        listePosition2.Insert(1, position2_2);
        listePosition2.Insert(2, position2_3);
        listePosition2.Insert(3, position2_4);
        listePosition2.Insert(4, position2_5);
        listePosition2.Insert(5, position2_6);

        listePosition3.Insert(0, position3_1);
        listePosition3.Insert(1, position3_2);

        listePosition0.Insert(0, position3_2);
        listePosition0.Insert(1, position3_1);
        listePosition0.Insert(2, position2_6);
        listePosition0.Insert(3, position2_5);
        listePosition0.Insert(4, position2_4);
        listePosition0.Insert(5, position2_3);
        listePosition0.Insert(6, position2_2);
        listePosition0.Insert(7, position1_4);
        listePosition0.Insert(8, position1_3);
        listePosition0.Insert(9, position1_1);
    }
	
	// Update is called once per frame

    public void ChangePosition()
    {
        actualPosition = actualPosition + 1;
        NextPosition(actualPosition);
        return;
    }

    private void NextPosition(int a)
    {
        switch (a)
        {
            case 0:
                for (int i = 0; i != listePosition1.Count; i++)
                {
                    myTransform.position = listePosition1[i];
                }

                myTransform.position = position0;
                return;

            case 1:
                for (int i = 0; i != listePosition2.Count; i++)
                {
                    myTransform.position = listePosition2[i];
                }

                myTransform.position = position1;
                return;

            case 2:
                for (int i = 0; i != listePosition3.Count; i++)
                {
                    myTransform.position = listePosition3[i];
                }

                myTransform.position = position2;
                return;

            case 3:
                for (int i = 0; i != listePosition0.Count; i++)
                {
                    myTransform.position = listePosition0[i];
                }

                actualPosition = -1;

                myTransform.position = position3;
                return;

            default:
                return;
        }
    }
}
