using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BBuildManager : MonoBehaviour
{
    #region Parametres

    private EditManager editManager;
    public BMenuManager menuManager;

    #region  Listes

    private List<Furniture_Recepteur> furnitureList = new List<Furniture_Recepteur>();
    private Stack<Commande> commandeList = new Stack<Commande>();

    #endregion

    #region Folders

    public Transform furnitureFolder;
    public Transform sensorsFolder;
    public Transform wallsFolder;

    #endregion

    #endregion

    #region Functions

    #region MonoBehavior

    private void Start()
    {
        if (furnitureFolder == null)
        {
            furnitureFolder = GameObject.Find("Furnitures").transform;

            if (furnitureFolder == null)
                Debug.Log("Select a default furnitures folder or rename it to Furnitures");
        }

        if (sensorsFolder == null)
        {
            sensorsFolder = GameObject.Find("Sensors").transform;

            if (sensorsFolder == null)
                Debug.Log("Select a default sensors folder or rename it to Sensors");
        }

        if (wallsFolder == null)
        {
            sensorsFolder = GameObject.Find("Walls").transform;

            if (sensorsFolder == null)
                Debug.Log("Select a default walls folder or rename it to Walls");
        }

        editManager = this.GetComponentInParent<EditManager>();
        furnitureFolder = this.transform.GetComponentInParent<BMenuManager>().furnituresFolder.transform;
        wallsFolder = this.transform.GetComponentInParent<BMenuManager>().wallsFolder.transform;
        sensorsFolder = this.transform.GetComponentInParent<BMenuManager>().sensorsFolder.transform;

        AddFurniture("Terrain");
    }
    private void Update()
    {
    }

    #endregion

    #region Commande

    public void AddFurniture(string id, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), Vector3 scale = default(Vector3), float thickness = 1f)
    {
        Commande commande = new AddFurniture(this, furnitureFolder, sensorsFolder, wallsFolder, id, position, rotation, scale, thickness);
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
    public void ScaleFurniture(Furniture_Recepteur recepteur, Vector3 oldScaling, Vector3 newScaling)
    {
        Commande commande = new ScaleTo(recepteur.getGameObject(), oldScaling, newScaling);
        commande.Do();
        commandeList.Push(commande);
        editManager.RefreshInfos();
    }
    public void SetThickness(Furniture_Recepteur recepteur, float oldThickness, float newThickness)
    {
        Commande commande = new SetThickness(recepteur.getFurniture(), oldThickness, newThickness);
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
        Commande commande = new AddFurniture(this, furnitureFolder, sensorsFolder, wallsFolder, newFurniture.getType(),newFurniture.getPosition(), newFurniture.getRotation(), newFurniture.getScale(), newFurniture.thickness);
        commande.Do();
        furnitureList.Add((commande as AddFurniture).getFurnitureRecepteur());
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
    public void RemoveAllFurniture()
    {
        int j = furnitureList.Count;

        for (int i=0; i<j;i++)
        {
            Destroy(furnitureList[i].getGameObject().gameObject);
        }

        furnitureList.Clear();
    }

    #endregion

    #region FurnitureList

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

    #endregion

    #endregion
}
