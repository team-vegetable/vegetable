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
    // �h���b�O�J�n�����Ƃ��̍��W
    private Vector3 initPosition = new();
    // �A�C�R�������ւ����Ƃ��̃C�x���g
    private UnityAction<GameObject, GameObject> onSwitch = null;

    // ��؂̃f�[�^
    public Vegetable Vegetable { get; private set; }

    public void Init(Vegetable vegetable, UnityAction<GameObject, GameObject> callback) {
        Vegetable = vegetable;
        onSwitch = callback;

        image = GetComponent<Image>();
        canvas = GetComponent<Canvas>();

        image.sprite = Vegetable.Icon;
    }

    // �h���b�O�J�n
    public void OnBeginDrag(PointerEventData eventData) {
        initPosition = transform.position;
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
            transform.position = initPosition;
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
            onSwitch?.Invoke(gameObject, hitIcon.gameObject);
        }
    }
}
