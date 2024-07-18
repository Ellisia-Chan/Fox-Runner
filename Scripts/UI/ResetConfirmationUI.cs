using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResetConfirmationUI : MonoBehaviour {

    public static ResetConfirmationUI Instance { get; private set; }

    [SerializeField] private Button cancelButton;
    [SerializeField] private Button confirmButton;

    private void Awake() {
        Instance = this;
        Hide();

        cancelButton.onClick.AddListener(() => {
            DeselectObject();
            Hide();
        });

        confirmButton.onClick.AddListener(() => {
            SettingsManager.Instance.ResetToDefault();
            DeselectObject();
            Hide();
        });
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void DeselectObject() {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
