using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 野菜を攻撃する敵に共通の基底クラス(継承する予定)
public class Enemy : MonoBehaviour
{
    // 移動スピード
    [SerializeField] private int speed = 1;
    // 攻撃範囲
    [SerializeField] private int attackRange = 1;

    // スプライトレンダラー
    private SpriteRenderer spriteRenderer = null;
    // 初期座標
    private Vector2 initPosition = new();
    // 攻撃する野菜の座標
    private Vector2 target = new();

    private bool isDying = false;

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
    public void Init(Vector2 target, int sortingOrder) {
        this.target = target;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = sortingOrder;
        initPosition = transform.position;
    }

    private void Update() {
        // ターゲットが指定されていなければ移動しない
        if (target == Vector2.zero) {
            return;
        }

        Vector2 currentPosition = transform.position;
        Vector2 direction = target - currentPosition;
        if (direction.magnitude <= attackRange && !isDying) {
            isDying = true;
            state = State.Attack;
            StartCoroutine(OnDeadTemp());
            return;
        }

        // TODO : 移動についてベストのものを選択する
        if (state != State.Attack) {
            if (state == State.Dying) {
                direction = initPosition - currentPosition;
            }
            transform.Translate(speed * Time.deltaTime * direction.normalized);
        }
    }
    
    // 仮の死亡処理
    private IEnumerator OnDeadTemp() {
        yield return new WaitForSeconds(2.0f);
        state = State.Dying;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
