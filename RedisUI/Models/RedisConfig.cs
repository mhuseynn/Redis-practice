using StackExchange.Redis;

namespace RedisUI.Models
{
    public static class RedisConfig
    {
        private static ConnectionMultiplexer muxer;
        public static IDatabase Database { get; private set; }

        static RedisConfig()
        {
            muxer = ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = { { "redis-18714.c16.us-east-1-3.ec2.redns.redis-cloud.com", 18714 } },
                User = "default",
                Password = "IgMjUjXVjS3u7yGevmQ0P3ju6rtW3ryk",
            });

            Database = muxer.GetDatabase();
        }
    }
}
