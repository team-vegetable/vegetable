using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

//�@�p�[�e�B�[�Ґ���ʂ�UI�̊Ǘ�
public class PartyEditUIHandler: MonoBehaviour
{
    // �ۑ��{�^��
    [SerializeField] private Button saveButton = null;
    // �퓬�Ɏg�p�����؂��i�[����e�I�u�W�F�N�g
    [SerializeField] private Transform mainVegetablesParent = null;
    // �T�u�̖�؂��i�[����e�I�u�W�F�N�g
    [SerializeField] private Transform reserveVegetablesParent = null;

    // �퓬�Ɏg�p�����؂��i�[����
    private readonly List<UnitIcon> mainVegetableIcons = new();
    private readonly List<Vegetable> mainVegetables = new();

    private readonly List<GameObject> mainVegetableObjects = new();
    private readonly List<GameObject> reserveVegetableObjects = new();

    private void Start() {
        saveButton.onClick.RemoveAllListeners();
        saveButton.onClick.AddListener(OnClickSaveButton);

        foreach (Transform child in mainVegetablesParent.transform) {
            mainVegetableObjects.Add(child.gameObject);
        }

        foreach (Transform child in reserveVegetablesParent.transform) {
            reserveVegetableObjects.Add(child.gameObject);
        }

        foreach (Transform child in mainVegetablesParent.transform) {
            var icon = child.GetComponent<UnitIcon>();
            icon.Init(SwitchIcon);
            mainVegetableIcons.Add(icon);
        }

        foreach (Transform child in reserveVegetablesParent.transform) {
            var icon = child.GetComponent<UnitIcon>();
            if (icon == null) {
                continue;
            }
            icon.Init(SwitchIcon);
            mainVegetableIcons.Add(icon);
        }
    }

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
        foreach (Transform child in mainVegetablesParent.transform) {
            var vegetable = child.GetComponent<UnitIcon>().Vegetable;
            mainVegetables.Add(vegetable);
        }

        // ���̃V�[���ł��g�p�ł���悤�ɕۑ�
        GameController.Instance.SetMainVegetables(mainVegetables);
    }
}
