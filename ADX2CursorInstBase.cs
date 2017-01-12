using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX2CursorInstBase : MonoBehaviour {

    public CriAtomSource atomSource;
    // Use this for initialization
    void Awake()
    {

    }

    public void Init()
    {
        //  ADX2
        atomSource = this.gameObject.AddComponent<CriAtomSource>();
    }

    public void SetCue(string cueSheetName, string cueName)
    {
        atomSource.cueSheet = cueSheetName;
        atomSource.cueName = cueName;
    }

    //  人差し指触れた時
    public virtual void RawTouchTrigger(bool trigger)
    {
        if (trigger)
        {
            atomSource.Play();
        }
        else
        {
            if (atomSource.player.GetStatus() == CriAtomExPlayer.Status.Playing)
            {
                atomSource.Stop();
            }
        }
    }
}
