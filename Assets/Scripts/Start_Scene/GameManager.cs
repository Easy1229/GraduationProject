using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Sequence = DG.Tweening.Sequence;
using UnityEngine.SceneManagement;

namespace Start_Scene
{
    public class GameManager : MonoBehaviour
    {
        public Slider slider;
        public TextMeshProUGUI textMeshProUGUI;
        private Vector3 _targetPos;
        public GameObject environment;
        public GameObject uiOfGame;
        private void Start()
        {
            slider.gameObject.SetActive(false);
            _targetPos = slider.transform.position;
            textMeshProUGUI.gameObject.SetActive(false);
        }

        public void OnButtonEvent()
        {
            //隐藏无关资源
            environment.SetActive(false);
            slider.gameObject.SetActive(true);
            uiOfGame.SetActive(false);
            //使用DOTween调出进度条
            Sequence sequence = DOTween.Sequence();
            slider.transform.position += new Vector3(0, -1000, 0);
            sequence.Append(slider.transform.DOMove(_targetPos, 0.8f));
            sequence.AppendCallback((() => { StartCoroutine(EnterGame()); }));
        }

        public void QuitGame()
        {
            Application.Quit();
        }
        IEnumerator EnterGame()
        {
            DOTween.PauseAll();
            yield return new WaitForSeconds(0.5f);
            var operation = SceneManager.LoadSceneAsync(2);
            operation.allowSceneActivation = false;
            
            //异步加载场景
            while (!operation.isDone)
            {
                slider.value = operation.progress;
                if (Math.Abs(operation.progress - 0.9) >= 0)
                {
                    slider.value = 1;
                    textMeshProUGUI.gameObject.SetActive(true);
                    textMeshProUGUI.text = "按任意按键进入游戏";

                    if (Input.anyKey)
                    {
                        DOTween.PauseAll();
                        operation.allowSceneActivation = true;
                    }
                }
                yield return null;
            }
        }
    }
}
