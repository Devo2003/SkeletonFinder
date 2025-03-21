using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class TransitionScript : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    public Button continueButton;
    public Button skipButton;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd;

        if (continueButton)
        {
            continueButton.gameObject.SetActive(false);
            continueButton.onClick.AddListener(LoadGameScene);
        }

        if (skipButton)
        {
            skipButton.onClick.AddListener(SkipVideo);
        }

        void OnVideoEnd(VideoPlayer vp)
        {
            EnableContinueButton();
        }

        void SkipVideo()
        {
            videoPlayer.Stop();
            EnableContinueButton();
        }

        void EnableContinueButton()
        {
            if (continueButton)
            {
                continueButton.gameObject.SetActive(true);
            }
        }

        void LoadGameScene()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
