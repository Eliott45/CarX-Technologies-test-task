namespace GameCore.Projectiles
{
    public class CannonProjectile : Projectile
    {
        private protected override void ReturnToPool() => 
            Destroy(gameObject); // TODO reset rigidbody before return to pool
    }
}