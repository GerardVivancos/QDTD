using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    Dictionary<Vector2Int, GridCell> grid = new Dictionary<Vector2Int, GridCell>();

	// Use this for initialization
	void Start () {
        LoadGrid();
	}
	
    private void LoadGrid() {
        GridCell[] cells = GetComponentsInChildren<GridCell>();
        foreach (GridCell cell in cells) {
            Vector2Int cellPosition = cell.GetGridPosition();

            if (grid.ContainsKey(cellPosition)) {
                Debug.LogWarning("Skipped loading of overlapping Cell in " + cellPosition);
            } else {
                grid.Add(cellPosition, cell);
            }
        }
    }
}
