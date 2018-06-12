using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    Dictionary<Vector2Int, GridCell> grid = new Dictionary<Vector2Int, GridCell>();
    [SerializeField]
    GridCell startCell;
    [SerializeField]
    GridCell endCell;

	// Use this for initialization
	void Start () {
        LoadGrid();
        FindPath(startCell, endCell);
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

        startCell.SetTopColor(Color.green);
        endCell.SetTopColor(Color.blue);

    }
}
