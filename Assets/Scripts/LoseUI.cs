using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �s�k����UI
public class LoseUI : MonoBehaviour
{
    // �w�i
    private Image background = null;

    private void Start() {
        background = GetComponent<Image>();
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
}
