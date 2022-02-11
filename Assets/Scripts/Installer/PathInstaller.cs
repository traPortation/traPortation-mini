using System.Collections.Generic;
using Zenject;

public class PathInstaller : Installer<PathInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<PathFactory>().AsSingle();

        Container.BindFactory<IReadOnlyList<Station>, TrainSection, TrainSection.Factory>().AsSingle();
    }
}