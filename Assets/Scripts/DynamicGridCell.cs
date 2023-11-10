using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGridCell : MonoBehaviour
{
    public GameObject buttonPrefab;
    public int rows;
    public int columns;
    public float cellSize;
    public float spacing;

    void Start()
    {
        GenerateGridCell();
    }

    void GenerateGridCell()
    {
        GridLayoutGroup gridLayout = GetComponent<GridLayoutGroup>();

        // Set the cell size and spacing of the grid layout
        gridLayout.cellSize = new Vector2(cellSize, cellSize);
        gridLayout.spacing = new Vector2(spacing, spacing);

        // Calculate the total number of buttons
        int totalButtons = rows * columns;

        for (int i = 0; i < totalButtons; i++)
        {
            // Instantiate the button prefab
            GameObject button = Instantiate(buttonPrefab, transform);

            // Set the button's position in the grid layout
            int row = i / columns;
            int column = i % columns;
            RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
            buttonRectTransform.localPosition = new Vector3(column * (cellSize + spacing), -row * (cellSize + spacing), 0);
        }
    }
}
   
