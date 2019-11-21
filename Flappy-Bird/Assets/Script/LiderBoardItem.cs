using Script.Responses;
using UnityEngine;
using UnityEngine.UI;

public class LiderBoardItem : MonoBehaviour
{
    [SerializeField] private Text name;
    [SerializeField] private Text score;

    public void Setup(LeaderboardEntry entry){
        name.text = entry.name;
        score.text = $"{entry.score}";
    }
}
