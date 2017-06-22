using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BBuildManager : MonoBehaviour {

    private EditManager editManager;

    private List<Furniture_Recepteur> furnitureList = new List<Furniture_Recepteur>();
    private Stack<Commande> commandeList = new Stack<Commande>();

    public Transform furnitureFolder;
    public Transform sensorsFolder;
    public Transform wallsFolder;

    private void Start()
    {
        editManager = this.GetComponentInParent<EditManager>();
        furnitureFolder = this.transform.GetComponentInParent<BMenuManager>().furnituresFolder.transform;
        wallsFolder = this.transform.GetComponentInParent<BMenuManager>().wallsFolder.transform;
        sensorsFolder = this.transform.GetComponentInParent<BMenuManager>().sensorsFolder.transform;
    }


    public void AddFurniture(string id, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), float width = 1f, float thickness = 1f)
    {
        Commande commande = new AddFurniture(this, furnitureFolder, sensorsFolder, wallsFolder, id, position, rotation, width, thickness);
        commande.Do();
        commandeList.Push(commande);
        furnitureList.Add((commande as AddFurniture).getFurnitureRecepteur());
    }
    public void MoveFurniture(Furniture_Recepteur recepteur, Vector3 oldPosition, Vector3 newPosition)
    {
        if (oldPosition != newPosition)
        {
            Commande commande = new MoveTo(recepteur.getGameObject(), oldPosition, newPosition);
            commande.Do();
            commandeList.Push(commande);
        }
    }
    public void SetName(Furniture_Recepteur recepteur, string oldName, string newName)
    {
        if (oldName != newName)
        {
            Commande commande = new SetName(recepteur.getGameObject(), oldName, newName);
            commande.Do();
            commandeList.Push(commande);
            editManager.RefreshInfos();
        }
    }
    public void RotateFurniture(Furniture_Recepteur recepteur, Vector3 oldRotation, Vector3 newRotation)
    {
            Commande commande = new RotateTo(recepteur.getGameObject(), oldRotation, newRotation);
            commande.Do();
            commandeList.Push(commande);
            editManager.RefreshInfos();
    }

   



    public void Cancel()
    {
        if (commandeList.Count != 0)
        {
            Commande commande = commandeList.Pop();
            commande.Undo();
            editManager.RefreshInfos();
        }

        else
            Debug.Log("Aucune commande ne peut etre annule");
    }

    public void LoadFurniture(string jsonTextLine)
    {
        Furniture newFurniture = JsonUtility.FromJson<Furniture>(jsonTextLine);

        GameObject newObject = Instantiate(Resources.Load("Furniture/" + newFurniture.getType()), newFurniture.getPosition(), newFurniture.getRotation()) as GameObject;

        newObject.transform.SetParent(this.transform.GetComponentInParent<BMenuManager>().furnituresFolder.transform);
        newObject.name = newFurniture.getName();

        Furniture_Recepteur newRecepteur = new global::Furniture_Recepteur(newObject, newFurniture);
        furnitureList.Add(newRecepteur);
    }

    public void RemoveFurniture(Furniture furniture)
    {
        foreach (Furniture_Recepteur currentFurniture in furnitureList)
        {
            if (currentFurniture.getFurniture() == furniture)
            {
                if (currentFurniture.getGameObject() == editManager.getSelected())
                    editManager.CloseEdit();

                Destroy(currentFurniture.getGameObject().gameObject);
                furnitureList.Remove(currentFurniture);
                return;        
            }
        }

        Debug.Log("Erreur: Le Furniture_Recepteur en parametre est invalide (Voir RemoveFurniture de BBuildManager)");
    }
    public List<Furniture_Recepteur> getFurnitureList()
    {
        Debug.Log(furnitureList.Count);
        return furnitureList;
    }

    public Furniture_Recepteur FindInFurnitureList(GameObject a)
    {
        for (int i = 0; i<furnitureList.Count; i++)
        {
            if (furnitureList[i].getGameObject() == a)
                return furnitureList[i];
        }

        Debug.Log("Erreur lors de la recherche du GameObject (Voir FindInFurnitureList dans BBuildManager)");
        return null;
    }



//    public void AddWall() {
////		GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
////		cube.name = "wall" + wallIndex;
////		wallIndex++;
////		cube.transform.position = new Vector3 (0, 0, 0);
////		cube.transform.localScale = new Vector3 (4,1,1);

//		GameObject wall = Instantiate(Resources.Load("Wall") , new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;

//        wall.transform.SetParent(wallsFolder.transform);
//		wall.name = "wall" + wallIndex;
//		wallIndex++;
//	}

//    public void AddWall(Vector3 position, Quaternion rotation)
//    {
//        GameObject wall = Instantiate(Resources.Load("Wall"), position, rotation) as GameObject;
//        wall.transform.SetParent(wallsFolder.transform);
//        wall.name = "wall" + wallIndex;
//        wallIndex++;

//    }

//    public void AddSensor(string id)
//    {
//        GameObject sensor= Instantiate(Resources.Load("Sensor/" + id), new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
//        sensor.transform.SetParent(sensorsFolder.transform);
//        sensor.name = "sensor" + sensorIndex;
//        sensorIndex++;
//    }

    public void AddSpeaker()
    {
	}

//	public void AddFurniture(Button button) {
//		Debug.Log (button.GetComponentsInChildren<Text> ());
////		GameObject furniture = Instantiate(Resources.Load(button.GetComponentsInChildren<Text>()) , new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
////		furniture.name = "furniture" + furnitureIndex;
////		furnitureIndex++;
//	}

	// Use this for initialization
	//void Start ()
 //   {
 //       if (furnituresFolder == null)
 //       {
 //           furnituresFolder = GameObject.Find("Furnitures");

 //           if (furnituresFolder == null)
 //            Debug.Log("Select a default furnitures folder or rename it to Furnitures");
 //       }

 //       if (sensorsFolder == null)
 //       {
 //           sensorsFolder = GameObject.Find("Sensors");

 //           if (sensorsFolder == null)
 //               Debug.Log("Select a default sensors folder or rename it to Sensors");
 //       }

 //       if (wallsFolder == null)
 //       {
 //           sensorsFolder = GameObject.Find("Walls");

 //           if (sensorsFolder == null)
 //               Debug.Log("Select a default walls folder or rename it to Walls");
 //       }

 //   }
	
	// Update is called once per frame
	void Update () {
		
	}
}
