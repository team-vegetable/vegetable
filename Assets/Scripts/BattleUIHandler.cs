using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

// バトルのUIを扱う
public class BattleUIHandler : MonoBehaviour
{
    // 残り時間を表示するテキスト
    [SerializeField] private TextMeshProUGUI timerText = null;
    // 残り何秒から開始するか
    [SerializeField] private float timer = 60.0f;

    private void Start() {
        StartTimer();
    }

    // 残り時間のタイマーをスタートさせる
    public void StartTimer() {
        StartCoroutine(OnStartTimer());
    }

    private IEnumerator OnStartTimer() {
        while (true) {
            timer -= Time.deltaTime;
            timerText.text = $"残り時間 : {(int)timer}秒";
            yield return null;
        }
    }
}
