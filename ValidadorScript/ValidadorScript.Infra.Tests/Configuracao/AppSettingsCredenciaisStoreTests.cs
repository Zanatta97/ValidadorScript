using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using ValidadorScript.Domain.Conexao;
using ValidadorScript.Infra.Configuracao;

namespace ValidadorScript.Infra.Tests.Configuracao
{
    public class AppSettingsCredenciaisStoreTests : IDisposable
    {

        private readonly string _pasta;
        private readonly string _caminho;

        // O xUnit cria uma instância nova por teste: cada [Fact] ganha a própria pasta
        public AppSettingsCredenciaisStoreTests()
        {
            _pasta = Path.Combine(Path.GetTempPath(), "ValidadorScriptTests", Guid.NewGuid().ToString());
            Directory.CreateDirectory(_pasta);
            _caminho = Path.Combine(_pasta, "appsettings.json");
        }

        public void Dispose() => Directory.Delete(_pasta, recursive: true);


        [Fact]
        public void Obter_ArquivoInexistente_RetornaCredenciaisVazias()
        {
            // Arrange: nenhum arquivo criado na pasta (cenário da primeira execução)
            var store = new AppSettingsCredenciaisStore(_caminho);

            // Act
            var resultado = store.Obter();

            // Assert: Credenciais é record, então a igualdade compara os três campos
            Assert.Equal(new Credenciais("", "", ""), resultado);
        }


        [Fact]
        public void Obter_ArquivoNoFormatoDoExemplo_LeCredenciais()
        {
            // Arrange: mesmo formato do appsettings.example.json (dados inventados)
            File.WriteAllText(_caminho, """
                {
                  "Credenciais": {
                    "Oracle": { "SenhaPadrao": "senha_oracle_teste" },
                    "SqlServer": { "Usuario": "usuario_teste", "SenhaPadrao": "senha_sql_teste" }
                  }
                }
                """);
            var store = new AppSettingsCredenciaisStore(_caminho);

            // Act
            var resultado = store.Obter();

            // Assert
            Assert.Equal(new Credenciais("senha_oracle_teste", "usuario_teste", "senha_sql_teste"), resultado);
        }


        [Fact]
        public void Salvar_LidoPorNovaInstancia_RetornaMesmosValores()
        {
            // Arrange
            var credenciais = new Credenciais("senha_oracle_teste", "usuario_teste", "senha_sql_teste");

            // Act: grava com uma instância e lê com outra, para forçar a leitura do disco (sem o cache _atual)
            new AppSettingsCredenciaisStore(_caminho).Salvar(credenciais);
            var resultado = new AppSettingsCredenciaisStore(_caminho).Obter();

            // Assert
            Assert.Equal(credenciais, resultado);
        }


        [Fact]
        public void Salvar_GravaNoFormatoDoAppSettings()
        {
            // Arrange
            var store = new AppSettingsCredenciaisStore(_caminho);

            // Act
            store.Salvar(new Credenciais("senha_oracle_teste", "usuario_teste", "senha_sql_teste"));

            // Assert: navega pelo JSON gravado e confere os nomes das chaves, não só os valores
            using var json = JsonDocument.Parse(File.ReadAllText(_caminho));
            var secao = json.RootElement.GetProperty("Credenciais");
            Assert.Equal("senha_oracle_teste", secao.GetProperty("Oracle").GetProperty("SenhaPadrao").GetString());
            Assert.Equal("usuario_teste", secao.GetProperty("SqlServer").GetProperty("Usuario").GetString());
            Assert.Equal("senha_sql_teste", secao.GetProperty("SqlServer").GetProperty("SenhaPadrao").GetString());
        }


        [Fact]
        public void Salvar_AposObter_ObterRetornaValorNovo()
        {
            // Arrange: primeira leitura popula o cache com credenciais vazias
            var store = new AppSettingsCredenciaisStore(_caminho);
            store.Obter();
            var novas = new Credenciais("senha_oracle_teste", "usuario_teste", "senha_sql_teste");

            // Act
            store.Salvar(novas);
            var resultado = store.Obter();

            // Assert: o cache foi atualizado, sem precisar reiniciar o app
            Assert.Equal(novas, resultado);
        }


        [Fact]
        public void Obter_ChamadoDuasVezes_NaoReleArquivo()
        {
            // Arrange: primeira leitura com o arquivo ausente
            var store = new AppSettingsCredenciaisStore(_caminho);
            var primeira = store.Obter();

            // Act: o arquivo muda no disco por fora da classe
            File.WriteAllText(_caminho, """{ "Credenciais": { "Oracle": { "SenhaPadrao": "alterada" } } }""");
            var segunda = store.Obter();

            // Assert: continua devolvendo o que já estava em memória
            Assert.Equal(primeira, segunda);
        }

    }
}
