// Copyright (C) Michael Agroskin 2010
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace UniGuy.Controls.Bindings
{
    public class BindingHubConnectionConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            var result = new List<List<BindingHubConnection>>();

            var groups = ((string)value).Split("()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var g in groups)
            {
                var connections = g.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (connections.Length == 0 || string.IsNullOrEmpty(connections[0]))
                    continue;

                var group = new List<BindingHubConnection>();
                foreach (var c in connections)
                {
                    var parms = c.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (parms.Length == 0 || string.IsNullOrEmpty(parms[0]))
                        continue;

                    group.Add(new BindingHubConnection(
                        IndexFromSocket(parms[0]),
                        parms.Length < 2 || string.IsNullOrEmpty(parms[1]) ? BindingHubConnectionDirection.InOut :
                            (BindingHubConnectionDirection)Enum.Parse(typeof(BindingHubConnectionDirection), parms[1], true)));
                }
                result.Add(group);
            }

            return result;
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            var groups = (List<List<BindingHubConnection>>)value;
            return
                string.Join(",",
                    (from grp in groups
                     let g =
                         (from conn in grp
                          select SocketFromIndex(conn.Socket) + " " + conn.Direction)
                     select "(" + string.Join(",", g.ToArray<string>()) + ")").ToArray<string>());
        }

        private static int IndexFromSocket(string socket)
        {
            //return int.Parse(socket);

            return BindingHub.Sockets.FindIndex(s => 
                s.Name.Equals(socket, StringComparison.InvariantCultureIgnoreCase) ||
                s.Name.Equals("Socket" + socket, StringComparison.InvariantCultureIgnoreCase)) + 1;
        }

        private static string SocketFromIndex(int index)
        {
            return index < 0 || index >= BindingHub.Sockets.Count ? index.ToString() : BindingHub.Sockets[index - 1].Name;
        }
    }
}
