using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX2CursorInstBar : ADX2CursorInstBase
{
    private List<Color> m_color = new List<Color>();

    private List<MeshRenderer[]> m_meshRenderers = new List<MeshRenderer[]>();
    // Use this for initialization
    void Start()
    {

        float x = 0, y = 0, z = 0;

        List<float> barScale = new List<float> { 0 - 12, 2 - 12, 4 - 12, 5 - 12, 7 - 12, 9 - 12, 11 - 12, 0, 2, 4, 5, 7, 9, 11, 12, 14, 16, 17, 19, 21, 23 };

        int cubeNo = 0;
        foreach (float scaleNo in barScale)
        {
            //  cube
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            cube.transform.position = new Vector3(x * 1.0f + this.gameObject.transform.localPosition.x,
                y + this.gameObject.transform.localPosition.y,
                z + this.gameObject.transform.localPosition.z);
            cube.transform.localScale = new Vector3(0.09f, 0.09f, 0.09f);

            Rigidbody rb = cube.AddComponent<Rigidbody>();
            rb.useGravity = false;

            cube.transform.parent = this.gameObject.transform;
            ADX2CursorInstCube instNote = cube.AddComponent<ADX2CursorInstCube>();
            cube.AddComponent<ADX2PlayVibe>();

            instNote.SetCue("CueSheet_0", "Vibraphone");
            instNote.pitch = scaleNo * 100f;
            
            m_color.Add(Color.HSVToRGB((float)(((12+scaleNo)%12) / 12f), (0.5f * scaleNo/12) + 0.5f, 0.8f));
            m_meshRenderers.Add(cube.GetComponentsInChildren<MeshRenderer>());
            SetColor(cubeNo, m_color[cubeNo]);
            x += 0.1f;
            cubeNo++;
        }
    }

    // Update is called once per frame
    void Update()
    {


    }
    
    private void SetColor(int busNo, Color color)
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
