using System;
using System.Collections.Generic;
using System.Text;

namespace ValidadorScript.Infra.Configuracao
{
    public sealed class CredenciaisDatabase
    {
        public CredenciaisDatabaseOracle Oracle { get; init; } = new();
        public CredenciaisDatabaseSqlServer SqlServer { get; init; } = new();
    }

    public sealed class CredenciaisDatabaseOracle
    {
        public string SenhaPadrao { get; init; } = "";
    }

    public sealed class CredenciaisDatabaseSqlServer
    {
        public string Usuario { get; init; } = "";
        public string SenhaPadrao { get; init; } = "";
    }
}
