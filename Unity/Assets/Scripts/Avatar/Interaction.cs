using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

	[SerializeField]
	private float range = 1000f;

	private bool isEnable;

	private GameObject currentObj;

	private GameObject itemPicked;
	private Vector3 screenPoint;
	private Vector3 offset;

	public bool IsEnable {
		get {
			return this.isEnable;
		}
	}

	public void Enable (bool value)
	{
		isEnable = value;
		if (currentObj != null) {
			ApplyShader (currentObj, "Diffuse");
		}
	}

	// Use this for initialization
	void Start ()
	{
		itemPicked = null;
		isEnable = true;
		currentObj = null;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update ()
	{
		RaycastHit hit = new RaycastHit ();
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, range)) {
			GameObject obj = hit.transform.gameObject;
			if ((obj.gameObject.layer == LayerMask.NameToLayer ("UI")) && (currentObj != obj)) {
				ApplyShader (obj, "Outlined/Silhouetted Diffuse");
				if (currentObj != null)
					ApplyShader (currentObj, "Diffuse");
				currentObj = obj;
			}
		} else if (currentObj != null) {
			ApplyShader (currentObj, "Diffuse");
			currentObj = null;
		}
		if (currentObj != null) {
			// FIRE1 = Pick up/Release
			if (Input.GetButtonDown ("Fire1")) {
				if(itemPicked != null){
//					itemPicked.transform.SetParent(null);
					itemPicked = null;
				}
				else if (currentObj.tag == "PickUp") {
					itemPicked = currentObj;
					screenPoint = Camera.main.WorldToScreenPoint (itemPicked.transform.position);
					offset = itemPicked.transform.position - Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
//					itemPicked.transform.SetParent(transform.parent);
				}
			}
			// FIRE2 = Activate/Desactivate
			if (Input.GetButtonDown ("Fire2")) {
				Activable activable = currentObj.GetComponent<Activable>();
				if (activable != null) {
					activable.Activate ();
				}
			}
			//FIRE3 = Rotate
			if (Input.GetButtonDown ("Fire3")) {

			}
		}
		if (itemPicked != null)
			MoveItem ();
	}

	private void ApplyShader (GameObject obj, string shader)
	{
		if (obj.GetComponent<Renderer> () != null) {
			obj.GetComponent<Renderer> ().material.shader = Shader.Find (shader);
		} else {
			Renderer[] render = obj.GetComponentsInChildren<Renderer> ();
			for (int i = 0; i < render.Length; i++) {
				render [i].material.shader = Shader.Find (shader);
			}
		}
	}

	private void MoveItem(){
		Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

		Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
		itemPicked.transform.position = curPosition;
	}

}
