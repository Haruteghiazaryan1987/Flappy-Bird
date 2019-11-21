using System;
using UnityEngine;

public class BirdControl : MonoBehaviour {
    public event Action OnCollision;
    Rigidbody2D rb;
    [SerializeField] float speed = 10;
    float gravity;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
        rb.gravityScale = 0;
    }

    public void AddGravityScale() {
        rb.gravityScale = gravity;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && rb.gravityScale != 0) {
            rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D() {
        OnCollision?.Invoke();
    }
}
