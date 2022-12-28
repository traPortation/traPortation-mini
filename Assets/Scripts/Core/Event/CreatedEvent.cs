namespace TraPortation.Event
{
    public enum CreateType
    {
        Train,
        Bus,
        Station,
        BusStation,
        Rail,
        BusRail,
    }

    public class CreatedEvent
    {
        public readonly CreateType Type;
        public CreatedEvent(CreateType type)
        {
            this.Type = type;
        }
    }
}