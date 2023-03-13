using Newtonsoft.Json;
using Supabase;
using Supabase.Gotrue;
using Supabase.Interfaces;
using Client = Supabase.Client;

namespace FoodSaverWebApp.Services
{
    internal class DbConnection : IDbConnection
    {
        private Client _database;

        private readonly ISupabaseSessionHandler _supabaseSessionHandler;

        public DbConnection(ISupabaseSessionHandler supabaseSessionHandler)
        {
            _supabaseSessionHandler = supabaseSessionHandler;
            InitializeDatabaseConnection();
        }

        private async void InitializeDatabaseConnection()
        {
            const string url = "https://bjgctikxmsxwksxpcbpr.supabase.co";
            const string key =
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImJqZ2N0aWt4bXN4d2tzeHBjYnByIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTY3ODIzODc4MiwiZXhwIjoxOTkzODE0NzgyfQ.qIUUqkzzcXnzzRhQKv72qmrUXsU3zFolmpVHtS2PeHY";

            var options = new SupabaseOptions
            {
                SessionHandler = _supabaseSessionHandler
            };

            _database = new Client(url, key, options);
            await _database.InitializeAsync();
        }

        public Client AccessDatabase()
        {
            return _database;
        }
    }

    internal class CustomSessionHandler : ISupabaseSessionHandler
    {
        private const string SessionCookieKey = "FoodSaver.tjyWsnfyma2opuZjwxAKNcds4zp7YB2mh55vwqEfpVyVvHHSUd";

        private readonly ILogger<CustomSessionHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomSessionHandler(ILogger<CustomSessionHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> SessionPersistor<TSession>(TSession session) where TSession : Session
        {
            _logger.LogInformation("-------- SessionPersistor --------");
            _logger.LogInformation(JsonConvert.SerializeObject(session));

            _httpContextAccessor.HttpContext?.Response.Cookies.Append(
                SessionCookieKey,
                JsonConvert.SerializeObject(session),
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(30)
                });

            return Task.FromResult(true);
        }

        public Task<TSession?> SessionRetriever<TSession>() where TSession : Session
        {
            _logger.LogInformation("-------- SessionRetriever --------");
            _logger.LogInformation(_httpContextAccessor.HttpContext?.Request.Cookies[SessionCookieKey]);

            var sessionCookie = _httpContextAccessor.HttpContext?.Request.Cookies[SessionCookieKey];

            if (sessionCookie == null)
            {
                return Task.FromResult<TSession?>(null);
            }

            try
            {
                var session = JsonConvert.DeserializeObject<TSession>(sessionCookie);

                return Task.FromResult<TSession?>(session);
            }
            catch (Exception)
            {
                return Task.FromResult<TSession?>(null);
            }
        }

        public Task<bool> SessionDestroyer()
        {
            _logger.LogInformation("-------- SessionDestroyer --------");

            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(SessionCookieKey);

            return Task.FromResult(true);
        }
    }
}