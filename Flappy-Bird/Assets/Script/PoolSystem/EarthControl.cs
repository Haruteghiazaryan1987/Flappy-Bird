using Script;
using UnityEngine;

public class EarthControl : AbstractControl {
    private float speed = 15f;
    private bool isPause;

    public override void Paused() {
        isPause = true;
    }

    void Update() {
        if (isPause) {
            return;
        }

        transform.Translate(Time.deltaTime * speed * Vector2.left);
        if (transform.localPosition.x < -90) {
            base.UnUse();
            PrefabOffScreen();
        }
    }
}
