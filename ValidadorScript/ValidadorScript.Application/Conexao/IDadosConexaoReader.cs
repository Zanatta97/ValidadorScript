using System;
using System.Collections.Generic;
using System.Text;
using ValidadorScript.Domain.Conexao;

namespace ValidadorScript.Application.Conexao
{
    public interface IDadosConexaoReader
    {
        DadosConexao ObterDadosConexao();
    }
}
