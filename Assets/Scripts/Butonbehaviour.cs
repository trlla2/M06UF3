using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Butonbehaviour : MonoBehaviour
{
    Vector2 position = Vector2.zero;
    private void Start()
    {
        Button cell = GetComponent<Button>();
        
    }

    private void DebugPos()
    {
        Debug.Log("position x=" + position.x + " y=" + position.y);
    }

    public void CellPosition(int x, int y)
    {
        position.x = x;
        position.y = y;
    }
}
