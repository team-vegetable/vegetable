using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 野菜を攻撃する敵に共通の基底クラス(継承する予定)
public class Enemy : MonoBehaviour
{
    // 移動スピード
    [SerializeField] private int speed = 1;
    // 生成されたとき
    //public void Generated() {

    //}

    private void Update() {
        transform.position += new Vector3(-1.0f * speed * Time.deltaTime, 0.0f, 0.0f);
    }
}
