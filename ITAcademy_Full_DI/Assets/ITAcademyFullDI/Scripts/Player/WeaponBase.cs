using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ITAcademy.FullDI
{
    public abstract class WeaponBase
    {
        protected Transform OriginPoint { get; private set; }
        protected GameObject VisualObject { get; set; }
        protected bool IsActive { get; set; }
        protected float FireDelay { get; }
        protected float FireDistance { get; }
        protected GameObject BulletPrefab { get; }
        protected Vector3 LookDirection { get; private set; }

        private float _delayTemp;
        private CancellationTokenSource _cancellationTokenSource;

        protected WeaponBase(float fireDelay, float fireDistance, GameObject bulletPrefab)
        {
            FireDelay = fireDelay;
            FireDistance = fireDistance;
            BulletPrefab = bulletPrefab;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void AddOriginElement(GameObject visualObject)
        {
            VisualObject = visualObject;
            AddOriginElement(VisualObject.transform);
        }

        public void AddOriginElement(Transform point)
        {
            OriginPoint = point;
        }

        public void UpdateTarget(Vector3 lookPos, float distanceToTarget)
        {
            LookDirection = lookPos;

            ActivationProcessing(distanceToTarget);
        }

        public void ResetTarget()
        {
            Rotate(Vector3.zero);
            IsActive = false;
            _delayTemp = 0;
        }

        public void Tick()
        {
            FireDelayProcessing();
        }

        private void ActivationProcessing(float distanceToTarget)
        {
            if (distanceToTarget < FireDistance)
            {
                IsActive = true;
                Rotate(LookDirection);
            }
            else
            {
                ResetTarget();
            }
        }

        private void FireDelayProcessing()
        {
            if (IsActive)
            {
                _delayTemp += Time.deltaTime;
                if (_delayTemp > FireDelay)
                {
                    _delayTemp = 0;
                    Fire(LookDirection);
                }
            }
        }

        public async UniTaskVoid StartShootingProcess()
        {
            Shoot();
            await UniTask.Delay(TimeSpan.FromSeconds(FireDelay), cancellationToken: _cancellationTokenSource.Token).SuppressCancellationThrow();
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                StartShootingProcess().Forget();
            }
        }

        public void CancelShootingProcess()
        {
            _cancellationTokenSource.Cancel();
        }

        private void Shoot()
        {
            Fire(LookDirection);
        }

        private void Rotate(Vector3 direction)
        {
            if (VisualObject != null)
            {
                VisualObject.transform.LookAt(direction);
            }
        }

        public Vector3 GetPosition() => OriginPoint.position;

        public abstract void Fire(Vector3 direction);
    }
}