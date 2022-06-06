using UnityEngine;
using Zenject;

public class UIInstaller : Installer<UIInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<UI.ILine>().To<UI.Line>().FromNewComponentOnNewGameObject().AsTransient();
    }
}