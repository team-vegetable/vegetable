using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

// �p�[�e�B�Ґ���ʂ̃r���[
public class PartyEditViewer : MonoBehaviour {
    // �ۑ��{�^��
    [SerializeField] private Button saveButton = null;
    public IObservable<Unit> OnClickSaveButton => saveButton.OnClickAsObservable();
}
