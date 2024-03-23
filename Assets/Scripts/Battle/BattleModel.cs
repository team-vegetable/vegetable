using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

// バトルで使用するモデル(MVP)
public class BattleModel : MonoBehaviour
{
    // 残り何秒から開始するか
    // [SerializeField] private float timer = 60.0f;

    // タイマー
    private readonly ReactiveProperty<float> timer = new(60);
    public ReactiveProperty<float> Timer => timer;

    // 敵を倒した数
    private readonly ReactiveProperty<int> count = new();
    public ReactiveProperty<int> Count => count;

    private async void Start() {
        await StartTimer();
    }

    public void Test() {
        count.Value++;
    }

    // 残り時間のタイマーをスタートさせる
    // TODO : モデルで実装するべきではない気がする
    public async UniTask StartTimer() {
        while (true) {
            await UniTask.Yield();
            timer.Value -= Time.deltaTime;
        }
    }
}
