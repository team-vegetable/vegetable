using UnityEngine;

// ‘S‚Ä‚Ì–ìØ‚ÉŒp³‚³‚¹‚éŠî’êƒNƒ‰ƒX
public class BaseVegetable : MonoBehaviour
{
    // Šî‘bî•ñ‚ð‚Ü‚Æ‚ß‚½‚à‚Ì
    [SerializeField] protected Vegetable vegetable = null;

    [SerializeField] private float radius = 0.0f;

    // ‘ÎÛ‚Ì“®•¨(‚Æ‚è‚ ‚¦‚¸ˆê‘Ì‚¾‚¯)
    protected GameObject target = null;
    // UŒ‚‚Å‚«‚é‚©‚Ç‚¤‚©
    protected bool canAttack = true;

    public Vegetable Vegetable { get => vegetable; }

    private void Update() {
        var collider = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Animal"));
        if (collider != null) {
            target = collider.gameObject;
            if (canAttack) {
                Attack();
            }
        }
    }

    public virtual void Attack() {

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
