using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using System.Collections.Generic;
using System;

// �o�g���Ŏg�p���郂�f��(MVP)
public class BattleModel : MonoBehaviour
{
    // �o�g���Ɏg�p������
    private Subject<List<Vegetable>> vegetables = new();
    public Subject<List<Vegetable>> Vegetables => vegetables;

    // �^�C�}�[
    // TODO : ���̐��l�̓o�g������̃}�X�^�[�f�[�^���܂Ƃ߂����̂���擾������
    private readonly ReactiveProperty<float> timer = new(60);
    public ReactiveProperty<float> Timer => timer;

    // �G��|������
    private readonly ReactiveProperty<int> count = new();
    public ReactiveProperty<int> Count => count;

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
        while (true) {
            await UniTask.Yield();
            timer.Value -= Time.deltaTime;
        }
    }
}
