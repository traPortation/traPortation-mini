using System.Collections.Generic;
using TraPortation.Moving;
using TraPortation.Moving.Section.Person;
using Zenject;

public class PathInstaller : Installer<PathInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<PersonPathFactory>().AsSingle();

        Container.BindFactory<IReadOnlyList<Station>, TrainUsingSection, TrainUsingSection.Factory>().AsSingle();
        Container.BindFactory<int, IReadOnlyList<Station>, TrainPath, TrainPath.Factory>().AsSingle();
    }
}