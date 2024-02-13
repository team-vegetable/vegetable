using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CI.QuickSave;
using UnityEditor;
using UnityEngine.Windows;

//�@�p�[�e�B�[�Ґ���ʂ�UI�̊Ǘ�
public class PartyEditUIHandler: MonoBehaviour
{
    // ��؂̃A�C�R���̃v���n�u
    [SerializeField] private UnitIcon vegetableIcon = null;

    // �o�g���V�[���ɑJ�ڂ�����{�^��(�e�X�g�p)
    [SerializeField] private Button transitionBattleSceneButton = null;
    // �ۑ��{�^��
    [SerializeField] private Button saveButton = null;
    // �퓬�Ɏg�p�����؂��i�[����e�I�u�W�F�N�g
    [SerializeField] private Transform mainVegetablesParent = null;
    // �T�u�̖�؂��i�[����e�I�u�W�F�N�g
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

        // �Z�[�u�f�[�^�����݂���Εۑ��f�[�^�ǂݍ���
        var path = Application.persistentDataPath + "/QuickSave/" + VegetableConstData.PARTY_DATA + ".json";
        if (File.Exists(path)) {
            QuickSaveReader reader = QuickSaveReader.Create(VegetableConstData.PARTY_DATA, settings);
            var mainVegetableIDs = reader.Read<List<int>>("MainVegetableIDs");

            // ���C���̖�؃A�C�R���̐���
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

        // ���C���̖�؃I�u�W�F�N�g���擾
        foreach (Transform child in mainVegetablesParent.transform) {
            mainVegetableObjects.Add(child.gameObject);
        }
        // �T�u�̖�؃I�u�W�F�N�g���擾
        foreach (Transform child in reserveVegetablesParent.transform) {
            reserveVegetableObjects.Add(child.gameObject);
        }
    }

    // �A�C�R���̓���ւ�
    private void SwitchIcon(GameObject moveIcon, GameObject hitIcon) {
        // �T�u���m�̓���ւ��͎󂯕t���Ȃ�(��肭�����Ȃ�)
        if (reserveVegetableObjects.Any(e => e == moveIcon) && reserveVegetableObjects.Any(e => e == hitIcon)) {
            return;
        }

        var moveSiblingIndex = moveIcon.transform.GetSiblingIndex();
        var hitSiblingIndex = hitIcon.transform.GetSiblingIndex();

        // ���C�����m�̓���ւ�
        if (mainVegetableObjects.Any(e => e == moveIcon) && mainVegetableObjects.Any(e => e == hitIcon)) {
            moveIcon.transform.SetSiblingIndex(hitSiblingIndex);
            hitIcon.transform.SetSiblingIndex(moveSiblingIndex);
        }
        else {
            moveIcon.transform.SetParent(null);
            hitIcon.transform.SetParent(null);

            // ���C������T�u�ɓ���ւ����ꍇ
            if (IsSwitchingMainToSub(moveIcon, hitIcon)) {
                moveIcon.transform.SetParent(reserveVegetablesParent);
                hitIcon.transform.SetParent(mainVegetablesParent);
                (reserveVegetableObjects[hitSiblingIndex], mainVegetableObjects[moveSiblingIndex]) = (mainVegetableObjects[moveSiblingIndex], reserveVegetableObjects[hitSiblingIndex]);
            }
            // �T�u���烁�C���ɓ���ւ����ꍇ
            else {
                moveIcon.transform.SetParent(mainVegetablesParent);
                hitIcon.transform.SetParent(reserveVegetablesParent);
                (reserveVegetableObjects[moveSiblingIndex], mainVegetableObjects[hitSiblingIndex]) = (mainVegetableObjects[hitSiblingIndex], reserveVegetableObjects[moveSiblingIndex]);
            }

            moveIcon.transform.SetSiblingIndex(hitSiblingIndex);
            hitIcon.transform.SetSiblingIndex(moveSiblingIndex);
        }
    }

    // ���C������T�u�ɓ���ւ������ǂ���
    private bool IsSwitchingMainToSub(GameObject moveIcon, GameObject hitIcon) {
        if (mainVegetableObjects.Any(e => e == moveIcon) && reserveVegetableObjects.Any(e => e == hitIcon)) {
            return true;
        }
        return false;
    }

    // �ۑ��{�^�����������Ƃ�
    private void OnClickSaveButton() {
        // �퓬�Ɏg�p�����؂̎擾
        List<int> mainVegetableIDs = new();
        foreach (Transform child in mainVegetablesParent.transform) {
            var id = child.GetComponent<UnitIcon>().Vegetable.ID;
            mainVegetableIDs.Add(id);
        }

        // �퓬�Ɏg�p�����؂̕ۑ�
        QuickSaveSettings settings = new() {
            //SecurityMode = SecurityMode.Aes,
            //Password = "Password",
            //CompressionMode = CompressionMode.Gzip,
        };

        QuickSaveWriter writer = QuickSaveWriter.Create(VegetableConstData.PARTY_DATA, settings);
        writer.Write("MainVegetableIDs", mainVegetableIDs);
        writer.Commit();
    }

    // �o�g���V�[���ɑJ�ڂ�����{�^�����������Ƃ�(�e�X�g�p)
    private void OnClickTransitionBattleSceneButton() {
        SceneManager.LoadScene("BattleScene");
    }

    // TODO : �����̏����͂���͂Ȃ�
    private List<T> LoadScriptableObjectFromFolder<T>(string folderPath) where T : ScriptableObject {
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { folderPath });
        if (guids.Length <= 0) {
            Debug.LogError("�p�X�̎w�肪�Ԉ���Ă��܂�");
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
