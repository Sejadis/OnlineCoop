using System.Collections.Generic;
using System.Linq;
using Shared.ChatCommands;

namespace Server.Chat
{
    public static class ChatCommandHandler
    {
        private static Dictionary<string, ChatCommand> registeredCommands = new Dictionary<string, ChatCommand>();

        static ChatCommandHandler()
        {
            ChatCommand cmd = new ChangeNameChatCommand();
            registeredCommands.Add(cmd.Command, cmd);
            cmd = new ChangeHealthChatCommand();
            registeredCommands.Add(cmd.Command, cmd);  
            cmd = new ChangeIconChatCommand();
            registeredCommands.Add(cmd.Command, cmd);
        }

        public static bool RunCommand(string command, ulong receiveSenderClientId)
        {
            var param = command.Replace("/", string.Empty).Split(' ').ToList();

            if (registeredCommands.TryGetValue(param[0], out var cmd) && cmd.Parameters == param.Count - 1)
            {
                param.RemoveAt(0);
                cmd.Execute(param.ToArray(), receiveSenderClientId);
                return true;
            }

            return false;
        }
    }
}