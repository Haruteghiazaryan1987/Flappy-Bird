using System;
using System.Collections.Generic;
using System.IO;
using Script;
using Script.Responses;
using Scripts.PoolSystem;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UiManager uiManager;
    [SerializeField] private ManagerAggregation managerAggregation;
    [SerializeField] private DreamloManager dreamloManager;

    [SerializeField] private CurrentGameScore currentGameScore;
    [SerializeField] private BirdControl birdControl;
    [SerializeField] private PoolController poolController;

    private List<LeaderboardEntry> entry;
    private Player player;
    private string playerName;
    
    public void StartGame(){
        birdControl.AddGravityScale();
        managerAggregation.PlayManagers();
    }

    public void GetPlayerName(string name){
        playerName = name;
    }

    public void GameOver(){
        int score = currentGameScore.Score;
        managerAggregation.PausedManager();
        uiManager.ShowPopUp(0, score);
        player.Score = score;
        player.Name = playerName;
        dreamloManager.AddScore(player.Name,player.Score);
        dreamloManager.LoadLeaderboard();
    }

    private void OnLeaderboardComplete(LeaderBoardResponse leaderBoard){
        entry = leaderBoard.dreamlo.leaderboard.entry;
        GetScoreText();
    }

    public void GetScoreText(){
        uiManager.InitsScoreText(entry);
    }
    
    public void SetCurrentGameScore(int score){
        uiManager.SetCurrentGameScore(score);
    }

    public void PassedAnObstacle(){
        currentGameScore.SetScore();
    }

    private void OnEnable(){
        uiManager.OnGameStarted += OnGameStarted;
        birdControl.OnCollision += GameOver;
        dreamloManager.LoadLeaderboard();
        dreamloManager.OnLeaderboard+=OnLeaderboardComplete;
    }

    private void OnGameStarted(){
        poolController.InitPool();
        managerAggregation.SetupManagers();
    }

    public void ResetGame(){

    }

    private void OnDisable(){
        uiManager.OnGameStarted -= OnGameStarted;
        birdControl.OnCollision -= GameOver;
        dreamloManager.OnLeaderboard-=OnLeaderboardComplete;
    }
}