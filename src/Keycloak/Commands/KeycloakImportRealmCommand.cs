using System.Text;

namespace Squadron
{
    internal class KeycloakImportRealmCommand : ICommand
    {
        private readonly StringBuilder _command = new StringBuilder();

        internal KeycloakImportRealmCommand(
            string realm,
            string file)
        {
            _command.Append("/opt/jboss/keycloak/bin/kcadm.sh create partialImport ");
            _command.Append($"-r {realm} ");
            _command.Append("-s ifResourceExists=OVERWRITE -o ");
            _command.Append($"-f {file}");
        }

        public string Command => _command.ToString();
    }
}
