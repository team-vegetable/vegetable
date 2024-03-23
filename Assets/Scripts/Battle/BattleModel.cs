using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using System.Collections.Generic;
using System;

// バトルで使用するモデル(MVP)
public class BattleModel : MonoBehaviour
{
    // バトルに使用する野菜
    private Subject<List<Vegetable>> vegetables = new();
    public Subject<List<Vegetable>> Vegetables => vegetables;

    // タイマー
    // TODO : この数値はバトル周りのマスターデータをまとめたものから取得したい
    private readonly ReactiveProperty<float> timer = new(60);
    public ReactiveProperty<float> Timer => timer;

    // 敵を倒した数
    private readonly ReactiveProperty<int> count = new();
    public ReactiveProperty<int> Count => count;

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
        while (true) {
            await UniTask.Yield();
            timer.Value -= Time.deltaTime;
        }
    }
}
