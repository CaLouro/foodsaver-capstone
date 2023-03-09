namespace FoodSaverWebApp.Services
{
    public interface IDbConnection
    {
        public Supabase.Client AccessDatabase();
    }
}