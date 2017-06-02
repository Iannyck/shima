using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditManager : MonoBehaviour {

	private GameObject selected;
	private bool mouseState = false;
	private Vector3 screenSpace;
	private Vector3 offset;

	public InputField objectNameInputField;
	public InputField angleNameInputField;
	public InputField lenghtNameInputField;
	public InputField widthNameInputField;
	public InputField thicknessNameInputField;

	public GameObject editPanel;
    public Furniture_Recepteur recepteur;

    private GameObject GetClickedGameObject() {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity))
			return hit.transform.gameObject;
		return null;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			
			GameObject target = GetClickedGameObject ();
			if (target != null) {
				selected = target;
                recepteur = this.GetComponentInParent<BBuildManager>().FindInFurnitureList(selected);   // Permet de trouver la structure de données contenant l'entité ciblée
                mouseState = true;
				screenSpace = Camera.main.WorldToScreenPoint (selected.transform.position);
				offset = selected.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
				objectNameInputField.text = selected.name;
				editPanel.SetActive (true);
			} else {
				mouseState = false;
				selected = null;
                recepteur = null; // Renitialise le recepteur a null
				objectNameInputField.text = "Name";
				angleNameInputField.text = "0";
				editPanel.SetActive (false);
			}
		}
		if (Input.GetMouseButtonUp (1))
			mouseState = false;
		if (mouseState) {
			Vector3 curScreenSpace = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

			Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenSpace) + offset;

			selected.transform.position = curPosition;
            
            if (recepteur != null && selected != null)     
                recepteur.UpdateInfos(selected);
            
        }
//		if(Input.GetMouseButtonDown (0)) {
//			mouseState = false;
//			selected = null;
//			objectNameInputField.text = "Name";
//			angleNameInputField.text = "0";
//			editPanel.SetActive (false);
//		}
	}

	public void RotateWall() {
		Debug.Log (angleNameInputField.text);
		if (selected != null) {
            if (angleNameInputField.text != null && angleNameInputField.text != "")
            {
                selected.transform.Rotate(new Vector3(0, float.Parse(angleNameInputField.text), 0));
                recepteur.UpdateInfos(selected);
            }
		}
	}

	public void SetLenght() {
		Debug.Log (angleNameInputField.text);
		if (selected != null) {
            if (lenghtNameInputField.text != null && lenghtNameInputField.text != "")
            {
                selected.transform.localScale = new Vector3(float.Parse(lenghtNameInputField.text) * selected.transform.localScale.x, selected.transform.localScale.y, selected.transform.localScale.z);
                recepteur.UpdateInfos(selected);
            }
                
		}
	}

	public void SetWidth() {
		Debug.Log (angleNameInputField.text);
		if (selected != null){
            if (widthNameInputField.text != null && widthNameInputField.text != "")
            {
                selected.transform.localScale = new Vector3(selected.transform.localScale.x, selected.transform.localScale.y, float.Parse(widthNameInputField.text) * selected.transform.localScale.z);
                recepteur.UpdateInfos(selected);
            }
		}
	}

	public void SetThickness() {
		Debug.Log (thicknessNameInputField.text);
        if (selected != null)
        {
            if (thicknessNameInputField != null && thicknessNameInputField.text != "")
            {
                // Fonction sur Thickness
                recepteur.UpdateInfos(selected);
            }
        }
    }

//	public void RotateWall(Slider angleSlider) {
//		Debug.Log (angleSlider.value);
//		selected.transform.Rotate (new Vector3(0, angleSlider.value, 0));
//	}
}
