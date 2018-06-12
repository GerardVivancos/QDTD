using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    Dictionary<Vector2Int, GridCell> grid = new Dictionary<Vector2Int, GridCell>();
    [SerializeField]
    GridCell startCell;
    [SerializeField]
    GridCell endCell;

    Vector2Int[] allowedDirections = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

	// Use this for initialization
	void Start () {
        LoadGrid();
        ColorStartEnd();
        FindPath(startCell, endCell);
        //ExploreNeighbours(startCell);
    }

    private void ColorStartEnd() {
        startCell.SetTopColor(Color.green);
        endCell.SetTopColor(Color.blue);
    }
	
    private void LoadGrid() {
        GridCell[] cells = GetComponentsInChildren<GridCell>();
        foreach (GridCell cell in cells) {
            Vector2Int cellPosition = cell.GetGridPosition();

            if (grid.ContainsKey(cellPosition)) {
                Debug.LogWarning("Skipped loading of overlapping Cell in " + cellPosition);
            } else {
                grid.Add(cellPosition, cell);
                Color color = Color.yellow;
                cell.SetTopColor(color);
            }
        }
    }

    private void FindPath(GridCell start, GridCell end) {
        FindPath(start.GetGridPosition(), end.GetGridPosition());
    }

    private void FindPath(Vector2Int startPosition, Vector2Int endPosition) {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        List<Vector2Int> alreadyVisitedPositions = new List<Vector2Int>();

        if (! (grid.ContainsKey(startPosition) && grid.ContainsKey(endPosition))) {
            print("Unexistant start or end position");
            return;
        }

        if ( startPosition == endPosition) {
            print("Start and end positions are the same");
            return;
        }

        queue.Enqueue(startPosition);
        while (queue.Count > 0) {
            Vector2Int searchPosition = queue.Dequeue();
            alreadyVisitedPositions.Add(searchPosition);
            foreach (Vector2Int neighbourPosition in ExploreNeighbours(searchPosition)) {
                if (neighbourPosition == endPosition) {
                    print("found end: " + endPosition + " from " + searchPosition);
                    return;
                } else if (!alreadyVisitedPositions.Contains(neighbourPosition) && !queue.Contains(neighbourPosition)) {
                    print("found neighbour: " + neighbourPosition + " from " + searchPosition );
                    queue.Enqueue(neighbourPosition);
                } else {
                    print("prevented " + neighbourPosition + " from " + searchPosition);
                }
            }
        }
    }

    private List<Vector2Int> ExploreNeighbours(GridCell cell) {
        Vector2Int currentCellPosition = cell.GetGridPosition();
        return ExploreNeighbours(currentCellPosition);
    }

    private List<Vector2Int> ExploreNeighbours(Vector2Int currentCellPosition) {
        List<Vector2Int> neighbours = new List<Vector2Int>();
        foreach (Vector2Int direction in allowedDirections) {
            Vector2Int neighbourPosition = currentCellPosition + direction;
            if (grid.ContainsKey(neighbourPosition)) {
                grid[neighbourPosition].SetTopColor(Color.magenta);
                neighbours.Add(neighbourPosition);
            }
        }
        return neighbours;
    }
}
