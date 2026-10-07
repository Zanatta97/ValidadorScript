using ValidadorScript.Domain.Conexao;
using ValidadorScript.Infra.Conexao;
using ValidadorScript.Infra.Configuracao;

namespace ValidadorScript.Infra.Tests.Conexao
{
    public class ConexaoDatReaderTests
    {
        //[Fact] de sucesso: um cenário fixo, um resultado esperado.
        [Fact]
        public void LerDadosConexao_ArquivoOracleValido_RetornaDadosConexao()
        {
            // Arrange: conteúdo do conexao.dat fixo no próprio teste (dados inventados)
            var reader = CriarReader();
            var conteudo = """
                // Arquivo de conexão de teste
                [BANCODADOS]=ORACLE


                [DATABASE]=servidor-teste:1521/BASE_TESTE
                [USUARIO_ORACLE]=usuario_teste
                //[USUARIO_ORACLE]=usuario_ignorado
                [CHAVE_IGNORADA]=teste
                Linha sem igual para teste
                """;

            // Act
            var resultado = reader.LerDadosConexao(conteudo);

            // Assert: DadosConexao é record, então a igualdade compara Tipo, Servidor, Database e Usuario
            var esperado = new DadosConexao(TipoDatabase.ORACLE, "servidor-teste:1521", "BASE_TESTE", "usuario_teste");
            Assert.Equal(esperado, resultado);
        }


        [Fact]
        public void LerDadosConexao_ArquivoOracleTesteTrim_RetornaDadosConexao()
        {
            // Arrange: conteúdo do conexao.dat fixo no próprio teste (dados inventados)
            var reader = CriarReader();
            var conteudo = """
                // Arquivo de conexão de teste
                 [BANCODADOS] = ORACLE 
                [DATABASE]=servidor-teste:1521/BASE_TESTE
                [USUARIO_ORACLE]=usuario_teste
                //[USUARIO_ORACLE]=usuario_ignorado
                [CHAVE_IGNORADA]=teste
                """;

            // Act
            var resultado = reader.LerDadosConexao(conteudo);

            // Assert: DadosConexao é record, então a igualdade compara Tipo, Servidor, Database e Usuario
            var esperado = new DadosConexao(TipoDatabase.ORACLE, "servidor-teste:1521", "BASE_TESTE", "usuario_teste");
            Assert.Equal(esperado, resultado);
        }

        //[Fact] de sucesso: um cenário fixo, um resultado esperado.
        [Fact]
        public void LerDadosConexao_ArquivoSqlValido_RetornaDadosConexao()
        {
            // Arrange: conteúdo do conexao.dat fixo no próprio teste (dados inventados)
            var reader = CriarReader();
            var conteudo = """
                // Arquivo de conexão de teste
                [BANCODADOS]=SQLSERVER
                [DATABASE]=servidor-teste\instancia-teste:BASE_TESTE
                //[DATABASE]=servidor-teste\instancia-teste:BASE_IGNORADA
                """;

            // Act
            var resultado = reader.LerDadosConexao(conteudo);

            // Assert: DadosConexao é record, então a igualdade compara Tipo, Servidor, Database e Usuario
            var esperado = new DadosConexao(TipoDatabase.SQLSERVER, "servidor-teste\\instancia-teste", "BASE_TESTE", "usuario_sqlserver_teste");
            Assert.Equal(esperado, resultado);
        }

        [Fact]
        public void LerDadosConexao_ChavesForaDeOrdem_RetornaDadosConexao()
        {
            // Arrange: conteúdo do conexao.dat fixo no próprio teste (dados inventados)
            var reader = CriarReader();
            var conteudo = """
                // Arquivo de conexão de teste
                [USUARIO_ORACLE]=usuario_teste
                [DATABASE]=servidor-teste:1521/BASE_TESTE
                [BANCODADOS]=ORACLE
                """;

            // Act
            var resultado = reader.LerDadosConexao(conteudo);

            // Assert: DadosConexao é record, então a igualdade compara Tipo, Servidor, Database e Usuario
            var esperado = new DadosConexao(TipoDatabase.ORACLE, "servidor-teste:1521", "BASE_TESTE", "usuario_teste");
            Assert.Equal(esperado, resultado);
        }

