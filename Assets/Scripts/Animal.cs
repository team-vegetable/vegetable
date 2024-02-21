using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����̃p�����[�^�[�֘A
[CreateAssetMenu(fileName = "Animal")]
public class Animal : ScriptableObject
{
    [Header("���O")]
    [SerializeField] private new string name = "";
    public string Name { get => name; }

    [Header("�摜")]
    [SerializeField] private Sprite sprite = null;
    public Sprite Sprite { get => sprite; }


}
