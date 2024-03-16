using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// バトルのターゲットをセットするクラス
public class BattleTargetSetter : MonoBehaviour {
    // 野菜とその攻撃対象
    public Dictionary<BaseVegetable, BaseAnimal> VegetableTargetPair { get; private set; } = new();

    // 野菜と攻撃対象を結びつける
    public void Set(BaseVegetable vegetable, BaseAnimal animal) {
        VegetableTargetPair.Add(vegetable, animal);
    }
}
