using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �~�j�g�}���甭�˂��ꂽ��
public class CherryTomatoBullet : MonoBehaviour
{
    // �ʂ̃X�s�[�h
    [SerializeField] private float speed = 0.0f;

    // ����
    private new Rigidbody2D rigidbody = null;
    // �_���[�W
    private int damage = 0;

    // ����
    public void Shoot(int damage, Vector2 target) {
        this.damage = damage;
        var direction = target - (Vector2)transform.position;

        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddForce(speed * direction.normalized, ForceMode2D.Impulse);
    }

    // �ڐG������
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Animal")) {
            var animal = collision.gameObject.GetComponent<BaseAnimal>();
            animal.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
