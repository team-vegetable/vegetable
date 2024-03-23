using UnityEngine;
using UniRx;

// バトルのモデルビューをつなぐプレゼンター
public class BattlePresenter : MonoBehaviour
{
    // モデル
    [SerializeField] private BattleModel model = null;
    // メインで使用するビュー
    [SerializeField] private BattleMainViewer mainView = null;

    private void Start() {
        mainView.Init();

        model.Count.Subscribe(count => mainView.SetCountText(count)).AddTo(this);
        model.Timer.Subscribe(timer => mainView.SetTimerText(timer)).AddTo(this);
    }
}
