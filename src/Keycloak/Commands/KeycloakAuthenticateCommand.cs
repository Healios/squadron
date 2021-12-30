using System.Text;

namespace Squadron
{
    internal class KeycloakAuthenticateCommand : ICommand
    {
        private readonly StringBuilder _command = new StringBuilder();

        internal KeycloakAuthenticateCommand(
            string server)
        {
            _command.Append("kcadm.sh config credentials ");
            _command.Append($"--server {server} ");
            _command.Append($"--realm Master ");
            _command.Append($"--user admin ");
            _command.Append($"--password admin");
        }

        public string Command => _command.ToString();
    }
}
