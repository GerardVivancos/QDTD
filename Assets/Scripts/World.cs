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
        List<Vector2Int> path = FindPath(startCell, endCell);
        StartCoroutine("TestPath", path);
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

    private List<Vector2Int> FindPath(GridCell start, GridCell end) {
        return FindPath(start.GetGridPosition(), end.GetGridPosition());
    }

    private List<Vector2Int> FindPath(Vector2Int startPosition, Vector2Int endPosition) {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> visitedPositions = new Dictionary<Vector2Int, Vector2Int>();

        if (! (grid.ContainsKey(startPosition) && grid.ContainsKey(endPosition))) {
            print("Unexistant start or end position");
            return null;
        }

        if ( startPosition == endPosition) {
            print("Start and end positions are the same");
            return null;
        }

        visitedPositions.Add(startPosition, startPosition);
        queue.Enqueue(startPosition);
        bool found = false;
        while (!found && queue.Count > 0) {
            Vector2Int searchPosition = queue.Dequeue();
            foreach (Vector2Int neighbourPosition in ExploreNeighbours(searchPosition)) {
                if (neighbourPosition == endPosition) {
                    visitedPositions.Add(endPosition, searchPosition);
                    print("found end: " + endPosition + " from " + searchPosition);
                    found = true;
                    break;
                } else if (!visitedPositions.ContainsKey(neighbourPosition) && !queue.Contains(neighbourPosition)) {
                    visitedPositions.Add(neighbourPosition, searchPosition);
                    print("found neighbour: " + neighbourPosition + " from " + searchPosition );
                    queue.Enqueue(neighbourPosition);
                } else {
                    print("prevented " + neighbourPosition + " from " + searchPosition);
                }
            }
        }

        //int loopCounter = visitedPositions.Count;

        //do {
        //    v = visitedPositions[v];
        //    print("traceback: " + v);
        //    loopCounter--;
        //} while (loopCounter > 0 && v != startPosition && visitedPositions.ContainsKey(v));

        Vector2Int v = endPosition;
        List<Vector2Int> path = new List<Vector2Int>();
        while (visitedPositions[v] != v) {
            path.Add(v);
            v = visitedPositions[v];
        }
        path.Add(v);
        path.Reverse();
        return path;
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

    private IEnumerator TestPath(List<Vector2Int> path) {
        foreach (Vector2Int v in path) {
            GridCell c;
            grid.TryGetValue(v, out c);
            c.SetTopColor(Color.red);
            yield return new WaitForSeconds(1f);
        }
    }
}
