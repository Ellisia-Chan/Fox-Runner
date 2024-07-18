using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameWaitngUI : MonoBehaviour {
    [SerializeField] private GameObject gameWaitingUI;
    [SerializeField] private TextMeshProUGUI highScoreValueText;

    private void Start() {
        ShowHighScore();
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void OnDestroy() {
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsWaitingKeyPress()) {
            Show();
        } else {
            Hide();
        }
    }

    private void ShowHighScore() {
        if (GameManager.Instance.IsWaitingKeyPress()) {
            highScoreValueText.text = Mathf.FloorToInt(ScoreManager.Instance.GetHighScore()).ToString();
        }
    }

    private void Show() {
        gameWaitingUI.SetActive(true);
    }

    private void Hide() {
        gameWaitingUI?.SetActive(false);
    }
}
