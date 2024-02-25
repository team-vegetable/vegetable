using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

// �o�g���̐i�s���Ǘ�����
public class BattleSceneManager : MonoBehaviour {
    // �o�g���ɕK�v��UI����
    [SerializeField] private BattleUIHandler battleUIHandler = null;
    // ���������p
    [SerializeField] private GenerateAnimals generateAnimals = null;
    // �퓬�Ɏg�p�����؂��i�[����e�I�u�W�F�N�g
    [SerializeField] private Transform mainVegetablesParent = null;

    // �G��|������
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

        // �G�̐������Ǘ�����A�Z�b�g�̓ǂݍ���
        // ���݂̓X�e�[�W�P���ߑł�
        spawnData = LoadAsset.LoadFromFolder<EnemySpawnData>(LoadAsset.SPAWN_DATA_PATH).FirstOrDefault(e => e.StageID == 1);

        // ��؂̃A�Z�b�g�ƕҐ���Ԃ̓ǂݍ���
        // var vegetableAssets = LoadAsset.LoadFromFolder<Vegetable>(LoadAsset.VEGETABLE_PATH);
        var mainVegetableIDs = QuickSave.Load<List<int>>(VegetableConstData.PARTY_DATA, "MainVegetableIDs");

#if UNITY_EDITOR
        // �Z�[�u�f�[�^������ĂȂ���Ώ����̖�؂��Z�b�g����
        if (mainVegetableIDs == default) {
            mainVegetableIDs = new() { (int)Vegetable.VEGETABLE.Carrot, (int)Vegetable.VEGETABLE.CherryTomato, (int)Vegetable.VEGETABLE.Cabbage };
        }
#endif
        // ��؂̃v���n�u�����X�g�Ŏ擾
        var prefabs = LoadAsset.LoadPrefab<BaseVegetable>("Assets/Prefabs/Vegetable");
        for (int index = 0; index < VegetableConstData.MAIN_VEGETABLES_COUNT; index++) {
            // �Z�[�u�f�[�^�ƈ�v�����؂𐶐�����
            var prefab = prefabs.FirstOrDefault(e => e.Vegetable.ID == mainVegetableIDs[index]);
            Instantiate(prefab, VEGETABLE_POSITIONS[index], Quaternion.identity);
            battleUIHandler.SetIcon(prefab.Vegetable.Icon, index);
        }

        // �����̃A�Z�b�g�̓ǂݍ���
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
    
    // �������|���ꂽ�Ƃ�
    private void OnAnimalDead() {
        battleUIHandler.SetCountText(++count);
    }
}
