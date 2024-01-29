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
