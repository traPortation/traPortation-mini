using System.Collections.Generic;
using TraPortation.Moving;
using TraPortation.Moving.Section.Person;
using TraPortation.Traffic;
using Zenject;

public class PathInstaller : Installer<PathInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<PersonPathFactory>().AsSingle();

        Container.BindFactory<IReadOnlyList<Station>, TrainUsingSection, TrainUsingSection.Factory>().AsSingle();
        Container.BindFactory<IReadOnlyList<BusStation>, BusUsingSection, BusUsingSection.Factory>().AsSingle();
        Container.BindFactory<int, IReadOnlyList<Station>, TrainPath, TrainPath.Factory>().AsSingle();
        Container.BindFactory<int, IReadOnlyList<BusRoute>, BusPath, BusPath.Factory>().AsSingle();
    }
}