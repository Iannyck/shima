using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SensorsGUI : MonoBehaviour {

	private bool showHelp;
	private bool showElectricityData;
	private bool showElectricityChartPhase1;
	private bool showElectricityChartPhase2;
	private bool showElectricityChartPhase3;
	private bool showDebugText;
	private bool showBehaviourTree;

	private AbstractBehaviour behaviourTreeToShow;

	private string[] debugText;

	private float deltaTime = 0.0f;

	private bool showFPS;

	public AbstractBehaviour BehaviourTreeToShow {
		get {
			return this.behaviourTreeToShow;
		}
		set {
			behaviourTreeToShow = value;
		}
	}

	private ElectricityLogger electricityLogger;

	// Use this for initialization
	void Start () {
		showHelp = false;
		showElectricityData = false;
		showElectricityChartPhase1 = false;
		showElectricityChartPhase2 = false;
		showElectricityChartPhase3 = false;
		showDebugText = false;
		showBehaviourTree = true;
		debugText = new string[10];
		showFPS = false;
		deltaTime = 0.0f;

		behaviourTreeToShow = null;
		electricityLogger = GetComponent<ElectricityLogger> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftControl) || Input.GetKeyDown (KeyCode.RightControl)) {
			showHelp = !showHelp;
		}
		if (showHelp) {
			if (Input.GetKeyDown (KeyCode.B)) {
				showDebugText = !showDebugText;
			}
			if (Input.GetKeyDown (KeyCode.F)) {
				showFPS = !showFPS;
			}
			if (Input.GetKeyDown (KeyCode.R)) {
				electricityLogger.ShowElectricityData = !electricityLogger.ShowElectricityData;
			}
			if (Input.GetKeyDown (KeyCode.C)) {
				electricityLogger.ShowElectronicCharts = !electricityLogger.ShowElectronicCharts;
			}
			if (Input.GetKeyDown (KeyCode.A)) {
				showBehaviourTree = !showBehaviourTree;
			}
		}
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}

	void OnGUI() {
		if (IsAllFalse ())
			ShowSimpleGUI (2, 2, 80, 20);
		if (showHelp)
			ShowHelp (2, 24, 200, 200);
		if(showDebugText)
			ShowDebug (300, 30, 300, 300);
		if(showBehaviourTree)
			ShowBehaviourTree (2, 500, 300, 128);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		if (showFPS) {
			string text = string.Format ("{0:0.0} ms ({1:0.} fps)", msec, fps);
			GUI.Label (new Rect (400, 16, 128, 24), text);
		}
	}

	private bool IsAllFalse() {
		if (showElectricityData || showElectricityChartPhase1
			|| showElectricityChartPhase2 || showElectricityChartPhase3
			|| showDebugText || showHelp)
			return false;
		return true;
	}

	public void ShowSimpleGUI(int x, int y, int witdh, int heigth){
		GUI.Box (new Rect (x, y, witdh, heigth), "Menu 'Ctrl'");
	}

	public void ShowHelp(int x, int y, int witdh, int heigth) {
		GUI.Box (new Rect (x, y, witdh, heigth), "Help Box");
		GUI.Label (new Rect (x + 8, y + 16, witdh - 32, 24), "Press 'c' to show charts");
		GUI.Label (new Rect (x + 8, y + 32, witdh - 32, 24), "Press 'b' to show debug");
		GUI.Label (new Rect (x + 8, y + 48, witdh - 32, 24), "Press 'f' to show FPS");
		GUI.Label (new Rect (x + 8, y + 64, witdh - 32, 24), "Press 'r' to show data");
		GUI.Label (new Rect (x + 8, y + 80, witdh - 32, 24), "Press 'a' to show activity");
	}

	public void ShowDebug(int x, int y, int witdh, int heigth) {
		GUI.Box (new Rect (x, y, witdh, heigth), "Debug Box");
		int deltaY = 16;
		for (int i = debugText.Length-1; i > 0; i--) {
			GUI.Label (new Rect (x + 8, y + deltaY, witdh - 32, 24), debugText [i]);
			deltaY += 24;
		}
	}

	public void SetDebugText(int number, string text) {
		debugText [number] = text;
	}

	private void ShowBehaviourTree(int x, int y, int witdh, int heigth) {
		if(behaviourTreeToShow != null) {
			GUI.Box (new Rect (x, y, witdh, heigth), "Activity Tree");
			AbstractBehaviour current = behaviourTreeToShow;
			int deltaX = 8;
			int deltaY = 16;
			string indent = "";
			while(current != null) {
				GUI.Label (new Rect (x + deltaX, y + deltaY, witdh - 32, 24), indent + current.BName);
				if (current is Sequence) {
					List<AbstractBehaviour> sub = ((Sequence)current).GetActiveBehaviours ();
					if (sub.Count == 1) {
						current = sub [0];
						indent += "-";
						deltaY += 24;
					} else
						current = null;
				} else if (current is AbstractScript) {
					current = ((AbstractScript)current).Behaviour;
					indent += "-";
					deltaY += 24;
				} else
					current = null;
			}
		}
	}


		
}
