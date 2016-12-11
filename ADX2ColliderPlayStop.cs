using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX2ColliderPlayStop : MonoBehaviour {

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
        }
        Debug.Log(other.gameObject.name);
    }

    void OnTriggerExit(Collider other) {
        CriAtomSource atomSource = other.gameObject.GetComponent<CriAtomSource>();
        if(atomSource != null)
        {
            atomSource.Stop();    
        }
        Debug.Log(other.gameObject.name);
    }
}
