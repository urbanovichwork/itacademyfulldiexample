using System;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ITAcademy.FullDI
{
    public class AutomatedPistol : WeaponBase
    {
        private readonly int _bulletCountPerBurst;
        private readonly float _burstDelay;

        public AutomatedPistol(WeaponAutomatedInfo weaponAutomatedInfo, GameObject bulletPrefab) : base(
            weaponAutomatedInfo.FireDelayInSeconds, weaponAutomatedInfo.FireDistance, bulletPrefab)
        {
            _bulletCountPerBurst = weaponAutomatedInfo.BurstCount;
            _burstDelay = weaponAutomatedInfo.BurstDelayInMilliseconds;
        }

        public override async void Fire(Vector3 direction)
        {
            for (int i = 0; i < _bulletCountPerBurst; i++)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(_burstDelay));
                FireBullet(direction);
            }
        }

        private void FireBullet(Vector3 direction)
        {
            var bullet = Object.Instantiate(BulletPrefab, OriginPoint.position, Quaternion.identity);
            bullet.transform.LookAt(direction);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 100f, ForceMode.Impulse);
        }
    }
}