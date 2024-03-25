using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

// �p�[�e�B�Ґ���ʂ̃��f��
public class PartyEditModel : MonoBehaviour {
    // ��؂̃A�C�R���̃v���n�u
    [SerializeField] private GameObject vegetableIcon = null;
    // �퓬�Ɏg�p�����؂��i�[����e�I�u�W�F�N�g
    [SerializeField] private Transform mainVegetablesParent = null;
    // �T�u�̖�؂��i�[����e�I�u�W�F�N�g
    [SerializeField] private Transform reserveVegetablesParent = null;
    // �X�N���[��
    [SerializeField] private ScrollRect scrollRect = null;

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

                var prefab = Instantiate(vegetableIcon, mainVegetablesParent);
                prefab.GetComponent<RectTransform>().anchoredPosition = VEGETABLE_POSITIONS[index];

                var icon = prefab.transform.GetChild(0).GetComponent<UnitIcon>();
                icon.Init(asset, scrollRect, false);
                icon.OnSwitch.Subscribe(pair => {
                    var (moveIcon, hitIcon) = pair;
                    SwitchIcon(moveIcon, hitIcon);
                }).AddTo(this);
            }

            for (int index = 0; index < 5; index++) {
                var asset = vegtableAssets.FirstOrDefault(e => e.ID == 1);
                if (asset == null) {
                    continue;
                }

                var prefab = Instantiate(vegetableIcon, reserveVegetablesParent);
                var icon = prefab.transform.GetChild(0).GetComponent<UnitIcon>();
                icon.Init(asset, scrollRect, false);
                icon.OnSwitch.Subscribe(pair => {
                    var (moveIcon, hitIcon) = pair;
                    SwitchIcon(moveIcon, hitIcon);
                }).AddTo(this);
            }
        }

        // ���C���̖�؃I�u�W�F�N�g���擾
        foreach (Transform child in mainVegetablesParent.transform) {
            mainVegetableObjects.Add(child.GetChild(0).gameObject);
        }
        // �T�u�̖�؃I�u�W�F�N�g���擾
        foreach (Transform child in reserveVegetablesParent.transform) {
            reserveVegetableObjects.Add(child.GetChild(0).gameObject);
        }
    }

    // �A�C�R���̓���ւ�
    private void SwitchIcon(UnitIcon moveIcon, UnitIcon hitIcon) {
        // �T�u���m�̖�؂̓���ւ��͎󂯕t���Ȃ�
        if (IsSwitchingSub(moveIcon.gameObject, hitIcon.gameObject)) {
            return;
        }

        // ���C�����m�̓���ւ�
        if (IsSwitchingMain(moveIcon.gameObject, hitIcon.gameObject)) {
            var moveIconParent = moveIcon.transform.parent;
            var hitIconParent = hitIcon.transform.parent;
            moveIcon.transform.SetParent(hitIconParent);
            hitIcon.transform.SetParent(moveIconParent);
            moveIcon.transform.localPosition = Vector3.zero;
            hitIcon.transform.localPosition = Vector3.zero;
        }
        // ���C���ƃT�u�̓���ւ�
        else {
            //// ���C������T�u�ɓ���ւ����ꍇ
            //if (IsSwitchingMainToSub(moveIcon.gameObject, hitIcon.gameObject)) {
            //    moveIcon.transform.SetParent(reserveVegetablesParent);
            //    hitIcon.transform.SetParent(mainVegetablesParent);
            //    hitIcon.transform.position = moveIcon.BeforeDragPosition;
            //}
            //// �T�u���烁�C���ɓ���ւ����ꍇ
            //else {
            //    moveIcon.transform.SetParent(mainVegetablesParent);
            //    hitIcon.transform.SetParent(reserveVegetablesParent);
            //    moveIcon.transform.position = hitIcon.BeforeDragPosition;
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

    // �T�u���m�̖�؂�����ւ�������ǂ���
    private bool IsSwitchingSub(GameObject moveIcon, GameObject hitIcon) {
        return reserveVegetableObjects.Any(e => e == moveIcon) && reserveVegetableObjects.Any(e => e == hitIcon);
    }

    // ���C�����m�̖�؂�����ւ�������ǂ���
    private bool IsSwitchingMain(GameObject moveIcon, GameObject hitIcon) {
        return mainVegetableObjects.Any(e => e == moveIcon) && mainVegetableObjects.Any(e => e == hitIcon);
    }

    // ���C������T�u�ɓ���ւ������ǂ���
    private bool IsSwitchingMainToSub(GameObject moveIcon, GameObject hitIcon) {
        return mainVegetableObjects.Any(e => e == moveIcon) && reserveVegetableObjects.Any(e => e == hitIcon);
    }
}
