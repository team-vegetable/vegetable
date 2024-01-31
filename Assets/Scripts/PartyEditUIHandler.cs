using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CI.QuickSave;
using UnityEditor;
using UnityEngine.Windows;

//　パーティー編成画面のUIの管理
public class PartyEditUIHandler: MonoBehaviour
{
    // 野菜のアイコンのプレハブ
    [SerializeField] private UnitIcon vegetableIcon = null;

    // バトルシーンに遷移させるボタン(テスト用)
    [SerializeField] private Button transitionBattleSceneButton = null;
    // 保存ボタン
    [SerializeField] private Button saveButton = null;
    // 戦闘に使用する野菜を格納する親オブジェクト
    [SerializeField] private Transform mainVegetablesParent = null;
    // サブの野菜を格納する親オブジェクト
    [SerializeField] private Transform reserveVegetablesParent = null;

    private readonly List<GameObject> mainVegetableObjects = new();
    private readonly List<GameObject> reserveVegetableObjects = new();

    private void Start() {
        saveButton.onClick.RemoveAllListeners();
        saveButton.onClick.AddListener(OnClickSaveButton);

        transitionBattleSceneButton.onClick.RemoveAllListeners();
        transitionBattleSceneButton.onClick.AddListener(OnClickTransitionBattleSceneButton);

        QuickSaveSettings settings = new() {
            //SecurityMode = SecurityMode.Aes,
            //Password = "Password",
            //CompressionMode = CompressionMode.Gzip,
        };

        // セーブデータが存在すれば保存データ読み込み
        var path = Application.persistentDataPath + "/QuickSave/" + VegetableConstData.PARTY_DATA + ".json";
        if (File.Exists(path)) {
            QuickSaveReader reader = QuickSaveReader.Create(VegetableConstData.PARTY_DATA, settings);
            var mainVegetableIDs = reader.Read<List<int>>("MainVegetableIDs");

            // メインの野菜アイコンの生成
            var vegtableAssets = LoadScriptableObjectFromFolder<Vegetable>("Assets/ScriptableObjects");
            foreach (var id in mainVegetableIDs) {
                var asset = vegtableAssets.FirstOrDefault(e => e.ID == id);
                if (asset == null) {
                    continue;
                }
                var icon = Instantiate(vegetableIcon, mainVegetablesParent);
                icon.Init(asset, SwitchIcon);
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
        List<int> mainVegetableIDs = new();
        foreach (Transform child in mainVegetablesParent.transform) {
            var id = child.GetComponent<UnitIcon>().Vegetable.ID;
            mainVegetableIDs.Add(id);
        }

        // 戦闘に使用する野菜の保存
        QuickSaveSettings settings = new() {
            //SecurityMode = SecurityMode.Aes,
            //Password = "Password",
            //CompressionMode = CompressionMode.Gzip,
        };

        QuickSaveWriter writer = QuickSaveWriter.Create(VegetableConstData.PARTY_DATA, settings);
        writer.Write("MainVegetableIDs", mainVegetableIDs);
        writer.Commit();
    }

    // バトルシーンに遷移させるボタンを押したとき(テスト用)
    private void OnClickTransitionBattleSceneButton() {
        SceneManager.LoadScene("BattleScene");
    }

    // TODO : ここの処理はきりはなす
    private List<T> LoadScriptableObjectFromFolder<T>(string folderPath) where T : ScriptableObject {
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { folderPath });
        if (guids.Length <= 0) {
            Debug.LogError("パスの指定が間違っています");
            return null;
        }

        List<T> vegetables = new();
        foreach (var guid in guids) {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            vegetables.Add(asset);
        }
        return vegetables;
    }
}
