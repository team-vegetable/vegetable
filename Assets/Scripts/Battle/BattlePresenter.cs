using UnityEngine;
using UniRx;

// �o�g���̃��f���r���[���Ȃ��v���[���^�[
public class BattlePresenter : MonoBehaviour
{
    // ���f��
    [SerializeField] private BattleModel model = null;
    // ���C���Ŏg�p����r���[
    [SerializeField] private BattleMainViewer mainView = null;

    private void Start() {
        mainView.Init();

        model.Count.Subscribe(count => mainView.SetCountText(count)).AddTo(this);
        model.Timer.Subscribe(timer => mainView.SetTimerText(timer)).AddTo(this);
    }
}
