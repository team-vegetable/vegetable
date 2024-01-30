using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 敗北時のUI
public class LoseUI : MonoBehaviour
{
    // ホームに戻るボタン
    [SerializeField] private Button backHomeButton = null;

    // 背景
    private Image background = null;

    private void Start() {
        background = GetComponent<Image>();

        backHomeButton.onClick.RemoveAllListeners();
        backHomeButton.onClick.AddListener(OnClickBackHomeButton);
    }

    // UIの表示
    public void ShowUI() {
        background.enabled = true;
        foreach (Transform child in gameObject.transform) {
            child.gameObject.SetActive(true);
        }
    }

    // UIの非表示
    public void HideUI() {
        background.enabled = false;
        foreach (Transform child in gameObject.transform) {
            child.gameObject.SetActive(false);
        }
    }

    // ホームに戻るボタンを押したとき
    public void OnClickBackHomeButton() {
        SceneManager.LoadScene("HomeScene");
    }
}
