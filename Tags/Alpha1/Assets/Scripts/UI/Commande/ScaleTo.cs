using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTo : Commande
{

    private GameObject furniture;
    private Vector3 oldScaling;
    private Vector3 newScaling;
    private Transform oldParent;


    public ScaleTo(GameObject furniture, Vector3 oldScaling, Vector3 newScaling)
    {
        this.furniture = furniture;
        this.newScaling = newScaling;
        this.oldScaling = oldScaling;
        this.oldParent = furniture.transform.parent;
    }

    public void Do()
    {
        GameObject emptyGo = new GameObject();
        emptyGo.transform.localScale = new Vector3(1, 1, 1);

        furniture.transform.parent = emptyGo.transform;
        furniture.transform.localScale = newScaling;

        furniture.transform.parent = oldParent;
        UnityEngine.Object.Destroy(emptyGo);
    }

    public void Undo()
    {
        furniture.transform.localScale = oldScaling;
    }
}
