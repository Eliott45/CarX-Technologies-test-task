using GameCore.Settings.Towers;
using UnityEngine;
using Zenject;

namespace GameCore.Towers.Infrastructure
{
    public class TowersInstaller : MonoInstaller
    {
        [SerializeField] private SimpleTowerSettings _simpleTowerSettings;

        public override void InstallBindings()
        {
            Container
                .Bind<SimpleTowerSettings>()
                .FromInstance(_simpleTowerSettings)
                .AsSingle();
        }
    }
}