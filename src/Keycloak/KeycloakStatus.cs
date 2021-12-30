using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Squadron
{
    /// <summary>
    /// Status checker for Keycloak
    /// </summary>
    /// <seealso cref="IResourceStatusProvider" />
    public class KeycloakStatus : IResourceStatusProvider
    {
        private readonly string _host;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeycloakStatus"/> class.
        /// </summary>
        /// <param name="host">Hostname</param>
        public KeycloakStatus(string host)
        {
            _host = host;
        }

        /// <inheritdoc/>
        public async Task<Status> IsReadyAsync(CancellationToken cancellationToken)
        {
            var client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, _host));

                return new Status
                {
                    IsReady = response.IsSuccessStatusCode,
                    Message = response.StatusCode.ToString()
                };

            }
            catch (Exception ex)
            {
                return new Status
                {
                    IsReady = false,
                    Message = ex.Message
                };
            }
            finally
            {
                client.Dispose();
            }
        }

    }
}