        [Fact]
        public void LerDadosConexao_ChavesEValorMinusculo_RetornaDadosConexao()
        {
            // Arrange: conteúdo do conexao.dat fixo no próprio teste (dados inventados)
            var reader = CriarReader();
            var conteudo = """
                // Arquivo de conexão de teste
                [bancodados]=oracle
                [database]=servidor-teste:1521/BASE_TESTE
                [usuario_oracle]=usuario_teste
                """;

            // Act
            var resultado = reader.LerDadosConexao(conteudo);

            // Assert: DadosConexao é record, então a igualdade compara Tipo, Servidor, Database e Usuario
            var esperado = new DadosConexao(TipoDatabase.ORACLE, "servidor-teste:1521", "BASE_TESTE", "usuario_teste");
            Assert.Equal(esperado, resultado);
        }

        //[Fact] de erro: confere a exceção e que os erros vêm acumulados na mesma mensagem.
        [Fact]
        public void LerDadosConexao_ArquivoSemChaves_LancaExcecaoComTodosOsErros()
        {
            // Arrange
            var reader = CriarReader();
            var conteudo = """
                // Arquivo sem nenhuma chave
                """;

            // Act: Assert.Throws executa a chamada e devolve a exceção para inspecionar a mensagem
            var excecao = Assert.Throws<InvalidOperationException>(() => reader.LerDadosConexao(conteudo));

            // Assert: trecho de cada mensagem, não o texto inteiro, para o teste não quebrar com ajuste de redação
            Assert.Contains("não contém o tipo de banco de dados", excecao.Message);
            Assert.Contains("informações de conexão [DATABASE]", excecao.Message);
        }

        [Fact]
        public void LerDadosConexao_ArquivoComDadosDuplicados_LancaExcecaoComErrosDeMaisDeUmaLinha()
        {
            // Arrange
            var reader = CriarReader();
            var conteudo = """
                // Arquivo de conexão de teste
                [BANCODADOS]=ORACLE
                [DATABASE]=servidor-teste:1521/BASE_TESTE
                [USUARIO_ORACLE]=usuario_teste
                [USUARIO_ORACLE]=usuario_teste2
                //
                [BANCODADOS]=SQLSERVER
                [DATABASE]=servidor-teste\instancia-teste:BASE_TESTE
                """;

            // Act: Assert.Throws executa a chamada e devolve a exceção para inspecionar a mensagem
            var excecao = Assert.Throws<InvalidOperationException>(() => reader.LerDadosConexao(conteudo));

            // Assert: trecho de cada mensagem, não o texto inteiro, para o teste não quebrar com ajuste de redação
            Assert.Contains("mais de um tipo de [BANCODADOS]", excecao.Message);
            Assert.Contains("mais de uma linha de [DATABASE]", excecao.Message);
            Assert.Contains("mais de uma linha de [USUARIO_ORACLE]", excecao.Message);
        }

        [Theory]
        [InlineData("MYSQL")]
        [InlineData("0")]                  // TryParse aceitava como ORACLE
        [InlineData("1")]                  // TryParse aceitava como SQLSERVER
        [InlineData("ORACLE, SQLSERVER")]
        public void LerDadosConexao_TipoInvalido_LancaExcecaoComTipoInvalido(string tipo)
        {
            // Arrange
            var reader = CriarReader();
            var conteudo = $"""
                // Arquivo de conexão de teste
                [BANCODADOS]={tipo}
                [DATABASE]=servidor-teste:1521/BASE_TESTE
                [USUARIO_ORACLE]=usuario_teste
                """;

            // Act: Assert.Throws executa a chamada e devolve a exceção para inspecionar a mensagem
            var excecao = Assert.Throws<InvalidOperationException>(() => reader.LerDadosConexao(conteudo));

            // Assert: trecho de cada mensagem, não o texto inteiro, para o teste não quebrar com ajuste de redação
            Assert.Contains("Tipo de [BANCODADOS] inválido", excecao.Message);
        }

