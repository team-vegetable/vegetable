using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ステージセレクトシーンの進行を管理する
public class StageSelectSceneManager : MonoBehaviour
{
    // ホームボタン
    [SerializeField] private Button homeButton = null;
    // 戦うボタン
    [SerializeField] private Button fightButton = null;
    

    private void Start() {
        // ボタンの登録
        homeButton.onClick.RemoveAllListeners();
        homeButton.onClick.AddListener(OnClickHomeButton);
        fightButton.onClick.RemoveAllListeners();
        fightButton.onClick.AddListener(OnClickFightButton);
    }

    // ホームボタンを押したとき
    private void OnClickHomeButton() {
        SceneManager.LoadScene("HomeScene");
    }

    // 戦うボタンを押したとき
    private void OnClickFightButton() {
        SceneManager.LoadScene("BattleScene");
    }
}
