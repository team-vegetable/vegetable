using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

// バトルの進行を管理する
public class BattleSceneManager : MonoBehaviour {
    // MVPのモデル
    [SerializeField] private BattleModel model = null;
    // 動物生成用
    [SerializeField] private GenerateAnimals generateAnimals = null;
    // 戦闘に使用する野菜を格納する親オブジェクト
    [SerializeField] private Transform mainVegetablesParent = null;

    // 敵の生成のデータ
    private EnemySpawnData spawnData = null;
    // 現在何番目の敵を生成しているか
    private int currentIndex = 0;

    // 野菜の座標
    private readonly List<Vector2> VEGETABLE_POSITIONS = new() {
        new(-6.88f, -3.02f), new(-5.24f, -0.81f), new(-3.38f, 1.28f)
    };

    private async void Start() {
        // 敵の生成を管理するアセットの読み込み
        // 現在はステージ１決め打ち
        spawnData = LoadAsset.LoadFromFolder<EnemySpawnData>(LoadAsset.SPAWN_DATA_PATH).FirstOrDefault(e => e.StageID == 1);

        // 野菜のアセットと編成状態の読み込み
        var mainVegetableIDs = QuickSave.Load(VegetableConstData.PARTY_DATA, "MainVegetableIDs", VegetableConstData.DefaultVegetableIds);

        List<Transform> vegetablePositions = new();
        List<Vegetable> vegetables = new();

        // 野菜のプレハブをリストで取得
        var prefabs = LoadAsset.LoadPrefab<BaseVegetable>("Assets/Prefabs/Vegetable");
        for (int index = 0; index < VegetableConstData.MAIN_VEGETABLES_COUNT; index++) {
            // セーブデータと一致する野菜を生成する
            var prefab = prefabs.FirstOrDefault(e => e.Vegetable.ID == mainVegetableIDs[index]);
            var vegetable = Instantiate(prefab, VEGETABLE_POSITIONS[index], Quaternion.identity);
            vegetables.Add(prefab.Vegetable);
            vegetablePositions.Add(vegetable.transform);
        }

        model.SetVegetableIcon(vegetables);
        generateAnimals.Init(vegetablePositions);

        // 動物の生成
        await GenerateAnimal();
    }

    // 動物の生成
    private async UniTask GenerateAnimal() {
        while (currentIndex < spawnData.SpawnMapList.Count) {
            generateAnimals.Generate(spawnData.SpawnMapList[currentIndex].Animal, OnAnimalDead);
            await UniTask.Delay(TimeSpan.FromSeconds(spawnData.SpawnMapList[currentIndex].Interval));
            currentIndex++;
        }
    }
    
    // 動物が倒されたとき
    private void OnAnimalDead() {
        model.UpdateAnimalCount();
    }
}
