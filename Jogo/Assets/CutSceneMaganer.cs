using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class CutSceneMaganer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nomeCenaMenu = "Menu";
    public bool permitirPular = true;  

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        videoPlayer.loopPointReached += AoTerminarVideo;
    }

    void AoTerminarVideo(VideoPlayer vp)
    {
        IrParaMenu();
    }

    void IrParaMenu()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= AoTerminarVideo;
        }

        SceneManager.LoadScene(nomeCenaMenu);
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= AoTerminarVideo;
        }
    }
}
