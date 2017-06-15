using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTo : Commande
{

    private GameObject furniture;
    private Quaternion oldRotation;
    private Quaternion newRotation;


    public RotateTo(GameObject furniture, Quaternion newRotation)
    {
        this.furniture = furniture;
        this.newRotation = newRotation;
        oldRotation = furniture.transform.localRotation;
        
    }

    public void Do()
    {
        furniture.transform.localRotation = newRotation;
    }

    public void Undo()
    {
        furniture.transform.localRotation = oldRotation;
    }
}
