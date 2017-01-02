using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX2ColliderPlayStop_Hand : MonoBehaviour
{

    public int handId = 0;  //  Left = 0,Right = 1

    private Color m_color = Color.black;
    private MeshRenderer[] m_meshRenderers = null;

    void Start()
    {
        m_color = new Color(
              UnityEngine.Random.Range(0.1f, 0.95f),
              UnityEngine.Random.Range(0.1f, 0.95f),
              UnityEngine.Random.Range(0.1f, 0.95f),
              1.0f
          );
        m_meshRenderers = this.GetComponentsInChildren<MeshRenderer>();
        SetColor(m_color);
    }

    void Update()
    {
        if (handId == 0 && !OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
        {
            SetColor(Color.red);
        }
        else if (handId == 0)
        {
            SetColor(Color.white);
        }

        if (handId == 1 && !OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            SetColor(Color.red);
        }
        else if (handId == 1)
        {
            SetColor(Color.white);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        CriAtomSource atomSource = other.gameObject.GetComponent<CriAtomSource>();
        if (atomSource != null)
        {
            if (handId == 0 && !OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
            {
                atomSource.Play();
                //Debug.Log("Play \"" + atomSource.cueName + "\"");
            }
            else if (handId == 0)
            {
                atomSource.Stop();
                //Debug.Log("Stop \"" + atomSource.cueName + "\"");
            }
            
            if (handId == 1 && !OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
            {
                atomSource.Play();
                //Debug.Log("Play \"" + atomSource.cueName + "\"");
            }
            else if (handId == 1)
            {
                atomSource.Stop();
                //Debug.Log("Stop \"" + atomSource.cueName + "\"");
            }
        }

    }
    
    private void SetColor(Color color)
    {
        for (int i = 0; i < m_meshRenderers.Length; ++i)
        {
            MeshRenderer meshRenderer = m_meshRenderers[i];
            for (int j = 0; j < meshRenderer.materials.Length; ++j)
            {
                Material meshMaterial = meshRenderer.materials[j];
                meshMaterial.color = color;
            }
        }
    }
}
