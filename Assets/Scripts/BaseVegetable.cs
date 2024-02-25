using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

// �S�Ă̖�؂Ɍp����������N���X
public class BaseVegetable : MonoBehaviour
{
    // �������U���̋�
    [SerializeField] private GameObject toamto = null;

    // SciprableObject�ɂ��ꂩ��ړ�
    // ���G�͈�
    [SerializeField] private float radius = 0.0f;
    // �~�j�g�}�g�𔭎˂���Ƃ��̃C���^�[�o��
    [SerializeField] private float interval = 0.0f;

    // �Ώۂ̓���(�Ƃ肠������̂���)
    private GameObject target = null;
    // �~�j�g�}�g�𔭎˂ł��邩�ǂ���
    private bool canShoot = true;

    private void Update() {
        var collider = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Animal"));
        if (collider != null) {
            target = collider.gameObject;
            if (canShoot) {
                StartCoroutine(OnShootCherryTomatoBUllet());
            }
        }
    }

    // �~�j�g�}�g�𔭎˂��鉓�����U��
    private IEnumerator OnShootCherryTomatoBUllet() {
        canShoot = false;
        var cherryTomatoBullet = Instantiate(toamto, transform.position, Quaternion.identity).GetComponent<CherryTomatoBullet>();
        cherryTomatoBullet.Shoot(0, target.transform.position);
        yield return new WaitForSeconds(interval);
        canShoot = true;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
