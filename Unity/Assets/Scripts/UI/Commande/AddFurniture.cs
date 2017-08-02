using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFurniture : Commande {

    private BBuildManager manager;
    private Transform furnituresFolder;
    private Transform sensorsFolder;
    private Transform wallsFolder;

    private string id;
    private Vector3 position;
    private Quaternion rotation;
    private float width;
    private float thickness;

    private Furniture newFurniture;
    private Furniture_Recepteur newRecepteur;

    public AddFurniture(BBuildManager manager, Transform furnituresFolder, Transform sensorsFolder, Transform wallsFolder, string id, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), float width = 1f, float thickness = 1f)
    {
        this.id = id;
        this.position = position;
        this.rotation = rotation;
        this.width = width;
        this.thickness = thickness;
        this.furnituresFolder = furnituresFolder;
        this.sensorsFolder = sensorsFolder;
        this.wallsFolder = wallsFolder;
        this.manager = manager;
    }

    public void Do()
    {
        GameObject newObject = Load();
   
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

    public GameObject Load()
    {
        GameObject obj = Resources.Load("Furniture/" + id) as GameObject;
        GameObject newObject = null;

        if (obj != null)
            if (rotation == default(Quaternion))
                rotation = obj.transform.rotation;

        if (obj != null)
        {
            newObject = GameObject.Instantiate(obj, position, rotation) as GameObject;
            newObject.transform.SetParent(furnituresFolder);
        }

        else
        {
            obj = Resources.Load("Sensor/" + id) as GameObject;
            if (obj != null)
            {
                newObject = GameObject.Instantiate(obj, position, rotation) as GameObject;
                newObject.transform.SetParent(sensorsFolder);
            }

            else
            {
                obj = Resources.Load("Wall/" + id) as GameObject;
                if (obj != null)
                {
                    newObject = GameObject.Instantiate(obj, position, rotation) as GameObject;
                    newObject.transform.SetParent(wallsFolder);
                }
            }
        }

        return newObject;
    }
};
