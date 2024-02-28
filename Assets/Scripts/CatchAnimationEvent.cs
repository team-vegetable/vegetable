using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

// アニメーションイベントを検知して登録されているイベントを呼び出す
public class CatchAnimationEvent : MonoBehaviour {
    // キーとイベントのマップ
    [SerializeField] private List<AnimationMap> animationMaps = new();

    // イベントを呼び出す
    public void InvokeEvent(string key) {
        var animationMap = animationMaps.FirstOrDefault(e => e.Key == key);
        if (animationMap == null) {
            Debug.LogError("指定したキーが存在しませんでした");
            return;
        }

        animationMap.Callback?.Invoke();
    }
}

// キーとイベントのディクショナリー
[System.Serializable]
public class AnimationMap {
    // キー
    [SerializeField] private string key = "";
    public string Key { get => key; }
    
    // 呼び出すイベント
    [SerializeField] private UnityEvent callback = null;
    public UnityEvent Callback { get => callback; }
}
