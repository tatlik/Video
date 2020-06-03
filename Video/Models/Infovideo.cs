using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Video.Models
{
    public class Infovideo
    {
        public int Id { get; set; }
        public string Name { get; set; } // название
        public string Opisanie { get; set; } // описание
        public string Date { get; set; } // год выпуска
        public string Producer { get; set; } // режиссёр
        public string User { get; set; } // имя пользователя
        public byte[] Poster { get; set; } // постер
    }
}
