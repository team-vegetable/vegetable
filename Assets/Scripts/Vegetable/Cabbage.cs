using Cysharp.Threading.Tasks;
using System;
using UnityEditor.UIElements;
using UnityEngine;

// �L���x�c
public class Cabbage : BaseVegetable
{
    // �U�����͈̔�
    [SerializeField] private float attackRadius = 0.0f;

    // �A�j���[�^�[
    private Animator animator = null;
    
    // �A�j���[�V�����̃n�b�V���L�[
    private readonly int attackKey = Animator.StringToHash("Attack");

    private void Start() {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // �U��
    public override async UniTask Attack() {
        canAttack = false;
        animator.SetTrigger(attackKey);
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        canAttack = true;
    }

    // �U�����[�V�������I�����Ē��n�����Ƃ�
    public void OnLand() {
        var colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, LayerMask.GetMask("Animal"));
        foreach (var item in colliders) {
            var animal = item.transform.parent.GetComponent<BaseAnimal>();
            animal.TakeDamage(10);
        }
    }

    // �ʏ�U���̃M�Y���̕\��
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
