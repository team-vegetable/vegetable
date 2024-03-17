using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

// バトルで使用するモデル(MVP)
public class BattleModel : MonoBehaviour
{
    private ReactiveProperty<int> count = new ReactiveProperty<int>();
    public ReactiveProperty<int> Count => count;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            count.Value++;
        }
    }
}
