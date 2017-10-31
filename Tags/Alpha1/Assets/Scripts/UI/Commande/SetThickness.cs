using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetThickness : Commande
{
    private Furniture furniture;

    private float oldThickness;
    private float newThickness;

    public SetThickness(Furniture furniture, float oldThickness, float newThickness)
    {
        this.furniture = furniture;
        this.oldThickness = oldThickness;
        this.newThickness = newThickness;
    }

    public void Do()
    {
        furniture.thickness = newThickness;
    }

    public void Undo()
    {
        furniture.thickness = oldThickness;
    }
}
