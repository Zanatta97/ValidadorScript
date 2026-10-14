using System;
using System.Collections.Generic;
using System.Text;

namespace ValidadorScript.Domain.Conexao
{
    public sealed record Credenciais(string SenhaOracle, string UsuarioSqlServer, string SenhaSqlServer);
}
