using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Tilemaps;

// 全ての野菜に継承させる基底クラス
public class BaseVegetable : MonoBehaviour
{
    // 基礎情報をまとめたもの
    [SerializeField] protected Vegetable vegetable = null;

    [SerializeField] private float radius = 0.0f;

    // 対象の動物(とりあえず一体だけ)
    protected BaseAnimal targetAnimal = null;
    // 攻撃できるかどうか
    protected bool canAttack = true;
    // 現在のHP
    private int currentHP = 0;

    public Vegetable Vegetable { get => vegetable; }

    private async void Start() {
        currentHP = Vegetable.BattleStatus.MaxHP;

        await GetTargetAnimal();
    }

    private async void Update() {
        // 対象が無ければ取得する
        //if (targetAnimal == null) {
        //    var collider = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Animal"));
        //    if (collider != null) {
        //        if (collider.gameObject.transform.parent.TryGetComponent<BaseAnimal>(out targetAnimal)) {
        //            targetAnimal.OnEnemyDead.Subscribe(_ => UpdateTarget()).AddTo(this);
        //        }
        //    }
        //}

        // 対象がいて攻撃可能なら攻撃する
        if (canAttack && targetAnimal != null) {
            await Attack();
        }
    }

    // 攻撃
    public virtual async UniTask Attack() {
        await UniTask.CompletedTask;
    }

    // ダメージを受けた時
    public void TakeDamage(int damage) {
        currentHP -= damage;
        // Debug.Log($"{gameObject.name}の残りのHP : {currentHP}");
    }

    // 対象の敵のHPが0になったら新しい対象の敵をセットする
    private async void UpdateTarget() {
        // 現在の対象をnullにする
        targetAnimal = null;
        await GetTargetAnimal();
    }

    // 対象の動物を取得する(1体だけ)
    private async UniTask GetTargetAnimal() {
        BaseAnimal animal = null;

        while (targetAnimal == null) {
            await UniTask.Yield();
            var collider2D = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Animal"));
            if (collider2D != null) {                
                if (animal == null) {
                    animal = collider2D.transform.parent.GetComponent<BaseAnimal>();
                }
                
                // 取得した動物のHPがまだあれば対象としてセットする
                if (animal != null) {
                    if (!animal.IsDead()) {
                        targetAnimal = animal;
                    }
                }
            }
        }

        targetAnimal.OnEnemyDead.Subscribe(_ => UpdateTarget()).AddTo(this);
    }

    // 索敵範囲のギズモの表示
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
