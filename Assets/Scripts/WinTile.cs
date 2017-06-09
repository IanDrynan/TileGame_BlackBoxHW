using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTile : MonoBehaviour
{

    public delegate void TileTouched();
    public static event TileTouched onTouchWin;

    void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.gameObject.CompareTag("Player"))
        {
            if (onTouchWin != null)
            {
                onTouchWin();
            }
        }
    }
}
