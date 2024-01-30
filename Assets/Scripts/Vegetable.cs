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

    [Header("�摜")]
    [SerializeField] private Sprite sprite = null;
    public Sprite Sprite { get => sprite; }


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

}
