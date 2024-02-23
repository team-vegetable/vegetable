using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

// �o�g���̐i�s���Ǘ�����
public class BattleSceneManager : MonoBehaviour
{
    // �o�g���ɕK�v��UI����
    [SerializeField] private BattleUIHandler battleUIHandler = null;
    // ���������p
    [SerializeField] private GenerateAnimals generateAnimals = null;
    // �퓬�Ɏg�p�����؂��i�[����e�I�u�W�F�N�g
    [SerializeField] private Transform mainVegetablesParent = null;

    // �o�g���J�n���̎c��̓G�̐�
    [SerializeField] private int count = 5;
    // �퓬�Ɏg�p�����؂��i�[����
    private List<GameObject> mainVegetables = new();


    private EnemySpawnData spawnData = null;
    private int currentIndex = 0;
    private float timer = 0.0f;

    private List<bool> isGenetate = new() { false, false};

    private void Start() {
        battleUIHandler.SetCountText(count);

        // �G�̐������Ǘ�����A�Z�b�g�̓ǂݍ���
        // ���݂̓X�e�[�W�P���ߑł�
        spawnData = LoadAsset.LoadFromFolder<EnemySpawnData>(LoadAsset.SPAWN_DATA_PATH).FirstOrDefault(e => e.StageID == 1);

        // ��؂̃A�Z�b�g�ƕҐ���Ԃ̓ǂݍ���
        var vegetableAssets = LoadAsset.LoadFromFolder<Vegetable>(LoadAsset.VEGETABLE_PATH);
        var mainVegetableIDs = QuickSave.Load<List<int>>(VegetableConstData.PARTY_DATA, "MainVegetableIDs");

#if UNITY_EDITOR
        // �Z�[�u�f�[�^������ĂȂ���Ώ����̖�؂��Z�b�g����
        if (mainVegetableIDs == default) {
            mainVegetableIDs = new() { (int)Vegetable.VEGETABLE.Carrot, (int)Vegetable.VEGETABLE.CherryTomato, (int)Vegetable.VEGETABLE.Cabbage };
        }
#endif

        // �퓬�Ɏg�p�����؂ɉ摜�̔��f
        for (int index = 0; index < VegetableConstData.MAIN_VEGETABLES_COUNT; index++) {
            var child = mainVegetablesParent.GetChild(index).gameObject;
            var vegetable = vegetableAssets.FirstOrDefault(e => e.ID == mainVegetableIDs[index]);

            var spriteRenderer = child.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = vegetable.BattleSprite;
            battleUIHandler.SetIcon(vegetable.Icon, index);
            // mainVegetables.Add(child.gameObject);
        }

        // �����̃A�Z�b�g�̓ǂݍ���
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
