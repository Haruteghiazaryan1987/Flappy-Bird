using System;
using Script;
using UnityEngine;
using Random = UnityEngine.Random;

public class PipeControl : AbstractControl {
    [SerializeField] private Transform childUp;
    [SerializeField] private Transform childDown;
    private float speed = 15f;
    private bool isPause;

    private void Start() {
        PipeSpacing();
    }

    public override void EnableItem() {
        PipeSpacing();
        base.EnableItem();
    }

    private void PipeSpacing() {
        float pipeSpacing = 10 * Random.Range(3, 7);
        childDown.localPosition = new Vector2(0, +pipeSpacing / 2);
        childUp.localPosition = new Vector2(0, -pipeSpacing / 2);

    }

    public override void Paused() {
        isPause = true;
    }

    public override void Play() {
        isPause = false;
    }

    private void Update() {
        if (isPause) {
            return;
        }

        transform.Translate(Time.deltaTime * speed * Vector2.left);
        if (transform.localPosition.x < -40) {
            base.UnUse();
            PrefabOffScreen();
        }
    }
}
