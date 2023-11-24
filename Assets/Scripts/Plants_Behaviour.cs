using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants_Behaviour : MonoBehaviour
{
    //[SerializeField]
   // private GameObject PlantPrefab;
    
    private int plantsCount;
    void Start()
    {
        plantsCount = DataBase.DB.GetPlants().Count;
        Debug.Log(plantsCount);
        //for (int i = 0; i < plantsCount; i++)
        //{
        //        GameObject Plant = Instantiate(PlantPrefab, transform);
        //}
    }

}
