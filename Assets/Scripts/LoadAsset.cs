using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// �A�Z�b�g�����[�h����
public static class LoadAsset
{
    // ��؂�ScraiptbleObject���i�[����p�X
    public static readonly string VEGETABLE_PATH = "Assets/ScriptableObjects/Vegetable";
    // ������ScraiptbleObject���i�[����p�X
    public static readonly string ANIMAL_PATH = "Assets/ScriptableObjects/Animal";

    // �w�肵���t�H���_�[����S�Ă�ScraiptableObject���擾����
    public static List<T> LoadFromFolder<T>(string folderPath) where T : ScriptableObject {
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { folderPath });
        if (guids.Length <= 0) {
            Debug.LogError("�p�X�̎w�肪�Ԉ���Ă��܂�");
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