using System.Collections.Generic;
using UnityEngine;

// ステージごとの敵の生成を管理するデータ
[CreateAssetMenu(fileName = "EnemySpawnData")]
public class EnemySpawnData : ScriptableObject
{
    [Header("ステージID")]
    [SerializeField] private int stageID = 0;

    public int StageID { get => stageID; }
    [Header("生成するプレハブとインターバル")]
    [SerializeField] private List<SpawnMap> spawnMapList = null;
}

[System.Serializable]
public class SpawnMap {
    // 生成する敵のプレハブ
    public GameObject prefab = null;
    // ひとつ前の生成から次の生成までのインターバル
    [Range(1, 10)] public float interval = 0.0f;
}