using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;

// 編成画面で使用するユニットのアイコン
public class UnitIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler {
    // 画像
    private Image image = null;
    // キャンバス
    private Canvas canvas = null;
    // アイコンを入れ替えたときのイベント
    private UnityAction<UnitIcon, UnitIcon> onSwitch = null;
    // スクロール
    private ScrollRect scrollRect = null;
    // クリックしたときの座標
    private Vector2 downPosition = new();
    // 長押ししているかどうか
    private bool isLongPress = false;

    // 野菜のデータ
    public Vegetable Vegetable { get; private set; }
    // ドラッグする前の座標
    public Vector3 BeforeDragPosition { get; private set; }

    public void Init(Vegetable vegetable, ScrollRect scrollRect, UnityAction<UnitIcon, UnitIcon> callback) {
        Vegetable = vegetable;
        onSwitch = callback;
        this.scrollRect = scrollRect;

        image = GetComponent<Image>();
        canvas = GetComponent<Canvas>();

        image.sprite = Vegetable.BattleSprite;
        BeforeDragPosition = new Vector3(transform.position.x, transform.position.y, 0.0f);

        // 長押しのイベントの登録
        var eventTrigger = GetComponent<ObservableEventTrigger>();
        eventTrigger.OnPointerDownAsObservable().Select(_ => true)
            .Merge(eventTrigger.OnPointerDownAsObservable().Select(_ => false))
            .Throttle(TimeSpan.FromSeconds(0.5f))
            .AsUnitObservable()
            .TakeUntilDestroy(this)
            .Subscribe(_ => isLongPress = true);
    }

    // ドラッグ開始
    public void OnBeginDrag(PointerEventData eventData) {
        canvas.sortingOrder = 1;
    }

    // ドラッグ中
    public void OnDrag(PointerEventData eventData) {
        // 長押しでなければ、スクロールビューを動かす
        if (!isLongPress) {
            SetScrollView(eventData);
            return;
        }

        var worldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
    }

    // ドラッグ終了
    public void OnEndDrag(PointerEventData eventData) {
        canvas.sortingOrder = 0;
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        // 正しく入れ替えることができなかった時は初期座標に戻す
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

            // アイコンの入れ替え
            onSwitch?.Invoke(this, hitIcon);
        }
    }

    // クリックしたとき
    public void OnPointerDown(PointerEventData eventData) {
        isLongPress = false;
        downPosition = eventData.position;
    }

    // ドラッグする前の座標の更新
    public void UpdateBeforeDragPosition(Vector3 position) {
        BeforeDragPosition = new Vector3(position.x, position.y, 0.0f);
    }

    // スクロールビューに基準を合わせる
    private void SetScrollView(PointerEventData pointerEventData) {
        pointerEventData.pointerDrag = scrollRect.gameObject;
        EventSystem.current.SetSelectedGameObject(scrollRect.gameObject);
        scrollRect.OnInitializePotentialDrag(pointerEventData);
        scrollRect.OnBeginDrag(pointerEventData);
    }
}
