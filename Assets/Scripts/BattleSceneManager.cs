using System.Collections.Generic;
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

        // foreach (Transform child in mainVegetablesParent.transform) {
        //     mainVegetables.Add(child.gameObject);
        // }
        // 
        // GameController.Instance.Test();

        // �퓬�Ɏg�p�����؂ɉ摜�̔��f
        // for (int index = 0; index < VegetableConstData.MAIN_VEGETABLES_COUNT; index++) {
        //     var spriteRenderer = mainVegetables[index].GetComponent<SpriteRenderer>();
        //     spriteRenderer.sprite = GameController.Instance.MainVegetables[index].Sprite;
        // }

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
