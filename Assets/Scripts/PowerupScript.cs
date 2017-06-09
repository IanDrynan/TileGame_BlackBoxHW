using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{

    public delegate void TileTouched();
    public static event TileTouched onTouchPower;

    void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.gameObject.CompareTag("Player"))
        {
            if (onTouchPower != null)
            {
                onTouchPower();
            }
        }
    }
}
