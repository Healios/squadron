using System.Text;

namespace Squadron
{
    internal class KeycloakCreateAdminUserCommand : ICommand
    {
        private readonly StringBuilder _command = new StringBuilder();

        internal KeycloakCreateAdminUserCommand(string user, string password)
        {
            _command.Append($"/opt/jboss/keycloak/bin/add-user-keycloak.sh -u {user} -p {password}");
        }

        public string Command => _command.ToString();
    }
}
