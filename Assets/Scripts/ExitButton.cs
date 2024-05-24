using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void Exit()
    {
        DataBase.DB.SaveData();

        Application.Quit();
    }
}
