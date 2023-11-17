using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGridCell : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private int rows;
    [SerializeField]
    private int columns;

    void Start()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                GameObject button = Instantiate(buttonPrefab, transform);
                button.GetComponent<Butonbehaviour>().CellPosition(x+1,y+1);
            }
        }
    }

    public void AddCells()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 200, transform.localScale.z);
        columns++;
        for (int x = 0; x < rows; x++)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            button.GetComponent<Butonbehaviour>().CellPosition(x + 1, columns);
        }
    }

}
   
