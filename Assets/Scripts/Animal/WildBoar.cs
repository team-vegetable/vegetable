using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using UnityEngine;

// �C�m�V�V
public class WildBoar : BaseAnimal
{
    private Animator animator = null;

    private int attackKey = Animator.StringToHash("Attack");

    private void Start() {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // �U��
    public override async UniTask Attack() {
        canAttack = false;
        await UniTask.Delay(TimeSpan.FromSeconds(animal.BattleStatus.Interval)); ;
        animator.SetTrigger(attackKey);
        canAttack = true;
    }

    // �ڐG������
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Vegetable")) {
            var vegetable = collision.gameObject.GetComponent<BaseVegetable>();
            vegetable.TakeDamage(animal.BattleStatus.Attack);
        }
    }
}
