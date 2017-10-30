using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetName : Commande
{

    private GameObject furniture;

    private string oldName;
    private string newName;

    public SetName(GameObject furniture, string oldName, string newName)
    {
        this.furniture = furniture;
        this.oldName = oldName;
        this.newName = newName;
    }

    public void Do()
    {
        furniture.transform.name = newName;
    }

    public void Undo()
    {
        furniture.transform.name = oldName;
    }
}
