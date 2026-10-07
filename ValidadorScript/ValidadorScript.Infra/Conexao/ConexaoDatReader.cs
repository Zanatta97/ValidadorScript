using System;
using System.Collections.Generic;
using System.Text;
using ValidadorScript.Application.Conexao;
using ValidadorScript.Domain.Conexao;
using ValidadorScript.Infra.Configuracao;

namespace ValidadorScript.Infra.Conexao
{
    public class ConexaoDatReader(CredenciaisDatabase credenciais) : IDadosConexaoReader
    {

        /// <summary>
        /// Lê os dados de conexão do arquivo conexao.dat e retorna um objeto DadosConexao com as informações lidas.
        /// </summary>
        /// <param name="dados"></param>
        /// <returns>Um objeto DadosConexao com as informações de conexão.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public DadosConexao LerDadosConexao(string dados)
        {
            string? valorTipoDB = null;
            string? valorConexao = null;
            string? valorUsuario = null;

            using var reader = new StringReader(dados);
            string? linha;
            List<string> erros = new List<string>();

            //Lê linha a linha para validar as informações
            while ((linha = reader.ReadLine()) != null)
            {

                linha = linha.Trim();

                //Ignora linhas de comentário e linhas em branco
                if (linha.StartsWith("//") || linha.Length == 0)
                    continue;

                //Valida se a linha contém um sinal de igual, caso contrário, ignora a linha
                var partes = linha.Split('=',2);
                if (partes.Length != 2)
                    continue;

                var chave = partes[0].Trim();
                var valor = partes[1].Trim();

                /*Lê os valores do arquivo e guarda em variáveis
                 *[BANCODADOS] = ORACLE ou SQLSERVER
                 *[DATABASE] = servidor:porta/banco se for Oracle, servidor\instancia:banco se for SQL Server
                 *[USUARIO_ORACLE] = usuário do Oracle
                 */

                switch (chave.ToUpper())
                {
                    case "[BANCODADOS]":

                        if (valorTipoDB != null)
                            erros.Add("O arquivo de conexão contém mais de um tipo de [BANCODADOS].");

                        valorTipoDB = valor;

                        break;
                    case "[DATABASE]":

                        if (valorConexao != null)
                            erros.Add("O arquivo de conexão contém mais de uma linha de [DATABASE].");

                        valorConexao = valor;
                        
                        break;
                    case "[USUARIO_ORACLE]":

                        if (valorUsuario != null)
                            erros.Add("O arquivo de conexão contém mais de uma linha de [USUARIO_ORACLE].");

                        valorUsuario = valor;

                        break;
                }

                continue; 
            }

            //Validações dos valores lidos do arquivo
            TipoDatabase? tipo = null;

            //Verifica se o tipo de banco de dados foi informado e se é válido
            if (valorTipoDB == null)
                erros.Add("O arquivo de conexão não contém o tipo de banco de dados. [BANCODADOS]");
            else if (Enum.GetNames<TipoDatabase>().Contains(valorTipoDB.ToUpper()))
                tipo = Enum.Parse<TipoDatabase>(valorTipoDB.ToUpper());
            else
                erros.Add($"Tipo de [BANCODADOS] inválido: {valorTipoDB}");

            string? servidor = null;
            string? database = null;

            //Valida se os dados de conexão estão preenchidos e se estão no formato correto
            if (string.IsNullOrEmpty(valorConexao))
                erros.Add("O arquivo de conexão não contém o informações de conexão [DATABASE].");
            else
            {
                if (tipo != null)
                {
                    var separador = tipo == TipoDatabase.ORACLE ? '/' : ':'; //Separador Oracle = /, Separador SQL Server = :
                    var partes = valorConexao.Split(separador);

                    if (partes.Length != 2)
                        erros.Add("O arquivo de conexão contém um valor inválido para o servidor e database do banco de dados. [DATABASE]");
                    else
                    {
                        servidor = partes[0].Trim();
                        database = partes[1].Trim();

                        if (string.IsNullOrEmpty(servidor))
                            erros.Add("O arquivo de conexão não contém o servidor do banco de dados. [DATABASE]");
                        if (string.IsNullOrEmpty(database))
                            erros.Add("O arquivo de conexão não contém o database do banco de dados. [DATABASE]");
                    }   
                }
            }

            //Validações para definid o usuário do banco de dados,
            //caso seja Oracle, o usuário deve ser informado no arquivo de conexão,
            //caso seja SQL Server, o usuário será obtido do arquivo de credenciais
            string? usuario = null;
            if (tipo != null)
            {
                if (tipo == TipoDatabase.ORACLE)
                {
                    // Lógica específica para Oracle
                    if (string.IsNullOrEmpty(valorUsuario))
                        erros.Add("O arquivo de conexão tem o tipo ORACLE, mas não contém o [USUARIO_ORACLE].");
                    else
                        usuario = valorUsuario; //Busca o valor informado no arquivo de conexão
                } else if (tipo == TipoDatabase.SQLSERVER)
                {
                    if (valorUsuario != null)
                    {
                        erros.Add("O arquivo de conexão tem o tipo SQLSERVER, mas contém o [USUARIO_ORACLE].");
                    } else
                    {
                        // Lógica específica para SQL Server
                        usuario = credenciais.SqlServer.Usuario; //Busca o valor padrão definido no appsettings.json
                    }
                }
            }

            //Se houver erros, lança uma exceção com a lista de erros encontrados
            if (erros.Count > 0)
                throw new InvalidOperationException(string.Join(Environment.NewLine, erros));
            else
            {
                DadosConexao dadosConexao = new DadosConexao(tipo!.Value, servidor!, database!, usuario!);
            
                return dadosConexao;
            }
        }

        /// <summary>
        /// Obtém os dados de conexão do arquivo conexao.dat.
        /// </summary>
        /// <returns>Um objeto DadosConexao com as informações de conexão.</returns>
        /// <exception cref="InvalidOperationException">Lançada quando há erros na leitura do arquivo de conexão.</exception>
        public DadosConexao ObterDadosConexao()
        {
            //Busca o arquivo conexao.dat da pasta do exe
            var arquivoConexao = Path.Combine(AppContext.BaseDirectory, "conexao.dat");

            //Lê os dados do arquivo
            var conexaoDat = File.ReadAllText(arquivoConexao);

            return LerDadosConexao(conexaoDat);
        }
    }
}
