using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseSection.Models
{
    public class dopTable
    {
        // Этот вспомогательный тип данных был необходим для правильной загрузки данных в datagrid
        // В себе он содержит просто два столбца из таблицы, оба являющиеся строкой, первый это дата, второй статус записи на эту дату
        // При записи этого типа данных в массив List<dopTable> мы получем табличку с датами и их статусами, все просто
        public string date { get; set; } // параметр типа данных обязан содержать в себе функции get и set, получить и установить соответственно
        public string status { get; set; } // в противном случае загрузка в datagrid не будет работать, ведь таблица не сможет получить данные без надлежащей функции

        public dopTable(string date, string status) // конструктор для dopTable принимает данные даты и статуса и сразу загружает их в переменные класса
        {
            this.date = date;
            this.status = status;
        }
    }

}
