using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 編成画面で使用するユニットのアイコン
public class UnitIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // 画像
    private Image image = null;
    // キャンバス
    private Canvas canvas = null;
    // ドラッグ開始したときの座標
    private Vector3 initPosition = new();
    // アイコンを入れ替えたときのイベント
    private UnityAction<GameObject, GameObject> onSwitch = null;

    // 野菜のデータ
    public Vegetable Vegetable { get; private set; }

    public void Init(Vegetable vegetable, UnityAction<GameObject, GameObject> callback) {
        Vegetable = vegetable;
        onSwitch = callback;

        image = GetComponent<Image>();
        canvas = GetComponent<Canvas>();

        image.sprite = Vegetable.Icon;
    }

    // ドラッグ開始
    public void OnBeginDrag(PointerEventData eventData) {
        initPosition = transform.position;
        canvas.sortingOrder = 1;
    }

    // ドラッグ中
    public void OnDrag(PointerEventData eventData) {
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

            // アイコンの入れ替え
            onSwitch?.Invoke(gameObject, hitIcon.gameObject);
        }
    }
}
