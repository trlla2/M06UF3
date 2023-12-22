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
    private bool isGrowing = false;
    private bool isCollectible = false;
    private Image cellImage;

    private void Start()
    {
        cellImage = GetComponent<Image>();

        cellText.text = "Empty";
    }

    private void LateUpdate()
    {
        if (isGrowing)
        {
            growingTime -= Time.deltaTime;
            

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

    public void OnClick(int id)
    {
       if(!isGrowing)
       {
            if(!isCollectible)
            {
                // restar cantidad de plantas con la id

                idGrowingPlant = id;
                growingTime = DataBase.DB.plants[idGrowingPlant].time;
                cellText.text = DataBase.DB.plants[idGrowingPlant].name;
                isGrowing = true;

            }
            else
            {
                GameManager._GM.CollectPlant(DataBase.DB.plants[idGrowingPlant].sell);
                cellImage.color = new Color(1, 1, 1);
                cellText.text = "Empty";
                isCollectible = false;
            }
       }
       
    }
}
