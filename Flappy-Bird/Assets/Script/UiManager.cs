using System;
using System.Collections.Generic;
using Script;
using Script.Responses;
using Script.ViewSystem;
using Scripts.PoolSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class UiManager : MonoBehaviour {
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PopUpController popUpController;

    [Space(50), Header("New View System")] 
    
    [SerializeField] private View menuView2;
    [SerializeField] private View gameView2;
    [SerializeField] private View scoreView2;

    private View activeView;

    public event Action OnClickTouchToPlay;
    public event Action OnClickMenu;
    public event Action OnGameStarted;

    private void Awake() {
        initViews();
        ShowView(menuView2);
    }
    private void initViews() {
        gameView2.OnViewShow += OnGameViewShow;
        menuView2.Setup(this);
        gameView2.Setup(this);
        scoreView2.Setup(this);
    }
    private void OnGameViewShow() {
        OnGameStarted?.Invoke();
    }

    private void ShowView(View view) {
        if (activeView != null) {
            activeView.HideView();
        }

        activeView = view;
        activeView.ShowView();
    }
    public void OpenMenuView() {
        ShowView(menuView2);
    }
    public void OpenGameView() {
        ShowView(gameView2);
        ShowPopUp(0,0);
    }

    public void GetPlayerName(string name)
    {
        gameManager.GetPlayerName(name);
    }
    public void ShowPopUp(int highScore,int courrentGameScore) {
        popUpController.Show(OnClickTouchToPlay,OnClickMenu, highScore, courrentGameScore);
        gameView2.DeactivateCurrentGameScore();
    }

    public void InitsScoreText(List<LeaderboardEntry> entry){
        scoreView2.InitsScoreText(entry);
    }

    public void OpenHighScoreView() {
        gameManager.GetScoreText();
        ShowView(scoreView2);
    }

    public void SetCurrentGameScore(int score) {
        gameView2.SetCurrentGameScore(score);
    }

    private void OnEnable() {
        OnClickTouchToPlay += gameManager.StartGame;
        OnClickMenu += OpenMenuView;
    }
    
    private void OnDisable() {
        OnClickTouchToPlay -= gameManager.StartGame;
        OnClickMenu -= OpenMenuView;
    }

}