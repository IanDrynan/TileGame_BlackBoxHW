using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    public delegate void TileTouched(Renderer rend);
    public static event TileTouched onTouch;

    void OnCollisionEnter(Collision collision) {

        if (collision.collider.gameObject.CompareTag("Player"))
        {
            if (onTouch != null)
            {
                onTouch(gameObject.GetComponent<Renderer>());
            }
        }
    }
}

