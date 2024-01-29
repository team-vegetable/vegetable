using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    private static GameController instance = null;
    public static GameController Instance {
        get {
            if (instance == null) {
                // Resources�t�H���_�[����̓ǂݍ��݂��s��
                var gameObject = (GameObject)Resources.Load("GameController");
                instance = Instantiate(gameObject).GetComponent<GameController>();
            }
            return instance;
        }

        private set {
            instance = value;
        }
    }

    // �퓬�Ɏg�p�����3�̖̂�؂��i�[����
    public List<Vegetable> MainVegetables { get; private set; } = new();

    // �퓬�Ŏg�p�����؂̃Z�b�g
    public void SetMainVegetables(List<Vegetable> vegetables) {
        MainVegetables = vegetables;
    }
}

