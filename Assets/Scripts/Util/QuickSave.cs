using CI.QuickSave;
using System.IO;
using UnityEngine;

// �Z�[�u�p�̃N���X
public static class QuickSave
{
    // �Z�[�u
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

    // ���[�h
    public static T Load<T>(string fileName, string key) {
        QuickSaveSettings settings = new() {
            //SecurityMode = SecurityMode.Aes,
            //Password = "Password",
            //CompressionMode = CompressionMode.Gzip,
        };

        var reader = QuickSaveReader.Create(fileName, settings);
        return reader.Read<T>(key);
    }

    // �Z�[�u�f�[�^�����݂��邩�ǂ���
    public static bool Exists(string fileName) {
        var path = Application.persistentDataPath + "/QuickSave/" + fileName + ".json";
        return File.Exists(path);
    }
}
