using System.Collections.Generic;
using UnityEngine;
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
    private readonly List<Vegetable> mainVegetables = new();

    private void Start() {
        saveButton.onClick.RemoveAllListeners();
        saveButton.onClick.AddListener(OnClickSaveButton);
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
