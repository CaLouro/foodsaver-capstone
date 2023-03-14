using Newtonsoft.Json;
using Supabase;
using Supabase.Gotrue;
using Supabase.Interfaces;

namespace FoodSaverWebApp.Services
{
    internal class DbConnection : IDbConnection
    {
        private Supabase.Client _database;

        private readonly ILogger<DbConnection> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISupabaseSessionHandler _supabaseSessionHandler;

        public DbConnection(ISupabaseSessionHandler supabaseSessionHandler, IHttpContextAccessor httpContextAccessor,
            ILogger<DbConnection> logger)
        {
            _supabaseSessionHandler = supabaseSessionHandler;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
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

            _database = new Supabase.Client(url, key, options);
            _database.Auth.StateChanged += AuthOnStateChanged;

            await _database.InitializeAsync();
        }

        public Supabase.Client AccessDatabase()
        {
            return _database;
        }

        private void AuthOnStateChanged(object? sender, ClientStateChanged clientState)
        {
            if (sender is Supabase.Gotrue.Client client)
            {
                _logger.LogInformation("Auth state changed to: " + clientState.State);
                switch (clientState.State)
                {
                    case Constants.AuthState.SignedIn:
                    case Constants.AuthState.UserUpdated:
                    case Constants.AuthState.PasswordRecovery:
                    case Constants.AuthState.TokenRefreshed:
                        SetHttpSessionData(client.CurrentUser);
                        break;
                    case Constants.AuthState.SignedOut:
                        RemoveHttpSessionData();
                        break;
                }
            }
        }

        private void SetHttpSessionData(User user)
        {
            string displayName;
            if (user.UserMetadata.ContainsKey("display_name"))
            {
                displayName = (string)user.UserMetadata["display_name"];
            }
            else
            {
                displayName = "Anonymous User";
            }

            _httpContextAccessor.HttpContext?.Session.SetString("_DisplayName", displayName);
        }

        private void RemoveHttpSessionData()
        {
            _httpContextAccessor.HttpContext?.Session.Remove("_DisplayName");
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
                    Expires = DateTime.Now.AddDays(30)
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