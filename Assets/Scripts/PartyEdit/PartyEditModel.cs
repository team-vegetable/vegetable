using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// パーティ編成画面のモデル
public class PartyEditModel : MonoBehaviour {
    // 野菜のアイコンのプレハブ
    [SerializeField] private UnitIcon vegetableIcon = null;
    // 戦闘に使用する野菜を格納する親オブジェクト
    [SerializeField] private Transform mainVegetablesParent = null;
    // サブの野菜を格納する親オブジェクト
    [SerializeField] private Transform reserveVegetablesParent = null;
    // スクロール
    [SerializeField] private ScrollRect scrollRect = null;

    // 戦闘で使用するオブジェクトのリスト
    private readonly List<GameObject> mainVegetableObjects = new();
    // サブで使用するオブジェクトのリスト
    private readonly List<GameObject> reserveVegetableObjects = new();

    // 野菜の座標
    private readonly List<Vector2> VEGETABLE_POSITIONS = new() {
        new(-152f, -351f), new(0, -92), new(183, 160)
    };

    private void Start() {
        // セーブデータが存在すれば保存データ読み込み
        if (QuickSave.Exists(VegetableConstData.PARTY_DATA)) {
            var mainVegetableIDs = QuickSave.Load<List<int>>(VegetableConstData.PARTY_DATA, "MainVegetableIDs");

            // メインの野菜アイコンの生成
            var vegtableAssets = LoadAsset.LoadFromFolder<Vegetable>(LoadAsset.VEGETABLE_PATH);
            for (int index = 0; index < VegetableConstData.MAIN_VEGETABLES_COUNT; index++) {
                var asset = vegtableAssets.FirstOrDefault(e => e.ID == mainVegetableIDs[index]);
                if (asset == null) {
                    continue;
                }
                var icon = Instantiate(vegetableIcon, mainVegetablesParent);
                icon.GetComponent<RectTransform>().anchoredPosition = VEGETABLE_POSITIONS[index];
                icon.Init(asset, scrollRect, SwitchIcon);
            }

            for (int index = 0; index < 20; index++) {
                var asset = vegtableAssets.FirstOrDefault(e => e.ID == 1);
                if (asset == null) {
                    continue;
                }
                var icon = Instantiate(vegetableIcon, reserveVegetablesParent);
                // icon.GetComponent<RectTransform>().anchoredPosition = VEGETABLE_POSITIONS[index];
                icon.Init(asset, scrollRect, SwitchIcon);
            }
        }

        // メインの野菜オブジェクトを取得
        foreach (Transform child in mainVegetablesParent.transform) {
            mainVegetableObjects.Add(child.gameObject);
        }
        // サブの野菜オブジェクトを取得
        foreach (Transform child in reserveVegetablesParent.transform) {
            reserveVegetableObjects.Add(child.gameObject);
        }
    }

    // アイコンの入れ替え
    private void SwitchIcon(UnitIcon moveIcon, UnitIcon hitIcon) {
        // サブ同士の野菜の入れ替えは受け付けない
        if (IsSwitchingSub(moveIcon.gameObject, hitIcon.gameObject)) {
            return;
        }

        var moveSiblingIndex = moveIcon.transform.GetSiblingIndex();
        var hitSiblingIndex = hitIcon.transform.GetSiblingIndex();

        // メイン同士の入れ替え
        if (IsSwitchingMain(moveIcon.gameObject, hitIcon.gameObject)) {
            // 座標の入れ替え
            var moveBeforeDragPosition = moveIcon.BeforeDragPosition;
            var hitBeforeDragPosition = hitIcon.BeforeDragPosition;
            moveIcon.transform.position = hitIcon.BeforeDragPosition;
            hitIcon.transform.position = moveIcon.BeforeDragPosition;

            // 座標の更新
            moveIcon.UpdateBeforeDragPosition(hitBeforeDragPosition);
            hitIcon.UpdateBeforeDragPosition(moveBeforeDragPosition);
        }
        // メインとサブの入れ替え
        else {
            //moveIcon.transform.SetParent(null);
            //hitIcon.transform.SetParent(null);

            // メインからサブに入れ替えた場合
            if (IsSwitchingMainToSub(moveIcon.gameObject, hitIcon.gameObject)) {
                moveIcon.transform.SetParent(reserveVegetablesParent);
                hitIcon.transform.SetParent(mainVegetablesParent);
                hitIcon.transform.position = moveIcon.BeforeDragPosition;
            }
            // サブからメインに入れ替えた場合
            else {
                moveIcon.transform.SetParent(mainVegetablesParent);
                hitIcon.transform.SetParent(reserveVegetablesParent);
                moveIcon.transform.position = hitIcon.BeforeDragPosition;
            }

            //moveIcon.transform.SetSiblingIndex(hitSiblingIndex);
            //hitIcon.transform.SetSiblingIndex(moveSiblingIndex);
        }
    }

    // 戦闘で使用する野菜の保存を行う
    public void SaveMainVegetables() {
        // 戦闘に使用する野菜の取得
        List<int> mainVegetableIDs = new();
        foreach (Transform child in mainVegetablesParent.transform) {
            var id = child.GetComponent<UnitIcon>().Vegetable.ID;
            mainVegetableIDs.Add(id);
        }

        // 戦闘に使用する野菜の保存
        QuickSave.Save(VegetableConstData.PARTY_DATA, "MainVegetableIDs", mainVegetableIDs);
    }

    // サブ同士の野菜が入れ替わったかどうか
    private bool IsSwitchingSub(GameObject moveIcon, GameObject hitIcon) {
        return reserveVegetableObjects.Any(e => e == moveIcon) && reserveVegetableObjects.Any(e => e == hitIcon);
    }

    // メイン同士の野菜が入れ替わったかどうか
    private bool IsSwitchingMain(GameObject moveIcon, GameObject hitIcon) {
        return mainVegetableObjects.Any(e => e == moveIcon) && mainVegetableObjects.Any(e => e == hitIcon);
    }

    // メインからサブに入れ替えたかどうか
    private bool IsSwitchingMainToSub(GameObject moveIcon, GameObject hitIcon) {
        return mainVegetableObjects.Any(e => e == moveIcon) && reserveVegetableObjects.Any(e => e == hitIcon);
    }
}
