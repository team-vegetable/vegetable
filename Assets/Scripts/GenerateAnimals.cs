using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// バトル時の動物生成用のクラス
public class GenerateAnimals : MonoBehaviour
{
    // 生成する動物
    [SerializeField] private GameObject prefab = null;
    // 生成する敵の親オブジェクト
    [SerializeField] private Transform parent = null;
    // 3体の野菜の座標
    [SerializeField] private List<Transform> vegetablePositions = null;
    // 連続で生成したときのYオフセット(重ならないようにするため)
    [SerializeField] private float offsetY = 0.0f;

    // 手前の野菜を攻撃している動物の数
    private int frontAnimalsCount = 0;
    // 動物のアセットをリストで格納
    private List<Animal> animalAssets = new();

    // 先に生成された動物ほど手前に表示する必要があるので、最大のOrderInLayerを指定
    private const int MAX_SORTING_ORDER = 100;

    // 初期化
    public void Init(List<Animal> animalAssets) {
        this.animalAssets = animalAssets;
    }

    // 生成
    public void Generate() {
        var animal = Instantiate(prefab, transform.position, Quaternion.identity, parent).GetComponent<Enemy>();
        var asset = animalAssets.FirstOrDefault(e => e.ID == (int)Animal.ANIMAL.WildBoar);

        // TODO : とりあえず人参をめがけて移動しているので後程変更
        var targetPosition = new Vector2(vegetablePositions[0].position.x, vegetablePositions[0].position.y + frontAnimalsCount * offsetY);
        animal.Init(asset, targetPosition, MAX_SORTING_ORDER - frontAnimalsCount);
        frontAnimalsCount++;
    }
}
