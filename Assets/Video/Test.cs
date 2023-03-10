using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class Test : MonoBehaviour
{
    public VideoPlayer VideoPlayer;

    private void Start()
    {
        StartCoroutine(VideoPause());
    }

    IEnumerator VideoPause()
    {
        yield return new WaitForSeconds(46);
        VideoPlayer.Pause();
        SceneManager.LoadScene(1);
    }
}
