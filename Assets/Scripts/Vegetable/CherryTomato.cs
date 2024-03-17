using Cysharp.Threading.Tasks;
using System;
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
    public override async UniTask Attack() {
        canAttack = false;
        var cherryTomatoBullet = Instantiate(toamto, transform.position, Quaternion.identity).GetComponent<CherryTomatoBullet>();
        cherryTomatoBullet.Shoot(damage, targetAnimal.transform.position);
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        canAttack = true;
    }
}
