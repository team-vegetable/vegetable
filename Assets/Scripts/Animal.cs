using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 動物のパラメーター関連
[CreateAssetMenu(fileName = "Animal")]
public class Animal : ScriptableObject
{
    [Header("名前")]
    [SerializeField] private new string name = "";
    public string Name { get => name; }

    [Header("画像")]
    [SerializeField] private Sprite sprite = null;
    public Sprite Sprite { get => sprite; }


}
