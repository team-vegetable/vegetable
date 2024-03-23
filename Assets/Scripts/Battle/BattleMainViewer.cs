using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
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

    // ������
    public void Init() {
        SetCountText(0);
    }

    // �c�莞�Ԃ̃^�C�}�[���X�^�[�g������
    public void SetTimerText(float timer) {
        timeText.text = $"�c��{(int)timer}�b";
    }

    // ��؂̃A�C�R���̃Z�b�g
    public void SetIcon(Sprite sprite, int index) {
        icons[index].sprite = sprite;
    }

    // ��؂̃A�C�R���̃Z�b�g
    public void SetIcon(List<Vegetable> vegetables) {
        for (int index = 0; index < icons.Count; index++) {
            icons[index].sprite = vegetables[index].Icon;
        }
    }

    // �|�����G�̐����e�L�X�g�ɔ��f������
    public void SetCountText(int count) {
        countText.text = $"�|�����G : {count}��";
    }
}
