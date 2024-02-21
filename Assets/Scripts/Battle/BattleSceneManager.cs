using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// バトルの進行を管理する
public class BattleSceneManager : MonoBehaviour
{
    // バトルに必要なUI周り
    [SerializeField] private BattleUIHandler battleUIHandler = null;
    // 動物生成用
    [SerializeField] private GenerateAnimals generateAnimals = null;
    // 戦闘に使用する野菜を格納する親オブジェクト
    [SerializeField] private Transform mainVegetablesParent = null;

    // バトル開始時の残りの敵の数
    [SerializeField] private int count = 5;
    // 生成するインターバル
    [SerializeField] private int interval = 0;

    private float timer = 1.0f;
    // 戦闘に使用する野菜を格納する
    private List<GameObject> mainVegetables = new();

    private void Start() {
        battleUIHandler.SetCountText(count);

        // アセットと編成状態の読み込み
        var vegetableAssets = LoadAsset.LoadFromFolder<Vegetable>(LoadAsset.VEGETABLE_PATH);
        var mainVegetableIDs = QuickSave.Load<List<int>>(VegetableConstData.PARTY_DATA, "MainVegetableIDs");

        // 戦闘に使用する野菜に画像の反映
        for (int index = 0; index < VegetableConstData.MAIN_VEGETABLES_COUNT; index++) {
            var child = mainVegetablesParent.GetChild(index).gameObject;
            var spriteRenderer = child.GetComponent<SpriteRenderer>();
            var vegetable = vegetableAssets.FirstOrDefault(e => e.ID == mainVegetableIDs[index]);
            spriteRenderer.sprite = vegetable.Sprite;
            mainVegetables.Add(child.gameObject);
        }

        // 動物のアセットの読み込み
        var animalAssets = LoadAsset.LoadFromFolder<Animal>(LoadAsset.ANIMAL_PATH);
        generateAnimals.Init(animalAssets);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            generateAnimals.Generate();
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
