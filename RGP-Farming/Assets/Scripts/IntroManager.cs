using UnityEngine;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{

    [SerializeField] private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;
    }

    private void EndReached(VideoPlayer source)
    {
        Utility.AddSceneIfNotLoaded("Main");
        gameObject.SetActive(false);
    }
}
