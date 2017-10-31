using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveExternalScene : MonoBehaviour {

    public string saveName;
    private List<Furniture_Recepteur> furnitureList = new List<Furniture_Recepteur>();
    private Stack<Commande> commandeList = new Stack<Commande>();

    public void Start()
    {
        // TestSave();   Produit l'erreur suivante : UNauthorizedAccessException - Acces to the path is denied
        Save();
    } 
    
    public void TestSave()
    {
        int nb = transform.childCount;

        for (int i = 0; i < nb; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            Furniture newFurniture = new global::Furniture(child.name, 1f, child); // Le nom de l'objet doit être le même que son prefab
            Furniture_Recepteur recepteur = new Furniture_Recepteur(child, newFurniture);
            furnitureList.Add(recepteur);
        }
        string[] stringTableau = new string[furnitureList.Count];

        for (int j=0;j < furnitureList.Count;j++)
        {
            stringTableau[j] = JsonUtility.ToJson(furnitureList[j].getFurniture());
        }

        System.IO.File.WriteAllLines(Application.dataPath + "/Extra/Saves", stringTableau);
    }  

    public void Save()
    {
        int nb = transform.childCount;

        for (int i = 0; i < nb; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            Furniture newFurniture = new global::Furniture(child.name, 1f, child); // Le nom de l'objet doit être le même que son prefab
            Furniture_Recepteur recepteur = new Furniture_Recepteur(child, newFurniture);
            furnitureList.Add(recepteur);
        }

        StreamWriter writer = new StreamWriter(saveName);

        foreach (Furniture_Recepteur currentFurniture in furnitureList)
        {
            writer.WriteLine(JsonUtility.ToJson(currentFurniture.getFurniture()));
        }

        writer.Close();
    }
}
