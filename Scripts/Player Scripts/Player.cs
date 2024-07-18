using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    private const string GROUND_LAYER_NAME = "Ground_";
    private const string OBSTACLE_NAME = "Obstacle_";

    public event EventHandler OnJump;
    public event EventHandler OnObjectHit;

    [SerializeField] private float jumpForce = 0.1f;
    [SerializeField] private GameInput gameInput;

    private Rigidbody2D rb;
    private bool isOnGround = false;
    private bool isFalling = false;
    private bool isJumping = false;
    private bool jumpRequest = false;
    private bool obstacleHit = false;

    private void Awake() {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        gameInput.OnJumpAction += GameInput_OnJumpAction;
    }

    private void OnDisable() {
        gameInput.OnJumpAction -= GameInput_OnJumpAction;
    }

    private void Update() {
        CheckIfFalling();
    }

    private void FixedUpdate() {
        HandleJump();
    }

    private void GameInput_OnJumpAction(object sender, System.EventArgs e) {
        if (isOnGround && !GameManager.Instance.IsGameOver()) {
            jumpRequest = true;
        }
    }

    private void HandleJump() {
        if (isOnGround && jumpRequest) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            isJumping = true;
            jumpRequest = false;
            isOnGround = false;

            OnJump?.Invoke(this, EventArgs.Empty);
        }
    }

    private void CheckIfFalling() {
        if (rb.velocity.y <= 0 && !isOnGround) {
            isFalling = true;
            isJumping = false;
        } else {
            isFalling = false;
        }
    }

    public void GameOverJump() {
        if (obstacleHit) {
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.name.StartsWith(GROUND_LAYER_NAME)) {
            isOnGround = true;
            isFalling = false;
            isJumping = false;

        } else {
            isOnGround = false;
        }

        if (collision.collider.name.StartsWith(OBSTACLE_NAME)) {
            obstacleHit = true;
            OnObjectHit?.Invoke(this, EventArgs.Empty);
        }

    }

    public bool IsOnGround() {
        return isOnGround;
    }

    public bool IsFalling() {
        return isFalling;
    }

    public bool IsJumping() {
        return isJumping;
    }

    public bool IsObstacleHit() {
        return obstacleHit;
    }

}
