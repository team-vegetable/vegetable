using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

// �~�j�g�}�g
public class CherryTomato : BaseVegetable
{
    // SciprableObject�ɂ��ꂩ��ړ�
    // ���G�͈�
    [SerializeField] private int damage = 0;
    [SerializeField] private float interval = 0.0f;

    // �������U���̋�
    [SerializeField] private GameObject toamto = null;

    // �U��
    public override async UniTask Attack() {
        canAttack = false;
        var cherryTomatoBullet = Instantiate(toamto, transform.position, Quaternion.identity).GetComponent<CherryTomatoBullet>();
        cherryTomatoBullet.Shoot(damage, targetAnimal.transform.position);
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        canAttack = true;
    }
}
