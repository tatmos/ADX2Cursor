using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  コライダーに当たった時に相手のオブジェクトのAtomSourceを鳴らすだけ。
public class ADX2CursorColliderPlayStop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other) {
        CriAtomSource atomSource = other.gameObject.GetComponent<CriAtomSource>();
        if(atomSource != null)
        {
            atomSource.Play();    
            Debug.Log("Play \"" + atomSource.cueName + "\"");
        }

    }

    void OnTriggerExit(Collider other) {
        CriAtomSource atomSource = other.gameObject.GetComponent<CriAtomSource>();
        if(atomSource != null)
        {
            atomSource.Stop();    
            Debug.Log("Stop \"" + atomSource.cueName + "\"");
        }
    }
}
