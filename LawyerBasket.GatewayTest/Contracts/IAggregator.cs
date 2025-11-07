namespace LawyerBasket.GatewayTest.Contracts
{
    public interface IAggregator<T>
    {
        Task<T> AggregateAsync(string id);
    }
}
