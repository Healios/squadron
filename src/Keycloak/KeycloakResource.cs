using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Squadron
{
    /// <inheritdoc/>
    public class KeycloakResource : KeycloakResource<KeycloakDefaultOptions> { }

    /// <summary>
    /// Represents a redis resource that can be used by unit tests.
    /// </summary>
    public class KeycloakResource<TOptions>
        : ContainerResource<TOptions>,
          IAsyncLifetime,
          IComposableResource
        where TOptions : ContainerResourceOptions, new()
    {
        /// <summary>
        /// ConnectionString
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <inheritdoc cref="IAsyncLifetime"/>
        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            ConnectionString = $"http://{Manager.Instance.Address}:{Manager.Instance.HostPort}/auth";
            await Initializer.WaitAsync(new KeycloakStatus(ConnectionString));
        }

        public override Dictionary<string, string> GetComposeExports()
        {
            var internalConnectionString =
                $"http://{Manager.Instance.Name}:{Settings.InternalPort}/auth";

            Dictionary<string, string> exports = base.GetComposeExports();
            exports.Add("CONNECTIONSTRING", ConnectionString);
            exports.Add("CONNECTIONSTRING_INTERNAL", internalConnectionString);

            return exports;
        }

        public async Task ImportRealmAsync(ImportRealmFromFileOptions options)
        {
            var copyContext = new CopyContext(
                options.File.FullName,
                Path.Combine(options.Destination, options.File.Name));

            await Manager.CopyToContainerAsync(copyContext);

            await Manager.InvokeCommandAsync(new KeycloakAuthenticateCommand(ConnectionString).ToContainerExecCreateParameters());

            await Manager.InvokeCommandAsync(new KeycloakCreateRealmCommand(options.Realm).ToContainerExecCreateParameters());

            await Manager.InvokeCommandAsync(new KeycloakImportRealmCommand(options.Realm, copyContext.Destination).ToContainerExecCreateParameters());
        }
    }
}
