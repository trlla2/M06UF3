using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Behaviour : MonoBehaviour
{
    public static Manager_Behaviour mainManager;
    private void Awake()
    {
        if (mainManager != null && mainManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            mainManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


}
