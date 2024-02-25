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

    public Vegetable Vegetable { get => vegetable; }

    private async void Update() {
        var collider = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Animal"));
        if (collider != null) {
            target = collider.gameObject;
            if (canAttack) {
                await Attack();
            }
        }
    }

    public virtual async Task Attack() {
        await Task.CompletedTask;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
