using UnityEngine;

namespace GameCore.Projectiles
{
    public class CannonProjectile : Projectile
    {
        private void Start()
        {
            Shot();
        }

        private void Shot()
        {
            rb.AddForce(Vector3.forward * 10, ForceMode.Impulse);
        }
    }
}