using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 class ManagerState
{
    public static gmPauseState gmPause = new gmPauseState();
    public static gmPlayState gmPlay = new gmPlayState();

    public ManagerState() { }
    public virtual void Update(GameManager gm) { }
    public virtual void enter(GameManager gm) { }
    public virtual void exit(GameManager gm) { }
}

 class gmPlayState : ManagerState
{
    //Timer runs when in Play state
    public override void Update(GameManager gm)
    {
        gm.UpdateTime();
    }
}

class gmPauseState : ManagerState
{
    //Timer doesn't run in Play state and the player can restart by pressing 'r'
    public override void Update(GameManager gm)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gm.ReloadScene();
        }
    }
}
