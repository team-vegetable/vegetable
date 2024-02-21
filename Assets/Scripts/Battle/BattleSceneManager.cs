using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    // ��������C���^�[�o��
    [SerializeField] private int interval = 0;

    private float timer = 1.0f;
    // �퓬�Ɏg�p�����؂��i�[����
    private List<GameObject> mainVegetables = new();

    private void Start() {
        battleUIHandler.SetCountText(count);

        // �A�Z�b�g�ƕҐ���Ԃ̓ǂݍ���
        var vegetableAssets = LoadAsset.LoadFromFolder<Vegetable>(LoadAsset.VEGETABLE_PATH);
        var mainVegetableIDs = QuickSave.Load<List<int>>(VegetableConstData.PARTY_DATA, "MainVegetableIDs");

        // �퓬�Ɏg�p�����؂ɉ摜�̔��f
        for (int index = 0; index < VegetableConstData.MAIN_VEGETABLES_COUNT; index++) {
            var child = mainVegetablesParent.GetChild(index).gameObject;
            var spriteRenderer = child.GetComponent<SpriteRenderer>();
            var vegetable = vegetableAssets.FirstOrDefault(e => e.ID == mainVegetableIDs[index]);
            spriteRenderer.sprite = vegetable.Sprite;
            mainVegetables.Add(child.gameObject);
        }

        // �����̃A�Z�b�g�̓ǂݍ���
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
