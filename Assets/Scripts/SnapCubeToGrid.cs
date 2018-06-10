﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class SnapCubeToGrid : MonoBehaviour {

    [SerializeField]
    [Range(1,100)]
    public float gridSize = 10f;
	
	void Update () {
        Vector3 p = gameObject.transform.position;

        p.x = Mathf.RoundToInt(p.x / gridSize) * gridSize;
        p.y = Mathf.RoundToInt(p.y / gridSize) * gridSize;
        p.z = Mathf.RoundToInt(p.z / gridSize) * gridSize;

        gameObject.transform.position = p;
        updateLabel();
    }

    private void updateLabel() {
        TextMesh label = GetComponentInChildren<TextMesh>();
        string objectPositionLabel = gameObject.transform.position.x / gridSize + "," + gameObject.transform.position.z / gridSize;
        label.text = objectPositionLabel;
        gameObject.name = objectPositionLabel;
    }
}
