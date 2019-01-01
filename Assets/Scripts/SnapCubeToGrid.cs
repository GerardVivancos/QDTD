using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SnapCubeToGrid : MonoBehaviour {

    GridCell cell;

    private void Awake() {
        cell = GetComponent<GridCell>();
    }

    void Update () {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid() {
        int gridSize = cell.GetCellSizeInWorldUnits();
        Vector3 p = transform.position;
        Vector2Int cellPosition = cell.GetCellPositionInGrid();

        p.x = cellPosition.x * gridSize;
        p.y = 0f;
        p.z = cellPosition.y * gridSize;

        transform.position = p;
    }

    private void UpdateLabel() {
        Vector2Int cellPosition = cell.GetCellPositionInGrid();
        TextMesh label = GetComponentInChildren<TextMesh>();
        string objectPositionLabel = cellPosition.x + "," + cellPosition.y;
        label.text = objectPositionLabel;

    }
}
