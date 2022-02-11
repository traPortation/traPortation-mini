using System.Collections.Generic;
using Zenject;
using Moving;
using Moving.Section;

public class PathInstaller : Installer<PathInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<PathFactory>().AsSingle();

        Container.BindFactory<IReadOnlyList<Station>, TrainSection, TrainSection.Factory>().AsSingle();
    }
}