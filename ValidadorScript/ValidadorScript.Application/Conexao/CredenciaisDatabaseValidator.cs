using System;
using System.Collections.Generic;
using System.Text;
using ValidadorScript.Domain.Conexao;

namespace ValidadorScript.Application.Conexao
{
    public static class CredenciaisDatabaseValidator
    {
        /// <summary>
        /// Valida as credenciais das configs, verificando se os campos obrigatórios estão preenchidos.
        /// </summary>
        /// <param name="credenciais"></param>
        /// <returns></returns>
        public static List<string> ValidarCredenciais(Credenciais credenciais)
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(credenciais.SenhaOracle))
                erros.Add("Informe a senha padrão para Oracle");
            if (string.IsNullOrWhiteSpace(credenciais.SenhaSqlServer))
                erros.Add("Informe a senha padrão para SQL Server");
            if (string.IsNullOrWhiteSpace(credenciais.UsuarioSqlServer))
                erros.Add("Informe o usuário padrão do SQL Server");

            return erros;
        }
    }
}
