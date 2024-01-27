using System;
using System.Collections;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

// PHP�ƒʐM����N���X
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

    // �f�[�^�擾�̂ݎ��̃f�[�^
    private string dataByGetOnly = "";

    // �f�[�^�̎擾�݂̂��s��
    public void Get(string controller, string function, UnityAction<string> callback) {
        StartCoroutine(OnGet(controller, function, callback));
    }

    private IEnumerator OnGet(string controller, string function, UnityAction<string> callback) {
        // URL�̍쐬
        string controllerName = controller + "Controller";
        string url = "http://localhost/vegetable/" + controllerName +".php?controller=" + controllerName +"&function=" + function;
        var request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success) {
            Debug.LogError("�ʐM�Ɏ��s���܂���");
        }

        callback?.Invoke(request.downloadHandler.text);
        // dataByGetOnly = request.downloadHandler.text;
    }
}
