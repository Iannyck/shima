using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Grid.
/// </summary>
public class Grid : MonoBehaviour {

	public Vector2 gridWorldSize;
	public Transform player;
	public LayerMask unwalkableMask;
	public float nodeRadius;
	Node[,] grid;
	float nodeDiameter;
	int gridSizeX, gridSizeY;

	/// <summary>
	/// The path to show on the grid.
	/// </summary>
	public List<Node> path;

	/// <summary>
	/// Raises the draw gizmos event.
	/// </summary>
	void OnDrawGizmos() {
		Gizmos.DrawWireCube (transform.position, new Vector3 (gridWorldSize.x, 1, gridWorldSize.y));
		if (grid != null) {
			Node playerNode = NodeFromWorldPoint (player.position);
			foreach(Node n in grid) {
				Gizmos.color = (n.walkable) ? Color.white : Color.red;
				if (playerNode == n) {
					Gizmos.color = Color.green;
				}
				if (path != null) {
					if (path.Contains (n))
						Gizmos.color = Color.yellow;
				}

				Gizmos.DrawCube (n.position, Vector3.one * (nodeDiameter - .1f));
			}
		}
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start() {
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		CreateGrid ();
	}

	/// <summary>
	/// Creates the grid.
	/// </summary>
	void CreateGrid() {
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
				grid [x, y] = new Node (walkable,worldPoint, x, y);
			}
		}
	}

	/// <summary>
	/// Nodes from world point.
	/// </summary>
	/// <returns>The from world point.</returns>
	/// <param name="worldPosition">World position.</param>
	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
		percentX = Mathf.Clamp01 (percentX);
		percentY = Mathf.Clamp01 (percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
		return grid [x, y];
	}

	/// <summary>
	/// Gets the node neighbours.
	/// </summary>
	/// <returns>The neighbours.</returns>
	/// <param name="node">Node.</param>
	public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node> ();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add (grid [checkX, checkY]);
				}
			}
		}
		return neighbours;
	}

}
