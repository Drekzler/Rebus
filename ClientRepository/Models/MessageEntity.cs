using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRepository.Models
{
    public class MessageEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string HiddenMessage { get; set; }
    }
}
