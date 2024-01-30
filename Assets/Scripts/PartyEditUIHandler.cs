using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

//　パーティー編成画面のUIの管理
public class PartyEditUIHandler: MonoBehaviour
{
    // 保存ボタン
    [SerializeField] private Button saveButton = null;
    // 戦闘に使用する野菜を格納する親オブジェクト
    [SerializeField] private Transform mainVegetablesParent = null;
    // サブの野菜を格納する親オブジェクト
    [SerializeField] private Transform reserveVegetablesParent = null;

    // 戦闘に使用する野菜を格納する
    private readonly List<UnitIcon> mainVegetableIcons = new();
    private readonly List<Vegetable> mainVegetables = new();

    private readonly List<GameObject> mainVegetableObjects = new();
    private readonly List<GameObject> reserveVegetableObjects = new();

    private void Start() {
        saveButton.onClick.RemoveAllListeners();
        saveButton.onClick.AddListener(OnClickSaveButton);

        foreach (Transform child in mainVegetablesParent.transform) {
            mainVegetableObjects.Add(child.gameObject);
        }

        foreach (Transform child in reserveVegetablesParent.transform) {
            reserveVegetableObjects.Add(child.gameObject);
        }

        foreach (Transform child in mainVegetablesParent.transform) {
            var icon = child.GetComponent<UnitIcon>();
            icon.Init(SwitchIcon);
            mainVegetableIcons.Add(icon);
        }

        foreach (Transform child in reserveVegetablesParent.transform) {
            var icon = child.GetComponent<UnitIcon>();
            if (icon == null) {
                continue;
            }
            icon.Init(SwitchIcon);
            mainVegetableIcons.Add(icon);
        }
    }

    private void SwitchIcon(GameObject moveIcon, GameObject hitIcon) {
        // サブ同士の入れ替えは受け付けない(上手く動かない)
        if (reserveVegetableObjects.Any(e => e == moveIcon) && reserveVegetableObjects.Any(e => e == hitIcon)) {
            return;
        }

        var moveSiblingIndex = moveIcon.transform.GetSiblingIndex();
        var hitSiblingIndex = hitIcon.transform.GetSiblingIndex();

        // メイン同士の入れ替え
        if (mainVegetableObjects.Any(e => e == moveIcon) && mainVegetableObjects.Any(e => e == hitIcon)) {
            moveIcon.transform.SetSiblingIndex(hitSiblingIndex);
            hitIcon.transform.SetSiblingIndex(moveSiblingIndex);
        }
        else {
            moveIcon.transform.SetParent(null);
            hitIcon.transform.SetParent(null);

            // メインからサブに入れ替えた場合
            if (IsSwitchingMainToSub(moveIcon, hitIcon)) {
                moveIcon.transform.SetParent(reserveVegetablesParent);
                hitIcon.transform.SetParent(mainVegetablesParent);
                (reserveVegetableObjects[hitSiblingIndex], mainVegetableObjects[moveSiblingIndex]) = (mainVegetableObjects[moveSiblingIndex], reserveVegetableObjects[hitSiblingIndex]);
            }
            // サブからメインに入れ替えた場合
            else {
                moveIcon.transform.SetParent(mainVegetablesParent);
                hitIcon.transform.SetParent(reserveVegetablesParent);
                (reserveVegetableObjects[moveSiblingIndex], mainVegetableObjects[hitSiblingIndex]) = (mainVegetableObjects[hitSiblingIndex], reserveVegetableObjects[moveSiblingIndex]);
            }

            moveIcon.transform.SetSiblingIndex(hitSiblingIndex);
            hitIcon.transform.SetSiblingIndex(moveSiblingIndex);
        }
    }

    // メインからサブに入れ替えたかどうか
    private bool IsSwitchingMainToSub(GameObject moveIcon, GameObject hitIcon) {
        if (mainVegetableObjects.Any(e => e == moveIcon) && reserveVegetableObjects.Any(e => e == hitIcon)) {
            return true;
        }
        return false;
    }

    // 保存ボタンを押したとき
    private void OnClickSaveButton() {
        // 戦闘に使用する野菜の取得
        foreach (Transform child in mainVegetablesParent.transform) {
            var vegetable = child.GetComponent<UnitIcon>().Vegetable;
            mainVegetables.Add(vegetable);
        }

        // 他のシーンでも使用できるように保存
        GameController.Instance.SetMainVegetables(mainVegetables);
    }
}
