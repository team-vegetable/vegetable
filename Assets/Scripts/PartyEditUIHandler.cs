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
    private readonly List<Vegetable> mainVegetables = new();

    private void Start() {
        saveButton.onClick.RemoveAllListeners();
        saveButton.onClick.AddListener(OnClickSaveButton);
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
