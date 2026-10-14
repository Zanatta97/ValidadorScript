using System;
using System.Collections.Generic;
using System.Text;
using ValidadorScript.Application.Conexao;
using ValidadorScript.Domain.Conexao;

namespace ValidadorScript.Application.Tests.Conexao
{
    public class CredenciaisDatabaseValidatorTests
    {
        [Fact]
        public void ValidarCredenciais_ComCredenciaisValidas_DeveRetornarListaVazia()
        {
            // Arrange
            var credenciais = new Credenciais("senhaOracle", "usuarioSql", "senhaSql");
            // Act
            var erros = CredenciaisDatabaseValidator.ValidarCredenciais(credenciais);
            // Assert
            Assert.Empty(erros);
        }

        [Fact]
        public void ValidarCredenciais_ComCredenciaisInvalidas_DeveRetornarListaComErros()
        {
            // Arrange
            var credenciais = new Credenciais("", "", "");
            // Act
            var erros = CredenciaisDatabaseValidator.ValidarCredenciais(credenciais);
            // Assert
            Assert.Equal(3, erros.Count());
            Assert.Contains("Informe a senha padrão para Oracle", erros);
            Assert.Contains("Informe o usuário padrão do SQL Server", erros);
            Assert.Contains("Informe a senha padrão para SQL Server", erros);
        }

        [Fact]
        public void ValidarCredenciais_ComCredenciaisEspacos_DeveRetornarListaComErros()
        {
            // Arrange
            var credenciais = new Credenciais(" ", " ", " ");
            // Act
            var erros = CredenciaisDatabaseValidator.ValidarCredenciais(credenciais);
            // Assert
            Assert.Equal(3, erros.Count());
            Assert.Contains("Informe a senha padrão para Oracle", erros);
            Assert.Contains("Informe o usuário padrão do SQL Server", erros);
            Assert.Contains("Informe a senha padrão para SQL Server", erros);
        }

        [Fact]
        public void ValidarCredenciais_ComCredenciaisSemSenhaOracle_DeveRetornarListaComErros()
        {
            // Arrange
            var credenciais = new Credenciais("", "usuarioSql", "senhaSql");
            // Act
            var erros = CredenciaisDatabaseValidator.ValidarCredenciais(credenciais);
            // Assert
            Assert.Single(erros);
            Assert.Contains("Informe a senha padrão para Oracle", erros);
            Assert.DoesNotContain("Informe o usuário padrão do SQL Server", erros);
            Assert.DoesNotContain("Informe a senha padrão para SQL Server", erros);
        }


        [Fact]
        public void ValidarCredenciais_ComCredenciaisSemUsuarioSql_DeveRetornarListaComErros()
        {
            // Arrange
            var credenciais = new Credenciais("senhaOracle", "", "senhaSql");
            // Act
            var erros = CredenciaisDatabaseValidator.ValidarCredenciais(credenciais);
            // Assert
            Assert.Single(erros);
            Assert.DoesNotContain("Informe a senha padrão para Oracle", erros);
            Assert.Contains("Informe o usuário padrão do SQL Server", erros);
            Assert.DoesNotContain("Informe a senha padrão para SQL Server", erros);
        }


        [Fact]
        public void ValidarCredenciais_ComCredenciaisSemSenhaSql_DeveRetornarListaComErros()
        {
            // Arrange
            var credenciais = new Credenciais("senhaOracle", "usuarioSql", "");
            // Act
            var erros = CredenciaisDatabaseValidator.ValidarCredenciais(credenciais);
            // Assert
            Assert.Single(erros);
            Assert.DoesNotContain("Informe a senha padrão para Oracle", erros);
            Assert.DoesNotContain("Informe o usuário padrão do SQL Server", erros);
            Assert.Contains("Informe a senha padrão para SQL Server", erros);
        }



        private sealed class CredenciaisStoreFake(Credenciais credenciais) : ICredenciaisStore
        {
            public Credenciais Obter() => credenciais;
            public void Salvar(Credenciais novas) { }
        }
    }
}
