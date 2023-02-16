using UnityEngine;

namespace GameCore.Towers.Controllers
{
    public class CannonTowerController : TowerController
    {
        protected override void Attack(GameObject target)
        {
            projectilesFactory.CreateProjectile(settings.ProjectileKeyword, muggleTransform);
            
            base.Attack(target);
        }
    }
}