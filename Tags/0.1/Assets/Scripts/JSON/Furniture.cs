using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Furniture
{
    #region Parametres

    [SerializeField]
    private string type;        // Correspond au prefab, ne doit, en aucun cas, être modifié

    public float width;         // Correspond a la largeur de l'objet, il s'agit d'une valeur modifiable par l'utilisateur dans l'interface
    public float thickness;     // Correspond a l'épaisseur de l'objet, il s'agit d'une valeur modifiable par l'utilisateur dans l'interface

    [SerializeField]
    private string entityName;        // Il s'agit du nom du GameObject qui lui est rattaché, doit être modifié avec la fonction UpdateName()

    [SerializeField]
    private Vector3 position;         // Il s'agit de la position du GameObject qui lui est rattaché, doit être modifié avec la fonction UpdatePosition()

    [SerializeField]
    private Quaternion rotation;      // Il s'agit de la rotation du GameObject qui lui est rattaché, doit être modifié avec la fonction UpdateRotation()

    [SerializeField]
    private Vector3 scale;           // Il s'agit du scale du GameObject qui lui est rattaché, doit être modifié avec la fonction UpdateScale()

    #endregion

    #region Constructeur

    //  Constructeur de la classe, il permet d'initialiser les valeurs propres au Furniture et celles du GameObject 

    public Furniture(string type, float thickness, GameObject newObject) 
    {
        this.type = type;
        this.thickness = thickness;

        entityName = newObject.name;
        position = newObject.transform.position;
        rotation = newObject.transform.rotation;
        scale = newObject.transform.localScale;
    }

    #endregion

    #region Update Methodes

    public void UpdateName(string newName)  // Permet de modifier le nom visible dans l'inteface utilisateur de l'objet, devrait être appelé uniquement par la classe Furniture_Recepteur
    {
        entityName = newName;
        return;
    }                                                    
    public void UpdatePosition(Vector3 newPosition) // Permet de modifier la position dans l'inteface de l'objet, devrait être appelé uniquement par la classe Furniture_Recepteur
    {
        position = newPosition;
        return;
    }                                          
    public void UpdateRotation(Quaternion newRotation) // Permet de modifier la rotation dans l'inteface de l'objet, devrait être appelé uniquement par la classe Furniture_Recepteur
    {
        rotation = newRotation;
        return;
    }                                       
    public void UpdateScale(Vector3 newScale) // Permet de modifier le scale dans l'inteface de l'objet, devrait être appelé uniquement par la classe Furniture_Recepteur
    {
        scale = newScale;
        return;
    }

    public void UpdateAll(string newName, Vector3 newPosition, Quaternion newRotation, Vector3 newScale)// Permet de mettre à jour l'ensemble des paramètres modifiables de l'objet, devrait être appelé lorsque de l'initialisation du GameObject qui s'y rattache
    {
        UpdateName(newName);
        UpdatePosition(newPosition);
        UpdateRotation(newRotation);
        UpdateScale(newScale);
    }        
    public void UpdateAll(GameObject a) // Permet de mettre à jour l'ensemble des paramètres modifiables de l'objet, cette fois en lui passant l'objet en question en parametre
    {
        UpdateName(a.name);
        UpdatePosition(a.transform.position);
        UpdateRotation(a.transform.rotation);
        UpdateScale(a.transform.localScale);
    }                                                       

    #endregion

    #region Access Method

    public string getType()
    {
        return type;
    }     // Permet d'accéder au type, soit au prefab de l'objet. Devrait être utilisé par le processus de chargement JSON afin de savoir quel prefab instancié
    public Vector3 getPosition()
    {
        return position;
    }
    public Quaternion getRotation()
    {
        return rotation;
    }
    public Vector3 getScale()
    {
        return scale;
    }
    public string getName()
    {
        return entityName;
    }

    #endregion
}
