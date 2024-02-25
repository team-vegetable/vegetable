using System.Collections;
using System.Collections.Generic;
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
    public override async Task Attack() {
        canAttack = false;
        animator.SetTrigger(attackKey);
        await Task.Delay(2000);
        canAttack = true;
    }
}
