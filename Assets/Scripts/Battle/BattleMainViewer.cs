using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// バトルの表示関係を扱う
public class BattleMainViewer : MonoBehaviour
{
    // 野菜のアイコン
    [SerializeField] private List<Image> icons = null;
    // 残り時間を表示するテキスト
    [SerializeField] private TextMeshProUGUI timeText = null;
    // 倒した敵の数を表示するテキスト
    [SerializeField] private TextMeshProUGUI countText = null;

    // 敗北時のUI
    [SerializeField] private LoseUI loseUI = null;
     
    // 残り何秒から開始するか
    [SerializeField] private float timer = 60.0f;

    // 残り時間のタイマーをスタートさせる
    public async UniTask StartTimer() {
        while (true) {
            await UniTask.Yield();
            timer -= Time.deltaTime;
            timeText.text = $"残り{(int)timer}秒";
        }
    }

    // 野菜のアイコンのセット
    public void SetIcon(Sprite sprite, int index) {
        icons[index].sprite = sprite;
    }

    // 倒した敵の数をテキストに反映させる
    public void SetCountText(int count) {
        countText.text = $"倒した敵 : {count}体";
    }
}
