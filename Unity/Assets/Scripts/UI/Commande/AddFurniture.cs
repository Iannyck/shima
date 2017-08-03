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
    private Vector3 scale;
    private float thickness;

    private Furniture newFurniture;
    private Furniture_Recepteur newRecepteur;

    public AddFurniture(BBuildManager manager, Transform furnituresFolder, Transform sensorsFolder, Transform wallsFolder, string id, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), Vector3 scale = default(Vector3), float thickness = 1f)
    {
        this.id = id;
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.thickness = thickness;
        this.furnituresFolder = furnituresFolder;
        this.sensorsFolder = sensorsFolder;
        this.wallsFolder = wallsFolder;
        this.manager = manager;
    }

    public void Do()
    {
        GameObject newObject = Load();
   
        newFurniture = new Furniture(id, thickness, newObject);

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
            newObject.transform.localScale = scale;
        }

        else
        {
            obj = Resources.Load("Sensor/" + id) as GameObject;
            if (obj != null)
            {
                newObject = GameObject.Instantiate(obj, position, rotation) as GameObject;
                newObject.transform.SetParent(sensorsFolder);
                newObject.transform.localScale = scale;
            }

            else
            {
                obj = Resources.Load("Wall/" + id) as GameObject;
                if (obj != null)
                {
                    newObject = GameObject.Instantiate(obj, position, rotation) as GameObject;
                    newObject.transform.SetParent(wallsFolder);
                    newObject.transform.localScale = scale;
                }
            }
        }

        if (newObject != null)
        return newObject;

        else
        {
            string a = id.ToString();
            Debug.Log("Impossible de charger le prefab du type indiqué (Voir AddFurniture.Load)");
            Debug.Log(a);
            return null;
        }
    }
};
