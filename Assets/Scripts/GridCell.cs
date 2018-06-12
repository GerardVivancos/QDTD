using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class GridCell : MonoBehaviour {

    private const int gridSize = 10;
    private Vector2Int gridPosition;

    public void Awake() {
        UpdateGridPosition();
    }

    private void UpdateGridPosition() {
        Vector3 p = transform.position;
        gridPosition = new Vector2Int(
            Mathf.RoundToInt(p.x / gridSize),
            Mathf.RoundToInt(p.z / gridSize)
            );
        //Also update the GameObject's name
        gameObject.name = gridPosition.x +","+ gridPosition.y;
    }

    public int GetGridSize() {
        return gridSize;
    }

    public Vector2Int GetGridPosition() {
        UpdateGridPosition();
        return gridPosition;
    }

    public void SetTopColor(Color color) {
        transform.Find("Top").GetComponent<MeshRenderer>().material.color = color;
    }
}
