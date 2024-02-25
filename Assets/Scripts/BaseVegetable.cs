using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

// 全ての野菜に継承させる基底クラス
public class BaseVegetable : MonoBehaviour
{
    // 遠距離攻撃の玉
    [SerializeField] private GameObject toamto = null;

    // SciprableObjectにこれから移動
    // 索敵範囲
    [SerializeField] private float radius = 0.0f;
    // ミニトマトを発射するときのインターバル
    [SerializeField] private float interval = 0.0f;

    // 対象の動物(とりあえず一体だけ)
    private GameObject target = null;
    // ミニトマトを発射できるかどうか
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

    // ミニトマトを発射する遠距離攻撃
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
