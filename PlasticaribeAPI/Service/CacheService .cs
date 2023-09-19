using Newtonsoft.Json;
using StackExchange.Redis;

namespace PlasticaribeAPI.Service
{
    public class CacheService : ICacheService
    {
        private IDatabase _db;
        public CacheService()
        {
            ConfigureRedis();
        }
        private void ConfigureRedis()
        {
            _db = ConnectionHelper.Connection.GetDatabase();
        }
        public T GetData<T>(string key)
        {
            var value = _db.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
#pragma warning disable CS8604 // Posible argumento de referencia nulo
                return JsonConvert.DeserializeObject<T>(value);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
            }
            return default;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
            return isSet;
        }
        public object RemoveData(string key)
        {
            bool _isKeyExist = _db.KeyExists(key);
            if (_isKeyExist == true)
            {
                return _db.KeyDelete(key);
            }
            return false;
        }
    }
}
