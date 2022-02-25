namespace FstrApi
{
    public static class Connection
    {
        public static string GetConnectionString()
        {
            string? host = Environment.GetEnvironmentVariable("FSTR_DB_HOST");
            string? port = Environment.GetEnvironmentVariable("FSTR_DB_PORT");
            string? login = Environment.GetEnvironmentVariable("FSTR_DB_LOGIN");
            string? password = Environment.GetEnvironmentVariable("FSTR_DB_PASS");

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("FSTR_DB_HOST", EnvironmentVariableTarget.Machine)))
            {
                host = Environment.GetEnvironmentVariable("FSTR_DB_HOST", EnvironmentVariableTarget.Machine);
                port = Environment.GetEnvironmentVariable("FSTR_DB_PORT", EnvironmentVariableTarget.Machine);
                login = Environment.GetEnvironmentVariable("FSTR_DB_LOGIN", EnvironmentVariableTarget.Machine);
                password = Environment.GetEnvironmentVariable("FSTR_DB_PASS", EnvironmentVariableTarget.Machine);
            }


            return $"Host={host};Port={port};Database=FSTR_DB;Username={login};Password={password}";
        }
    }
}
