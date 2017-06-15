using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTo : Commande
{

    private GameObject furniture;
    private Vector3 oldScaling;
    private Vector3 newScaling;


    public ScaleTo(GameObject furniture, Vector3 newScaling)
    {
        this.furniture = furniture;
        this.newScaling = newScaling;
        oldScaling = furniture.transform.localScale;
    }

    public void Do()
    {
        furniture.transform.localScale = newScaling;
    }

    public void Undo()
    {
        furniture.transform.localScale = oldScaling;
    }
}
