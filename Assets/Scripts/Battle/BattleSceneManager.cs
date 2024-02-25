using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

// バトルの進行を管理する
public class BattleSceneManager : MonoBehaviour {
    // バトルに必要なUI周り
    [SerializeField] private BattleUIHandler battleUIHandler = null;
    // 動物生成用
    [SerializeField] private GenerateAnimals generateAnimals = null;
    // 戦闘に使用する野菜を格納する親オブジェクト
    [SerializeField] private Transform mainVegetablesParent = null;

    // 敵を倒した数
    private int count = 0;
    private EnemySpawnData spawnData = null;
    private int currentIndex = 0;
    private float timer = 0.0f;

    private List<bool> isGenetate = new() { false, false };
    private bool isGenerate = false;

    private readonly List<Vector2> VEGETABLE_POSITIONS = new() {
        new(-6.88f, -3.02f), new(-5.24f, -0.81f), new(-3.38f, 1.28f)
    };

    private void Start() {
        battleUIHandler.SetCountText(count);

        // 敵の生成を管理するアセットの読み込み
        // 現在はステージ１決め打ち
        spawnData = LoadAsset.LoadFromFolder<EnemySpawnData>(LoadAsset.SPAWN_DATA_PATH).FirstOrDefault(e => e.StageID == 1);

        // 野菜のアセットと編成状態の読み込み
        // var vegetableAssets = LoadAsset.LoadFromFolder<Vegetable>(LoadAsset.VEGETABLE_PATH);
        var mainVegetableIDs = QuickSave.Load<List<int>>(VegetableConstData.PARTY_DATA, "MainVegetableIDs");

#if UNITY_EDITOR
        // セーブデータを作ってなければ初期の野菜をセットする
        if (mainVegetableIDs == default) {
            mainVegetableIDs = new() { (int)Vegetable.VEGETABLE.Carrot, (int)Vegetable.VEGETABLE.CherryTomato, (int)Vegetable.VEGETABLE.Cabbage };
        }
#endif
        // 野菜のプレハブをリストで取得
        var prefabs = LoadAsset.LoadPrefab<BaseVegetable>("Assets/Prefabs/Vegetable");
        for (int index = 0; index < VegetableConstData.MAIN_VEGETABLES_COUNT; index++) {
            // セーブデータと一致する野菜を生成する
            var prefab = prefabs.FirstOrDefault(e => e.Vegetable.ID == mainVegetableIDs[index]);
            Instantiate(prefab, VEGETABLE_POSITIONS[index], Quaternion.identity);
            battleUIHandler.SetIcon(prefab.Vegetable.Icon, index);
        }

        // 動物のアセットの読み込み
        var animalAssets = LoadAsset.LoadFromFolder<Animal>(LoadAsset.ANIMAL_PATH);
        generateAnimals.Init(animalAssets);
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            generateAnimals.Generate(spawnData.SpawnMapList[currentIndex++].Animal, OnAnimalDead);
        }

        //if (currentIndex < spawnData.SpawnMapList.Count && spawnData.SpawnMapList[currentIndex] != null) {
        //    if (spawnData.SpawnMapList[currentIndex].Interval >= timer) {
        //        timer = 0.0f;
        //        Debug.Log("Generate");
        //        Debug.Log($"currentIndex : {currentIndex}");
        //        generateAnimals.Generate(spawnData.SpawnMapList[currentIndex].Animal, OnAnimalDead);
        //        currentIndex++;                
        //        // currentIndex++;
        //    }
        //}
    }
    
    // 動物が倒されたとき
    private void OnAnimalDead() {
        battleUIHandler.SetCountText(++count);
    }
}
