using System;
using System.Collections.Generic;
using System.Text;
using ValidadorScript.Domain.Conexao;

namespace ValidadorScript.Application.Conexao
{
    public interface ICredenciaisStore
    {
        Credenciais Obter();
        void Salvar(Credenciais credenciais);
    }
}
