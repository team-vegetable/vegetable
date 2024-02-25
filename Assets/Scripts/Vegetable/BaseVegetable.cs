using System.Threading.Tasks;
using UnityEngine;

// ëSÇƒÇÃñÏçÿÇ…åpè≥Ç≥ÇπÇÈäÓíÍÉNÉâÉX
public class BaseVegetable : MonoBehaviour
{
    // äÓëbèÓïÒÇÇ‹Ç∆ÇﬂÇΩÇ‡ÇÃ
    [SerializeField] protected Vegetable vegetable = null;

    [SerializeField] private float radius = 0.0f;

    // ëŒè€ÇÃìÆï®(Ç∆ÇËÇ†Ç¶Ç∏àÍëÃÇæÇØ)
    protected GameObject target = null;
    // çUåÇÇ≈Ç´ÇÈÇ©Ç«Ç§Ç©
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

    // çUåÇ
    public virtual async Task Attack() {
        await Task.CompletedTask;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
