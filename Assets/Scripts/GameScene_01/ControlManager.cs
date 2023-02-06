
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public GameObject ControlUI;
    private void Start()
    {
#if UNITY_IOS || UNITY_ANDROID
    ControlUI.SetActive(true);
#endif
        
#if UNITY_STANDALONE_WIN
    ControlUI.SetActive(false);    
#endif
        
#if UNITY_EDITOR
        ControlUI.SetActive(false);    
#endif
    }
}

