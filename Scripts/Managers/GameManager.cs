using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    public enum State {
        WaitingKeyPress,
        GamePlaying,
        GameOver,
    }

    private State state;
    private int score;
    private int lastScoreThresholdIncrease = 0;
    private float backgroundMoveSpeedIncrease = 0.5f;
    private float middlegroundMoveSpeedIncrease = 0.5f;
    private float groundMoveSpeedIncrease = 1f;
    private float obstacleMoveSpeedIncrease = 1f;
    private bool obstacleHit = false;
    private bool isGamePause = false;

    private void Awake() {
        Instance = this;
        state = State.WaitingKeyPress;
    }

    private void OnEnable() {
        GameInput.Instance.OnJumpAction += GameInput_OnJumpAction;
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void OnDestroy() {
        GameInput.Instance.OnJumpAction -= GameInput_OnJumpAction;
        GameInput.Instance.OnPauseAction -= GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e) {
        TogglePause();
    }

    private void GameInput_OnJumpAction(object sender, System.EventArgs e) {
        if (state == State.WaitingKeyPress) {
            state = State.GamePlaying;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update() {
        switch (state) {
            case State.WaitingKeyPress:
                break;

            case State.GamePlaying:
                IncreaseDifficulty();
                obstacleHit = Player.Instance.IsObstacleHit();

                if (obstacleHit) {
                    Player.Instance.GameOverJump();
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GameOver:
                break;
        }
    }

    private void IncreaseDifficulty() {
        score = Mathf.FloorToInt(ScoreManager.Instance.GetScore());

        if (score % 100 == 0 && lastScoreThresholdIncrease != score) {
            ObjectMovementManager.Instance.SetBackgroundMoveSpeed(backgroundMoveSpeedIncrease);
            ObjectMovementManager.Instance.SetMiddlegroundMoveSpeed(middlegroundMoveSpeedIncrease);
            ObjectMovementManager.Instance.SetGroundMoveSpeed(groundMoveSpeedIncrease);
            ObjectMovementManager.Instance.SetObstacleMoveSpeed(obstacleMoveSpeedIncrease);
            lastScoreThresholdIncrease = score;
        }
    }

    public void TogglePause() {
        isGamePause = !isGamePause;

        if (isGamePause) {
            Time.timeScale = 0f;
            GameInput.Instance.playerInputActions.Player.Jump.Disable();
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        } else {
            Time.timeScale = 1f;
            GameInput.Instance.playerInputActions.Player.Jump.Enable();
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsWaitingKeyPress() {
        return state == State.WaitingKeyPress;
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public bool IsGamePaused() {
        return isGamePause;
    }
}