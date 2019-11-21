using System;
using System.Collections.Generic;
using Script.Responses;
using UnityEngine;
using UnityEngine.UI;

namespace Script.ViewSystem {
    public abstract class View : MonoBehaviour {
        protected UiManager uiManager;
        public event Action OnViewShow;
        public event Action OnViewHide;

        public void Setup(UiManager uiManager) {
            this.uiManager = uiManager;
        }
        protected virtual void subscribeEvents() {
        }

        protected virtual void unSubscribeEvents() {
        }
        protected virtual void OnEnable() {
            subscribeEvents();
        }
        protected virtual void OnDisable() {
            unSubscribeEvents();
        }

        public void ShowView() {
            OnViewShow?.Invoke();
            gameObject.SetActive(true);
        }

        public void HideView() {
            OnViewHide?.Invoke();
            gameObject.SetActive(false);
        }

        public virtual void SetCurrentGameScore(int score) {
        }

        public virtual void DeactivateCurrentGameScore() {
        }
        public virtual void InitsScoreText(List<LeaderboardEntry> entry) {
        }
    }
}