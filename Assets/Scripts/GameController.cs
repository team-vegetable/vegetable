using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    private static GameController instance = null;
    public static GameController Instance {
        get {
            if (instance == null) {
                // Resourcesフォルダーからの読み込みを行う
                var gameObject = (GameObject)Resources.Load("GameController");
                instance = Instantiate(gameObject).GetComponent<GameController>();
            }
            return instance;
        }

        private set {
            instance = value;
        }
    }

    // デバッグ用にメインの野菜を初期から指定する
    [SerializeField] private List<Vegetable> mainVegetables = new();

    // 戦闘に使用するの3体の野菜を格納する
    public List<Vegetable> MainVegetables { get; private set; } = new();

    private void Start() {
        MainVegetables = mainVegetables;
    }

    // 戦闘で使用する野菜のセット
    public void SetMainVegetables(List<Vegetable> vegetables) {
        MainVegetables = vegetables;
    }

    public void Test() {
        MainVegetables = mainVegetables;
    }
}

