namespace Common.Network.Packets
{
    public static class Container
    {
        public static MessageContainer GetContainer(string identifier,object valueForContainer)
        {
            var container = new MessageContainer
            {
                Identifier = identifier,
                Payload = valueForContainer
            };

            return container;
        }
    }
}
