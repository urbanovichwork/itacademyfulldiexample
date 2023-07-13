using System;
using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace ITAcademy.FullDI
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [Range(0, 1)] [SerializeField] private NavMeshAgent _agent;

        private PlayerFactory _playerFactory;
        private EnemyType _enemyType;
        private float _speed;

        public void Initialize(PlayerFactory playerFactory, EnemyType enemyType, float speed)
        {
            _playerFactory = playerFactory;
            _enemyType = enemyType;
            _speed = speed;

            _agent.speed = _speed;

            //StartBehavior();
        }

        private void StartBehavior()
        {
            Observable.Timer(TimeSpan.FromSeconds(1)).Repeat().Subscribe(_ => FollowPlayer());
        }

        private void FollowPlayer()
        {
            var playerController = _playerFactory.PlayerController;
            if (playerController == null)
                return;

            _agent.SetDestination(playerController.GetPosition());
        }

        public Vector3 GetPosition() => transform.position;

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, transform.up, _radius);
            Handles.color = Color.white;
        }
#endif
    }
}