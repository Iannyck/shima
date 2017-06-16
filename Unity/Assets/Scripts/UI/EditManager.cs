using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditManager : MonoBehaviour {

    private BBuildManager buildManager;
    public GameObject editPanel;

	private GameObject selected;
	private bool mouseState = false;
	private Vector3 screenSpace;
	private Vector3 offset;

    private bool updateInfo = false;
    private Vector3 oldPosition;
    private string oldName;
    private Quaternion oldRotation;

    private Vector3 newPosition;
	public InputField objectNameInputField;
    public InputField angleNameInputField_X;
    public InputField angleNameInputField_Y;
    public InputField angleNameInputField_Z;
    public InputField lenghtNameInputField;
	public InputField widthNameInputField;
	public InputField thicknessNameInputField;

    public Furniture_Recepteur recepteur;

    private GameObject GetClickedGameObject() {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity))
			return hit.transform.gameObject;
		return null;
	}

	void Start ()
    {
        buildManager = this.GetComponentInParent<BBuildManager>();
            
        objectNameInputField.onEndEdit.AddListener(delegate { if (recepteur != null) { recepteur.UpdateInfos(selected); buildManager.SetName(recepteur, oldName, objectNameInputField.text); RefreshInfos();}});
        angleNameInputField_X.onEndEdit.AddListener(delegate { });
        angleNameInputField_Y.onEndEdit.AddListener(delegate { });
        angleNameInputField_Z.onEndEdit.AddListener(delegate { });
        lenghtNameInputField.onEndEdit.AddListener(delegate { });
        widthNameInputField.onEndEdit.AddListener(delegate { });
        thicknessNameInputField.onEndEdit.AddListener(delegate { });
    }

    public void RefreshInfos()
    {
        oldName = selected.name;
        objectNameInputField.text = selected.name;

        oldRotation = selected.transform.localRotation;
        angleNameInputField_X.text = (selected.transform.localRotation.x.ToString());
        angleNameInputField_Y.text = (selected.transform.localRotation.y.ToString());
        angleNameInputField_Z.text = (selected.transform.localRotation.z.ToString());
    }
    public void CloseEdit()
    {
        editPanel.SetActive(false);
    }
    public GameObject getSelected()
    {
        return selected;
    }
	
	void Update ()
    {
        // Rénitialise les paramètres si l'utilisateur n'est pas en train de sélectionner un objet
        #region GetMouseButtonDown(1)

        if (Input.GetMouseButtonDown (1)) {
			
			GameObject target = GetClickedGameObject ();
			if (target != null) {
				selected = target;
                recepteur = buildManager.FindInFurnitureList(selected);   // Permet de trouver la structure de données contenant l'entité ciblée
                mouseState = true;
				screenSpace = Camera.main.WorldToScreenPoint (selected.transform.position);
				offset = selected.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
				editPanel.SetActive (true);

                // Initialise les parametres de la fenetre
                oldName = selected.name;
                objectNameInputField.text = selected.name;

                oldRotation = selected.transform.localRotation;
                angleNameInputField_X.text = (selected.transform.localRotation.x.ToString());
                angleNameInputField_Y.text = (selected.transform.localRotation.y.ToString());
                angleNameInputField_Z.text = (selected.transform.localRotation.z.ToString());



            } else {

                // Rénitialise les parametres
                mouseState = false;
				selected = null;
                recepteur = null;
				objectNameInputField.text = "Name";
                angleNameInputField_X.text = "0";
                angleNameInputField_Y.text = "0";
                angleNameInputField_Z.text = "0";

                updateInfo = false;
                oldPosition = new Vector3(0,0,0);
                oldName = null;
                oldRotation = new Quaternion(0, 0, 0, 0);


                editPanel.SetActive (false);
			}
		}
        #endregion

        // Créé une commande de déplacement si l'utilisateur relâche le bouton
        #region GetButtonUp(1)
        if (Input.GetMouseButtonUp(1))
        {
            mouseState = false;
            updateInfo = false;

            if (recepteur != null && selected != null)
            {
                this.GetComponentInParent<BBuildManager>().MoveFurniture(recepteur, oldPosition, newPosition);

            }
                
        }

        #endregion

        // L'utilisateur a sélectionné un objet
        #region MouseState
        if (mouseState)
        {
            // Conserve la position initiale de l'objet sélectionné, s'effectue qu'une seule fois
            if (updateInfo == false)
            {
                oldPosition = selected.transform.position;
                updateInfo = true;
            }

            // Déplace l'objet sélectionné selon le curseur de la souris
			Vector3 curScreenSpace = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
			newPosition = Camera.main.ScreenToWorldPoint (curScreenSpace) + offset;
            selected.transform.position = newPosition;

            // Met à jour le Furniture_Recepteur
            if (recepteur != null && selected != null)
                recepteur.UpdateInfos(selected);   
        }

        #endregion
    }

    public void SetName()
    {
        Debug.Log(objectNameInputField.text);
        if (selected != null)
        {
            if (objectNameInputField.text != null && objectNameInputField.text != "")
            {
                selected.name = objectNameInputField.text;
                recepteur.UpdateInfos(selected);
            }
        }

    }

	//public void RotateWall() {
	//	Debug.Log (angleNameInputField.text);
	//	if (selected != null) {
 //           if (angleNameInputField.text != null && angleNameInputField.text != "")
 //           {
 //               selected.transform.Rotate(new Vector3(0, float.Parse(angleNameInputField.text), 0));
 //               recepteur.UpdateInfos(selected);
 //           }
	//	}
	//}

	//public void SetLenght() {
	//	Debug.Log (angleNameInputField.text);
	//	if (selected != null) {
 //           if (lenghtNameInputField.text != null && lenghtNameInputField.text != "")
 //           {
 //               selected.transform.localScale = new Vector3(float.Parse(lenghtNameInputField.text) * selected.transform.localScale.x, selected.transform.localScale.y, selected.transform.localScale.z);
 //               recepteur.UpdateInfos(selected);
 //           }
                
	//	}
	//}

	//public void SetWidth() {
	//	Debug.Log (angleNameInputField.text);
	//	if (selected != null){
 //           if (widthNameInputField.text != null && widthNameInputField.text != "")
 //           {
 //               selected.transform.localScale = new Vector3(selected.transform.localScale.x, selected.transform.localScale.y, float.Parse(widthNameInputField.text) * selected.transform.localScale.z);
 //               recepteur.UpdateInfos(selected);
 //           }
	//	}


	//public void SetThickness() {
	//	Debug.Log (thicknessNameInputField.text);
 //       if (selected != null)
 //       {
 //           if (thicknessNameInputField != null && thicknessNameInputField.text != "")
 //           {
 //               // Fonction sur Thickness
 //               recepteur.UpdateInfos(selected);
 //           }
 //       }
 //   }

	}