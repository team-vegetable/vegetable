using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ResetPartyDataEditor : Editor
{
    private static readonly string path = Application.persistentDataPath + "/QuickSave/" + VegetableConstData.PARTY_DATA + ".json";

    // ID順にソート
    [MenuItem("Debug/メイン編成のリセット")]
    private static void ResetPartyData() {
        if (File.Exists(path)) {
            FileUtil.DeleteFileOrDirectory(path);
        }

        // 新しくセーブデータの作成
        List<int> mainVegetableIDs = new() { (int)Vegetable.VEGETABLE.Carrot, (int)Vegetable.VEGETABLE.CherryTomato, (int)Vegetable.VEGETABLE.Cabbage };
        QuickSaveSettings settings = new() {
            //SecurityMode = SecurityMode.Aes,
            //Password = "Password",
            //CompressionMode = CompressionMode.Gzip,
        };

        QuickSaveWriter writer = QuickSaveWriter.Create(VegetableConstData.PARTY_DATA, settings);
        writer.Write("MainVegetableIDs", mainVegetableIDs);
        writer.Commit();

        if (File.Exists(path)) {
            Debug.Log("<color=cyan>セーブデータの削除と新規のセーブデータの追加</color>");
        }
        else {
            Debug.Log("<color=cyan>新規のセーブデータの追加</color>");
        }
    }

}
