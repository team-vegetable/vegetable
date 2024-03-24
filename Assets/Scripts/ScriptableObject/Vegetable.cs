using System.Collections.Generic;
using UnityEngine;

// ��؂̃p�����[�^�[�֘A
[CreateAssetMenu(fileName = "Vegetable")]
public class Vegetable : ScriptableObject
{
    [Header("ID")]
    [SerializeField] private int id = 0;
    public int ID { get => id; }

    [Header("���O")]
    [SerializeField] private new string name = "";
    public string Name { get => name; }

    [Header("�o�g���Ɏg�p����X�e�[�^�X")]
    [SerializeField] private VegetableBattleStatus battleStatus = null;
    public VegetableBattleStatus BattleStatus { get => battleStatus; }

    [Header("�A�C�R��")]
    [SerializeField] private Sprite icon = null;
    public Sprite Icon { get => icon; }

    [Header("�o�g���Ɏg�p����摜")]
    [SerializeField] private Sprite battleSprite = null;
    public Sprite BattleSprite { get => battleSprite; }

    // ��؂��Ƃ�ID
    public enum VEGETABLE {
        // �l�Q
        Carrot = 1,
        // �~�j�g�}�g
        CherryTomato,
        // �L���x�c
        Cabbage
    }
}

// �o�g���p�̖�؂̃X�e�[�^�X
[System.Serializable]
public class VegetableBattleStatus {
    [Header("�ő�HP")]
    [SerializeField] private int maxHP = 0;
    public int MaxHP { get => maxHP; }

    [Header("�U����")]
    [SerializeField] private int attack = 0;
    public int Attack { get => attack; }

    [Header("�U���͈�")]
    [SerializeField] private int attackRange = 0;
    public int AttackRange { get => attackRange; }
}

// �萔�̊Ǘ�
public static class VegetableConstData {
    // �퓬�Ɏg�p�����؂̐�
    public static readonly int MAIN_VEGETABLES_COUNT = 3;

    // �퓬�̐�������
    public static readonly int TIMER = 60;

    // �f�t�H���g�̖�؂̕Ґ�
    public readonly static List<int> DefaultVegetableIds = new() { (int)Vegetable.VEGETABLE.Carrot, (int)Vegetable.VEGETABLE.CherryTomato, (int)Vegetable.VEGETABLE.Cabbage };

    // �Z�[�u�f�[�^�̃p�X
    // �Ґ����
    public static readonly string PARTY_DATA = "PartyData";
}
