using Cysharp.Threading.Tasks;
using UnityEngine;

// ‘S‚Ä‚Ì–ìØ‚ÉŒp³‚³‚¹‚éŠî’êƒNƒ‰ƒX
public class BaseVegetable : MonoBehaviour
{
    // Šî‘bî•ñ‚ğ‚Ü‚Æ‚ß‚½‚à‚Ì
    [SerializeField] protected Vegetable vegetable = null;

    [SerializeField] private float radius = 0.0f;

    // ‘ÎÛ‚Ì“®•¨(‚Æ‚è‚ ‚¦‚¸ˆê‘Ì‚¾‚¯)
    protected GameObject target = null;
    // UŒ‚‚Å‚«‚é‚©‚Ç‚¤‚©
    protected bool canAttack = true;
    // Œ»İ‚ÌHP
    private int currentHP = 0;

    public Vegetable Vegetable { get => vegetable; }

    private void Start() {
        currentHP = Vegetable.BattleStatus.MaxHP;
    }

    private async void Update() {
        // ‘ÎÛ‚ª–³‚¯‚ê‚Îæ“¾‚·‚é
        if (target == null) {
            var collider = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Animal"));
            if (collider != null) {
                target = collider.gameObject;
            }
        }

        // ‘ÎÛ‚ª‚¢‚ÄUŒ‚‰Â”\‚È‚çUŒ‚‚·‚é
        if (canAttack && target != null) {
            await Attack();
        }
    }

    // UŒ‚
    public virtual async UniTask Attack() {
        await UniTask.CompletedTask;
    }

    // ƒ_ƒ[ƒW‚ğó‚¯‚½
    public void TakeDamage(int damage) {
        currentHP -= damage;
        // Debug.Log($"{gameObject.name}‚Ìc‚è‚ÌHP : {currentHP}");
    }

    // õ“G”ÍˆÍ‚ÌƒMƒYƒ‚‚Ì•\¦
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
