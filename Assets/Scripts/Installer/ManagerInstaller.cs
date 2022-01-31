using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class ManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Board>().AsSingle().NonLazy();
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<StationManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<RailManager>().FromNew().AsSingle().NonLazy();

        // 増えてきたら単独のInstallerに分ける
        Container.BindFactory<List<PathNode>, int, string, Rail, Rail.Factory>();

        UIInstaller.Install(Container);
        MessagePipeInstaller.Install(Container);
    }
}