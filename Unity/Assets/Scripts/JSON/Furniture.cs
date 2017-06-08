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
    private Quaternion rotation;      // Il s'agit du nom du GameObject qui lui est rattaché, doit être modifié avec la fonction UpdateRotation()

    #endregion

    #region Constructeur

    //  Constructeur de la classe, il permet d'initialiser les valeurs propres au Furniture et celles du GameObject 

    public Furniture(string type, float width, float thickness, GameObject newObject) 
    {
        this.type = type;
        this.width = width;
        this.thickness = thickness;

        entityName = newObject.name;
        position = newObject.transform.position;
        rotation = newObject.transform.rotation;
    }

    #endregion

    #region Update Methodes

    public void UpdateName(string newName)
    {
        entityName = newName;
        return;
    }                                                    // Permet de modifier le nom visible dans l'inteface utilisateur de l'objet, devrait être appelé uniquement par la classe Furniture_Recepteur
    public void UpdatePosition(Vector3 newPosition)
    {
        position = newPosition;
        return;
    }                                           // Permet de modifier la position dans l'inteface de l'objet, devrait être appelé uniquement par la classe Furniture_Recepteur
    public void UpdateRotation(Quaternion newRotation)
    {
        rotation = newRotation;
        return;
    }                                        // Permet de modifier la rotation dans l'inteface de l'objet, devrait être appelé uniquement par la classe Furniture_Recepteur

    public void UpdateAll(string newName, Vector3 newPosition, Quaternion newRotation)
    {
        UpdateName(newName);
        UpdatePosition(newPosition);
        UpdateRotation(newRotation);
    }        // Permet de mettre à jour l'ensemble des paramètres modifiables de l'objet, devrait être appelé lorsque de l'initialisation du GameObject qui s'y rattache
    public void UpdateAll(GameObject a)
    {
        UpdateName(a.name);
        UpdatePosition(a.transform.position);
        UpdateRotation(a.transform.rotation);
    }                                                       // Permet de mettre à jour l'ensemble des paramètres modifiables de l'objet, cette fois en lui passant l'objet en question en parametre

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
    public string getName()
    {
        return entityName;
    }

    #endregion
}
