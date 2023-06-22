using UnityEngine;
using Object = UnityEngine.Object;

namespace ITAcademy.FullDI
{
    public class Pistol : WeaponBase
    {
        public Pistol(WeaponBaseInfo baseInfo, GameObject bulletPrefab) : base(baseInfo.FireDelayInSeconds, baseInfo.FireDistance,
            bulletPrefab)
        {
        }

        public override void Fire(Vector3 direction)
        {
            var bullet = Object.Instantiate(BulletPrefab, OriginPoint.position, Quaternion.identity);
            bullet.transform.LookAt(direction);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 100f, ForceMode.Impulse);
        }
    }
}