using System;
using System.Collections;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

// PHPと通信するクラス
public class APIController : MonoBehaviour
{
    private static APIController instance = null;
    public static APIController Instance {
        get {
            if (instance == null) {
                var gameObject = (GameObject)Resources.Load("APIController");
                instance = Instantiate(gameObject).GetComponent<APIController>();
            }
            return instance;
        }

        private set {
            instance = value;
        }
    }

    // データ取得のみ時のデータ
    private string dataByGetOnly = "";

    // データの取得のみを行う
    public void Get(string controller, string function, UnityAction<string> callback) {
        StartCoroutine(OnGet(controller, function, callback));
    }

    private IEnumerator OnGet(string controller, string function, UnityAction<string> callback) {
        // URLの作成
        string controllerName = controller + "Controller";
        string url = "http://localhost/vegetable/" + controllerName +".php?controller=" + controllerName +"&function=" + function;
        var request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success) {
            Debug.LogError("通信に失敗しました");
        }

        callback?.Invoke(request.downloadHandler.text);
        // dataByGetOnly = request.downloadHandler.text;
    }
}
