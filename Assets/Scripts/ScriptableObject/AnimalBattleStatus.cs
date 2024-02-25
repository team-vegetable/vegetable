using UnityEngine;

// �o�g���p�̓����̃p�����[�^�[
[CreateAssetMenu(fileName = "Animal Battle Status")]
public class AnimalBattleStatus : ScriptableObject
{
    [Header("ID")]
    [SerializeField] private int id = 0;
    public int ID { get => id; }

    [Header("�ړ��X�s�[�h")]
    [SerializeField] private int speed = 0;
    public int Speed { get => speed; }

    [Header("�U���͈�")]
    [SerializeField] private int attackRange = 0;
    public int AttackRange { get => attackRange; }

    [Header("�ő�HP")]
    [SerializeField] private int maxHP = 0;
    public int MaxHP { get => maxHP; }
}
