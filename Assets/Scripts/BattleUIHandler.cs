using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

// �o�g����UI������
public class BattleUIHandler : MonoBehaviour
{
    // �c�莞�Ԃ�\������e�L�X�g
    [SerializeField] private TextMeshProUGUI timerText = null;
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
            timerText.text = $"�c�莞�� : {(int)timer}�b";
            yield return null;
        }
    }
}
