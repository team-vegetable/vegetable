using System.Collections;
using System.Threading.Tasks;
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
    public override async Task Attack() {
        canAttack = false;
        var cherryTomatoBullet = Instantiate(toamto, transform.position, Quaternion.identity).GetComponent<CherryTomatoBullet>();
        cherryTomatoBullet.Shoot(damage, target.transform.position);
        await Task.Delay(2000);
        canAttack = true;
    }
}
