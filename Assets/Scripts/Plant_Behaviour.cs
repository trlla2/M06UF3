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

    public void SetIdPlant(int id)
    {
        idPlant = id;
    }

    public void Start()
    {
       // Debug.Log(DataBase.DB.plants[idPlant].quantity);
        quantityPlantText.text =  DataBase.DB.plants[idPlant].quantity.ToString();

        plantText.text = DataBase.DB.plants[idPlant].name;
    }
}
