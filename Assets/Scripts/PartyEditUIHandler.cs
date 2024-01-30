using System.Collections.Generic;
using UnityEngine;
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
    }

    private void SwitchIcon(GameObject moveIcon, GameObject hitIcon) {
        var moveSiblingIndex = moveIcon.transform.GetSiblingIndex();
        var hitSiblingIndex = hitIcon.transform.GetSiblingIndex();

        // TODO : ���C���ǂ����̓���ւ��Ȃ�Ή��ł��Ă��邯�ǁA���C���ƃT�u�̓���ւ��ɑΉ��ł��Ȃ�
        moveIcon.transform.SetSiblingIndex(hitSiblingIndex);
        hitIcon.transform.SetSiblingIndex(moveSiblingIndex);
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
