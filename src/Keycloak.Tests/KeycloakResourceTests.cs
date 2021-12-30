using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Squadron
{
    public class KeycloakResourceTests : IClassFixture<KeycloakResource>
    {
        private readonly KeycloakResource _keycloakResource;

        public KeycloakResourceTests(KeycloakResource keycloakResource)
        {
            _keycloakResource = keycloakResource;
        }

        [Fact]
        public async Task Is_Available()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, _keycloakResource.ConnectionString));
                response.IsSuccessStatusCode.Should().BeTrue();
            }
        }

        [Fact]
        public async Task Realm_Has_Been_Imported()
        {
            var options = new ImportRealmFromFileOptions()
            {
                File = new FileInfo(Path.Combine("Resources", "realm.json")),
                Realm = "Demo"
            };
            await _keycloakResource.ImportRealmAsync(options);

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, _keycloakResource.ConnectionString + $"/realms/{options.Realm}/.well-known/openid-configuration"));
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                response.IsSuccessStatusCode.Should().BeTrue();
                content.Should().Contain($"issuer: {_keycloakResource.ConnectionString}/realms/{options.Realm}");
            }
        }
    }
}
