using System.Collections.Generic;
using Script;
using UnityEngine;

public class EarthManager : AbstractManager {
    private EarthControl earthControl;
    [SerializeField] private Transform gameView;
    private int scalePref = 110;
    private Vector3 pos;
    private int activePrefabCountbg = 2;
    private List<AbstractControl> earthList;
    private AbstractManager abstractManager;

    public override void Setup() {
        earthList = new List<AbstractControl>();
        for (int i = 0; i < activePrefabCountbg; i++) {

            if (i == 0) {
                pos = new Vector2(gameView.localPosition.x, gameView.localPosition.y - 33);
            }
            else {
                pos = new Vector2(gameView.localPosition.x + scalePref, gameView.localPosition.y - 33);
            }

            earthControl = PoolController.Use(ControlPrefab, pos) as EarthControl;
            earthControl.PipeOffScreen += OnEarthOffScreen;
            earthList.Add(earthControl);
        }
    }
    public override void Paused() {
        base.Paused();
        StopPrefab(earthList);
    }
    private void OnEarthOffScreen() {
        ReusePrefab();
    }
    public override void ReusePrefab() {
        AbstractControl lastearth = earthList[earthList.Count - 1];
        Vector3 pos = new Vector2(lastearth.transform.localPosition.x + scalePref,
            lastearth.transform.localPosition.y);
        AbstractControl go = PoolController.Use(ControlPrefab, pos) as EarthControl;
        earthList.Add(go);
        earthList.Remove(earthList[earthList.Count - 1]);
    }
}