        //[Theory]: o mesmo teste roda uma vez para cada [InlineData].
        [Theory]
        [InlineData("ORACLE", "servidor-teste:BASE_TESTE")]     // Oracle espera '/', recebeu ':'
        [InlineData("SQLSERVER", "servidor-teste/BASE_TESTE")]  // SQL Server espera ':', recebeu '/'
        [InlineData("ORACLE", "servidor-teste/1521/BASE_TESTE")]  // Espera apenas 1 separador e recebeu 2
        [InlineData("SQLSERVER", "servidor-teste:1521:BASE_TESTE")]  // Espera apenas 1 separador e recebeu 2
        public void LerDadosConexao_SeparadorInvalido_LancaExcecaoDeDatabaseInvalido(string tipo, string database)
        {
            // Arrange: os parâmetros do método recebem os valores de cada [InlineData]
            var reader = CriarReader();
            var conteudo = $"""
                [BANCODADOS]={tipo}
                [DATABASE]={database}
                [USUARIO_ORACLE]=usuario_teste
                """;

            // Act
            var excecao = Assert.Throws<InvalidOperationException>(() => reader.LerDadosConexao(conteudo));

            // Assert
            Assert.Contains("valor inválido para o servidor e database", excecao.Message);
        }

        [Fact]
        public void LerDadosConexao_ArquivoComServidorEBaseVazios_LancaExcecaoComErrosDeServidorEBanco()
        {
            // Arrange
            var reader = CriarReader();
            var conteudo = """
                // Arquivo de conexão de teste
                [BANCODADOS]=ORACLE
                [DATABASE]= / 
                [USUARIO_ORACLE]=usuario_teste
                """;

            // Act: Assert.Throws executa a chamada e devolve a exceção para inspecionar a mensagem
            var excecao = Assert.Throws<InvalidOperationException>(() => reader.LerDadosConexao(conteudo));

            // Assert: trecho de cada mensagem, não o texto inteiro, para o teste não quebrar com ajuste de redação
            Assert.Contains("não contém o servidor do banco de dados", excecao.Message);
            Assert.Contains("não contém o database do banco de dados", excecao.Message);
        }

        [Fact]
        public void LerDadosConexao_ArquivoOracleSemUsuario_LancaExcecaoComErroDeUsuario()
        {
            // Arrange
            var reader = CriarReader();
            var conteudo = """
                // Arquivo de conexão de teste
                [BANCODADOS]=ORACLE
                [DATABASE]=servidor-teste/BASE_TESTE
                """;

            // Act: Assert.Throws executa a chamada e devolve a exceção para inspecionar a mensagem
            var excecao = Assert.Throws<InvalidOperationException>(() => reader.LerDadosConexao(conteudo));

            // Assert: trecho de cada mensagem, não o texto inteiro, para o teste não quebrar com ajuste de redação
            Assert.Contains("tem o tipo ORACLE, mas não contém o [USUARIO_ORACLE]", excecao.Message);
        }

        [Fact]
        public void LerDadosConexao_UsuarioEmSql_LancaExecaoUsuarioEmSql()
        {
            // Arrange: conteúdo do conexao.dat fixo no próprio teste (dados inventados)
            var reader = CriarReader();
            var conteudo = """
                // Arquivo de conexão de teste
                [BANCODADOS]=SQLSERVER
                [DATABASE]=servidor-teste\instancia-teste:BASE_TESTE
                [USUARIO_ORACLE]=usuario_teste
                """;

            // Act
            var excecao = Assert.Throws<InvalidOperationException>(() => reader.LerDadosConexao(conteudo));

            // Assert: trecho de cada mensagem, não o texto inteiro, para o teste não quebrar com ajuste de redação
            Assert.Contains("tem o tipo SQLSERVER, mas contém o [USUARIO_ORACLE]", excecao.Message);
        }

        // Monta a dependência em memória, sem appsettings.json (dados inventados)
        private static ConexaoDatReader CriarReader()
        {
            var credenciais = new CredenciaisDatabase
            {
                SqlServer = new CredenciaisDatabaseSqlServer
                {
                    Usuario = "usuario_sqlserver_teste",
                    SenhaPadrao = "senha_teste"
                }
            };

            return new ConexaoDatReader(credenciais);
        }
    }
}
