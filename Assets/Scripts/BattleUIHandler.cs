using System.Collections;
using TMPro;
using UnityEngine;

// �o�g����UI������
public class BattleUIHandler : MonoBehaviour
{
    // �c�莞�Ԃ�\������e�L�X�g
    [SerializeField] private TextMeshProUGUI timeText = null;
    // �c��̓G�̐���\������e�L�X�g
    [SerializeField] private TextMeshProUGUI countText = null;

    // �c�艽�b����J�n���邩
    [SerializeField] private float timer = 60.0f;

    private void Start() {
        StartTimer();
    }

    // �c�莞�Ԃ̃^�C�}�[���X�^�[�g������
    public void StartTimer() {
        StartCoroutine(OnStartTimer());
    }

    private IEnumerator OnStartTimer() {
        while (true) {
            timer -= Time.deltaTime;
            timeText.text = $"�c��{(int)timer}�b";
            yield return null;
        }
    }

    // �c��̓G�̐����e�L�X�g�ɔ��f������
    public void SetCountText(int count) {
        countText.text = $"�c��{count}��";
    }
}
