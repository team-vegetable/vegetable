using System.Collections;
using TMPro;
using UnityEngine;

// �o�g����UI������
public class BattleUIHandler : MonoBehaviour
{
    // �c�莞�Ԃ�\������e�L�X�g
    [SerializeField] private TextMeshProUGUI timerText = null;

    private float timer = 60;

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
            timerText.text = $"�c�莞�� : {(int)timer}�b";
            yield return null;
        }
    }
}
