using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// �p�[�e�B�Ґ���ʂ̃��f��
public class PartyEditModel : MonoBehaviour {
    // ��؂̃A�C�R���̃v���n�u
    [SerializeField] private UnitIcon vegetableIcon = null;
    // �퓬�Ɏg�p�����؂��i�[����e�I�u�W�F�N�g
    [SerializeField] private Transform mainVegetablesParent = null;
    // �T�u�̖�؂��i�[����e�I�u�W�F�N�g
    [SerializeField] private Transform reserveVegetablesParent = null;

    // �퓬�Ŏg�p����I�u�W�F�N�g�̃��X�g
    private readonly List<GameObject> mainVegetableObjects = new();
    // �T�u�Ŏg�p����I�u�W�F�N�g�̃��X�g
    private readonly List<GameObject> reserveVegetableObjects = new();

    // ��؂̍��W
    private readonly List<Vector2> VEGETABLE_POSITIONS = new() {
        new(-152f, -351f), new(0, -92), new(183, 160)
    };

    private void Start() {
        // �Z�[�u�f�[�^�����݂���Εۑ��f�[�^�ǂݍ���
        if (QuickSave.Exists(VegetableConstData.PARTY_DATA)) {
            var mainVegetableIDs = QuickSave.Load<List<int>>(VegetableConstData.PARTY_DATA, "MainVegetableIDs");

            // ���C���̖�؃A�C�R���̐���
            var vegtableAssets = LoadAsset.LoadFromFolder<Vegetable>(LoadAsset.VEGETABLE_PATH);
            for (int index = 0; index < VegetableConstData.MAIN_VEGETABLES_COUNT; index++) {
                var asset = vegtableAssets.FirstOrDefault(e => e.ID == mainVegetableIDs[index]);
                if (asset == null) {
                    continue;
                }
                var icon = Instantiate(vegetableIcon, mainVegetablesParent);
                icon.GetComponent<RectTransform>().anchoredPosition = VEGETABLE_POSITIONS[index];
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
    private void SwitchIcon(UnitIcon moveIcon, UnitIcon hitIcon) {
        // �T�u���m�̓���ւ��͎󂯕t���Ȃ�(��肭�����Ȃ�)
        if (reserveVegetableObjects.Any(e => e == moveIcon) && reserveVegetableObjects.Any(e => e == hitIcon)) {
            return;
        }

        var moveSiblingIndex = moveIcon.transform.GetSiblingIndex();
        var hitSiblingIndex = hitIcon.transform.GetSiblingIndex();

        // ���C�����m�̓���ւ�
        if (mainVegetableObjects.Any(e => e == moveIcon.gameObject) && mainVegetableObjects.Any(e => e == hitIcon.gameObject)) {
            var moveBeforeDragPosition = moveIcon.BeforeDragPosition;
            var hitBeforeDragPosition = hitIcon.BeforeDragPosition;

            moveIcon.transform.position = hitIcon.BeforeDragPosition;
            hitIcon.transform.position = moveIcon.BeforeDragPosition;

            moveIcon.UpdateBeforeDragPosition(hitBeforeDragPosition);
            hitIcon.UpdateBeforeDragPosition(moveBeforeDragPosition);
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

    // �퓬�Ŏg�p�����؂̕ۑ����s��
    public void SaveMainVegetables() {
        // �퓬�Ɏg�p�����؂̎擾
        List<int> mainVegetableIDs = new();
        foreach (Transform child in mainVegetablesParent.transform) {
            var id = child.GetComponent<UnitIcon>().Vegetable.ID;
            mainVegetableIDs.Add(id);
        }

        // �퓬�Ɏg�p�����؂̕ۑ�
        QuickSave.Save(VegetableConstData.PARTY_DATA, "MainVegetableIDs", mainVegetableIDs);
    }

    // ���C������T�u�ɓ���ւ������ǂ���
    private bool IsSwitchingMainToSub(GameObject moveIcon, GameObject hitIcon) {
        if (mainVegetableObjects.Any(e => e == moveIcon) && reserveVegetableObjects.Any(e => e == hitIcon)) {
            return true;
        }
        return false;
    }
}
