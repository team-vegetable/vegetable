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
}

