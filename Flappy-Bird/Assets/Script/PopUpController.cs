using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour {
    [SerializeField] private Button touchToPlay;
    [SerializeField] private Button menu;
    [SerializeField] private GameObject popUp;
    [SerializeField] private Text highScore;
    [SerializeField] private Text currentGameScore;

    public void Show(Action OnClickTouchToPlay,Action OnClickMenu, int highScore, int currentGameScore) {
       
        if (currentGameScore == 0) {
            this.highScore.text =string.Empty;
            this.currentGameScore.text = string.Empty;
        }
        else
        {
            this.highScore.text = "HIGHSCORES: " + $"{highScore}";
            this.currentGameScore.text = "YOUR SCORE: " + $"{currentGameScore}";
        }

        popUp.SetActive(true);
        touchToPlay.onClick.AddListener((() => {
            OnClickTouchToPlay?.Invoke();
            Close();
        }));
        menu.onClick.AddListener((() => {
            OnClickMenu?.Invoke();
            Close();
        }));
    }

    private void Close() {
        popUp.SetActive(false);
    }
}