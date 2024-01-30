using System.Collections.Generic;
using UnityEngine;

// �o�g���̐i�s���Ǘ�����
public class BattleSceneManager : MonoBehaviour
{
    // �o�g���ɕK�v��UI����
    [SerializeField] private BattleUIHandler battleUIHandler = null;
    // �퓬�Ɏg�p�����؂��i�[����e�I�u�W�F�N�g
    [SerializeField] private Transform mainVegetablesParent = null;

    // ��������G
    [SerializeField] private GameObject prefab = null;
    // ��������G�̐e�I�u�W�F�N�g
    [SerializeField] private Transform parent = null;

    // �o�g���J�n���̎c��̓G�̐�
    [SerializeField] private int count = 5;
    // ��������C���^�[�o��
    [SerializeField] private int interval = 0;

    private float timer = 1.0f;
    // �퓬�Ɏg�p�����؂��i�[����
    private List<GameObject> mainVegetables = new();

    private void Start() {
        battleUIHandler.SetCountText(count);

        foreach (Transform child in mainVegetablesParent.transform) {
            mainVegetables.Add(child.gameObject);
        }

        // �퓬�Ɏg�p�����؂ɉ摜�̔��f
        // for (int index = 0; index < VegetableConstData.MAIN_VEGETABLES_COUNT; index++) {
        //     var spriteRenderer = mainVegetables[index].GetComponent<SpriteRenderer>();
        //     spriteRenderer.sprite = GameController.Instance.MainVegetables[index].Sprite;
        // }
    }

    private void Update() {
        if (count <= 0) {
            return;
        }

        timer += Time.deltaTime;
        if (timer >= interval) {
            timer = 0.0f;
            Instantiate(prefab, parent);
            battleUIHandler.SetCountText(--count);
        }
    }
}
