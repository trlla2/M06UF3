using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants_Behaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject PlantPrefab;
    [SerializeField]
    private int plantsCount;
    void Start()
    {
        for(int i = 0; i < plantsCount; i++)
        {
                GameObject Plant = Instantiate(PlantPrefab, transform);
                
        }
    }

}
