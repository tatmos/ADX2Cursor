using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX2CursorOculusTouch : MonoBehaviour
{

    public int handId = 0;  //  Left = 0,Right = 1

    private Color m_color = Color.black;
    private MeshRenderer[] m_meshRenderers = null;
    
    public OVRHapticsClip hapticClipHi;
    public OVRHapticsClip hapticClipLow;

    void Start()
    {
        //  振動用クリップ
        byte[] samples = new byte[8];
        for (int i = 0; i < samples.Length; i++)
        {
            samples[i] = 128;
        }
        hapticClipHi = new OVRHapticsClip(samples, samples.Length);
        for (int i = 0; i < samples.Length; i++)
        {
            samples[i] = 64;
        }
        hapticClipLow = new OVRHapticsClip(samples, samples.Length);

        //hapticClipHi = new OVRHapticsClip(audioClipHi);  
        //hapticClipLow = new OVRHapticsClip(audioClipLow);  //  振動用クリップ

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
        if (handId == 0 && OVRInput.Get(OVRInput.RawTouch.LIndexTrigger))
        {
            SetColor(Color.red);
        }
        else if (handId == 0)
        {
            SetColor(Color.white);
        }

        if (handId == 1 && OVRInput.Get(OVRInput.RawTouch.RIndexTrigger))
        {
            SetColor(Color.red);
        }
        else if (handId == 1)
        {
            SetColor(Color.white);
        }
    }

    bool[] rawTouchTriggered = { false, false };

    void OnTriggerEnter(Collider other)
    {
        ADX2CursorInstBase adx2CursorTouchSource = other.gameObject.GetComponent<ADX2CursorInstBase>();
        if (adx2CursorTouchSource != null)
        {
            if (
                (handId == 0 && OVRInput.Get(OVRInput.RawTouch.LIndexTrigger)) //  右手でトリガーに触れている

                )
            {
                //if (rawTouchTriggered[handId] == false)
                {
                    adx2CursorTouchSource.RawTouchTrigger(true);
                    //Debug.Log("Play \"" + adx2CursorTouchSource.atomSource.cueName + "\"");
                    OVRHaptics.LeftChannel.Mix(this.hapticClipHi);
                    rawTouchTriggered[handId] = true;
                }
            }
            else if (handId == 0)
            {
                //if (rawTouchTriggered[handId])
                {
                    adx2CursorTouchSource.RawTouchTrigger(false);
                    //Debug.Log("Stop \"" + adx2CursorTouchSource.atomSource.cueName + "\"");
                    OVRHaptics.LeftChannel.Mix(this.hapticClipLow);
                    rawTouchTriggered[handId] = false;
                }
            }

            if ((handId == 1 && OVRInput.Get(OVRInput.RawTouch.RIndexTrigger))  //  左手でトリガーに触れている
                )
            {
                //if (rawTouchTriggered[handId] == false)
                {
                    adx2CursorTouchSource.RawTouchTrigger(true);
                    //Debug.Log("Play \"" + atomSource.cueName + "\"");
                    OVRHaptics.RightChannel.Mix(this.hapticClipHi);
                    rawTouchTriggered[handId] = true;
                }
            }
            else if (handId == 1)
            {
                //if (rawTouchTriggered[handId])
                {
                    adx2CursorTouchSource.RawTouchTrigger(false);
                    //Debug.Log("Stop \"" + atomSource.cueName + "\"");
                    OVRHaptics.RightChannel.Mix(this.hapticClipLow);
                    rawTouchTriggered[handId] = false;
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        ADX2CursorInstBase adx2CursorTouchSource = other.gameObject.GetComponent<ADX2CursorInstBase>();
        if (adx2CursorTouchSource != null)
        {
            if (
                (handId == 0 && OVRInput.Get(OVRInput.RawTouch.LIndexTrigger)) //  右手でトリガーに触れている

                )
            {
                if (rawTouchTriggered[handId] == false)
                {
                    adx2CursorTouchSource.RawTouchTrigger(true);
                    //Debug.Log("Play \"" + adx2CursorTouchSource.atomSource.cueName + "\"");
                    OVRHaptics.LeftChannel.Mix(this.hapticClipHi);
                    rawTouchTriggered[handId] = true;
                }
            }
            else if (handId == 0)
            {
                if (rawTouchTriggered[handId])
                {
                    adx2CursorTouchSource.RawTouchTrigger(false);
                    //Debug.Log("Stop \"" + adx2CursorTouchSource.atomSource.cueName + "\"");
                    OVRHaptics.LeftChannel.Mix(this.hapticClipLow);
                    rawTouchTriggered[handId] = false;
                }
            }

            if ((handId == 1 && OVRInput.Get(OVRInput.RawTouch.RIndexTrigger))  //  左手でトリガーに触れている
                )
            {
                if (rawTouchTriggered[handId] == false)
                {
                    adx2CursorTouchSource.RawTouchTrigger(true);
                    //Debug.Log("Play \"" + atomSource.cueName + "\"");
                    OVRHaptics.RightChannel.Mix(this.hapticClipHi);
                    rawTouchTriggered[handId] = true;
                }
            }
            else if (handId == 1)
            {
                if (rawTouchTriggered[handId])
                {
                    adx2CursorTouchSource.RawTouchTrigger(false);
                    //Debug.Log("Stop \"" + atomSource.cueName + "\"");
                    OVRHaptics.RightChannel.Mix(this.hapticClipLow);
                    rawTouchTriggered[handId] = false;
                }
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
