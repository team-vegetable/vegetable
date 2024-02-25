using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

// ÉCÉmÉVÉV
public class WildBoar : BaseAnimal
{
    private Animator animator = null;

    private int attackKey = Animator.StringToHash("Attack");

    private void Start() {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // çUåÇ
    public override async Task Attack() {
        canAttack = false;
        animator.SetTrigger(attackKey);
        await Task.Delay(2000);
        canAttack = true;
    }
}
