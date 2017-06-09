using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject Win;
    public GameObject Lose;

    //Subscribe to events
    void Awake()
    {
        WinTile.onTouchWin += ShowWin;
        PlayerController.onGameLost += ShowLost;
        GameManager.onRestart += HideText;
    }

	// Use this for initialization
	void Start () {
		
	}


    void ShowWin()
    {
        Lose.SetActive(false);
        Win.SetActive(true);
    }

    void ShowLost()
    {
        Win.SetActive(false);
        Lose.SetActive(true);
    }

    void HideText()
    {
        Lose.SetActive(false);
        Win.SetActive(false);
    }

    //Unsubscribe
    void onDestroy()
    {
        WinTile.onTouchWin -= ShowWin;
        PlayerController.onGameLost -= ShowLost;
        GameManager.onRestart -= HideText;
    }
}
