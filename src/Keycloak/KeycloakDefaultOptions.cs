using System;

namespace Squadron
{
    /// <summary>
    /// Default Keycloak resource options
    /// </summary>
    public class KeycloakDefaultOptions
        : ContainerResourceOptions,
        IComposableResourceOption
    {
        public Type ResourceType => typeof(KeycloakResource);

        /// <summary>
        /// Configure resource options
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(ContainerResourceBuilder builder)
        {
            builder
                .Name("keycloak")
                .Image("jboss/keycloak:latest")
                .AddEnvironmentVariable("KEYCLOAK_USER=admin")
                .AddEnvironmentVariable("KEYCLOAK_PASSWORD=admin")
                .WaitTimeout(60)
                .InternalPort(8080);
        }
    }
}
