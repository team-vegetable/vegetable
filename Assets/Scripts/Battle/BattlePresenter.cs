using UnityEngine;
using UniRx;

// �o�g���̃��f���r���[���Ȃ��v���[���^�[
public class BattlePresenter : MonoBehaviour
{
    // ���f��
    [SerializeField] private BattleModel model = null;
    // ���C���Ŏg�p����r���[
    [SerializeField] private BattleMainViewer mainView = null;

    private async void Start() {
        model.Count.Subscribe(count => mainView.SetCountText(count)).AddTo(this);

        await mainView.StartTimer();
    }
}
