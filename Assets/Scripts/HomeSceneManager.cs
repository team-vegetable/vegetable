using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ホームシーンの進行を管理する
public class HomeSceneManager : MonoBehaviour
{
    // パーティー編成ボタン
    [SerializeField] private Button partyEditButton = null;
    // 報酬受け取りボタン
    [SerializeField] private Button rewardRecievedButton = null;
    // ステージセレクトボタン
    [SerializeField] private Button stageSelectButton = null;

    // 放置した秒数掛ける何ポイントもらえるか
    [SerializeField] private int pointPerSecond = 1;

    // 前回報酬を受け取った時間
    private DateTime prevTime = default;

    private void Start() {
        // ボタンの登録
        partyEditButton.onClick.RemoveAllListeners();
        partyEditButton.onClick.AddListener(OnClickPartyEdittButton);
        rewardRecievedButton.onClick.RemoveAllListeners();
        rewardRecievedButton.onClick.AddListener(OnClickRewardRecievedButton);
        stageSelectButton.onClick.RemoveAllListeners();
        stageSelectButton.onClick.AddListener(OnStageSelectButton);
    }

    // パーティー編成ボタンを押したとき
    private void OnClickPartyEdittButton() {
        SceneManager.LoadScene("PartyEditScene");
    }

    // 報酬受け取りボタンを押したとき
    private void OnClickRewardRecievedButton() {
        if (prevTime != default) {
            var diff = DateTime.Now - prevTime;
            Debug.Log($"{diff.TotalSeconds}秒間放置していました");
        } 
        else {
            Debug.Log("初めて放置報酬を受け取った");
        }
        

        prevTime = DateTime.Now;
    }

    // ステージセレクトボタンを押したとき
    private void OnStageSelectButton() {
        SceneManager.LoadScene("StageSelectScene");
    }
}
