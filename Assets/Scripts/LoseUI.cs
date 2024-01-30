using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ”s–kŽž‚ÌUI
public class LoseUI : MonoBehaviour
{
    // ”wŒi
    private Image background = null;

    private void Start() {
        background = GetComponent<Image>();
    }

    // UI‚Ì•\Ž¦
    public void ShowUI() {
        background.enabled = true;
        foreach (Transform child in gameObject.transform) {
            child.gameObject.SetActive(true);
        }
    }

    // UI‚Ì”ñ•\Ž¦
    public void HideUI() {
        background.enabled = false;
        foreach (Transform child in gameObject.transform) {
            child.gameObject.SetActive(false);
        }
    }
}
