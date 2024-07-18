using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    private const string IS_ON_GROUND = "isOnGround";
    private const string IS_GAME_PLAYING = "isGamePlaying";
    private const string IS_GAME_OVER = "isGameOver";
    private const string IS_FALLING = "isFalling";
    private const string IS_JUMPING = "isJumping";

    [SerializeField] Player player;

    private Animator animator;
    private bool isOnGround;
    private bool isGamePlaying;
    private bool isGameOver;
    private bool isFalling;
    private bool isJumping;

    private void Awake() {
        animator = GetComponent<Animator>();
        animator.SetBool(IS_ON_GROUND, player.IsOnGround());    
    }

    private void Update() {
        UpdateAnimatorParameters();
    }

    private void UpdateAnimatorParameters() {
        isGamePlaying = GameManager.Instance.IsGamePlaying();
        isGameOver = GameManager.Instance.IsGameOver();

        if (isGamePlaying) {
            isOnGround = player.IsOnGround();
            isFalling = player.IsFalling();
            isJumping = player.IsJumping();

            animator.SetBool(IS_GAME_PLAYING, isGamePlaying);
            animator.SetBool(IS_ON_GROUND, isOnGround);
            animator.SetBool(IS_FALLING, isFalling);
            animator.SetBool(IS_JUMPING, isJumping);
        }

        if (isGameOver) {
            animator.SetBool(IS_GAME_PLAYING, isGamePlaying);
            animator.SetBool(IS_GAME_OVER, isGameOver);

        }
    }
}
