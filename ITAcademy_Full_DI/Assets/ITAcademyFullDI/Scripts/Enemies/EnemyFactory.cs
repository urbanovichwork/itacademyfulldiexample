using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ITAcademy.FullDI
{
    public class EnemyFactory
    {
        private readonly PlayerFactory _playerFactory;
        private readonly EnemyInfos _enemyInfos;
        private readonly EnemyService _enemyService;

        public EnemyFactory(PlayerFactory playerFactory, EnemyService enemyService, EnemyInfos enemyInfos)
        {
            _enemyService = enemyService;
            _playerFactory = playerFactory;
            _enemyInfos = enemyInfos;
        }

        public async UniTask Create(EnemyType enemyType)
        {
            var enemyInfo = _enemyInfos.GetEnemy(enemyType);
            var prefab = await Addressables.LoadAssetAsync<GameObject>(enemyInfo.PrefabReference).ToUniTask();
            var enemyObject = Object.Instantiate(prefab);
            var randomPointInCircle = Random.insideUnitCircle * 10f;
            enemyObject.transform.position = new Vector3(randomPointInCircle.x, 0, randomPointInCircle.y);
            var enemyController = enemyObject.GetComponent<EnemyController>();
            enemyController.Initialize(_playerFactory, enemyType, enemyInfo.Speed);
            _enemyService.AddEnemy(enemyController);
        }
    }
}