namespace Spring.Event
{
    public interface IEventReceiver
    {
        void invoke(IEvent eve);
    }
}