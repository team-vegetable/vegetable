using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Threading.Tasks;

// 野菜を攻撃する敵に共通の基底クラス(継承する予定)
public class BaseAnimal : MonoBehaviour
{
    // 動物のステータスをまとめたもの
    [SerializeField] private Animal animal = null;
    // HPバー
    [SerializeField] private Image hpBar = null;

    // スプライトレンダラー
    private SpriteRenderer spriteRenderer = null;
    // 初期座標
    private Vector2 initPosition = new();
    // 攻撃する野菜の座標
    private Vector2 target = new();
    // 死亡時のイベント
    private UnityAction onDead = null;
    // 現在のHP
    private int currentHP = 0;
    // 攻撃可能かどうか
    protected bool canAttack = true;

    // 現在のステート
    private enum State {
        // 狙う
        Target,
        // 攻撃
        Attack,
        // 瀕死
        Dying,
    }
    private State state = State.Target;

    // 初期化
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
        // ターゲットが指定されていなければ移動しない
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

    // ダメージを受けた時
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

    // 攻撃
    public virtual async Task Attack() {
        await Task.CompletedTask;
    }
}
