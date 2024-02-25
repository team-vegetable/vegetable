using UnityEngine;

// バトル用の動物のパラメーター
[CreateAssetMenu(fileName = "Animal Battle Status")]
public class AnimalBattleStatus : ScriptableObject
{
    [Header("ID")]
    [SerializeField] private int id = 0;
    public int ID { get => id; }

    [Header("移動スピード")]
    [SerializeField] private int speed = 0;
    public int Speed { get => speed; }

    [Header("攻撃範囲")]
    [SerializeField] private int attackRange = 0;
    public int AttackRange { get => attackRange; }

    [Header("最大HP")]
    [SerializeField] private int maxHP = 0;
    public int MaxHP { get => maxHP; }
}
