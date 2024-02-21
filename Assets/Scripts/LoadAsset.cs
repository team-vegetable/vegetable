using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// アセットをロードする
public static class LoadAsset
{
    // 野菜のScraiptbleObjectを格納するパス
    public static readonly string VEGETABLE_PATH = "Assets/ScriptableObjects/Vegetable";
    // 動物のScraiptbleObjectを格納するパス
    public static readonly string ANIMAL_PATH = "Assets/ScriptableObjects/Animal";

    // 指定したフォルダーから全てのScraiptableObjectを取得する
    public static List<T> LoadFromFolder<T>(string folderPath) where T : ScriptableObject {
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { folderPath });
        if (guids.Length <= 0) {
            Debug.LogError("パスの指定が間違っています");
            return null;
        }

        List<T> list = new();
        foreach (var guid in guids) {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            list.Add(asset);
        }
        return list;
    }
}
