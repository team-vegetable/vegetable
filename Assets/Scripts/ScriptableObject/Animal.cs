using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����̃p�����[�^�[�֘A
[CreateAssetMenu(fileName = "Animal")]
public class Animal : ScriptableObject
{
    [Header("ID")]
    [SerializeField] private int id = 0;
    public int ID { get => id; }

    [Header("���O")]
    [SerializeField] private new string name = "";
    public string Name { get => name; }

    [Header("�ړ��X�s�[�h")]
    [SerializeField] private int speed = 0;
    public int Speed { get => speed; }

    [Header("�U���͈�")]
    [SerializeField] private int attackRange = 0;
    public int AttackRange { get => attackRange; }

    [Header("�ő�HP")]
    [SerializeField] private int maxHP = 0;
    public int MaxHP { get => maxHP; }

    [Header("�摜")]
    [SerializeField] private Sprite sprite = null;
    public Sprite Sprite { get => sprite; }

    [Header("�v���n�u")]
    [SerializeField] private GameObject prefab = null;
    public GameObject Prefab { get => prefab; }

    // �������Ƃ�ID
    public enum ANIMAL {
        // �C�m�V�V
        WildBoar = 1,
    }
}