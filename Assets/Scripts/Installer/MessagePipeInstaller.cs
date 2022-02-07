using Zenject;
using MessagePipe;
using Event;

public class MessagePipeInstaller : Installer<MessagePipeInstaller>
{
    public override void InstallBindings()
    {
        var option = Container.BindMessagePipe();

        Container.BindMessageBroker<int, StationArrivedEvent>(option);
    }
}