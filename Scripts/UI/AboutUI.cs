using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AboutUI : MonoBehaviour {

    public static AboutUI Instance { get; private set; }

    private const string GITHUB_LINK = "https://github.com/Ellisia-Chan/Fox-Runner";
    private const string ITCH_LINK = "https://ellisya.itch.io/";

    [SerializeField] private Button githubButton;
    [SerializeField] private Button itchButton;
    [SerializeField] private Button exitButton;

    private void Awake() {
        Instance = this;

        githubButton.onClick.AddListener(() => {
            Application.OpenURL(GITHUB_LINK);
            DeselectButton();
        });

        itchButton.onClick.AddListener(() => {
            Application.OpenURL(ITCH_LINK);
            DeselectButton();
        });

        exitButton.onClick.AddListener(() => { Hide(); });

        Hide();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void DeselectButton() {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
