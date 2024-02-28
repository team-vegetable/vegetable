using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// �o�g�����̓��������p�̃N���X
public class GenerateAnimals : MonoBehaviour
{
    // �������铮��
    [SerializeField] private GameObject prefab = null;
    // ��������G�̐e�I�u�W�F�N�g
    [SerializeField] private Transform parent = null;
    // 3�̖̂�؂̍��W
    [SerializeField] private List<Transform> vegetablePositions = null;
    // �A���Ő��������Ƃ���Y�I�t�Z�b�g(�d�Ȃ�Ȃ��悤�ɂ��邽��)
    [SerializeField] private float offsetY = 0.0f;

    // ��O�̖�؂��U�����Ă��铮���̐�
    private int frontAnimalsCount = 0;

    // ��ɐ������ꂽ�����قǎ�O�ɕ\������K�v������̂ŁA�ő��OrderInLayer���w��
    private const int MAX_SORTING_ORDER = 100;

    // ������
    public void Init(List<Transform> vegetablePositions) {
        this.vegetablePositions = vegetablePositions;
    }

    public void Generate(Animal animalData, UnityAction onDead) {
        var animal = Instantiate(animalData.Prefab, transform.position, Quaternion.identity, parent).GetComponent<BaseAnimal>();

        // TODO : �Ƃ肠�����l�Q���߂����Ĉړ����Ă���̂Ō���ύX
        var targetPosition = new Vector2(vegetablePositions[0].position.x, vegetablePositions[0].position.y + frontAnimalsCount * offsetY);
        animal.Init(targetPosition, MAX_SORTING_ORDER - frontAnimalsCount, onDead);
        frontAnimalsCount++;
    }
}
