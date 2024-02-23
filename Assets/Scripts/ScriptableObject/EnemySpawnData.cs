using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

// �X�e�[�W���Ƃ̓G�̐������Ǘ�����f�[�^
[CreateAssetMenu(fileName = "EnemySpawnData")]
public class EnemySpawnData : ScriptableObject {
    [Header("�X�e�[�WID")]
    [SerializeField] private int stageID = 0;
    public int StageID { get => stageID; }

    [Header("��������v���n�u�ƃC���^�[�o��")]
    [SerializeField] private List<SpawnMap> spawnMapList = null;
    public List<SpawnMap> SpawnMapList { get => spawnMapList; }
}

[System.Serializable]
public class SpawnMap {
    // �������铮���̃f�[�^
    [SerializeField] private Animal animal = null;
    public Animal Animal { get => animal; }

    // �ЂƂO�̐������玟�̐����܂ł̃C���^�[�o��
    [SerializeField, Range(1, 10)] private float interval = 0.0f;
    public float Interval { get => interval; }
}