using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �o�g���̃^�[�Q�b�g���Z�b�g����N���X
public class BattleTargetSetter : MonoBehaviour {
    // ��؂Ƃ��̍U���Ώ�
    public Dictionary<BaseVegetable, BaseAnimal> VegetableTargetPair { get; private set; } = new();

    // ��؂ƍU���Ώۂ����т���
    public void Set(BaseVegetable vegetable, BaseAnimal animal) {
        VegetableTargetPair.Add(vegetable, animal);
    }
}
