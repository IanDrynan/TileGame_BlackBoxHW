using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfuseTileScript : MonoBehaviour
{

    public delegate void TileTouched();
    public static event TileTouched onTouchConfuse;

    void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.gameObject.CompareTag("Player"))
        {
            if (onTouchConfuse != null)
            {
                onTouchConfuse();
            }
        }
    }
}
