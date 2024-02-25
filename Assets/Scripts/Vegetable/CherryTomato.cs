using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ミニトマト
public class CherryTomato : BaseVegetable
{
    // SciprableObjectにこれから移動
    // 索敵範囲
    [SerializeField] private int damage = 0;
    [SerializeField] private float interval = 0.0f;

    // 遠距離攻撃の玉
    [SerializeField] private GameObject toamto = null;

    // 攻撃
    public override void Attack() {
        StartCoroutine(OnShootCherryTomatoBUllet());
    }

    // ミニトマトを発射する遠距離攻撃
    private IEnumerator OnShootCherryTomatoBUllet() {
        canAttack = false;
        var cherryTomatoBullet = Instantiate(toamto, transform.position, Quaternion.identity).GetComponent<CherryTomatoBullet>();
        cherryTomatoBullet.Shoot(damage, target.transform.position);
        yield return new WaitForSeconds(interval);
        canAttack = true;
    }
}
