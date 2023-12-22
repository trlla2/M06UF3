using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGridCell : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private int rows = 4;
    [SerializeField]
    private int columns = 4;

    [SerializeField]
    private float priceCells = 20;

    private int maxLandsUpgrade = 9;
    private int currentLandsUpgrade = 0;

    GridLayoutGroup gridLayout;


    private void Awake()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        gridLayout.constraintCount = columns;
    }

    void Start()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                GameObject button = Instantiate(buttonPrefab, transform);
                button.GetComponent<Cell_Behaviour>().CellPosition(x+1,y+1);
            }
        }
    }

    public void AddCells()
    {
        if (currentLandsUpgrade < maxLandsUpgrade && GameManager._GM.GetMoney() > priceCells)
        {
            GameManager._GM.BuyThings(priceCells);
            rows++;
            for (int x = 0; x < columns; x++)
            {
                GameObject button = Instantiate(buttonPrefab, transform);
                button.GetComponent<Cell_Behaviour>().CellPosition(rows, x +1);
            }
            currentLandsUpgrade++;
        }
        else
        {
            Debug.Log("fck that");
        }
    }

}
   
