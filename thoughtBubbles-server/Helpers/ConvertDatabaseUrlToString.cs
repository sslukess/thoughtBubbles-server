using System;

namespace ThoughtBubbles.Helpers // Replace with your actual namespace
{
    public static class DatabaseConnectionHelper
    {
        public static string ConvertDatabaseUrlToConnectionString(string databaseUrl)
        {
            var uri = new Uri(databaseUrl);
            var dbName = uri.Segments.Last(); // Get the database name from the URL
            var userInfo = uri.UserInfo.Split(':');
            var username = userInfo[0];
            var password = userInfo[1];

            // Construct the connection string for PostgreSQL
            return $"Host={uri.Host};Port={uri.Port};Database={dbName};Username={username};Password={password};";
        }
    }
}