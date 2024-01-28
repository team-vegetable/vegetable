using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// �X�e�[�W�Z���N�g�V�[���̐i�s���Ǘ�����
public class StageSelectSceneManager : MonoBehaviour
{
    // �z�[���{�^��
    [SerializeField] private Button homeButton = null;
    // �키�{�^��
    [SerializeField] private Button fightButton = null;
    

    private void Start() {
        // �{�^���̓o�^
        homeButton.onClick.RemoveAllListeners();
        homeButton.onClick.AddListener(OnClickHomeButton);
        fightButton.onClick.RemoveAllListeners();
        fightButton.onClick.AddListener(OnClickFightButton);
    }

    // �z�[���{�^�����������Ƃ�
    private void OnClickHomeButton() {
        SceneManager.LoadScene("HomeScene");
    }

    // �키�{�^�����������Ƃ�
    private void OnClickFightButton() {
        SceneManager.LoadScene("BattleScene");
    }
}
