using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class PlayerState
{
    public static PowerState power = new PowerState();
    public static GroundState grounded = new GroundState();
    public static PauseState pause = new PauseState();
    public static JumpState jumping = new JumpState();
    public static ConfuseState confused = new ConfuseState();
    public static SidePowerState sidePower = new SidePowerState();

    public PlayerState() { }
    public virtual void Update(PlayerController pc) { }
    public virtual void enter(PlayerController pc) { }
    public virtual void exit(PlayerController pc) { }
}

//Normal state
class GroundState : PlayerState
{

    public GroundState() { }

    override public void Update(PlayerController pc)
    {
        Vector3 position = pc.transform.position;

        if (Input.GetKeyDown(KeyCode.A))
        {
            pc.GetComponent<Rigidbody>().AddForce(new Vector3(-pc.speed, pc.jumpForce, 0), ForceMode.Impulse);
            pc.Jumped();

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            pc.GetComponent<Rigidbody>().AddForce(new Vector3(pc.speed, pc.jumpForce, 0), ForceMode.Impulse);
            pc.Jumped();

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            pc.GetComponent<Rigidbody>().AddForce(new Vector3(0, pc.jumpForce, pc.speed), ForceMode.Impulse);
            pc.Jumped();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            pc.GetComponent<Rigidbody>().AddForce(new Vector3(0, pc.jumpForce, -pc.speed), ForceMode.Impulse);
            pc.Jumped();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            pc.gameObject.GetComponent<Renderer>().material = pc.black;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            pc.gameObject.GetComponent<Renderer>().material = pc.white;
        }
    }
}
 
//While Jumping, cannot jump in midair.
class JumpState : PlayerState
{
    public JumpState() { }

    override public void Update(PlayerController pc) {

        if (Input.GetKeyDown(KeyCode.J))
        {
            pc.gameObject.GetComponent<Renderer>().material = pc.black;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            pc.gameObject.GetComponent<Renderer>().material = pc.white;
        }
    }
}

//This state is used when the game is won or lost. Since there is no difference between the two to the player, there is only one state.
//Dont allow player to move while the gamemanager is waiting for a restart. 
class PauseState : PlayerState
{
    public PauseState() { }

    public new void Update(PlayerController pc)
    {
        //while paused, don't let player move. Game Pauses when player has won or lost. A generic state where the player should not be able to move.
    }
}

//Moving forward cause player to hop over a tile, which is faster and results in a better time if used well
class PowerState : PlayerState
{
    private int skip = 2;

    public PowerState() { }

    override public void enter(PlayerController pc)
    {

    }
    public override void Update(PlayerController pc)
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            pc.GetComponent<Rigidbody>().AddForce(new Vector3(-pc.speed, pc.jumpForce, 0), ForceMode.Impulse);
            pc.Jumped();

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            pc.GetComponent<Rigidbody>().AddForce(new Vector3(pc.speed, pc.jumpForce, 0), ForceMode.Impulse);
            pc.Jumped();

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            pc.GetComponent<Rigidbody>().AddForce(new Vector3(0, pc.jumpForce, pc.speed * skip), ForceMode.Impulse);
            pc.Jumped();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            pc.GetComponent<Rigidbody>().AddForce(new Vector3(0, pc.jumpForce, -pc.speed), ForceMode.Impulse);
            pc.Jumped();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            pc.gameObject.GetComponent<Renderer>().material = pc.black;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            pc.gameObject.GetComponent<Renderer>().material = pc.white;
        }
    }
}

//Moving sideways cause player to hop over a tile
class SidePowerState : PlayerState
{
    private int skip = 2;

    public SidePowerState() { }

    override public void enter(PlayerController pc)
    {

    }
    public override void Update(PlayerController pc)
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            pc.GetComponent<Rigidbody>().AddForce(new Vector3(-pc.speed * skip, pc.jumpForce, 0), ForceMode.Impulse);
            pc.Jumped();

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            pc.GetComponent<Rigidbody>().AddForce(new Vector3(pc.speed * skip, pc.jumpForce, 0), ForceMode.Impulse);
            pc.Jumped();

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            pc.GetComponent<Rigidbody>().AddForce(new Vector3(0, pc.jumpForce, pc.speed), ForceMode.Impulse);
            pc.Jumped();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            pc.GetComponent<Rigidbody>().AddForce(new Vector3(0, pc.jumpForce, -pc.speed), ForceMode.Impulse);
            pc.Jumped();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            pc.gameObject.GetComponent<Renderer>().material = pc.black;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            pc.gameObject.GetComponent<Renderer>().material = pc.white;
        }
    }
}

//Movements and color switching are reversed
class ConfuseState : PlayerState{

    public ConfuseState() { }

    override public void Update(PlayerController pc)
    {
        Vector3 position = pc.transform.position;

        if (Input.GetKeyDown(KeyCode.A))
        {
            pc.GetComponent<Rigidbody>().AddForce(new Vector3(pc.speed, pc.jumpForce, 0), ForceMode.Impulse);
            pc.Jumped();

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            pc.GetComponent<Rigidbody>().AddForce(new Vector3(-pc.speed, pc.jumpForce, 0), ForceMode.Impulse);
            pc.Jumped();

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            pc.GetComponent<Rigidbody>().AddForce(new Vector3(0, pc.jumpForce, -pc.speed), ForceMode.Impulse);
            pc.Jumped();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            pc.GetComponent<Rigidbody>().AddForce(new Vector3(0, pc.jumpForce, pc.speed), ForceMode.Impulse);
            pc.Jumped();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            pc.gameObject.GetComponent<Renderer>().material = pc.black;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            pc.gameObject.GetComponent<Renderer>().material = pc.white;
        }
    }
}

