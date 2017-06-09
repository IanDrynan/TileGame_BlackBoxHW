using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject[] Tiles;
    public GameObject[] EventTiles;
    public GameObject GoalTile;
    public GameObject DeathTile;
    public Text Timer;
    public int sizeX;
    public int sizeZ;
    public int eventChance;
    public float tileSize;
    public Text BestText;
    public float tileUnits;
    private float gridX;
    private float gridZ;
    private Transform GridObject;
    private float counter;
    private static float bestTime;
    private TimeSpan timespan;

    private ManagerState state;

    public delegate void Restart();
    public static event Restart onRestart;

    public static GameManager instance;

    public static GameManager Instance { get { return instance; } }

	// Use this for initialization
	void Awake () {

        //Subscribe to Events
        WinTile.onTouchWin += WonGame;
        PlayerController.onGameLost += StopGame;

        //destroys duplicate GameManagers
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
	}

    void Start()
    {
        counter = 0;
        bestTime = PlayerPrefs.GetFloat("bestTime", 600f);
        BestText.text = TimeFormat(bestTime);
        tileUnits = 10 * tileSize; // a plane is 10 units by 10 units arbitrarily. So a scale of 1 has 10 units.
        gridX = tileUnits * (sizeX);  //calculate when to stop placing tiles by multiplying tile size by number of tiles.
        gridZ = tileUnits * (sizeZ);  //Adding 4 since we need buffer tiles in order to keep the player on the game grid.
        state = ManagerState.gmPlay;
        InitWorld();
    }
	void Update () {
        //delegate update to current state's update function
        state.Update(this);
	}

    // Update the timer text per frame.
    public void UpdateTime()
    {
        counter += Time.deltaTime * 1.0f;
        Timer.text = TimeFormat(counter);
    }

    //Formats the counter variable to fit a "00:00" format. 
    String TimeFormat(float time)
    {
        timespan = TimeSpan.FromSeconds(time);
        var result = string.Format("{0:D2}:{1:D2}", (int)timespan.TotalMinutes, timespan.Seconds);
        return result.ToString();
    }

    private void InitWorld()
    {
        GridObject = new GameObject("grid").transform;
        GameObject toInstantiate;
        GameObject tileInstance;

        //create grid of tiles
        for (float x = 0; x < gridX;)
        {
            for (float z = 0; z < gridZ;)
            {
                //Create a grid of deathtiles around the grid
                if (x <= tileUnits || x >= gridX - (2*tileUnits) || z == 0)
                {
                    toInstantiate = DeathTile;
                }
                else if(z == tileUnits)
                {
                    toInstantiate = Tiles[0];
                }
                else {
                    if (UnityEngine.Random.Range(0, 100) < eventChance)
                    {
                        toInstantiate = EventTiles[UnityEngine.Random.Range(0, EventTiles.Length)];
                    }
                    else {
                        toInstantiate = Tiles[UnityEngine.Random.Range(0, Tiles.Length)];
                    }
                }

                tileInstance = Instantiate(toInstantiate, new Vector3(x, 0f, z), Quaternion.Euler(0, 180, 0)) as GameObject;

                tileInstance.transform.localScale = new Vector3(tileSize, tileSize, tileSize);

                tileInstance.transform.SetParent(GridObject);

                z += tileUnits;
            }
            x += tileUnits;
        }
        
        //create 2 rows of goal tiles at the end of the grid.
        for (float x = 0; x < gridX;)
        {
            for (float z = gridZ; z < gridZ + tileUnits*2;)
            {
                tileInstance = Instantiate(GoalTile, new Vector3(x, 0f, z), Quaternion.identity) as GameObject;
                tileInstance.transform.localScale = new Vector3(tileSize, tileSize, tileSize);
                tileInstance.transform.SetParent(GridObject);

                z += tileUnits;
            }
            x += tileUnits;
        }
    }	

    public void ReloadScene()
    {
        counter = 0;
        state = ManagerState.gmPlay;
        Destroy(GridObject.gameObject);
        InitWorld();

        if (onRestart != null)
        {
            onRestart();  //Let Everyone know game has restarted
        }
    }

    private void StopGame()
    {
        state = ManagerState.gmPause;
    }

    //stores new fastest time if needed.
    private void WonGame()
    {
        state = ManagerState.gmPause;

        
        if (counter < bestTime)
        {
            bestTime = counter;
            BestText.text = TimeFormat(bestTime);
            PlayerPrefs.SetFloat("bestTime", bestTime);
        }
    }

    //Unsubscribe
    public void OnDestroy()
    {
        WinTile.onTouchWin -= StopGame;
        PlayerController.onGameLost -= StopGame;
    }
}

