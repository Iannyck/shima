using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : Commande {

    private GameObject furniture;

    private Vector3 oldPosition;
    private Vector3 newPosition;

    public MoveTo (GameObject furniture, Vector3 newPosition)
    {
        this.furniture = furniture;
        oldPosition = furniture.transform.position;
        this.newPosition = newPosition;
    }

	public void Do()
    {
        furniture.transform.position = newPosition;
    }

    public void Undo()
    {
        furniture.transform.position = oldPosition;
    }
}
