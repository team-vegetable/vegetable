using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using System.Collections.Generic;

// バトルで使用するモデル(MVP)
public class BattleModel : MonoBehaviour {
    // バトルに使用する野菜
    private readonly Subject<List<Vegetable>> vegetables = new();
    public Subject<List<Vegetable>> Vegetables => vegetables;

    // 制限時間
    private readonly ReactiveProperty<float> timer = new();
    public ReactiveProperty<float> Timer => timer;

    // 敵を倒した数
    private readonly ReactiveProperty<int> count = new();
    public ReactiveProperty<int> Count => count;

    private async void Start() {
        await StartTimer();
    }

    // 野菜のアイコンのセット
    public void SetVegetableIcon(List<Vegetable> vegetables) {
        this.vegetables.OnNext(vegetables);
    }

    // 動物を倒した数の更新
    public void UpdateAnimalCount() {
        count.Value++;
    }

    // 残り時間のタイマーをスタートさせる
    public async UniTask StartTimer() {
        timer.Value = VegetableConstData.TIMER;
        while (true) {
            await UniTask.Yield();
            timer.Value -= Time.deltaTime;
        }
    }
}
