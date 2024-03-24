using UnityEngine;
using UniRx;

// パーティ編成画面のプレゼンター
public class PartyEditPresenter : MonoBehaviour {
    // モデル
    [SerializeField] private PartyEditModel model = null;
    // ビュー
    [SerializeField] private PartyEditViewer view = null;

    private void Start() {
        view.OnClickSaveButton.Subscribe(_ => model.SaveMainVegetables()).AddTo(this);
    }
}
