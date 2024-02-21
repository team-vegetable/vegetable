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

    [Header("バトルに使用する画像")]
    [SerializeField] private Sprite battleSprite = null;
    public Sprite BattleSprite { get => battleSprite; }

    [Header("アイコン")]
    [SerializeField] private Sprite icon = null;
    public Sprite Icon { get => icon; }


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

// 定数の管理
public static class VegetableConstData {
    // 戦闘に使用する野菜の数
    public static readonly int MAIN_VEGETABLES_COUNT = 3;

    // セーブデータのパス
    // 編成状態
    public static readonly string PARTY_DATA = "PartyData";
}
