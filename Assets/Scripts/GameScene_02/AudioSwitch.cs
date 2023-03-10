using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitch : MonoBehaviour
{
   private AudioSource _audioSource;
   public AudioClip audioClip;
   public static AudioSwitch Instance;
   private void Awake()
   {
      Instance = this;
      _audioSource = GetComponent<AudioSource>();
   }

   public void SwitchMusic()
   {
      _audioSource.clip = audioClip;
      _audioSource.Play();
   }
}
