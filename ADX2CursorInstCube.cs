using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX2CursorInstCube : ADX2CursorInstBase
{
    public float pitch = 0;

    // Use this for initialization
    void Awake () {
        Init();
    }
    
	//  人差し指触れた時
	public override void RawTouchTrigger(bool trigger) {
        if (trigger)
        {
            atomSource.pitch = pitch;
            atomSource.player.UpdateAll();
            atomSource.Play();
        } else
        {
            if (atomSource.player.GetStatus() == CriAtomExPlayer.Status.Playing)
            {
                atomSource.Stop();
            }
        }
    }
}
