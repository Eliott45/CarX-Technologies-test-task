using UnityEngine;

namespace GameCore.Towers.Controllers
{
    public class SimpleTowerController : TowerController
    {
        protected override void Attack(GameObject target) 
        {
            var projectile = projectilesFactory.CreateProjectile(settings.ProjectileKeyword, muggleTransform);
            projectile.SetTarget(target);
            
            base.Attack(target);
        }
    }
}