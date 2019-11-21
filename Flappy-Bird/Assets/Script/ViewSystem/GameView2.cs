using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Script.ViewSystem {
    public class GameView2 : View {
        [SerializeField] private Text scoreText;

        public override void SetCurrentGameScore(int score) {
            if (score != 0) { 
                scoreText.text = $"{score}";
            }
        }
        public override void DeactivateCurrentGameScore() {
            scoreText.text = string.Empty;
        }
    }
}