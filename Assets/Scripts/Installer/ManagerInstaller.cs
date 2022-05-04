using System.Collections.Generic;
using Zenject;
using Traffic;
using Traffic.Node;
using Moving;

public class ManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Board>().AsSingle().NonLazy();
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<StationManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<RailManager>().FromNew().AsSingle().NonLazy();

        // 増えてきたら単独のInstallerに分ける
        Container.BindFactory<List<Station>, int, string, Rail, Rail.Factory>();
        Container.BindFactory<IntersectionNode, IntersectionNode, Road, Road.Factory>();

        UIInstaller.Install(Container);
        MessagePipeInstaller.Install(Container);
        PathInstaller.Install(Container);
    }
}