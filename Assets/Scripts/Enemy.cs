using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��؂��U������G�ɋ��ʂ̊��N���X(�p������\��)
public class Enemy : MonoBehaviour
{
    // �ړ��X�s�[�h
    [SerializeField] private int speed = 1;
    // �������ꂽ�Ƃ�
    //public void Generated() {

    //}

    private void Update() {
        transform.position += new Vector3(-1.0f * speed * Time.deltaTime, 0.0f, 0.0f);
    }
}
