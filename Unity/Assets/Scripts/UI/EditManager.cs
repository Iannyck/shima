using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditManager : MonoBehaviour
{

    private BBuildManager buildManager;
    public GameObject editPanel;

    private GameObject selected;
    private bool mouseState = false;
    private Vector3 screenSpace;
    private Vector3 offset;

    private bool updateInfo = false;
    private Vector3 oldPosition;
    private string oldName;
    private float oldThickness;
    private Vector3 oldRotation;
    private Vector3 oldScaling;

    private Vector3 newPosition;
    public InputField objectNameInputField;
    public InputField angleNameInputField_X;
    public InputField angleNameInputField_Y;
    public InputField angleNameInputField_Z;
    public InputField positionNameInputField_X;
    public InputField positionNameInputField_Y;
    public InputField positionNameInputField_Z;
    public InputField scaleNameInputField_X;
    public InputField scaleNameInputField_Y;
    public InputField scaleNameInputField_Z;

    public InputField thicknessNameInputField;

    [HideInInspector]
    public Furniture_Recepteur recepteur;

    private GameObject GetClickedGameObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            return hit.transform.gameObject;
        return null;
    }

    void Start()
    {
        buildManager = this.GetComponentInParent<BBuildManager>();

        objectNameInputField.onEndEdit.AddListener(delegate { if (recepteur != null) { recepteur.UpdateInfos(selected); buildManager.SetName(recepteur, oldName, objectNameInputField.text); RefreshInfos(); } });

        angleNameInputField_X.onEndEdit.AddListener(delegate { if (recepteur != null) { recepteur.UpdateInfos(selected); buildManager.RotateFurniture(recepteur, oldRotation, new Vector3(float.Parse(angleNameInputField_X.text), float.Parse(angleNameInputField_Y.text), float.Parse(angleNameInputField_Z.text)));}});
        angleNameInputField_Y.onEndEdit.AddListener(delegate { if (recepteur != null) { recepteur.UpdateInfos(selected); buildManager.RotateFurniture(recepteur, oldRotation, new Vector3(float.Parse(angleNameInputField_X.text), float.Parse(angleNameInputField_Y.text), float.Parse(angleNameInputField_Z.text)));}});
        angleNameInputField_Z.onEndEdit.AddListener(delegate { if (recepteur != null) { recepteur.UpdateInfos(selected); buildManager.RotateFurniture(recepteur, oldRotation, new Vector3(float.Parse(angleNameInputField_X.text), float.Parse(angleNameInputField_Y.text), float.Parse(angleNameInputField_Z.text)));}});

        scaleNameInputField_X.onEndEdit.AddListener(delegate { if (recepteur != null) { recepteur.UpdateInfos(selected); buildManager.ScaleFurniture(recepteur, oldScaling, new Vector3(float.Parse(scaleNameInputField_X.text), float.Parse(scaleNameInputField_Y.text), float.Parse(scaleNameInputField_Z.text)));}});
        scaleNameInputField_Y.onEndEdit.AddListener(delegate { if (recepteur != null) { recepteur.UpdateInfos(selected); buildManager.ScaleFurniture(recepteur, oldScaling, new Vector3(float.Parse(scaleNameInputField_X.text), float.Parse(scaleNameInputField_Y.text), float.Parse(scaleNameInputField_Z.text)));}});
        scaleNameInputField_Z.onEndEdit.AddListener(delegate { if (recepteur != null) { recepteur.UpdateInfos(selected); buildManager.ScaleFurniture(recepteur, oldScaling, new Vector3(float.Parse(scaleNameInputField_X.text), float.Parse(scaleNameInputField_Y.text), float.Parse(scaleNameInputField_Z.text)));}});

        positionNameInputField_X.onEndEdit.AddListener(delegate { if (recepteur != null) { recepteur.UpdateInfos(selected); buildManager.MoveFurniture(recepteur, oldPosition, new Vector3(float.Parse(positionNameInputField_X.text), float.Parse(positionNameInputField_Y.text), float.Parse(positionNameInputField_Z.text)));}});
        positionNameInputField_Y.onEndEdit.AddListener(delegate { if (recepteur != null) { recepteur.UpdateInfos(selected); buildManager.MoveFurniture(recepteur, oldPosition, new Vector3(float.Parse(positionNameInputField_X.text), float.Parse(positionNameInputField_Y.text), float.Parse(positionNameInputField_Z.text)));}});
        positionNameInputField_Z.onEndEdit.AddListener(delegate { if (recepteur != null) { recepteur.UpdateInfos(selected); buildManager.MoveFurniture(recepteur, oldPosition, new Vector3(float.Parse(positionNameInputField_X.text), float.Parse(positionNameInputField_Y.text), float.Parse(positionNameInputField_Z.text)));}});

        thicknessNameInputField.onEndEdit.AddListener(delegate { if (recepteur != null) { recepteur.UpdateInfos(selected); buildManager.SetThickness(recepteur, oldThickness, float.Parse(thicknessNameInputField.text));}});
    }

    public void RefreshInfos()
    {
        if (selected != null)
        {
            oldName = selected.name;
            objectNameInputField.text = selected.name;
            oldThickness = buildManager.FindInFurnitureList(selected).getFurniture().thickness;
            oldRotation = selected.transform.eulerAngles;
            oldScaling = new Vector3(selected.transform.localScale.x, selected.transform.localScale.y, selected.transform.localScale.z);

            angleNameInputField_X.text = (selected.transform.localRotation.eulerAngles.x.ToString());
            angleNameInputField_Y.text = (selected.transform.localRotation.eulerAngles.y.ToString());
            angleNameInputField_Z.text = (selected.transform.localRotation.eulerAngles.z.ToString());

            scaleNameInputField_X.text = (selected.transform.localScale.x.ToString());
            scaleNameInputField_Y.text = (selected.transform.localScale.y.ToString());
            scaleNameInputField_Z.text = (selected.transform.localScale.z.ToString());

            positionNameInputField_X.text = (selected.transform.position.x.ToString());
            positionNameInputField_Y.text = (selected.transform.position.y.ToString());
            positionNameInputField_Z.text = (selected.transform.position.z.ToString());

            thicknessNameInputField.text = buildManager.FindInFurnitureList(selected).getFurniture().thickness.ToString();
        }

    }
    public void CloseEdit()
    {
        editPanel.SetActive(false);
    }
    public GameObject getSelected()
    {
        return selected;
    }

    void Update()
    {
        // Rénitialise les paramètres si l'utilisateur n'est pas en train de sélectionner un objet
        #region GetMouseButtonDown(1)

        if (Input.GetMouseButtonDown(1))
        {

            GameObject target = GetClickedGameObject();
            if (target != null)
            {
                selected = target;
                recepteur = buildManager.FindInFurnitureList(selected);   // Permet de trouver la structure de données contenant l'entité ciblée
                mouseState = true;
                screenSpace = Camera.main.WorldToScreenPoint(selected.transform.position);
                offset = selected.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
                editPanel.SetActive(true);

                // Initialise les parametres de la fenetre
                RefreshInfos();
            }
            else
            {
                // Rénitialise les parametres
                mouseState = false;
                selected = null;
                recepteur = null;
                objectNameInputField.text = "Name";
                angleNameInputField_X.text = "0";
                angleNameInputField_Y.text = "0";
                angleNameInputField_Z.text = "0";

                updateInfo = false;
                oldPosition = new Vector3(0, 0, 0);
                oldName = null;
                oldRotation = new Vector3(0, 0, 0);
                oldScaling = new Vector3(0, 0, 0);
                oldThickness = 0;

                editPanel.SetActive(false);
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
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
            newPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            selected.transform.position = newPosition;

            // Met à jour le Furniture_Recepteur
            if (recepteur != null && selected != null)
                recepteur.UpdateInfos(selected);
        }

        #endregion

        // Commande au clavier pour annuler la dernière action
        #region CTRL

        if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z))
            buildManager.Cancel();

        #endregion
    }
}