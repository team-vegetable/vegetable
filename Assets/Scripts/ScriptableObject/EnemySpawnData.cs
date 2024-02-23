using System.Collections.Generic;
using UnityEngine;

// �X�e�[�W���Ƃ̓G�̐������Ǘ�����f�[�^
[CreateAssetMenu(fileName = "EnemySpawnData")]
public class EnemySpawnData : ScriptableObject
{
    [Header("�X�e�[�WID")]
    [SerializeField] private int stageID = 0;

    public int StageID { get => stageID; }
    [Header("��������v���n�u�ƃC���^�[�o��")]
    [SerializeField] private List<SpawnMap> spawnMapList = null;
}

[System.Serializable]
public class SpawnMap {
    // ��������G�̃v���n�u
    public GameObject prefab = null;
    // �ЂƂO�̐������玟�̐����܂ł̃C���^�[�o��
    [Range(1, 10)] public float interval = 0.0f;
}