using UnityEngine;

public class CurrentGameScore : MonoBehaviour {
    [SerializeField] private GameManager gameManager;
    private int score = 0;

    private void Awake() {
        gameManager.SetCurrentGameScore(score);
    }

    public void SetScore() {
        score += 1;
        gameManager.SetCurrentGameScore(score);
    }

    public int Score {
        get => score;
        set => score = value;
    }
}
