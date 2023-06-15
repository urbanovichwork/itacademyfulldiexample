using System.Collections.Generic;
using UnityEngine;

namespace ITAcademy.FullDI
{
    public class EnemyFactory
    {
        private readonly PlayerFactory _playerFactory;
        private readonly EnemyInfos _enemyInfos;

        private List<EnemyController> _enemies;

        public EnemyFactory(PlayerFactory playerFactory, EnemyInfos enemyInfos)
        {
            _playerFactory = playerFactory;
            _enemyInfos = enemyInfos;

            _enemies = new List<EnemyController>();
        }

        public void Create(EnemyType enemyType)
        {
            var enemyInfo = _enemyInfos.GetEnemy(enemyType);
            var enemyObject = Object.Instantiate(enemyInfo.Prefab);
            var enemyController = enemyObject.GetComponent<EnemyController>();
            enemyController.Initialize(_playerFactory, enemyType, enemyInfo.Speed);
            _enemies.Add(enemyController);
        }
    }
}