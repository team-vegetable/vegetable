using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

// �A�j���[�V�����C�x���g�����m���ēo�^����Ă���C�x���g���Ăяo��
public class CatchAnimationEvent : MonoBehaviour {
    // �L�[�ƃC�x���g�̃}�b�v
    [SerializeField] private List<AnimationMap> animationMaps = new();

    // �C�x���g���Ăяo��
    public void InvokeEvent(string key) {
        var animationMap = animationMaps.FirstOrDefault(e => e.Key == key);
        if (animationMap == null) {
            Debug.LogError("�w�肵���L�[�����݂��܂���ł���");
            return;
        }

        animationMap.Callback?.Invoke();
    }
}

// �L�[�ƃC�x���g�̃f�B�N�V���i���[
[System.Serializable]
public class AnimationMap {
    // �L�[
    [SerializeField] private string key = "";
    public string Key { get => key; }
    
    // �Ăяo���C�x���g
    [SerializeField] private UnityEvent callback = null;
    public UnityEvent Callback { get => callback; }
}
