using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class Plant_Behaviour : MonoBehaviour
{
    private int idPlant = 0;
    [SerializeField]
    private TMP_Text plantText;
    [SerializeField]
    private TMP_Text quantityPlantText;

    public void Start()
    {
        plantText.text = DataBase.DB.plants[idPlant].name;
    }
    private void LateUpdate()
    {
        quantityPlantText.text =  DataBase.DB.plants[idPlant].quantity.ToString();
    }
    public void SetIdPlant(int id)
    { 
        idPlant = id;
    }

    public void ReturnPlant()
    {
        GameManager._GM.SetHoldingPlant(idPlant);
    }
}
