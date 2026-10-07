using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ValidadorScript.Infra.Configuracao;
using ValidadorScript.Application.Conexao;
using ValidadorScript.Infra.Conexao;

namespace ValidadorScript.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, CredenciaisDatabase credenciais)
        {
            services.AddSingleton(credenciais);
            services.AddSingleton<IDadosConexaoReader, ConexaoDatReader>();

            return services;
        }
    }
}
