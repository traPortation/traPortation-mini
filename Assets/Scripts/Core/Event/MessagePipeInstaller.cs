using MessagePipe;
using TraPortation.Event;
using TraPortation.Event.Bus;
using TraPortation.Event.Train;
using Zenject;

public class MessagePipeInstaller : Installer<MessagePipeInstaller>
{
    public override void InstallBindings()
    {
        var option = Container.BindMessagePipe();

        Container.BindMessageBroker<int, StationEvent>(option);
        Container.BindMessageBroker<int, TrainEvent>(option);
        Container.BindMessageBroker<GetOnTrainEvent>(option);

        Container.BindMessageBroker<int, BusEvent>(option);
        Container.BindMessageBroker<int, BusStationEvent>(option);

        Container.BindMessageBroker<CreatedEvent>(option);
    }
}