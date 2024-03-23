using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

// �o�g���Ŏg�p���郂�f��(MVP)
public class BattleModel : MonoBehaviour
{
    // �c�艽�b����J�n���邩
    // [SerializeField] private float timer = 60.0f;

    // �^�C�}�[
    private readonly ReactiveProperty<float> timer = new(60);
    public ReactiveProperty<float> Timer => timer;

    // �G��|������
    private readonly ReactiveProperty<int> count = new();
    public ReactiveProperty<int> Count => count;

    private async void Start() {
        await StartTimer();
    }

    public void Test() {
        count.Value++;
    }

    // �c�莞�Ԃ̃^�C�}�[���X�^�[�g������
    // TODO : ���f���Ŏ�������ׂ��ł͂Ȃ��C������
    public async UniTask StartTimer() {
        while (true) {
            await UniTask.Yield();
            timer.Value -= Time.deltaTime;
        }
    }
}
