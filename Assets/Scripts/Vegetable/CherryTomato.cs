using System.Collections;
using System.Collections.Generic;
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
    public override void Attack() {
        StartCoroutine(OnShootCherryTomatoBUllet());
    }

    // �~�j�g�}�g�𔭎˂��鉓�����U��
    private IEnumerator OnShootCherryTomatoBUllet() {
        canAttack = false;
        var cherryTomatoBullet = Instantiate(toamto, transform.position, Quaternion.identity).GetComponent<CherryTomatoBullet>();
        cherryTomatoBullet.Shoot(damage, target.transform.position);
        yield return new WaitForSeconds(interval);
        canAttack = true;
    }
}
