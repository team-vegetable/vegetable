using System.Threading.Tasks;
using UnityEngine;

// �S�Ă̖�؂Ɍp����������N���X
public class BaseVegetable : MonoBehaviour
{
    // ��b�����܂Ƃ߂�����
    [SerializeField] protected Vegetable vegetable = null;

    [SerializeField] private float radius = 0.0f;

    // �Ώۂ̓���(�Ƃ肠������̂���)
    protected GameObject target = null;
    // �U���ł��邩�ǂ���
    protected bool canAttack = true;
    // ���݂�HP
    private int currentHP = 0;

    public Vegetable Vegetable { get => vegetable; }

    private void Start() {
        currentHP = Vegetable.BattleStatus.MaxHP;
    }

    private async void Update() {
        var collider = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Animal"));
        if (collider != null) {
            target = collider.gameObject;
            if (canAttack) {
                await Attack();
            }
        }
    }

    // �U��
    public virtual async Task Attack() {
        await Task.CompletedTask;
    }

    // �_���[�W���󂯂���
    public void TakeDamage(int damage) {
        currentHP -= damage;
        Debug.Log($"�c���HP : {currentHP}");
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
