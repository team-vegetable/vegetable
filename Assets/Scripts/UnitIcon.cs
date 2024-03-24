using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// �Ґ���ʂŎg�p���郆�j�b�g�̃A�C�R��
public class UnitIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // �摜
    private Image image = null;
    // �L�����o�X
    private Canvas canvas = null;
    // �A�C�R�������ւ����Ƃ��̃C�x���g
    private UnityAction<UnitIcon, UnitIcon> onSwitch = null;

    // ��؂̃f�[�^
    public Vegetable Vegetable { get; private set; }
    // �h���b�O����O�̍��W
    public Vector3 BeforeDragPosition { get; private set; }

    public void Init(Vegetable vegetable, UnityAction<UnitIcon, UnitIcon> callback) {
        Vegetable = vegetable;
        onSwitch = callback;

        image = GetComponent<Image>();
        canvas = GetComponent<Canvas>();

        image.sprite = Vegetable.BattleSprite;
        BeforeDragPosition = new Vector3(transform.position.x, transform.position.y, 0.0f);
    }

    // �h���b�O�J�n
    public void OnBeginDrag(PointerEventData eventData) {
        canvas.sortingOrder = 1;
    }

    // �h���b�O��
    public void OnDrag(PointerEventData eventData) {
        var worldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
    }

    // �h���b�O�I��
    public void OnEndDrag(PointerEventData eventData) {
        canvas.sortingOrder = 0;
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        // ����������ւ��邱�Ƃ��ł��Ȃ��������͏������W�ɖ߂�
        if (raycastResults.Count == 1 && raycastResults[0].gameObject == gameObject) {
            transform.position = BeforeDragPosition;
            return;
        }

        foreach (var item in raycastResults) {
            if (item.gameObject == gameObject) {
                continue;
            }

            if (!item.gameObject.TryGetComponent<UnitIcon>(out var hitIcon)) {
                continue;
            }

            // �A�C�R���̓���ւ�
            onSwitch?.Invoke(this, hitIcon);
        }
    }

    // �h���b�O����O�̍��W�̍X�V
    public void UpdateBeforeDragPosition(Vector3 position) {
        BeforeDragPosition = new Vector3(position.x, position.y, 0.0f);
    }
}
