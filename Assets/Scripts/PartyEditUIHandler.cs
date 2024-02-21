using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        // �Z�[�u�f�[�^�����݂���Εۑ��f�[�^�ǂݍ���
        if (QuickSave.Exists(VegetableConstData.PARTY_DATA)) {
            var mainVegetableIDs = QuickSave.Load<List<int>>(VegetableConstData.PARTY_DATA, "MainVegetableIDs");

            // ���C���̖�؃A�C�R���̐���
            var vegtableAssets = LoadAsset.LoadFromFolder<Vegetable>(LoadAsset.VEGETABLE_PATH);
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
            // ���C���ƃT�u�̓���ւ��͈�U�R�����g�A�E�g
            //moveIcon.transform.SetParent(null);
            //hitIcon.transform.SetParent(null);

            //// ���C������T�u�ɓ���ւ����ꍇ
            //if (IsSwitchingMainToSub(moveIcon, hitIcon)) {
            //    moveIcon.transform.SetParent(reserveVegetablesParent);
            //    hitIcon.transform.SetParent(mainVegetablesParent);
            //    (reserveVegetableObjects[hitSiblingIndex], mainVegetableObjects[moveSiblingIndex]) = (mainVegetableObjects[moveSiblingIndex], reserveVegetableObjects[hitSiblingIndex]);
            //}
            //// �T�u���烁�C���ɓ���ւ����ꍇ
            //else {
            //    moveIcon.transform.SetParent(mainVegetablesParent);
            //    hitIcon.transform.SetParent(reserveVegetablesParent);
            //    (reserveVegetableObjects[moveSiblingIndex], mainVegetableObjects[hitSiblingIndex]) = (mainVegetableObjects[hitSiblingIndex], reserveVegetableObjects[moveSiblingIndex]);
            //}

            //moveIcon.transform.SetSiblingIndex(hitSiblingIndex);
            //hitIcon.transform.SetSiblingIndex(moveSiblingIndex);
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
        QuickSave.Save(VegetableConstData.PARTY_DATA, "MainVegetableIDs", mainVegetableIDs);
    }

    // �o�g���V�[���ɑJ�ڂ�����{�^�����������Ƃ�(�e�X�g�p)
    private void OnClickTransitionBattleSceneButton() {
        SceneManager.LoadScene("BattleScene");
    }
}
