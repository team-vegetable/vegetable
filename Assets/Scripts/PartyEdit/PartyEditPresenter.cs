using UnityEngine;
using UniRx;

// �p�[�e�B�Ґ���ʂ̃v���[���^�[
public class PartyEditPresenter : MonoBehaviour {
    // ���f��
    [SerializeField] private PartyEditModel model = null;
    // �r���[
    [SerializeField] private PartyEditViewer view = null;

    private void Start() {
        view.OnClickSaveButton.Subscribe(_ => model.SaveMainVegetables()).AddTo(this);
    }
}
