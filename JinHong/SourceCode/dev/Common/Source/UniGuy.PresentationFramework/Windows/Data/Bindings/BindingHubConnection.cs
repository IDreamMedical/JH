// Copyright (C) Michael Agroskin 2010

namespace UniGuy.Controls.Bindings
{
    public class BindingHubConnection
    {
        public BindingHubConnection(int socket, BindingHubConnectionDirection direction)
        {
            Socket = socket;
            Direction = direction;
        }

        public int Socket { get; set; }
        public BindingHubConnectionDirection Direction { get; set; }
    }
}
