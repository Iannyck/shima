using UnityEngine;
using System.Collections;

/// <summary>
/// Node.
/// A node of the grid. Node is used in path finding scripts.
/// </summary>
public class Node {

	/// <summary>
	/// The walkable variable indicates whereas the node is walkable or not.
	/// </summary>
	public bool walkable;

	/// <summary>
	/// The position of the node in the physical world.
	/// </summary>
	public Vector3 position;

	/// <summary>
	/// The x position of the node in the grid.
	/// </summary>
	public int gridX;

	/// <summary>
	/// The y position of the node in the grid.
	/// </summary>
	public int gridY;

	public int gCost;
	public int hCost;
	public Node parent;

	public Node (bool walkable, Vector3 position, int gridX, int gridY)
	{
		this.walkable = walkable;
		this.position = position;
		this.gridX = gridX;
		this.gridY = gridY;
	}

	public int fCost {
		get { 
			return gCost + hCost;
		}
	}

}
