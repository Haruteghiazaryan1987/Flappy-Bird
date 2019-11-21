using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Script.ViewSystem {
    public class MenuView2 : View {
        
        [SerializeField] private Button startButton;
        [SerializeField] private Button highScore;
        [SerializeField] private InputField playerName;
        private string plName;

//        public void GetPlayerName()
//        {
//            playerName= string.Intern(playerName.text);
//            if(playerName.Length==0)
//                playerName="Player";
//        }

        public void GetPlayerName()
        {
            plName= string.Intern(playerName.text);
            if(plName.Length==0)
                plName="Player" + Random.Range(1000, 10000);
        }
        private void OnStartButtonClick() {
            uiManager.OpenGameView();
            GetPlayerName();
            uiManager.GetPlayerName(plName);
        }

        private void OnHighScoreButtoClick() {
            uiManager.OpenHighScoreView();
        }

        protected override void unSubscribeEvents() {
            startButton.onClick.RemoveListener(OnStartButtonClick);
            highScore.onClick.RemoveListener(OnHighScoreButtoClick);
        }
        protected override void subscribeEvents() {
            startButton.onClick.AddListener(OnStartButtonClick);            
            highScore.onClick.AddListener(OnHighScoreButtoClick);
        }
    }
}