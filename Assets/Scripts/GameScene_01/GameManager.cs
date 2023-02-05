using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace GameScene_01
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public UnityEvent DiedAudio;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        //游戏暂停
        public void GamePause()
        {
            Time.timeScale = 0;
        }
        //游戏继续
        public void GameContinue()
        {
            Time.timeScale = 1;
        }
        //重新开始
        public void GameReStart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
        //玩家死亡>>>>复活
        public void PlayerDefeat()
        {
            UIManager.Instance.PlayerDefeat();
            DiedAudio?.Invoke();
        }
        //退出游戏
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
