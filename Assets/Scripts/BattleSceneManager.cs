using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �o�g���̐i�s���Ǘ�����
public class BattleSceneManager : MonoBehaviour
{
    // �o�g���ɕK�v��UI����
    [SerializeField] private BattleUIHandler battleUIHandler = null;

    // ��������G
    [SerializeField] private GameObject prefab = null;
    // ��������G�̐e�I�u�W�F�N�g
    [SerializeField] private Transform parent = null;

    // �o�g���J�n���̎c��̓G�̐�
    [SerializeField] private int count = 5;
    // ��������C���^�[�o��
    [SerializeField] private int interval = 0;

    private float timer = 1.0f;

    private void Start() {
        battleUIHandler.SetCountText(count);
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= interval) {
            timer = 0.0f;
            Instantiate(prefab, parent);
            battleUIHandler.SetCountText(--count);
        }
    }
}
