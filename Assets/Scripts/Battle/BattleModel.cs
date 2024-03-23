using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using System.Collections.Generic;

// �o�g���Ŏg�p���郂�f��(MVP)
public class BattleModel : MonoBehaviour {
    // �o�g���Ɏg�p������
    private readonly Subject<List<Vegetable>> vegetables = new();
    public Subject<List<Vegetable>> Vegetables => vegetables;

    // ��������
    private readonly ReactiveProperty<float> timer = new();
    public ReactiveProperty<float> Timer => timer;

    // �G��|������
    private readonly ReactiveProperty<int> count = new();
    public ReactiveProperty<int> Count => count;

    private async void Start() {
        await StartTimer();
    }

    // ��؂̃A�C�R���̃Z�b�g
    public void SetVegetableIcon(List<Vegetable> vegetables) {
        this.vegetables.OnNext(vegetables);
    }

    // ������|�������̍X�V
    public void UpdateAnimalCount() {
        count.Value++;
    }

    // �c�莞�Ԃ̃^�C�}�[���X�^�[�g������
    public async UniTask StartTimer() {
        timer.Value = VegetableConstData.TIMER;
        while (true) {
            await UniTask.Yield();
            timer.Value -= Time.deltaTime;
        }
    }
}
