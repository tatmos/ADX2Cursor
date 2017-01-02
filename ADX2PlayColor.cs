using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX2PlayColor : MonoBehaviour
{

    CriAtomSource atomSource;
    private Color m_color = Color.black;
    private MeshRenderer[] m_meshRenderers = null;

    void Start()
    {
        atomSource = this.gameObject.GetComponent<CriAtomSource>();
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
        if (atomSource != null)
        {
            if (atomSource.player.GetStatus() == CriAtomExPlayer.Status.Playing)
            {
                SetColor(Color.red);
            }
            else
            {
                SetColor(Color.white);
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
