using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX2Levelmeter : MonoBehaviour
{
    public int maxBusNum = 7;
    private List<Color> m_color = new List<Color>();
    private List<MeshRenderer[]> m_meshRenderers = new List<MeshRenderer[]>();

    List<float> lastLevel = new List<float>();

    List<GameObject> cubes = new List<GameObject>();

    void Start()
    {
        for (int busNo = 0; busNo < maxBusNum; busNo++)
        {
            //  cube
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.parent = this.gameObject.transform;
            cube.transform.position = new Vector3(busNo * 0.2f + this.gameObject.transform.localPosition.x,
                0 + this.gameObject.transform.localPosition.y,
                0 + this.gameObject.transform.localPosition.z);
            cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            cubes.Add(cube);

            lastLevel.Add(1.0f);

            m_color.Add(Color.HSVToRGB((float)busNo/7f, 0.5f, 0.8f));
            m_meshRenderers.Add(cube.GetComponentsInChildren<MeshRenderer>());
            SetColor(busNo,m_color[busNo]);
        }
    }

    void Update()
    {
        for (int busNo = 0; busNo < maxBusNum; busNo++)
        {
            CriAtomExAsr.BusAnalyzerInfo lBusInfo = CriAtom.GetBusAnalyzerInfo(busNo);

            lastLevel[busNo] = Mathf.Lerp(lastLevel[busNo], lBusInfo.rmsLevels[0], 0.27f);

            SetColor(busNo, Color.Lerp(m_color[busNo], Color.white, lastLevel[busNo]));// Mathf.PingPong(Time.time, lastLevel[busNo])));
            cubes[busNo].transform.localScale = new Vector3(0.1f, 1f + lastLevel[busNo] * 10f, 1f);
        }
    }
    
    private void SetColor(int busNo,Color color)
    {
        for (int i = 0; i < m_meshRenderers[busNo].Length; ++i)
        {
            MeshRenderer meshRenderer = m_meshRenderers[busNo][i];
            for (int j = 0; j < meshRenderer.materials.Length; ++j)
            {
                Material meshMaterial = meshRenderer.materials[j];
                meshMaterial.color = color;
            }
        }
    }

}
