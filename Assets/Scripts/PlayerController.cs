using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public Material black;
    public Material white;

    private PlayerState state;
    private PlayerState lastActionableState;
    public Vector3 initialPosition;
    private Material initialMaterial;

    public delegate void GameLost();
    public static event GameLost onGameLost;

    //subscribe to events
    void Awake()
    {
        TileScript.onTouch += CheckColor;
        PowerupScript.onTouchPower += GivePower;
        WinTile.onTouchWin += WinGame;
        GameManager.onRestart += RestartGame;
        ConfuseTileScript.onTouchConfuse += ConfuseSelf;
        DeathTileScript.onTouchDeath += LoseGame;
        SidePowerScript.onTouchSide += GiveSidePower;
        
    }

    //initialize variables
    void Start()
    {
        state = PlayerState.grounded;
        lastActionableState = PlayerState.grounded;
        initialPosition = transform.position;
        initialMaterial = gameObject.GetComponent<Renderer>().material;
    }

    public void Update()
    {
        //delegate update to current state's update function
        state.Update(this);
    }

    //keep track of whether or not the player was poweredup or confused or normal
    private void StoreLastActionableState(PlayerState toState) 
    {
        //store previous state
        lastActionableState = toState;
        state = toState;
        //call entry and exit functions if I include them later
        //state.enter();
        //state.exit();
    }

    //Helper function used in PlayerStates
    public void Jumped()
    {
        state = PlayerState.jumping;
    }

    //Everytime the player moves and collides with a black or white tile, we need to check if the color of the  tile matches the player's color
    void CheckColor(Renderer rend)
    {
        //If not the same color, player loses
        if (gameObject.GetComponent<Renderer>().material.color != rend.material.color)
        {
            LoseGame();
        }
        else
        {
           state = lastActionableState; //Set state to the last state that the player can move in a direction. So "confused", grounded", or "power".
        }
    }

    void LoseGame()
    {
        //change state here to pause game state
        state = PlayerState.pause;

        if (onGameLost != null)
        {
            onGameLost();  //For GameManager to know the game is lost
        }
    }

    void GivePower()
    {
        //change state here to powered up state
        StoreLastActionableState(PlayerState.power); ;
    }

    void GiveSidePower()
    {
        //change state here to side powered up state
        StoreLastActionableState(PlayerState.sidePower);
    }
    void ConfuseSelf()
    {
        //change state to confused
        StoreLastActionableState(PlayerState.confused);
        
    }

    //Could remove this and just use LoseGame but is useful for debugging purposes
    void WinGame()
    {
        //print("win');
        state = PlayerState.pause;
    }

    //restore initial material and position. Restoring material is important because if the starting tile and the player's material dont match, you instantly lose.
    void RestartGame()
    {
        StoreLastActionableState(PlayerState.grounded);
        transform.position = initialPosition;
        gameObject.GetComponent<Renderer>().material = initialMaterial;
    }

    //Unsubscribe
    public void OnDestroy()
    {
        TileScript.onTouch -= CheckColor;
        PowerupScript.onTouchPower -= GivePower;
        WinTile.onTouchWin -= WinGame;
        GameManager.onRestart -= RestartGame;
        ConfuseTileScript.onTouchConfuse -= ConfuseSelf;
        DeathTileScript.onTouchDeath -= LoseGame;
    }

    void OnCollisionEnter(Collision collision)
    {
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}


