using StackExchange.Redis;

namespace PlasticaribeAPI.Service
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ConnectionHelper
    {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
        static ConnectionHelper()
        {
            ConnectionHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(ConfigurationManager.AppSetting["RedisURL"]);
            });
        }
        private static Lazy<ConnectionMultiplexer> lazyConnection;
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
#pragma warning restore CS8604 // Posible argumento de referencia nulo
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
