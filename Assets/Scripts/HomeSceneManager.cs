using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// �z�[���V�[���̐i�s���Ǘ�����
public class HomeSceneManager : MonoBehaviour
{
    // �p�[�e�B�[�Ґ��{�^��
    [SerializeField] private Button partyEditButton = null;
    // ��V�󂯎��{�^��
    [SerializeField] private Button rewardRecievedButton = null;
    // �X�e�[�W�Z���N�g�{�^��
    [SerializeField] private Button stageSelectButton = null;

    // ���u�����b���|���鉽�|�C���g���炦�邩
    [SerializeField] private int pointPerSecond = 1;

    // �O���V���󂯎��������
    private DateTime prevTime = default;

    private void Start() {
        // �{�^���̓o�^
        partyEditButton.onClick.RemoveAllListeners();
        partyEditButton.onClick.AddListener(OnClickPartyEdittButton);
        rewardRecievedButton.onClick.RemoveAllListeners();
        rewardRecievedButton.onClick.AddListener(OnClickRewardRecievedButton);
        stageSelectButton.onClick.RemoveAllListeners();
        stageSelectButton.onClick.AddListener(OnStageSelectButton);
    }

    // �p�[�e�B�[�Ґ��{�^�����������Ƃ�
    private void OnClickPartyEdittButton() {
        SceneManager.LoadScene("PartyEditScene");
    }

    // ��V�󂯎��{�^�����������Ƃ�
    private void OnClickRewardRecievedButton() {
        if (prevTime != default) {
            var diff = DateTime.Now - prevTime;
            Debug.Log($"{diff.TotalSeconds}�b�ԕ��u���Ă��܂���");
        } 
        else {
            Debug.Log("���߂ĕ��u��V���󂯎����");
        }
        

        prevTime = DateTime.Now;
    }

    // �X�e�[�W�Z���N�g�{�^�����������Ƃ�
    private void OnStageSelectButton() {
        SceneManager.LoadScene("StageSelectScene");
    }
}
