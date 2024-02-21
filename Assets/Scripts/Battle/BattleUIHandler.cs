using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// バトルのUIを扱う
public class BattleUIHandler : MonoBehaviour
{
    // 野菜のアイコン
    [SerializeField] private List<Image> icons = null;
    // 残り時間を表示するテキスト
    [SerializeField] private TextMeshProUGUI timeText = null;
    // 残りの敵の数を表示するテキスト
    [SerializeField] private TextMeshProUGUI countText = null;
    // テスト用に負けた判定にする
    [SerializeField] private Button loseButton = null;

    // 敗北時のUI
    [SerializeField] private LoseUI loseUI = null;
     
    // 残り何秒から開始するか
    [SerializeField] private float timer = 60.0f;

    private void Start() {
        loseButton.onClick.RemoveAllListeners();
        loseButton.onClick.AddListener(OnClickLoseButton);

        StartTimer();
    }

    // 残り時間のタイマーをスタートさせる
    public void StartTimer() {
        StartCoroutine(OnStartTimer());
    }

    // 野菜のアイコンのセット
    public void SetIcon(Sprite sprite, int index) {
        icons[index].sprite = sprite;
    }

    private IEnumerator OnStartTimer() {
        while (true) {
            timer -= Time.deltaTime;
            timeText.text = $"残り{(int)timer}秒";
            yield return null;
        }
    }

    // 残りの敵の数をテキストに反映させる
    public void SetCountText(int count) {
        countText.text = $"残り{count}体";
    }

    // テスト用の敗北ボタンを押したとき
    private void OnClickLoseButton() {
        loseUI.ShowUI();
    }
}
