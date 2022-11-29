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
            Container.Bind<LineManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<SetTrainManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<BusStationManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<SetBusRailManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();

            // 増えてきたら単独のInstallerに分ける
            Container.BindFactory<List<Station>, int, string, Rail, Rail.Factory>();
            Container.BindFactory<IReadOnlyList<IBoardNode>, BusRail, BusRail.Factory>();
            Container.BindFactory<IntersectionNode, IntersectionNode, Road, Road.Factory>();

            UIInstaller.Install(Container);
            MessagePipeInstaller.Install(Container);
            PathInstaller.Install(Container);
        }
    }
}