using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// �s�k����UI
public class LoseUI : MonoBehaviour
{
    // �z�[���ɖ߂�{�^��
    [SerializeField] private Button backHomeButton = null;

    // �w�i
    private Image background = null;

    private void Start() {
        background = GetComponent<Image>();

        backHomeButton.onClick.RemoveAllListeners();
        backHomeButton.onClick.AddListener(OnClickBackHomeButton);
    }

    // UI�̕\��
    public void ShowUI() {
        background.enabled = true;
        foreach (Transform child in gameObject.transform) {
            child.gameObject.SetActive(true);
        }
    }

    // UI�̔�\��
    public void HideUI() {
        background.enabled = false;
        foreach (Transform child in gameObject.transform) {
            child.gameObject.SetActive(false);
        }
    }

    // �z�[���ɖ߂�{�^�����������Ƃ�
    public void OnClickBackHomeButton() {
        SceneManager.LoadScene("HomeScene");
    }
}
