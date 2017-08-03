using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Furniture_Recepteur
{
    private GameObject gameobject;
    private Furniture furniture;

    public Furniture_Recepteur(GameObject gameobject, Furniture furniture)
    {
        this.gameobject = gameobject;
        this.furniture = furniture;
    }

    public GameObject getGameObject()
    {
        return gameobject;
    }
    public Furniture getFurniture()
    {
        return furniture;
    }

    public void UpdateInfos(GameObject a)
    {
        furniture.UpdateAll(a.name, a.transform.position, a.transform.rotation, a.transform.localScale);
        SetGameObject(a);
    }

    private void SetGameObject(GameObject a)
    {
        gameobject = a;
    }
}
