using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRootMove : MonoBehaviour
{

    public Transform targetTransform;

    Vector3 startPos = Vector3.zero;

    // Use this for initialization
    void Start()
    {

    }

    bool tumbstickDown = false;
    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.RThumbstick))
        {
            if (!tumbstickDown)
            {
                //  ドラッグ開始　位置
                startPos = this.targetTransform.position;
            }

            this.gameObject.transform.Translate(startPos - targetTransform.position);
            tumbstickDown = true;

            //  ドラッグ中に表示されるおまけCube
            //  cube
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            cube.transform.position = targetTransform.position;

            //Rigidbody rb = cube.AddComponent<Rigidbody>();
            
            cube.transform.parent = this.gameObject.transform;
            ADX2PlayColor adx2PlayColor = cube.AddComponent<ADX2PlayColor>();
            adx2PlayColor.SetColors(Color.white, Color.red);

            adx2PlayColor.SetColors(Color.HSVToRGB((float)(Time.time%10)/10.0f, 0.95f, 0.95f), Color.blue);

            Destroy(cube.gameObject, 2f);
        }
        else
        {
            if (tumbstickDown)
            {
                tumbstickDown = false;
            }
        }

    }
}
