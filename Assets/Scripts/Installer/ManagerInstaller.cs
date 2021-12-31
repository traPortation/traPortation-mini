using UnityEngine;
using Zenject;

public class ManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {

        Container.Bind<Board>().AsSingle().NonLazy();

        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle().NonLazy();

        Container.Bind<StationManager>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}