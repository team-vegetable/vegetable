using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 動物のパラメーター関連
[CreateAssetMenu(fileName = "Animal")]
public class Animal : ScriptableObject
{
    [Header("ID")]
    [SerializeField] private int id = 0;
    public int ID { get => id; }

    [Header("名前")]
    [SerializeField] private new string name = "";
    public string Name { get => name; }

    [Header("移動スピード")]
    [SerializeField] private int speed = 0;
    public int Speed { get => speed; }

    [Header("攻撃範囲")]
    [SerializeField] private int attackRange = 0;
    public int AttackRange { get => attackRange; }

    [Header("最大HP")]
    [SerializeField] private int maxHP = 0;
    public int MaxHP { get => maxHP; }

    [Header("画像")]
    [SerializeField] private Sprite sprite = null;
    public Sprite Sprite { get => sprite; }

    [Header("プレハブ")]
    [SerializeField] private GameObject prefab = null;
    public GameObject Prefab { get => prefab; }

    // 動物ごとのID
    public enum ANIMAL {
        // イノシシ
        WildBoar = 1,
    }
}