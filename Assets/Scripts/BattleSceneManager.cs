using System.Collections.Generic;
using UnityEngine;

// バトルの進行を管理する
public class BattleSceneManager : MonoBehaviour
{
    // バトルに必要なUI周り
    [SerializeField] private BattleUIHandler battleUIHandler = null;
    // 戦闘に使用する野菜を格納する親オブジェクト
    [SerializeField] private Transform mainVegetablesParent = null;
    // 3体の野菜の座標
    [SerializeField] private List<Transform> vegetablePositions = null;

    // 生成する敵
    [SerializeField] private GameObject prefab = null;
    // 敵を生成する座標
    [SerializeField] private Transform generatePosition = null;
    // 生成する敵の親オブジェクト
    [SerializeField] private Transform parent = null;

    // バトル開始時の残りの敵の数
    [SerializeField] private int count = 5;
    // 生成するインターバル
    [SerializeField] private int interval = 0;

    // 連続で生成したときのYオフセット(重ならないようにするため)
    [SerializeField] private float offsetY = 0.0f;

    private float timer = 1.0f;
    // 戦闘に使用する野菜を格納する
    private List<GameObject> mainVegetables = new();

    // 手前の野菜を攻撃している動物の数
    private int frontAnimalsCount = 0;

    private const int MAX_SORTING_ORDER = 100;

    private void Start() {
        battleUIHandler.SetCountText(count);

        // foreach (Transform child in mainVegetablesParent.transform) {
        //     mainVegetables.Add(child.gameObject);
        // }
        // 
        // GameController.Instance.Test();

        // 戦闘に使用する野菜に画像の反映
        // for (int index = 0; index < VegetableConstData.MAIN_VEGETABLES_COUNT; index++) {
        //     var spriteRenderer = mainVegetables[index].GetComponent<SpriteRenderer>();
        //     spriteRenderer.sprite = GameController.Instance.MainVegetables[index].Sprite;
        // }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            // 動物の生成
            var animal = Instantiate(prefab, generatePosition.transform.position, Quaternion.identity, parent).GetComponent<Enemy>();

            // TODO : とりあえず人参をめがけて移動しているので後程変更
            var targetPosition = new Vector2(vegetablePositions[0].position.x, vegetablePositions[0].position.y + frontAnimalsCount * offsetY);
            animal.Init(targetPosition, MAX_SORTING_ORDER - frontAnimalsCount);
            frontAnimalsCount++;
        }

        // if (count <= 0) {
        //     return;
        // }
        // 
        // timer += Time.deltaTime;
        // if (timer >= interval) {
        //     timer = 0.0f;
        //     Instantiate(prefab, parent);
        //     battleUIHandler.SetCountText(--count);
        // }
    }
}
