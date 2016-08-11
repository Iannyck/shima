using UnityEngine;
using System.Collections;

public class ElectricityLogger : MonoBehaviour {

	public int window = 30;
	public float frequency = 1;

	private float currentTime = 0;
	private readonly int deltaX = 10;

	private Phase phase1;
	private Phase phase2;
	private Phase phase3;

	private ArrayList phases1;
	private ArrayList phases2;
	private ArrayList phases3;

	private bool showElectricityData;
	private bool showElectronicCharts;

	void OnGUI() {
		if(showElectricityData)
			ShowElectronicData(2, 400);
		if (showElectronicCharts) {
			ShowElectronicChart (700, 32, 300, 200, phases1, "Phase 1");
			ShowElectronicChart (700, 264, 300, 200, phases2, "Phase 2");
			ShowElectronicChart (700, 496, 300, 200, phases3, "Phase 3");
		}
	}

	// Use this for initialization
	void Start () {
		phases1 = new ArrayList ();
		phases2 = new ArrayList ();
		phases3 = new ArrayList ();
		phase1 = new Phase (0, 0);
		phase2 = new Phase (0, 0);
		phase3 = new Phase (0, 0);

		showElectricityData = false;
		showElectronicCharts = false;
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if(currentTime >= frequency) {
			RefreshFlowchart (phases1);
			RefreshFlowchart (phases2);
			RefreshFlowchart (phases3);
			currentTime = 0;
		}
	}

	private void RefreshFlowchart(ArrayList phases) {
		if (phases != null) {
			if (phases.Count > 0)
				AddToQueue (phases, ((Phase)phases [phases.Count - 1]).Clone ());
			else
				AddToQueue (phases, new Phase (0, 0));
		}
	}

	public void PhasesStates(Phase phase1, Phase phase2, Phase phase3) {
		AddToQueue (phases1, phase1);
		AddToQueue (phases2, phase2);
		AddToQueue (phases3, phase3);
		this.phase1 = phase1;
		this.phase2 = phase2;
		this.phase3 = phase3;
	}
		
	public bool ShowElectricityData {
		get {
			return this.showElectricityData;
		}
		set {
			showElectricityData = value;
		}
	}

	public bool ShowElectronicCharts {
		get {
			return this.showElectronicCharts;
		}
		set {
			showElectronicCharts = value;
		}
	}

	private void ShowElectronicChart(int x, int y, int width, int height, ArrayList phases, string label) {
		GUI.Box(new Rect(x, y, width, height) , label);
		int currentX = x + 5;
		int startY = y + height - 5;
		Vector2 point1 = new Vector2 (currentX, startY);
		Vector2 point2 = new Vector2 (currentX, y + 25);
		Drawing.DrawLine(point1, point2, Color.white, 3);
		point1 = new Vector2 (currentX, startY);
		point2 = new Vector2 (currentX + width - 25, startY);
		Drawing.DrawLine(point1, point2, Color.white, 3);
		currentX = currentX + 1;
		Vector2 pointReactive1;
		Vector2 pointReactive2;
		if (phases.Count > 2) {
			for(int index = 1; index < phases.Count; index++) {
				/// Draws Active power dots
				point1 = new Vector2 (currentX, startY - ((Phase)phases[index-1]).Active_power / 10);
				point2 = new Vector2 (currentX + deltaX, startY - ((Phase)phases[index]).Active_power / 10);
				Drawing.DrawLine(point1, point2, Color.red, 2);
				/// Draws Reactive power dots
				pointReactive1 = new Vector2 (currentX, startY - ((Phase)phases[index-1]).Reactive_power / 10);
				pointReactive2 = new Vector2 (currentX + deltaX, startY - ((Phase)phases[index]).Reactive_power / 10);
				Drawing.DrawLine(pointReactive1, pointReactive2, Color.green, 2);
				currentX = currentX + deltaX ;
			}
		}
	}

	private void ShowElectronicData(int x, int y) {
		GUI.Box(new Rect(x,y,250,105) , "Electricity");
		GUI.Label(new Rect(x + 5,y + 20,240,150) , "Phase 1: [act: "+phase1.Active_power + " - react: "+phase1.Reactive_power+" ]");
		GUI.Label(new Rect(x + 5,y + 40,240,150) , "Phase 2: [act: "+phase2.Active_power + " - react: "+phase2.Reactive_power+" ]");
		GUI.Label(new Rect(x + 5,y + 60,240,150) , "Phase 3: [act: "+phase3.Active_power + " - react: "+phase3.Reactive_power+" ]");
	}

	private void AddToQueue(ArrayList phases, Phase phase) {
		if (phases != null) {
			if (phases.Count >= window) {
				phases.RemoveAt (0);
			}
			phases.Add (phase.Clone ());
		}
	}

	class Drawing {
		public static Texture2D lineTex;

		public static void DrawLine(Rect rect) { DrawLine(rect, GUI.contentColor, 1.0f); }
		public static void DrawLine(Rect rect, Color color) { DrawLine(rect, color, 1.0f); }
		public static void DrawLine(Rect rect, float width) { DrawLine(rect, GUI.contentColor, width); }
		public static void DrawLine(Rect rect, Color color, float width) { DrawLine(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height), color, width); }
		public static void DrawLine(Vector2 pointA, Vector2 pointB) { DrawLine(pointA, pointB, GUI.contentColor, 1.0f); }
		public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color) { DrawLine(pointA, pointB, color, 1.0f); }
		public static void DrawLine(Vector2 pointA, Vector2 pointB, float width) { DrawLine(pointA, pointB, GUI.contentColor, width); }
		public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
		{
			// Save the current GUI matrix, since we're going to make changes to it.
			Matrix4x4 matrix = GUI.matrix;

			// Generate a single pixel texture if it doesn't exist
			if (!lineTex) { lineTex = new Texture2D(1, 1); }

			// Store current GUI color, so we can switch it back later,
			// and set the GUI color to the color parameter
			Color savedColor = GUI.color;
			GUI.color = color;

			// Determine the angle of the line.
			float angle = Vector3.Angle(pointB - pointA, Vector2.right);

			// Vector3.Angle always returns a positive number.
			// If pointB is above pointA, then angle needs to be negative.
			if (pointA.y > pointB.y) { angle = -angle; }

			// Use ScaleAroundPivot to adjust the size of the line.
			// We could do this when we draw the texture, but by scaling it here we can use
			//  non-integer values for the width and length (such as sub 1 pixel widths).
			// Note that the pivot point is at +.5 from pointA.y, this is so that the width of the line
			//  is centered on the origin at pointA.
			GUIUtility.ScaleAroundPivot(new Vector2((pointB - pointA).magnitude, width), new Vector2(pointA.x, pointA.y + 0.5f));

			// Set the rotation for the line.
			//  The angle was calculated with pointA as the origin.
			GUIUtility.RotateAroundPivot(angle, pointA);

			// Finally, draw the actual line.
			// We're really only drawing a 1x1 texture from pointA.
			// The matrix operations done with ScaleAroundPivot and RotateAroundPivot will make this
			//  render with the proper width, length, and angle.
			GUI.DrawTexture(new Rect(pointA.x, pointA.y, 1, 1), lineTex);

			// We're done.  Restore the GUI matrix and GUI color to whatever they were before.
			GUI.matrix = matrix;
			GUI.color = savedColor;
		}
	}
		
}
