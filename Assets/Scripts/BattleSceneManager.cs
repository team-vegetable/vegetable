using System.Collections.Generic;
using UnityEngine;

// バトルの進行を管理する
public class BattleSceneManager : MonoBehaviour
{
    // バトルに必要なUI周り
    [SerializeField] private BattleUIHandler battleUIHandler = null;
    // 戦闘に使用する野菜を格納する親オブジェクト
    [SerializeField] private Transform mainVegetablesParent = null;

    // 生成する敵
    [SerializeField] private GameObject prefab = null;
    // 生成する敵の親オブジェクト
    [SerializeField] private Transform parent = null;

    // バトル開始時の残りの敵の数
    [SerializeField] private int count = 5;
    // 生成するインターバル
    [SerializeField] private int interval = 0;

    private float timer = 1.0f;
    // 戦闘に使用する野菜を格納する
    private List<GameObject> mainVegetables = new();

    private void Start() {
        battleUIHandler.SetCountText(count);

        foreach (Transform child in mainVegetablesParent.transform) {
            mainVegetables.Add(child.gameObject);
        }

        // 戦闘に使用する野菜に画像の反映
        // for (int index = 0; index < VegetableConstData.MAIN_VEGETABLES_COUNT; index++) {
        //     var spriteRenderer = mainVegetables[index].GetComponent<SpriteRenderer>();
        //     spriteRenderer.sprite = GameController.Instance.MainVegetables[index].Sprite;
        // }
    }

    private void Update() {
        if (count <= 0) {
            return;
        }

        timer += Time.deltaTime;
        if (timer >= interval) {
            timer = 0.0f;
            Instantiate(prefab, parent);
            battleUIHandler.SetCountText(--count);
        }
    }
}
