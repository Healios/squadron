using System.Text;

namespace Squadron
{
    internal class KeycloakCreateRealmCommand : ICommand
    {
        private readonly StringBuilder _command = new StringBuilder();

        internal KeycloakCreateRealmCommand(string realm)
        {
            _command.Append("kcadm.sh create realms ");
            _command.Append($"-s realm={realm} ");
            _command.Append("-s enabled=true");
        }

        public string Command => _command.ToString();
    }
}
