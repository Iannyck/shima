using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {

	[SerializeField]
	private float range = 1000f;

	private bool isEnable;

	private GameObject currentObj;

	public bool IsEnable {
		get {
			return this.isEnable;
		}
	}

	public void Enable(bool value){
		isEnable = value;
		if (currentObj != null) {
			ApplyShader(currentObj, "Diffuse");
		}
	}

	// Use this for initialization
	void Start () {
		isEnable = true;
		currentObj = null;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit = new RaycastHit ();
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, range)) {
			Debug.Log (hit.transform.name);
			GameObject obj = hit.transform.gameObject;
			if (currentObj == null || currentObj != obj) {
				ApplyShader (obj, "Outlined/Silhouetted Diffuse");
				if(currentObj != null)
					ApplyShader (currentObj, "Diffuse");
				currentObj = obj;
			}
		}
		else if(currentObj != null) {
			ApplyShader (currentObj, "Diffuse");
			currentObj = null;
		}
//		if (Input.GetButtonDown ("Fire1")) {
//			RaycastHit hit = new RaycastHit ();
//			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//			if (Physics.Raycast (ray, out hit, range)) {
//				Debug.Log (hit.transform.name);
//				GameObject obj = hit.transform.gameObject;
//				ApplyShader(obj, "Outlined/Silhouetted Diffuse");
////				if (hit.transform.tag == "Actionable") {
////				}
//			}
//		}
	}

	private void ApplyShader(GameObject obj, string shader) {
		if (obj.GetComponent<Renderer> () != null) {
			obj.GetComponent<Renderer> ().material.shader = Shader.Find (shader);
		} else {
			Renderer[] render = obj.GetComponentsInChildren<Renderer> ();
			for(int i =0; i<render.Length; i++) {
				render[i].material.shader = Shader.Find (shader);
			}
		}
	}

}
