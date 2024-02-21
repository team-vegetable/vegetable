using CI.QuickSave;
using System.IO;
using UnityEngine;

// セーブ用のクラス
public static class QuickSave
{
    // セーブ
    public static void Save<T>(string fileName, string key, T data) {
        QuickSaveSettings settings = new() {
            //SecurityMode = SecurityMode.Aes,
            //Password = "Password",
            //CompressionMode = CompressionMode.Gzip,
        };

        QuickSaveWriter writer = QuickSaveWriter.Create(fileName, settings);
        writer.Write(key, data);
        writer.Commit();
    }

    // ロード
    public static T Load<T>(string fileName, string key) {
        QuickSaveSettings settings = new() {
            //SecurityMode = SecurityMode.Aes,
            //Password = "Password",
            //CompressionMode = CompressionMode.Gzip,
        };

        var reader = QuickSaveReader.Create(fileName, settings);
        return reader.Read<T>(key);
    }

    // セーブデータが存在するかどうか
    public static bool Exists(string fileName) {
        var path = Application.persistentDataPath + "/QuickSave/" + fileName + ".json";
        return File.Exists(path);
    }
}
