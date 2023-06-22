using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace ITAcademy.FullDI
{
    public class EnemyService
    {
        private readonly List<EnemyController> _enemies;

        public EnemyService()
        {
            _enemies = new List<EnemyController>();
        }

        public void AddEnemy(EnemyController enemyController)
        {
            _enemies.Add(enemyController);
        }

        public (Vector3 Position, float Distance)? GetEnemyClosestTo(Vector3 pos)
        {
            if (_enemies == null || _enemies.Count == 0)
            {
                return null;
            }

            EnemyController enemy = _enemies.OrderBy(enemy => Vector3.Distance(pos, enemy.GetPosition())).First();
            var smallestDistance = Vector3.Distance(pos, enemy.GetPosition());
            return (enemy.GetPosition(), smallestDistance);
        }
    }
}