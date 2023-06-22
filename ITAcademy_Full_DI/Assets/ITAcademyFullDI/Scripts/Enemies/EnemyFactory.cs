using UnityEngine;

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

        public void Create(EnemyType enemyType)
        {
            var enemyInfo = _enemyInfos.GetEnemy(enemyType);
            var enemyObject = Object.Instantiate(enemyInfo.Prefab);
            var randomPointInCircle = Random.insideUnitCircle * 10f;
            enemyObject.transform.position = new Vector3(randomPointInCircle.x, 0, randomPointInCircle.y);
            var enemyController = enemyObject.GetComponent<EnemyController>();
            enemyController.Initialize(_playerFactory, enemyType, enemyInfo.Speed);
            _enemyService.AddEnemy(enemyController);
        }
    }
}