using System.Collections.Generic;
using UnityEngine;

// �o�g���̐i�s���Ǘ�����
public class BattleSceneManager : MonoBehaviour
{
    // �o�g���ɕK�v��UI����
    [SerializeField] private BattleUIHandler battleUIHandler = null;
    // �퓬�Ɏg�p�����؂��i�[����e�I�u�W�F�N�g
    [SerializeField] private Transform mainVegetablesParent = null;
    // 3�̖̂�؂̍��W
    [SerializeField] private List<Transform> vegetablePositions = null;

    // ��������G
    [SerializeField] private GameObject prefab = null;
    // �G�𐶐�������W
    [SerializeField] private Transform generatePosition = null;
    // ��������G�̐e�I�u�W�F�N�g
    [SerializeField] private Transform parent = null;

    // �o�g���J�n���̎c��̓G�̐�
    [SerializeField] private int count = 5;
    // ��������C���^�[�o��
    [SerializeField] private int interval = 0;

    // �A���Ő��������Ƃ���Y�I�t�Z�b�g(�d�Ȃ�Ȃ��悤�ɂ��邽��)
    [SerializeField] private float offsetY = 0.0f;

    private float timer = 1.0f;
    // �퓬�Ɏg�p�����؂��i�[����
    private List<GameObject> mainVegetables = new();

    // ��O�̖�؂��U�����Ă��铮���̐�
    private int frontAnimalsCount = 0;

    private const int MAX_SORTING_ORDER = 100;

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
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            // �����̐���
            var animal = Instantiate(prefab, generatePosition.transform.position, Quaternion.identity, parent).GetComponent<Enemy>();

            // TODO : �Ƃ肠�����l�Q���߂����Ĉړ����Ă���̂Ō���ύX
            var targetPosition = new Vector2(vegetablePositions[0].position.x, vegetablePositions[0].position.y + frontAnimalsCount * offsetY);
            animal.Init(targetPosition, MAX_SORTING_ORDER - frontAnimalsCount);
            frontAnimalsCount++;
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
