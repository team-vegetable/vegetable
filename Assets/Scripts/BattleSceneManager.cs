using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// バトルの進行を管理する
public class BattleSceneManager : MonoBehaviour
{
    // バトルに必要なUI周り
    [SerializeField] private BattleUIHandler battleUIHandler = null;

    // 生成する敵
    [SerializeField] private GameObject prefab = null;
    // 生成する敵の親オブジェクト
    [SerializeField] private Transform parent = null;

    // バトル開始時の残りの敵の数
    [SerializeField] private int count = 5;
    // 生成するインターバル
    [SerializeField] private int interval = 0;

    private float timer = 1.0f;

    private void Start() {
        battleUIHandler.SetCountText(count);
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= interval) {
            timer = 0.0f;
            Instantiate(prefab, parent);
            battleUIHandler.SetCountText(--count);
        }
    }
}
