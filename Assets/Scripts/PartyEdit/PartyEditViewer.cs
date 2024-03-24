using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

// パーティ編成画面のビュー
public class PartyEditViewer : MonoBehaviour {
    // 保存ボタン
    [SerializeField] private Button saveButton = null;
    public IObservable<Unit> OnClickSaveButton => saveButton.OnClickAsObservable();
}
