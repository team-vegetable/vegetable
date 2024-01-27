using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// �Ґ���ʂŎg�p���郆�j�b�g�̃A�C�R��
public class UnitIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // �L�����o�X
    private Canvas canvas = null;
    // �h���b�O�J�n�����Ƃ��̍��W
    private Vector3 initPosition = new();

    // �V�u�����O�C���f�b�N�X
    public int SiblingIndex { get; private set; } = 0;

    private void Start() {
        canvas = GetComponent<Canvas>();
        SiblingIndex = transform.GetSiblingIndex();
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

            int otherSiblingIndex = hitIcon.SiblingIndex;
            hitIcon.SetSibligIndex(SiblingIndex);
            SiblingIndex = otherSiblingIndex;
            transform.SetSiblingIndex(SiblingIndex);
        }
    }

    // �q�G�����L�[�̏��Ԃ̕ύX
    public void SetSibligIndex(int index) {
        SiblingIndex = index;
        transform.SetSiblingIndex(SiblingIndex);
    }
}
