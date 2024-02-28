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
    // 現在のHP
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

    // 攻撃
    public virtual async Task Attack() {
        await Task.CompletedTask;
    }

    // ダメージを受けた時
    public void TakeDamage(int damage) {
        currentHP -= damage;
        Debug.Log($"残りのHP : {currentHP}");
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
