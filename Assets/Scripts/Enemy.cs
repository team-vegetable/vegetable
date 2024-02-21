using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��؂��U������G�ɋ��ʂ̊��N���X(�p������\��)
public class Enemy : MonoBehaviour
{
    // �ړ��X�s�[�h
    [SerializeField] private int speed = 1;
    // �U���͈�
    [SerializeField] private int attackRange = 1;

    // �X�v���C�g�����_���[
    private SpriteRenderer spriteRenderer = null;
    // �������W
    private Vector2 initPosition = new();
    // �U�������؂̍��W
    private Vector2 target = new();

    private bool isDying = false;

    private enum State {
        // �_��
        Target,
        // �U��
        Attack,
        // �m��
        Dying,
    }
    private State state = State.Target;

    // ������
    public void Init(Vector2 target, int sortingOrder) {
        this.target = target;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = sortingOrder;
        initPosition = transform.position;
    }

    private void Update() {
        // �^�[�Q�b�g���w�肳��Ă��Ȃ���Έړ����Ȃ�
        if (target == Vector2.zero) {
            return;
        }

        Vector2 currentPosition = transform.position;
        Vector2 direction = target - currentPosition;
        if (direction.magnitude <= attackRange && !isDying) {
            isDying = true;
            state = State.Attack;
            StartCoroutine(OnDeadTemp());
            return;
        }

        // TODO : �ړ��ɂ��ăx�X�g�̂��̂�I������
        if (state != State.Attack) {
            if (state == State.Dying) {
                direction = initPosition - currentPosition;
            }
            transform.Translate(speed * Time.deltaTime * direction.normalized);
        }
    }
    
    // ���̎��S����
    private IEnumerator OnDeadTemp() {
        yield return new WaitForSeconds(2.0f);
        state = State.Dying;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
