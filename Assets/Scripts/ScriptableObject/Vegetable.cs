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

    [Header("バトルに使用するステータス")]
    [SerializeField] private VegetableBattleStatus battleStatus = null;
    public VegetableBattleStatus BattleStatus { get => battleStatus; }

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

// バトル用の野菜のステータス
[System.Serializable]
public class VegetableBattleStatus {
    [Header("最大HP")]
    [SerializeField] private int maxHP = 0;
    public int MaxHP { get => maxHP; }

    [Header("攻撃力")]
    [SerializeField] private int attack = 0;
    public int Attack { get => attack; }

    [Header("攻撃範囲")]
    [SerializeField] private int attackRange = 0;
    public int AttackRange { get => attackRange; }
}

// 定数の管理
public static class VegetableConstData {
    // 戦闘に使用する野菜の数
    public static readonly int MAIN_VEGETABLES_COUNT = 3;

    // セーブデータのパス
    // 編成状態
    public static readonly string PARTY_DATA = "PartyData";
}
