using System;
using System.Collections.Generic;
using System.Text;

namespace ValidadorScript.Domain.Conexao
{
    public sealed record DadosConexao
    (
        TipoDatabase Tipo,
        string Servidor,
        string Database,
        string Usuario);
}
