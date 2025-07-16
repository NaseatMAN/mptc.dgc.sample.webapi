using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mptc.dgc.sample.application.DTOs.File
{
    public class SftpConfig
    {
        public string Host { get; set; } = "";
        public int Port { get; set; } = 22;
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string RemoteDirectory { get; set; } = "";
    }
}
