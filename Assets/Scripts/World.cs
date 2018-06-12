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
        //FindPath(startCell, endCell);
        ExploreNeighbours(startCell);

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
        GridCell startCell, endCell;
        grid.TryGetValue(startPosition,  out startCell);
        grid.TryGetValue(endPosition, out endCell);
    }

    private void ExploreNeighbours(GridCell cell) {
        Vector2Int currentCellPosition = cell.GetGridPosition();

        foreach (Vector2Int direction in allowedDirections) {
            Vector2Int neighbourPosition = currentCellPosition + direction;
            if (grid.ContainsKey(neighbourPosition)) {
                grid[neighbourPosition].SetTopColor(Color.magenta);
            }
        }
    }
}
