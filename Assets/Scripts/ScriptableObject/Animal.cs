using UnityEngine;

// �����̃p�����[�^�[�֘A
[CreateAssetMenu(fileName = "Animal")]
public class Animal : ScriptableObject {
    [Header("ID")]
    [SerializeField] private int id = 0;
    public int ID { get => id; }

    [Header("���O")]
    [SerializeField] private new string name = "";
    public string Name { get => name; }

    [Header("�v���n�u")]
    [SerializeField] private GameObject prefab = null;
    public GameObject Prefab { get => prefab; }

    [Header("�o�g���p�̃X�e�[�^�X")]
    [SerializeField] private AnimalBattleStatus battleStatus = null;
    public AnimalBattleStatus BattleStatus { get => battleStatus; }

    // �������Ƃ�ID
    public enum ANIMAL {
        // �C�m�V�V
        WildBoar = 1,
    }
}

// �����̃o�g���p�̃X�e�[�^�X
[System.Serializable]
public class AnimalBattleStatus {
    [Header("�ő�HP")]
    [SerializeField] private int maxHP = 0;
    public int MaxHP { get => maxHP; }

    [Header("�ړ��X�s�[�h")]
    [SerializeField] private int speed = 0;
    public int Speed { get => speed; }

    [Header("�U����")]
    [SerializeField] private int attack = 0;
    public int Attack { get => attack; }

    [Header("�U���͈�")]
    [SerializeField] private int attackRange = 0;
    public int AttackRange { get => attackRange; }
}