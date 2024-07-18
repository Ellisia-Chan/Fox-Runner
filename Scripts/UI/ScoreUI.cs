using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI scoreText;

    private float scoreValue;
    private float fullAlpha = 1.0f;
    private float zeroAlpha = 0f;

    private void Awake() {
        scoreText.text = "0";
    }

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void OnDestroy() {
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        ShowHideScoreText();
    }

    private void Update() {
        if (GameManager.Instance.IsGamePlaying()) {
            scoreValue = ScoreManager.Instance.GetScore();
            UpdateScoreText();
        }
    }

    private void UpdateScoreText() {
        scoreText.text = Mathf.FloorToInt(scoreValue).ToString();
    }

    private void ShowHideScoreText() {
        if (GameManager.Instance.IsGamePlaying()) {
            scoreText.alpha = fullAlpha;
        } else {
            scoreText.alpha = zeroAlpha;
        }
    }
}
