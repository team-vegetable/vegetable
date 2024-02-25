using System.Threading.Tasks;
using UnityEngine;

// 全ての野菜に継承させる基底クラス
public class BaseVegetable : MonoBehaviour
{
    // 基礎情報をまとめたもの
    [SerializeField] protected Vegetable vegetable = null;

    [SerializeField] private float radius = 0.0f;

    // 対象の動物(とりあえず一体だけ)
    protected GameObject target = null;
    // 攻撃できるかどうか
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
