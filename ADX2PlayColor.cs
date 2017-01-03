using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX2PlayColor : MonoBehaviour
{

    CriAtomSource atomSource;
    private Color m_color = Color.white;
    private Color m_playColor = Color.red;
    private MeshRenderer[] m_meshRenderers = null;

    public void SetColors(Color color, Color color2)
    {
        m_color = color;
        m_playColor = color2;
    }

    void Start()
    {
        atomSource = this.gameObject.GetComponent<CriAtomSource>();
        
        m_meshRenderers = this.GetComponentsInChildren<MeshRenderer>();
        SetColor(m_color);
    }

    void Update()
    {
        if (atomSource != null)
        {
            if (atomSource.player.GetStatus() == CriAtomExPlayer.Status.Playing)
            {
                SetColor(m_playColor);
            }
            else
            {
                SetColor(m_color);
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
