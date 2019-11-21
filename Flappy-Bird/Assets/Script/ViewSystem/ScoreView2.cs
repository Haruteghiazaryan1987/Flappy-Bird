using System.Collections.Generic;
using Script.Responses;
using UnityEngine;
using UnityEngine.UI;

namespace Script.ViewSystem
{
    public class ScoreView2:View
    {
        [SerializeField] private Button menu;
        [SerializeField] private Text highScoreText;
        [SerializeField] private RectTransform textPrefab;
        [SerializeField] private RectTransform content;

        public void OnMenuButtonCkick() {
            uiManager.OpenMenuView();
        }
    public override void InitsScoreText(List<LeaderboardEntry> entry)
    {
        if (content.childCount != 0)
        {
            for (int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }
        }
        if(entry==null)
            return;
        for (int i = entry.Count-1 ; i >-1; i--)
        {
            LeaderboardEntry pl = entry[i];
            var obj = Instantiate(textPrefab, content);
            obj.GetComponent<LiderBoardItem>().Setup(pl);
        }
    }

        protected override void subscribeEvents() {
            menu.onClick.AddListener(OnMenuButtonCkick);
        }

        protected override void unSubscribeEvents() {
            menu.onClick.RemoveListener(OnMenuButtonCkick);
        }
    }
}