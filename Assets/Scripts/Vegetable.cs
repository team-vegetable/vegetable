using UnityEngine;

// 野菜のパラメーター関連
[CreateAssetMenu(fileName = "Vegetable")]
public class Vegetable : ScriptableObject
{
    [Header("ID")]
    [SerializeField] private int id = 0;
    public int ID { get => id; }

    [Header("名前")]
    [SerializeField] private new string name = "";
    public string Name { get => name; }


    // 野菜ごとのID
    public enum VEGETABLE {
        // 人参
        Carrot = 1,
        // ミニトマト
        CherryTomato,
        // キャベツ
        Cabbage
    }
}
