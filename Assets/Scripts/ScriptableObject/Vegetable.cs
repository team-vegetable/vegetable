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

    [Header("�o�g���Ɏg�p����摜")]
    [SerializeField] private Sprite battleSprite = null;
    public Sprite BattleSprite { get => battleSprite; }

    [Header("�A�C�R��")]
    [SerializeField] private Sprite icon = null;
    public Sprite Icon { get => icon; }


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

// �萔�̊Ǘ�
public static class VegetableConstData {
    // �퓬�Ɏg�p�����؂̐�
    public static readonly int MAIN_VEGETABLES_COUNT = 3;

    // �Z�[�u�f�[�^�̃p�X
    // �Ґ����
    public static readonly string PARTY_DATA = "PartyData";
}
