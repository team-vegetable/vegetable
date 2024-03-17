using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Tilemaps;

// �S�Ă̖�؂Ɍp����������N���X
public class BaseVegetable : MonoBehaviour
{
    // ��b�����܂Ƃ߂�����
    [SerializeField] protected Vegetable vegetable = null;

    [SerializeField] private float radius = 0.0f;

    // �Ώۂ̓���(�Ƃ肠������̂���)
    protected BaseAnimal targetAnimal = null;
    // �U���ł��邩�ǂ���
    protected bool canAttack = true;
    // ���݂�HP
    private int currentHP = 0;

    public Vegetable Vegetable { get => vegetable; }

    private async void Start() {
        currentHP = Vegetable.BattleStatus.MaxHP;

        await GetTargetAnimal();
    }

    private async void Update() {
        // �Ώۂ�������Ύ擾����
        //if (targetAnimal == null) {
        //    var collider = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Animal"));
        //    if (collider != null) {
        //        if (collider.gameObject.transform.parent.TryGetComponent<BaseAnimal>(out targetAnimal)) {
        //            targetAnimal.OnEnemyDead.Subscribe(_ => UpdateTarget()).AddTo(this);
        //        }
        //    }
        //}

        // �Ώۂ����čU���\�Ȃ�U������
        if (canAttack && targetAnimal != null) {
            await Attack();
        }
    }

    // �U��
    public virtual async UniTask Attack() {
        await UniTask.CompletedTask;
    }

    // �_���[�W���󂯂���
    public void TakeDamage(int damage) {
        currentHP -= damage;
        // Debug.Log($"{gameObject.name}�̎c���HP : {currentHP}");
    }

    // �Ώۂ̓G��HP��0�ɂȂ�����V�����Ώۂ̓G���Z�b�g����
    private async void UpdateTarget() {
        // ���݂̑Ώۂ�null�ɂ���
        targetAnimal = null;
        await GetTargetAnimal();
    }

    // �Ώۂ̓������擾����(1�̂���)
    private async UniTask GetTargetAnimal() {
        BaseAnimal animal = null;

        while (targetAnimal == null) {
            await UniTask.Yield();
            var collider2D = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Animal"));
            if (collider2D != null) {                
                if (animal == null) {
                    animal = collider2D.transform.parent.GetComponent<BaseAnimal>();
                }
                
                // �擾����������HP���܂�����ΑΏۂƂ��ăZ�b�g����
                if (animal != null) {
                    if (!animal.IsDead()) {
                        targetAnimal = animal;
                    }
                }
            }
        }

        targetAnimal.OnEnemyDead.Subscribe(_ => UpdateTarget()).AddTo(this);
    }

    // ���G�͈͂̃M�Y���̕\��
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
