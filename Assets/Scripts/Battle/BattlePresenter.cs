using UnityEngine;
using UniRx;

// バトルのモデルビューをつなぐプレゼンター
public class BattlePresenter : MonoBehaviour
{
    // モデル
    [SerializeField] private BattleModel model = null;
    // メインで使用するビュー
    [SerializeField] private BattleMainViewer mainView = null;

    private async void Start() {
        model.Count.Subscribe(count => mainView.SetCountText(count)).AddTo(this);

        await mainView.StartTimer();
    }
}
