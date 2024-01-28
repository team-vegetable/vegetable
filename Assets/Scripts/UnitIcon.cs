using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 編成画面で使用するユニットのアイコン
public class UnitIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // キャンバス
    private Canvas canvas = null;
    // ドラッグ開始したときの座標
    private Vector3 initPosition = new();

    // シブリングインデックス
    public int SiblingIndex { get; private set; } = 0;

    private void Start() {
        canvas = GetComponent<Canvas>();
        SiblingIndex = transform.GetSiblingIndex();
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

            int otherSiblingIndex = hitIcon.SiblingIndex;
            hitIcon.SetSibligIndex(SiblingIndex);
            SiblingIndex = otherSiblingIndex;
            transform.SetSiblingIndex(SiblingIndex);
        }
    }

    // ヒエラルキーの順番の変更
    public void SetSibligIndex(int index) {
        SiblingIndex = index;
        transform.SetSiblingIndex(SiblingIndex);
    }
}
