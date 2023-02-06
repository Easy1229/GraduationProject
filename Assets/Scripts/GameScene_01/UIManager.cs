using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace GameScene_01
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        //获得遮罩图片
        public GameObject maskImage;
        //获得提示面板
        public GameObject tipPanel;
        //提示框文字
        public TextMeshProUGUI tipWords;
        //对话框UI
        public GameObject talkPanel;
        //对话框文字
        public TextMeshProUGUI talkWords;
        //对话列表
        public List<string> talkWordsList = new List<string>();
        //对话框图像
        public Image playImage;
        public Image npc01Image;
        public Image npc02Image;
        //成就面板
        public GameObject achievementPanel;
        //游戏相关UI
        public GameObject UIOfGame;
        private int index = 0;
        //玩家死亡UI
        public GameObject playerDie;
        //音量调节
        public Slider GameAudioSlider;
        public AudioSource bgmDefault;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            playerDie.gameObject.SetActive(false);
            
            UIOfGame.SetActive(false);
        }

        private void Start()
        {
            MaskControl();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TalkWithNpc01();
            }
            GameAudioControl();
        }
        //游戏音量调节
        private void GameAudioControl()
        {
            bgmDefault.volume = GameAudioSlider.value;
        }
        //幕布控制
        private void MaskControl()
        {
            maskImage.SetActive(true);
            Sequence sequence = DOTween.Sequence();
            //露出全屏
            sequence.Append(maskImage.transform.DOScale(new Vector3(13, 13, 13), 6f));
            //隐藏自身
            sequence.AppendCallback(() =>
            {
                maskImage.SetActive(false);
                UIOfGame.SetActive(true);
            });
        }
        //对话面板显示
        public void TalkPanelShow()
        {
            talkPanel.transform.DOLocalMove(new Vector3(0, 540, 0), 1.5f);
        }
        //和NPC01对话，代入剧情
        private void TalkWithNpc01()
        {
            //对话结束
            if (index == talkWordsList.Count)
            {
                talkPanel.transform.DOLocalMove(new Vector3(0, 780, 0), 1.5f);
                talkWords.text = null;
                return;
            }
            //对话框人物照片显示与隐藏
            if (index%2!= 0)
            {
                playImage.gameObject.SetActive(true);
                npc01Image.gameObject.SetActive(false);
            }else if (index % 2 == 0)
            {
                playImage.gameObject.SetActive(false);
                npc01Image.gameObject.SetActive(true);
            }
            //加载对话内容
            talkWords.text = talkWordsList[index];
            index++;
        }
        //和NPC02进行对话，获得第一个武器
        public void TalkWithNpc02()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(talkPanel.transform.DOLocalMove(new Vector3(0, 540, 0), 1.5f));
            sequence.AppendInterval(3f);
            sequence.Append(talkPanel.transform.DOLocalMove(new Vector3(0, 780, 0), 1.5f));
            talkWords.text = "我为你打造了武器，带上它吧";
            playImage.gameObject.SetActive(false);
            npc01Image.gameObject.SetActive(false);
            npc02Image.gameObject.SetActive(true);
        }
        //显示成就面板
        public void GetWeapon()
        {
            achievementPanel.SetActive(true);
        }
        //玩家死亡
        public void PlayerDefeat()
        {
            playerDie.gameObject.SetActive(true);
        }
    }
}
