using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

// イノシシ
public class WildBoar : BaseAnimal
{
    // アニメーター
    private Animator animator = null;

    // アニメーターキー
    private readonly int attackKey = Animator.StringToHash("Attack");

    private void Start() {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // 攻撃
    public override async UniTask Attack() {
        canAttack = false;
        animator.SetTrigger(attackKey);
        await UniTask.Delay(TimeSpan.FromSeconds(animal.BattleStatus.Interval));
        canAttack = true;
    }

    // 接触した時
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Vegetable")) {
            var vegetable = collision.gameObject.GetComponent<BaseVegetable>();
            vegetable.TakeDamage(animal.BattleStatus.Attack);
        }
    }
}
