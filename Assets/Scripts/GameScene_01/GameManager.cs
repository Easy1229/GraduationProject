using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScene_01
{
    public class GameManager : MonoBehaviour
    {
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
        //退出游戏
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
