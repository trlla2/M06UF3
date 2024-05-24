using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;

public class Cell_Behaviour : MonoBehaviour
{
    private Vector2 position = Vector2.zero;
    [SerializeField]
    private TMP_Text cellText;
    private int idGrowingPlant = 0;
    private float growingTime = 0;
    private int cellID = 0;
    private bool isGrowing = false;
    private bool isCollectible = false;
    private Image cellImage;

    private void Start()
    {
        cellImage = GetComponent<Image>();

        cell c = new cell();
        c = DataBase.DB.GetCell((int)position.x, (int)position.y);

        c.id = cellID;

        if(c.id_plant != 0)
        {
            idGrowingPlant = c.id_plant;
            cellText.text = DataBase.DB.plants[idGrowingPlant].name;
            if (c.time > 0)
            {
                cellImage.color = new Color(1, 1, 0);
                growingTime = c.time;
                isGrowing = true;
                isCollectible = false;
            }
            else
            {
                cellImage.color = new Color(0, 1, 0);

                isGrowing = false;
                isCollectible = true;
            }
        }
        else
        {
            cellText.text = "Empty";
        }



    }

    private void Update()
    {
        if (isGrowing)
        {
            growingTime -= Time.deltaTime;

            //Update Cell     LO SIENTO POR EL FOREACH EN EL UPDATE S K NO ME OCURRE OTRA MANERA Y NO TENGO TIEMPO :(
            foreach(cell cell in DataBase.DB.cells)
            {
                if(cell.id == cellID)
                {
                    cell.time = growingTime;
                }
            }
            

            if (growingTime < 0) 
            {
                isGrowing = false;
                isCollectible = true;
                cellImage.color =  new Color(0, 1, 0);
            }
        }
    }

    public void CellPosition(int x, int y)
    {
        position.x = x;
        position.y = y;

    }

    public void OnClick()
    {
       if(!isGrowing)
       {
            if (!isCollectible)
            {
                idGrowingPlant = GameManager._GM.GetHoldingPlantId();
                if (DataBase.DB.plants[idGrowingPlant].quantity > 0)
                {
                    cellImage.color = new Color(1, 1, 0);
                    DataBase.DB.plants[idGrowingPlant].quantity--;
                    growingTime = DataBase.DB.plants[idGrowingPlant].time;
                    cellText.text = DataBase.DB.plants[idGrowingPlant].name;
                    isGrowing = true;
                }
            }
            else
            {
                GameManager._GM.CollectPlant(DataBase.DB.plants[idGrowingPlant].sell);
                cellImage.color = new Color(1, 1, 1);
                cellText.text = "Empty";
                isCollectible = false;
                idGrowingPlant = 0;
            }

            //UPDATE IDPLANT
            foreach (cell cell in DataBase.DB.cells)
            {
                if (cell.id == cellID)
                {
                    cell.id_plant = idGrowingPlant;
                }
            }
        }
       
    }
}
