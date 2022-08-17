using TraPortation.UI;
using UnityEngine;
using Zenject;

public class UIInstaller : Installer<UIInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ILine>().To<Line>().FromNewComponentOnNewGameObject().AsTransient();
        Container.Bind<IRoadView>().To<RoadView>().FromNewComponentOnNewGameObject().AsTransient();
        Container.Bind<IRailView>().To<RailView>().FromNewComponentOnNewGameObject().AsTransient();
    }
}