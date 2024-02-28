using Cysharp.Threading.Tasks;
using System;
using UnityEditor.UIElements;
using UnityEngine;

// キャベツ
public class Cabbage : BaseVegetable
{
    // 攻撃時の範囲
    [SerializeField] private float attackRadius = 0.0f;

    // アニメーター
    private Animator animator = null;
    
    // アニメーションのハッシュキー
    private readonly int attackKey = Animator.StringToHash("Attack");

    private void Start() {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // 攻撃
    public override async UniTask Attack() {
        canAttack = false;
        animator.SetTrigger(attackKey);
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        canAttack = true;
    }

    // 攻撃モーションが終了して着地したとき
    public void OnLand() {
        var colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, LayerMask.GetMask("Animal"));
        foreach (var item in colliders) {
            var animal = item.transform.parent.GetComponent<BaseAnimal>();
            animal.TakeDamage(10);
        }
    }

    // 通常攻撃のギズモの表示
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
