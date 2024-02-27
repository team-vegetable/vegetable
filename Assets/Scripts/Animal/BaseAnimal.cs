using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Threading.Tasks;

// ��؂��U������G�ɋ��ʂ̊��N���X(�p������\��)
public class BaseAnimal : MonoBehaviour
{
    // �����̃X�e�[�^�X���܂Ƃ߂�����
    [SerializeField] private Animal animal = null;
    // HP�o�[
    [SerializeField] private Image hpBar = null;

    // �X�v���C�g�����_���[
    private SpriteRenderer spriteRenderer = null;
    // �������W
    private Vector2 initPosition = new();
    // �U�������؂̍��W
    private Vector2 target = new();
    // ���S���̃C�x���g
    private UnityAction onDead = null;
    // ���݂�HP
    private int currentHP = 0;
    // �U���\���ǂ���
    protected bool canAttack = true;

    // ���݂̃X�e�[�g
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
        // this.animal = animal;
        this.target = target;
        this.onDead = onDead;

        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = sortingOrder;
        initPosition = transform.position;
        currentHP = animal.BattleStatus.MaxHP;
    }

    private async void Update() {
        // �^�[�Q�b�g���w�肳��Ă��Ȃ���Έړ����Ȃ�
        if (target == Vector2.zero) {
            return;
        }

        Vector2 currentPosition = transform.position;
        Vector2 direction = target - currentPosition;
        if (direction.magnitude <= animal.BattleStatus.AttackRange && canAttack) {
            state = State.Attack;
            await Attack();
        }

        if (state != State.Attack) {
            if (state == State.Dying) {
                direction = initPosition - currentPosition;
            }
            transform.Translate(animal.BattleStatus.Speed * Time.deltaTime * direction.normalized);
        }
    }

    // �_���[�W���󂯂���
    public void TakeDamage(int damage) {
        if (state == State.Dying) {
            return;
        }

        currentHP -= damage;
        if (currentHP <= 0) {
            state = State.Dying;
            onDead?.Invoke();
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        hpBar.fillAmount = (float)currentHP / animal.BattleStatus.MaxHP;
    }

    // �U��
    public virtual async Task Attack() {
        await Task.CompletedTask;
    }
}
