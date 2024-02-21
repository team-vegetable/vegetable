using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ResetPartyDataEditor : Editor
{
    private static readonly string path = Application.persistentDataPath + "/QuickSave/" + VegetableConstData.PARTY_DATA + ".json";

    // �p�[�e�B�[�Ґ������Z�b�g���Ă���V�K�ō쐬����
    [MenuItem("Debug/���C���Ґ��̃��Z�b�g")]
    private static void ResetPartyData() {
        if (File.Exists(path)) {
            FileUtil.DeleteFileOrDirectory(path);
        }

        // �V�����Z�[�u�f�[�^�̍쐬
        List<int> mainVegetableIDs = new() { (int)Vegetable.VEGETABLE.Carrot, (int)Vegetable.VEGETABLE.CherryTomato, (int)Vegetable.VEGETABLE.Cabbage };
        QuickSave.Save(VegetableConstData.PARTY_DATA, "MainVegetableIDs", mainVegetableIDs);

        //QuickSaveSettings settings = new() {
        //    //SecurityMode = SecurityMode.Aes,
        //    //Password = "Password",
        //    //CompressionMode = CompressionMode.Gzip,
        //};

        //QuickSaveWriter writer = QuickSaveWriter.Create(VegetableConstData.PARTY_DATA, settings);
        //writer.Write("MainVegetableIDs", mainVegetableIDs);
        //writer.Commit();

        if (File.Exists(path)) {
            Debug.Log("<color=cyan>�Z�[�u�f�[�^�̍폜�ƐV�K�̃Z�[�u�f�[�^�̒ǉ�</color>");
        }
        else {
            Debug.Log("<color=cyan>�V�K�̃Z�[�u�f�[�^�̒ǉ�</color>");
        }
    }

}
