using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private const string PLAYER_PREFS_HIGH_SCORE = "HighScore";

    public static ScoreManager Instance { get; private set; }

    private float score;
    private float finalScore;
    private float highScore;
    private bool isNewHighScore = false;

    private void Awake() {
        Instance = this;
        highScore = PlayerPrefs.GetFloat(PLAYER_PREFS_HIGH_SCORE);
    }

    private void Start() {
        score = 0;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void OnDestroy() {
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGameOver()) {
            CheckHighScore();
        }
    }

    private void Update() {
        if (GameManager.Instance.IsGamePlaying()) {
            score += 10 * Time.deltaTime;
            finalScore = score;
        }
    }

    private void CheckHighScore() {
        if (finalScore > highScore) {
            isNewHighScore = true;
            highScore = finalScore;
            PlayerPrefs.SetFloat(PLAYER_PREFS_HIGH_SCORE, highScore);
            PlayerPrefs.Save();
        } else {
            isNewHighScore = false;
        }
    }

    public float GetScore() {
        return score;
    }

    public float GetFinalScore() {
        return finalScore;
    }

    public bool IsNewHighScore() {
        return isNewHighScore;
    }

    public float GetHighScore() {
        return highScore;
    }
}
