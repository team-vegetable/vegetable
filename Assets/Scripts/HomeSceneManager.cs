using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ホームシーンの進行を管理する
public class HomeSceneManager : MonoBehaviour
{
    // 戦闘開始ボタン
    [SerializeField] private Button fightButton = null;
    // 報酬受け取りボタン
    [SerializeField] private Button rewardRecievedButton = null;
    // パーティー編成ボタン
    [SerializeField] private Button partyEditButton = null;

    // 放置した秒数掛ける何ポイントもらえるか
    [SerializeField] private int pointPerSecond = 1;

    private void Start() {
        // ボタンの登録
        fightButton.onClick.RemoveAllListeners();
        fightButton.onClick.AddListener(OnClickFightButton);
        rewardRecievedButton.onClick.RemoveAllListeners();
        rewardRecievedButton.onClick.AddListener(OnClickRewardRecievedButton);
        partyEditButton.onClick.RemoveAllListeners();
        partyEditButton.onClick.AddListener(OnClickPartyEdittButton);
    }

    // 戦闘開始ボタンを押したとき
    private void OnClickFightButton() {
        // SceneManager.LoadScene("BattleScene");
    }

    // 報酬受け取りボタンを押したとき
    private void OnClickRewardRecievedButton() {
        APIController.Instance.Get("UserLeavingRewardTime", "getLastRecievedTime", getLastReardRecievedTime);
    }

    // パーティー編成ボタンを押したとき
    private void OnClickPartyEdittButton() {
        SceneManager.LoadScene("PartyEditScene");
    }

    // 
    private void getLastReardRecievedTime(string data) {
        Debug.Log(data);
        // int parse = int.Parse(data);
        // int point = int.Parse(data) * pointPerSecond;
        // Debug.Log($"point : {parse}");
    }
}
