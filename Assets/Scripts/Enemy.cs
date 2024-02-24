using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// ��؂��U������G�ɋ��ʂ̊��N���X(�p������\��)
public class Enemy : MonoBehaviour
{
    // HP�o�[
    [SerializeField] private Image hpBar = null;

    // �����̃X�e�[�^�X���܂Ƃ߂�����
    private Animal animal = null;
    // �X�v���C�g�����_���[
    private SpriteRenderer spriteRenderer = null;
    // �������W
    private Vector2 initPosition = new();
    // �U�������؂̍��W
    private Vector2 target = new();
    // ���S���̃C�x���g
    private UnityAction onDead = null;


    private bool isDying = false;
    // ���݂�HP
    private int currentHP = 0;

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
    public void Init(Animal animal, Vector2 target, int sortingOrder, UnityAction onDead) {
        this.animal = animal;
        this.target = target;
        this.onDead = onDead;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = sortingOrder;
        initPosition = transform.position;
        currentHP = animal.MaxHP;
    }

    private void Update() {
        // �^�[�Q�b�g���w�肳��Ă��Ȃ���Έړ����Ȃ�
        if (target == Vector2.zero) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && state != State.Dying) {
            UpdateCurrentHP();
        }

        Vector2 currentPosition = transform.position;
        Vector2 direction = target - currentPosition;
        if (direction.magnitude <= animal.AttackRange && !isDying) {
            isDying = true;
            state = State.Attack;
            return;
        }

        // TODO : �ړ��ɂ��ăx�X�g�̂��̂�I������
        if (state != State.Attack) {
            if (state == State.Dying) {
                direction = initPosition - currentPosition;
            }
            transform.Translate(animal.Speed * Time.deltaTime * direction.normalized);
        }
    }

    // ���݂�HP���X�V����
    private void UpdateCurrentHP() {
        currentHP -= 100;
        if (currentHP <= 0) {
            state = State.Dying;
            onDead?.Invoke();
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        hpBar.fillAmount = (float)currentHP / animal.MaxHP;
    }
}
