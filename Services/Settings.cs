﻿namespace ApiRESTWithNet6.Services
{
    public static class Settings
    {
        private static readonly IConfigurationRoot _settings = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

        private static readonly string default_connection = _settings.GetConnectionString("DefaultConnection");
        public static JwtConfig GetJwtConfig()
        {
            var jwtconfig = _settings.GetSection("Jwt");
            return new JwtConfig() { 
                Key = jwtconfig["Key"] ?? "rPl0d^Zu&HJF5jI0aKvX1%7yzSPM2H$@", 
                Issuer = jwtconfig["Issuer"] ?? "*", 
                Audience = jwtconfig["Audience"] ?? "*"
            };
        }

        public static string GetDefaulConnectionString()
        {
            return default_connection;
        }
    }

    public class JwtConfig
    {
        public string Key { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
    }
}