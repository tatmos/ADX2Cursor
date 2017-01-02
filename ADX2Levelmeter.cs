using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX2Levelmeter : MonoBehaviour
{
    private Color m_color = Color.black;
    private MeshRenderer[] m_meshRenderers = null;

    public Vector3 sunrise = new Vector3(0, 1, 10);
    public Vector3 sunset = new Vector3(0, 0, 10);
    public float journeyTime = 1.0F;
    float lastLevel;

    void Start()
    {
        lastLevel = 1.0f;
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
        CriAtomExAsr.BusAnalyzerInfo lBusInfo = CriAtom.GetBusAnalyzerInfo(0); 

        lastLevel = Mathf.Lerp(lastLevel, lBusInfo.rmsLevels[1], 0.27f);

        SetColor(new Color(0, lastLevel * 0.5f + 0.5f, 0));
        this.gameObject.transform.localScale = new Vector3(10, 5f+lastLevel * 10f, 0.1f);
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
