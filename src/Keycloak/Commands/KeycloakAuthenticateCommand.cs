using System.Text;

namespace Squadron
{
    internal class KeycloakAuthenticateCommand : ICommand
    {
        private readonly StringBuilder _command = new StringBuilder();

        internal KeycloakAuthenticateCommand(
            string server,
            string realm,
            string user,
            string password)
        {
            _command.Append("/opt/jboss/keycloak/bin/kcadm.sh config credentials ");
            _command.Append($"--server {server} ");
            _command.Append($"--realm {realm} ");
            _command.Append($"--user {user} ");
            _command.Append($"--password {password}");
        }

        public string Command => _command.ToString();
    }
}
