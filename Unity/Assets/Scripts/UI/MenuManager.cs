using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	public GameObject menuPanel;

	public void Menu() {
		if (menuPanel != null)
			menuPanel.SetActive (!menuPanel.activeInHierarchy);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
