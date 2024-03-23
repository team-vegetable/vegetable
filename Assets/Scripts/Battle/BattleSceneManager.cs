using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

// �o�g���̐i�s���Ǘ�����
public class BattleSceneManager : MonoBehaviour {
    // �o�g���ɕK�v��UI����
    // TODO �p�~�\��
    [SerializeField] private BattleMainViewer battleUIHandler = null;

    [SerializeField] private BattleModel model = null;
    // ���������p
    [SerializeField] private GenerateAnimals generateAnimals = null;
    // �퓬�Ɏg�p�����؂��i�[����e�I�u�W�F�N�g
    [SerializeField] private Transform mainVegetablesParent = null;

    // �G��|������
    private int count = 0;
    // �G�̐����̃f�[�^
    private EnemySpawnData spawnData = null;
    // ���݉��Ԗڂ̓G�𐶐����Ă��邩
    private int currentIndex = 0;

    // ��؂̍��W
    private readonly List<Vector2> VEGETABLE_POSITIONS = new() {
        new(-6.88f, -3.02f), new(-5.24f, -0.81f), new(-3.38f, 1.28f)
    };

    private async void Start() {

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
        List<Transform> transforms = new();
        // ��؂̃v���n�u�����X�g�Ŏ擾
        var prefabs = LoadAsset.LoadPrefab<BaseVegetable>("Assets/Prefabs/Vegetable");
        for (int index = 0; index < VegetableConstData.MAIN_VEGETABLES_COUNT; index++) {
            // �Z�[�u�f�[�^�ƈ�v�����؂𐶐�����
            var prefab = prefabs.FirstOrDefault(e => e.Vegetable.ID == mainVegetableIDs[index]);
            var vegetable = Instantiate(prefab, VEGETABLE_POSITIONS[index], Quaternion.identity);
            transforms.Add(vegetable.transform);
            battleUIHandler.SetIcon(prefab.Vegetable.Icon, index);
        }

        generateAnimals.Init(transforms);

        await GenerateAnimal();
    }

    // �����̐���
    private async UniTask GenerateAnimal() {
        while (currentIndex < spawnData.SpawnMapList.Count) {
            generateAnimals.Generate(spawnData.SpawnMapList[currentIndex].Animal, OnAnimalDead);
            await UniTask.Delay(TimeSpan.FromSeconds(spawnData.SpawnMapList[currentIndex].Interval));
            currentIndex++;
        }
    }
    
    // �������|���ꂽ�Ƃ�
    private void OnAnimalDead() {
        model.Test();
    }
}
