using System.Collections.Generic;
using TraPortation.Moving;
using TraPortation.Traffic;
using TraPortation.Traffic.Node;
using TraPortation.UI;
using Zenject;

namespace TraPortation
{
    public class ManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Board>().AsSingle().NonLazy();
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<StationManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<RailManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<ManageMoney>().FromNew().AsSingle().NonLazy();
            Container.Bind<LineManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<SetTrainManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<BusStationManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<SetBusRailManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<SetBusManager>().FromComponentInHierarchy().AsSingle().NonLazy();

            Container.Bind<AudioSwitcher>().FromComponentInHierarchy().AsSingle().NonLazy();

            // 増えてきたら単独のInstallerに分ける
            Container.BindFactory<List<Station>, int, string, Rail, Rail.Factory>();
            Container.BindFactory<int, IReadOnlyList<IBoardNode>, BusRail, BusRail.Factory>();
            Container.BindFactory<IntersectionNode, IntersectionNode, float, Road, Road.Factory>();

            UIInstaller.Install(Container);
            MessagePipeInstaller.Install(Container);
            PathInstaller.Install(Container);
        }
    }
}