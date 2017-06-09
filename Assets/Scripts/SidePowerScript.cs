using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePowerScript : MonoBehaviour {

    public delegate void TileTouched();
    public static event TileTouched onTouchSide;

    void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.gameObject.CompareTag("Player"))
        {
            if (onTouchSide != null)
            {
                onTouchSide();
            }
        }
    }
}
