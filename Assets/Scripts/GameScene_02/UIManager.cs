using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameScene_02
{
    public class UIManager : MonoBehaviour
    {
        public GameObject PlayerDieUI;
        public GameObject ControlUI;
        public UnityEvent PlayerDieAudio;
        public static UIManager Instance;
        //游戏音量调节
        public Slider GameAudioSlider;
        public AudioSource BGMSource;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            PlayerDieUI.SetActive(false);
        }

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

        private void GameAudioControl()
        {
            BGMSource.volume = GameAudioSlider.value;
        }
        public void PlayerDie()
        {
            PlayerDieAudio?.Invoke();
            Time.timeScale = 0;
            PlayerDieUI.SetActive(true);
        }
    }
}