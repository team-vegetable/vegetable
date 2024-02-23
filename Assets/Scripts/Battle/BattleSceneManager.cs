using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

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
    // 戦闘に使用する野菜を格納する
    private List<GameObject> mainVegetables = new();


    private EnemySpawnData spawnData = null;
    private int currentIndex = 0;
    private float timer = 0.0f;

    private List<bool> isGenetate = new() { false, false};

    private void Start() {
        battleUIHandler.SetCountText(count);

        // 敵の生成を管理するアセットの読み込み
        // 現在はステージ１決め打ち
        spawnData = LoadAsset.LoadFromFolder<EnemySpawnData>(LoadAsset.SPAWN_DATA_PATH).FirstOrDefault(e => e.StageID == 1);

        // 野菜のアセットと編成状態の読み込み
        var vegetableAssets = LoadAsset.LoadFromFolder<Vegetable>(LoadAsset.VEGETABLE_PATH);
        var mainVegetableIDs = QuickSave.Load<List<int>>(VegetableConstData.PARTY_DATA, "MainVegetableIDs");

#if UNITY_EDITOR
        // セーブデータを作ってなければ初期の野菜をセットする
        if (mainVegetableIDs == default) {
            mainVegetableIDs = new() { (int)Vegetable.VEGETABLE.Carrot, (int)Vegetable.VEGETABLE.CherryTomato, (int)Vegetable.VEGETABLE.Cabbage };
        }
#endif

        // 戦闘に使用する野菜に画像の反映
        for (int index = 0; index < VegetableConstData.MAIN_VEGETABLES_COUNT; index++) {
            var child = mainVegetablesParent.GetChild(index).gameObject;
            var vegetable = vegetableAssets.FirstOrDefault(e => e.ID == mainVegetableIDs[index]);

            var spriteRenderer = child.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = vegetable.BattleSprite;
            battleUIHandler.SetIcon(vegetable.Icon, index);
            // mainVegetables.Add(child.gameObject);
        }

        // 動物のアセットの読み込み
        var animalAssets = LoadAsset.LoadFromFolder<Animal>(LoadAsset.ANIMAL_PATH);
        generateAnimals.Init(animalAssets);

        //Observable.EveryUpdate()
        //    .Subscribe(_ => UpdateTimer())
        //    .AddTo(this);
    }

    //private void UpdateTimer() {
    //    timer += Time.deltaTime;
    //    if (currentIndex < spawnData.SpawnMapList.Count && spawnData.SpawnMapList[currentIndex] != null) {
    //        if (spawnData.SpawnMapList[currentIndex].Interval >= timer) {
    //            timer = 0.0f;
    //            generateAnimals.Generate(spawnData.SpawnMapList[currentIndex].Animal);
    //            currentIndex++;
    //        }
    //    }
    //}

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            generateAnimals.Generate(spawnData.SpawnMapList[currentIndex++].Animal);
        }

        //timer += Time.deltaTime;
        //if (currentIndex < spawnData.SpawnMapList.Count && spawnData.SpawnMapList[currentIndex] != null) {
        //    if (spawnData.SpawnMapList[currentIndex].Interval >= timer) {
        //        timer = 0.0f;
        //        generateAnimals.Generate(spawnData.SpawnMapList[currentIndex].Animal);
        //        currentIndex++;
        //    }
        //}
    }
}
