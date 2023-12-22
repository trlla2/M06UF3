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
        if (currentLandsUpgrade < maxLandsUpgrade)
        {
            //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 20, transform.localScale.z);
            rows++;
            //gridLayout.constraintCount = columns;

            //RemoveCells();

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

    public void RemoveCells()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

}
   
