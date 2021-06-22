using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using TextSpeech;


public class VoiceController : MonoBehaviour
{
    public string locale="en-US";
    public float pitch=1;
    public float rate=1;
    void Start()
    {
        
        OnClickApply();

        TextToSpeech.instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.instance.onDoneCallback = OnSpeakStop;
    }

    #region Text to Speech

    public void StartSpeaking(Text message)
    {
        TextToSpeech.instance.StartSpeak(message.text);
    }

    public void StopSpeaking()
    {
        TextToSpeech.instance.StopSpeak();
    }

    void OnSpeakStart()
    {
        Debug.Log("Talking started...");
    }

    void OnSpeakStop()
    {
        Debug.Log("Talking stopped...");
    }

    public void SetCode(string l)
    {
        locale=l;
        OnClickApply();
        Debug.Log(l);
    }
    public void SetPitch(float p)
    {
        pitch=p;
        OnClickApply();
        
    }
    public void SetRate(float r)
    {
        rate=r;
        OnClickApply();
    }


    #endregion


    void Setting(string code, float p, float r)
    {
        TextToSpeech.instance.Setting(code, p, r);
    }
    public void OnClickApply()
    {
        Setting(locale, pitch, rate);
    }
    
    public void DropDownCheck(int value)
    {
        if (value == 0)
        {
            SetCode("en-US");
        }

        if (value == 1)
        {
            SetCode("en-GB");
        }

        if (value == 2)
        {
            SetCode("de-DE");
        }

        if (value == 3)
        {
            SetCode("ko-KR");
        }

        if (value == 4)
        {
            SetCode("fr-FR");
        }

        if (value == 5)
        {
            SetCode("vi-VN");
        }
    }
}
