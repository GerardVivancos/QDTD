using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class GridCell : MonoBehaviour {

    private const int cellSizeInWorldUnits = 10;
    private Vector2Int cellPositionInGrid;

    public void Awake() {
        UpdateCellPositionInGrid();
    }

    private void UpdateCellPositionInGrid() {
        Vector3 p = transform.position;
        cellPositionInGrid = new Vector2Int(
            Mathf.RoundToInt(p.x / cellSizeInWorldUnits),
            Mathf.RoundToInt(p.z / cellSizeInWorldUnits)
            );
        //Also update the GameObject's name
        gameObject.name = cellPositionInGrid.x +","+ cellPositionInGrid.y;
    }

    public int GetCellSizeInWorldUnits() {
        return cellSizeInWorldUnits;
    }

    public Vector2Int GetCellPositionInGrid() {
        UpdateCellPositionInGrid();
        return cellPositionInGrid;
    }

    public void SetTopColor(Color color) {
        transform.Find("Top").GetComponent<MeshRenderer>().material.color = color;
    }
}
