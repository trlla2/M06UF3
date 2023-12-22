using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager _GM;

    private int idHoldingPlant = 0;
    private float currentMoney = 9999.0f; 
    [SerializeField]
    private TMP_Text moneyText;

    private void Awake()
    {
        if(_GM != null && _GM != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _GM = this; 
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        //current money = DataBase.DB.getUserMoney();
    }
    private void LateUpdate()
    {
        moneyText.text = currentMoney.ToString();
    }
    public void SetHoldingPlant(int id)
    {
        idHoldingPlant = id;
    }
    public int GetHoldingPlantId()
    {
        return idHoldingPlant;
    }
    public void CollectPlant(float price)
    {
        currentMoney += price;
    }
    

}
