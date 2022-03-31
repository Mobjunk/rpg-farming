using UnityEngine;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{

    [SerializeField] private VideoPlayer _videoPlayer;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.loopPointReached += EndReached;
    }

    private void EndReached(VideoPlayer pSource)
    {
        Utility.AddSceneIfNotLoaded("Main");
        gameObject.SetActive(false);
    }
}
