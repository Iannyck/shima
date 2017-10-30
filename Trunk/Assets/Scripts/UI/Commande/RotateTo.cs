using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTo : Commande
{

    private GameObject furniture;
    private Vector3 oldRotation;
    private Vector3 newRotation;


    public RotateTo(GameObject furniture, Vector3 oldRotation, Vector3 newRotation)
    {
        this.furniture = furniture;
        this.newRotation = newRotation;
        this.oldRotation = oldRotation;
        
    }

    public void Do()
    {
        furniture.transform.eulerAngles = newRotation;
    }

    public void Undo()
    {
        furniture.transform.eulerAngles = oldRotation;
    }
}
