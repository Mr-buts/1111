using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseSection.Models
{
    internal static class BD // Класс используемый для упрощения обращения к БД
    {
        public static HorseSectionModelEntities db = new HorseSectionModelEntities();
    }
}
