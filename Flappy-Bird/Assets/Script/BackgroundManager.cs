using System;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class BackgroundManager : AbstractManager {
    public event Action<object> GoOnInits1;
    private BackgroundControl bgControl;
    private int scalePref = 209;
    private Vector3 pos;
    private int activePrefabCountbg = 2;
    private List<AbstractControl> backGroundList;
    [SerializeField] private Transform gameView;
    [SerializeField] private SelectionOfObjects selObject;


    public override void Setup() {
        backGroundList = new List<AbstractControl>();
        for (int i = 0; i < activePrefabCountbg; i++) {
            if (i == 0) {
                pos = new Vector2(gameView.localPosition.x, gameView.position.y);
            }
            else {
                pos = new Vector2(gameView.localPosition.x + scalePref, gameView.position.y);
            }

            bgControl = PoolController.Use(ControlPrefab, pos) as BackgroundControl;
            bgControl.PipeOffScreen += OnBackgroundOffScreen;
            backGroundList.Add(bgControl);

            GoOnInits1?.Invoke(bgControl);

        }
    }

    public override void Paused() {
        base.Paused();
        StopPrefab(backGroundList);
    }
    private void OnBackgroundOffScreen() {
        ReusePrefab();
    }

    public override void ReusePrefab() {
        AbstractControl lastBgControl = backGroundList[backGroundList.Count - 1];
        Vector3 pos = new Vector2(lastBgControl.transform.localPosition.x + scalePref,
            lastBgControl.transform.localPosition.y);
        AbstractControl go = PoolController.Use(ControlPrefab, pos) as BackgroundControl;
        backGroundList.Add(go);
        backGroundList.Remove(backGroundList[backGroundList.Count - 1]);
    }
}