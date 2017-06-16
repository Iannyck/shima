using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFurniture : Commande {

    private BBuildManager manager;
    private Transform furnitureFolder;

    private string id;
    private Vector3 position;
    private Quaternion rotation;
    private float width;
    private float thickness;

    private Furniture newFurniture;
    private Furniture_Recepteur newRecepteur;

    public AddFurniture(BBuildManager manager, Transform furnitureFolder,string id, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), float width = 1f, float thickness = 1f)
    {
        this.id = id;
        this.position = position;
        this.rotation = rotation;
        this.width = width;
        this.thickness = thickness;
        this.furnitureFolder = furnitureFolder;
        this.manager = manager;
    }

    public void Do()
    {
        GameObject newObject = GameObject.Instantiate(Resources.Load("Furniture/" + id), position, rotation) as GameObject;
        newObject.transform.SetParent(furnitureFolder);

        newFurniture = new Furniture(id, width, thickness, newObject);

        newRecepteur = new Furniture_Recepteur(newObject, newFurniture);
    }

    public void Undo()
    {
        manager.RemoveFurniture(newFurniture);
    }

    public Furniture_Recepteur getFurnitureRecepteur()
    {
        return newRecepteur;
    }   
};
