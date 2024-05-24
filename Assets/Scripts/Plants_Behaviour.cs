using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Plants_Behaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject PlantPrefab;

    private int plantsCount;
    private void Start()
    {
        plantsCount = DataBase.DB.userPlants.Count;
        for (int i = 0; i < plantsCount; i++)
        {
            GameObject Plant = Instantiate(PlantPrefab, transform);
            Plant.GetComponent<Plant_Behaviour>().SetIdPlant(i);
        }
    }

}
