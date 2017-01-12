using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX2PlayVibe : MonoBehaviour {

    CriAtomSource atomSource;
    Vector3 startPos;
    Vector3 endPos;

    // Use this for initialization
    void Start ()
    {
        atomSource = this.gameObject.GetComponent<CriAtomSource>();
        startPos = this.transform.localPosition;
        endPos = startPos;
        endPos.y += 0.05f;
    }
	
	// Update is called once per frame
	void Update () {
        if (atomSource != null)
        {
            if (atomSource.player.GetStatus() == CriAtomExPlayer.Status.Playing)
            {

                this.transform.localPosition = Vector3.Lerp(startPos, endPos, Mathf.Abs( Mathf.Sin(atomSource.player.GetTime()* 100f)));
            }
            else
            {
                this.transform.localPosition = startPos;
            }
        }
    }
}
