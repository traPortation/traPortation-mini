using Zenject;
using MessagePipe;
using Event.Train;
using Event;

public class MessagePipeInstaller : Installer<MessagePipeInstaller>
{
    public override void InstallBindings()
    {
        var option = Container.BindMessagePipe();

        Container.BindMessageBroker<int, StationEvent>(option);
        Container.BindMessageBroker<int, TrainEvent>(option);
        Container.BindMessageBroker<StationClickedEvent>(option);
    }
}