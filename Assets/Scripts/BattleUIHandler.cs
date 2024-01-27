using System.Collections;
using TMPro;
using UnityEngine;

// バトルのUIを扱う
public class BattleUIHandler : MonoBehaviour
{
    // 残り時間を表示するテキスト
    [SerializeField] private TextMeshProUGUI timerText = null;

    private float timer = 60;

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
