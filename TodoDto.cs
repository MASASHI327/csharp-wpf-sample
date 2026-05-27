using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class TodoDto
    {
        public int TODO_ID { get; set; }
        public string TITLE { get; set; } = string.Empty;
        public string CONTENT { get; set; } = string.Empty;
    }
}
