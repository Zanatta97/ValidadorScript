using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using ValidadorScript.Application.Conexao;
using ValidadorScript.Domain.Conexao;

namespace ValidadorScript.Infra.Configuracao
{
    public class AppSettingsCredenciaisStore(string caminho) : ICredenciaisStore
    {
        private sealed class ArquivoAppSettings
        {
            public CredenciaisDatabase Credenciais { get; init; } = new();
        }

        private static readonly JsonSerializerOptions Opcoes = new() { WriteIndented = true };
        private Credenciais? _atual;


        /// <summary>
        /// Retorna os dados de credencial de acesso ao banco de dados,
        /// caso não exista nenhum arquivo de configuração, retorna credenciais padrão.
        /// </summary>
        /// <returns></returns>
        public Credenciais Obter()
        {
            //Valida se já existe alguma credencial carregada, se existir só deolve ela.
            if (_atual != null)
                return _atual;

            //Busca do arquivo de configuração, caso não exista, retorna credenciais padrão.
            var arquivo = File.Exists(caminho) ? JsonSerializer.Deserialize<ArquivoAppSettings>(File.ReadAllText(caminho), Opcoes) : null;

            var c = arquivo?.Credenciais ?? new CredenciaisDatabase();

            return _atual = new Credenciais(c.Oracle.SenhaPadrao, c.SqlServer.Usuario, c.SqlServer.SenhaPadrao);

        }

        /// <summary>
        /// Salva as credenciais de acesso ao banco de dados no arquivo de configuração.
        /// </summary>
        /// <param name="credenciais"></param>
        public void Salvar(Credenciais credenciais)
        {
            var arquivo = new ArquivoAppSettings
            {
                Credenciais = new CredenciaisDatabase
                {
                    Oracle = new CredenciaisDatabaseOracle { SenhaPadrao = credenciais.SenhaOracle },
                    SqlServer = new CredenciaisDatabaseSqlServer
                    {
                        Usuario = credenciais.UsuarioSqlServer,
                        SenhaPadrao = credenciais.SenhaSqlServer
                    }
                }
            };

            File.WriteAllText(caminho, JsonSerializer.Serialize(arquivo, Opcoes));
            _atual = credenciais;  // altera para o valor novo, sem reiniciar
        }
    }
}
