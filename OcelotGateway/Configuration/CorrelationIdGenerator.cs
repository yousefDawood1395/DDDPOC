namespace OcelotGateway.Configuration
{
    public class CorrelationIdGenerator : ICorrelationIdGenerator
    {
        private string _correlationId = new Guid().ToString();
        public string Get() => _correlationId;
        

        public void Set(string correlationId)=>_correlationId = correlationId;
        
    }
}
