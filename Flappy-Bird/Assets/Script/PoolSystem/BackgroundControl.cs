using Script;
using UnityEngine;

public class BackgroundControl : AbstractControl {
    private float speed = 12f;
    private bool isPause;

    public override void Paused() {
        isPause = true;
    }

    void Update() {
        if (isPause) {
            return;
        }

        transform.Translate(Time.deltaTime * speed * Vector2.left);
        if (transform.localPosition.x < -140) {
            base.UnUse();
            PrefabOffScreen();
        }
    }
}
