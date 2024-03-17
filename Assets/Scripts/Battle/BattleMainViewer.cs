using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// �o�g���̕\���֌W������
public class BattleMainViewer : MonoBehaviour
{
    // ��؂̃A�C�R��
    [SerializeField] private List<Image> icons = null;
    // �c�莞�Ԃ�\������e�L�X�g
    [SerializeField] private TextMeshProUGUI timeText = null;
    // �|�����G�̐���\������e�L�X�g
    [SerializeField] private TextMeshProUGUI countText = null;

    // �s�k����UI
    [SerializeField] private LoseUI loseUI = null;
     
    // �c�艽�b����J�n���邩
    [SerializeField] private float timer = 60.0f;

    // �c�莞�Ԃ̃^�C�}�[���X�^�[�g������
    public async UniTask StartTimer() {
        while (true) {
            await UniTask.Yield();
            timer -= Time.deltaTime;
            timeText.text = $"�c��{(int)timer}�b";
        }
    }

    // ��؂̃A�C�R���̃Z�b�g
    public void SetIcon(Sprite sprite, int index) {
        icons[index].sprite = sprite;
    }

    // �|�����G�̐����e�L�X�g�ɔ��f������
    public void SetCountText(int count) {
        countText.text = $"�|�����G : {count}��";
    }
}
