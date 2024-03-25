using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;

// �Ґ���ʂŎg�p���郆�j�b�g�̃A�C�R��
public class UnitIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler {
    // �摜
    private Image image = null;
    // �L�����o�X
    private Canvas canvas = null;
    // �X�N���[��
    private ScrollRect scrollRect = null;
    // ���������Ă��邩�ǂ���
    private bool isLongPress = false;
    // �X�N���[���Ώۂ��ǂ���
    private bool isSchollObject = false;

    // ��؂̃f�[�^
    public Vegetable Vegetable { get; private set; }
    // �h���b�O����O�̍��W
    public Vector3 BeforeDragPosition { get; private set; }

    // �A�C�R�������ւ����Ƃ��̃C�x���g
    private readonly Subject<(UnitIcon, UnitIcon)> onSwitch = new();
    public IObservable<(UnitIcon, UnitIcon)> OnSwitch => onSwitch;

    // ������
    public void Init(Vegetable vegetable, ScrollRect scrollRect, bool isSchollObject = false) {
        Vegetable = vegetable;
        this.scrollRect = scrollRect;
        this.isSchollObject = isSchollObject;

        image = GetComponent<Image>();
        canvas = GetComponent<Canvas>();

        image.sprite = Vegetable.BattleSprite;
        // BeforeDragPosition = new Vector3(transform.position.x, transform.position.y, 0.0f);
        //BeforeDragPosition = GetComponent<RectTransform>().localPosition;
        //Debug.Log(BeforeDragPosition);

        // �������̃C�x���g�̓o�^
        var eventTrigger = GetComponent<ObservableEventTrigger>();
        eventTrigger.OnPointerDownAsObservable().Select(_ => true)
            .Merge(eventTrigger.OnPointerDownAsObservable().Select(_ => false))
            .Throttle(TimeSpan.FromSeconds(0.5f))
            .AsUnitObservable()
            .TakeUntilDestroy(this)
            .Subscribe(_ => isLongPress = true);
    }

    // �h���b�O�J�n
    public void OnBeginDrag(PointerEventData eventData) {
        canvas.sortingOrder = 1;
    }

    // �h���b�O��
    public void OnDrag(PointerEventData eventData) {
        // �������łȂ���΁A�X�N���[���r���[�𓮂���
        if (isSchollObject && !isLongPress) {
            SetScrollView(eventData);
            return;
        }

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
            transform.localPosition = Vector3.zero;
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
            onSwitch.OnNext((this, hitIcon));
        }
    }

    // �N���b�N�����Ƃ�
    public void OnPointerDown(PointerEventData eventData) {
        isLongPress = false;
    }

    // �h���b�O����O�̍��W�̍X�V
    public void UpdateBeforeDragPosition(Vector3 position) {
        BeforeDragPosition = new Vector3(position.x, position.y, 0.0f);
    }

    // �X�N���[���r���[�Ɋ�����킹��
    private void SetScrollView(PointerEventData pointerEventData) {
        pointerEventData.pointerDrag = scrollRect.gameObject;
        EventSystem.current.SetSelectedGameObject(scrollRect.gameObject);
        scrollRect.OnInitializePotentialDrag(pointerEventData);
        scrollRect.OnBeginDrag(pointerEventData);
    }
}
