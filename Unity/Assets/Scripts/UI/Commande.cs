using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commande {

    public string nomCommande;
    public Furniture_Recepteur furniture;

    public Commande(string nom, Furniture_Recepteur newFurniture)
    {
        nomCommande = nom;
        furniture = newFurniture;
    }
      


}
