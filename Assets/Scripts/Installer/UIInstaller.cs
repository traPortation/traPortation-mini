using UnityEngine;
using Zenject;

public class UIInstaller : Installer<UIInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<Line>().FromNewComponentOnNewGameObject().AsTransient();
    }
}