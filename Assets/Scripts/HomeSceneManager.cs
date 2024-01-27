using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// �z�[���V�[���̐i�s���Ǘ�����
public class HomeSceneManager : MonoBehaviour
{
    // �퓬�J�n�{�^��
    [SerializeField] private Button fightButton = null;
    // ��V�󂯎��{�^��
    [SerializeField] private Button rewardRecievedButton = null;
    // �p�[�e�B�[�Ґ��{�^��
    [SerializeField] private Button partyEditButton = null;

    // ���u�����b���|���鉽�|�C���g���炦�邩
    [SerializeField] private int pointPerSecond = 1;

    private void Start() {
        // �{�^���̓o�^
        fightButton.onClick.RemoveAllListeners();
        fightButton.onClick.AddListener(OnClickFightButton);
        rewardRecievedButton.onClick.RemoveAllListeners();
        rewardRecievedButton.onClick.AddListener(OnClickRewardRecievedButton);
        partyEditButton.onClick.RemoveAllListeners();
        partyEditButton.onClick.AddListener(OnClickPartyEdittButton);
    }

    // �퓬�J�n�{�^�����������Ƃ�
    private void OnClickFightButton() {
        // SceneManager.LoadScene("BattleScene");
    }

    // ��V�󂯎��{�^�����������Ƃ�
    private void OnClickRewardRecievedButton() {
        APIController.Instance.Get("UserLeavingRewardTime", "getLastRecievedTime", getLastReardRecievedTime);
    }

    // �p�[�e�B�[�Ґ��{�^�����������Ƃ�
    private void OnClickPartyEdittButton() {
        SceneManager.LoadScene("PartyEditScene");
    }

    // 
    private void getLastReardRecievedTime(string data) {
        Debug.Log(data);
        // int parse = int.Parse(data);
        // int point = int.Parse(data) * pointPerSecond;
        // Debug.Log($"point : {parse}");
    }
}
