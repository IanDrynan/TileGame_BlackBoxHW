using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTileScript : MonoBehaviour
{

    public delegate void TileTouched();
    public static event TileTouched onTouchDeath;

    void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.gameObject.CompareTag("Player"))
        {
            if (onTouchDeath != null)
            {
                onTouchDeath();
            }
        }
    }
}
