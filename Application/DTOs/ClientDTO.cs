using System;
using System.Collections.Generic;
using System.Text;

namespace Generator_Faktur.Application.DTOs
{
    public class ClientDTO
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Identifier { get; set; }
        public bool LocalClient { get; set; }

    }
}
