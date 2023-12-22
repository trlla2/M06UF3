using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlantShop_Behavior : MonoBehaviour
{
    private int idPlant = 0;
    [SerializeField]
    private TMP_Text plantText;
    [SerializeField]
    private TMP_Text pricePlantText;

    private void Start()
    {
        plantText.text = DataBase.DB.plants[idPlant].name;
    }
    private void LateUpdate()
    {
        pricePlantText.text = DataBase.DB.plants[idPlant].buy.ToString();
    }
    public void SetIdPlant(int id)
    {
        idPlant = id;
    }
    public void BuyPlant() 
    {
        if(GameManager._GM.GetMoney() >= DataBase.DB.plants[idPlant].buy)
        {
            DataBase.DB.plants[idPlant].quantity++;
            GameManager._GM.BuyThings(DataBase.DB.plants[idPlant].buy);
        }
    }
}
