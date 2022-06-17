using System.Collections.Generic;
using Zenject;
using TraPortation.Moving;
using TraPortation.Moving.Section.Person;
using TraPortation.Moving.Section.Train;

public class PathInstaller : Installer<PathInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<PathFactory>().AsSingle();

        Container.BindFactory<IReadOnlyList<Station>, TrainUsingSection, TrainUsingSection.Factory>().AsSingle();
        Container.BindFactory<IReadOnlyList<Station>, int, float, TrainSection, TrainSection.Factory>().AsSingle();
    }
}