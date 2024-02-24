using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// 野菜を攻撃する敵に共通の基底クラス(継承する予定)
public class Enemy : MonoBehaviour
{
    // HPバー
    [SerializeField] private Image hpBar = null;

    // 動物のステータスをまとめたもの
    private Animal animal = null;
    // スプライトレンダラー
    private SpriteRenderer spriteRenderer = null;
    // 初期座標
    private Vector2 initPosition = new();
    // 攻撃する野菜の座標
    private Vector2 target = new();
    // 死亡時のイベント
    private UnityAction onDead = null;


    private bool isDying = false;
    // 現在のHP
    private int currentHP = 0;

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
        this.animal = animal;
        this.target = target;
        this.onDead = onDead;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = sortingOrder;
        initPosition = transform.position;
        currentHP = animal.MaxHP;
    }

    private void Update() {
        // ターゲットが指定されていなければ移動しない
        if (target == Vector2.zero) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && state != State.Dying) {
            UpdateCurrentHP();
        }

        Vector2 currentPosition = transform.position;
        Vector2 direction = target - currentPosition;
        if (direction.magnitude <= animal.AttackRange && !isDying) {
            isDying = true;
            state = State.Attack;
            return;
        }

        // TODO : 移動についてベストのものを選択する
        if (state != State.Attack) {
            if (state == State.Dying) {
                direction = initPosition - currentPosition;
            }
            transform.Translate(animal.Speed * Time.deltaTime * direction.normalized);
        }
    }

    // 現在のHPを更新する
    private void UpdateCurrentHP() {
        currentHP -= 100;
        if (currentHP <= 0) {
            state = State.Dying;
            onDead?.Invoke();
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        hpBar.fillAmount = (float)currentHP / animal.MaxHP;
    }
}
