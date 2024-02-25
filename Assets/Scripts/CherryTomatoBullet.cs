using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ミニトマから発射された玉
public class CherryTomatoBullet : MonoBehaviour
{
    // 玉のスピード
    [SerializeField] private float speed = 0.0f;

    // 剛体
    private new Rigidbody2D rigidbody = null;
    // ダメージ
    private int damage = 0;

    // 発射
    public void Shoot(int damage, Vector2 target) {
        this.damage = damage;
        var direction = target - (Vector2)transform.position;

        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddForce(speed * direction.normalized, ForceMode2D.Impulse);
    }

    // 接触した時
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Animal")) {
            var animal = collision.gameObject.GetComponent<BaseAnimal>();
            animal.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
