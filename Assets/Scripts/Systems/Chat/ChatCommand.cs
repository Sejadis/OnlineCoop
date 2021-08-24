using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChatCommand
{
    public string Command { get;  }
    public int Parameters{ get;}

    protected ChatCommand(string command, int parameters)
    {
        Command = command;
        Parameters = parameters;
    }
    
    public abstract void Execute(string[] parameters, ulong receiveSenderClientId);

}
