using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

// ステージごとの敵の生成を管理するデータ
[CreateAssetMenu(fileName = "EnemySpawnData")]
public class EnemySpawnData : ScriptableObject {
    [Header("ステージID")]
    [SerializeField] private int stageID = 0;
    public int StageID { get => stageID; }

    [Header("生成するプレハブとインターバル")]
    [SerializeField] private List<SpawnMap> spawnMapList = null;
    public List<SpawnMap> SpawnMapList { get => spawnMapList; }
}

[System.Serializable]
public class SpawnMap {
    // 生成する動物のデータ
    [SerializeField] private Animal animal = null;
    public Animal Animal { get => animal; }

    // ひとつ前の生成から次の生成までのインターバル
    [SerializeField, Range(1, 10)] private float interval = 0.0f;
    public float Interval { get => interval; }
}