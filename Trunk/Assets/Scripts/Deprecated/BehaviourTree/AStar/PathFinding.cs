using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour {


	public Transform seeker, target;

	void Update() {
//		grid.path = FindPath (seeker.position, target.position);
	}

	Grid grid;

	void Awake() {
		grid = GetComponent<Grid> ();
	}
	public void SetPath(List<Node> path) {
		grid.path = path;
	}

	public List<Node> FindPath(Vector3 startPos, Vector3 targetPos) {
		Node startNode = grid.NodeFromWorldPoint (startPos);
		Node targetNode = grid.NodeFromWorldPoint (targetPos);

		List<Node> openSet = new List<Node> ();
		HashSet<Node> closedSet = new HashSet<Node> ();
		openSet.Add (startNode);

		while (openSet.Count > 0) {
			Node currentNode = openSet [0];
			for (int i = 1; i < openSet.Count; i++) {
				if (openSet [i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) {
					currentNode = openSet [i];
				}
			}
			openSet.Remove (currentNode);
			closedSet.Add (currentNode);

			if (currentNode == targetNode) {
				return RetracePath (startNode, targetNode);
				//return;
			}

			foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
				if (!neighbour.walkable || closedSet.Contains (neighbour)) {
					continue;
				}

				int penality = 0;
				if (IsNearToUnwalkable (neighbour)) {
					penality = 1000;
				}

//				int newMovementCostToNeighbour = currentNode.gCost + GetDistance (currentNode, neighbour);
				int newMovementCostToNeighbour = currentNode.gCost + GetDistance (currentNode, neighbour) + penality;
//				Debug.Log (newMovementCostToNeighbour + "gCost "+neighbour.gCost);
				if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains (neighbour)) {
					neighbour.gCost = newMovementCostToNeighbour;
					neighbour.hCost = GetDistance (neighbour, targetNode);
					neighbour.parent = currentNode;

					if (!openSet.Contains (neighbour))
						openSet.Add (neighbour);
				}
			}
		}
		return null;

	}

	int GetDistance(Node nodeA, Node nodeB) {
		int dstX = Mathf.Abs (nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs (nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
			return 14 * dstY + 10 * (dstX - dstY);
		return 14 * dstX + 10 * (dstY - dstX);
	}

	List<Node> RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node> ();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add (currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse ();
		return path;
		//grid.path = path;
	}


	public bool IsOnThisNode(Vector3 position, Node node) {
		Node currentNode = grid.NodeFromWorldPoint(position);
		if (currentNode.gridX == node.gridX && currentNode.gridY == node.gridY)
			return true;
		return false;
	}
		
	public Node GetReplacementNode(Node currentNode, Node nextNode) {
		List<Node> currentNodeNeighbours = grid.GetNeighbours (currentNode);
		List<Node> nextNodeNeighbours = grid.GetNeighbours (nextNode);
		foreach(Node node in currentNodeNeighbours) {
			if(node.walkable) {
				if(IsNodeInNeighbours(node,nextNodeNeighbours)) {
					return node;
				}
			}
		}
		return null;
	}

	private bool IsNodeInNeighbours(Node node, List<Node> neighbours) {
		foreach(Node cnode in neighbours) {
			if (cnode.gridX == node.gridX && cnode.gridY == node.gridY)
				return true;
		}
		return false;
	}

	public Node GetCurrentNode(Vector3 position) {
		return grid.NodeFromWorldPoint(position);
	}

	private bool IsNearToUnwalkable(Node node) {
		List<Node> neighbour = grid.GetNeighbours (node);
		foreach(Node cnode in neighbour){
			if (!cnode.walkable)
				return true;
		}
		return false;
	}

	
}
