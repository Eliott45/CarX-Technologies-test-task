using GameCore.Enemies;

namespace GameCore.Towers.Controllers
{
    public class SimpleTowerController : TowerController
    {
        protected override void Attack(IEnemy target) 
        {
            var projectile = projectilesFactory.CreateProjectile(settings.ProjectileKeyword, muggleTransform);
            projectile.SetTarget(target.GetGameObject());
            
            base.Attack(target);
        }
    }
}