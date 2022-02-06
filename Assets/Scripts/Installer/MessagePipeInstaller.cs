using UnityEngine;
using Zenject;
using MessagePipe;

public class MessagePipeInstaller : Installer<MessagePipeInstaller>
{
    public override void InstallBindings()
    {
        var option = Container.BindMessagePipe();

        Container.BindMessageBroker<int, VehicleArrivedEvent>(option);
    }
}