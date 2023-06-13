using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HorseSection.Models;

namespace HorseSection.Windows.Pages
{
    /// <summary>
    /// Логика взаимодействия для RecordPage.xaml
    /// </summary>
    public partial class RecordPage : Page
    {
        // Две глобальные переменный этой страницы - пользователь 
        user currentUser;
        // и вспомогательная таблица table с новым типом данных dopTable его код находиться в папке Models\dopTable.cs
        List<dopTable> table = new List<dopTable>();

        // Конструктор для страницы записи
        public RecordPage(user user)
        {
            InitializeComponent();

            // получаем данные из окна личного кабинета
            currentUser = user;

            // Загружаем все данные из таблицы рабочих дней в массив workdayList, получаем ту же таблицу, но в виде стандартного массива C#
            List<workday> workdayList = BD.db.workday.ToList();
            
            // Создаем дополнительный массив для временного хранения дат и работы с ними
            List<DateTime> dates = new List<DateTime>();

            // Проходим в цикле по всем датам массива workday
            for (int i = 0; i < workdayList.Count; i++)
            {
                // для нахождения статуса записи нужно в таблице записей искать строчки с совпадением текущей даты цикла и пользователя
                // если запись была найдена то currentRecord будет содержать ее в себе, в противном случае будет содержать null
                var currentRecord = BD.db.record.FirstOrDefault(r => r.workdayid == i + 1 && r.userid == user.userid);

                // заполняем вспомогательный массив дат dates
                dates.Add((DateTime)(workdayList[i].workdaydate));

                // Заполняем главный глобальный массив дат и статусов
                // если текущая выбранная в массиве дата меньше текущей то в статус записываем что занятие прошло (Прошло)
                if (workdayList[i].workdaydate < DateTime.Now)
                    table.Add( new dopTable(dates[i].ToShortDateString(), "Прошло")); // функция типа данных DateTime с названием ToShortDateString позволяет избавиться от времени и записать исключительно дату
                // если запись на выбранную дату была найдена, то в статусе (Записаны)
                else if (currentRecord != null)
                    table.Add( new dopTable(dates[i].ToShortDateString(), "Записаны"));
                // если запись на выбранную дату не была найдена, то в статусе (Предстоит)
                else
                    table.Add( new dopTable(dates[i].ToShortDateString(), "Предстоит"));
            }

            // Устанавлеваем ссылку на данные для таблицы datagrid, ей является массив(таблица) дат и статусов, что позволяет правильно использовать данные
            datagrid.ItemsSource = table;

        }

        // При нажатии на кнопку в таблице переходим на следующую страницу записи
        private void buttonReg_Click(object sender, RoutedEventArgs e)
        {
            // числовая перменная row хранит строку, при помощи функции selectedIndex узнаем в какой строке была нажата кнопка, для того чтобы получить дату и статус этой строки
            int row = datagrid.SelectedIndex;
            // при помощи функции NavigationService обращаемся к frame и через его функцию Navigate переходи на следующую страницу записи,
            // стоит заметить что конструктор следующей страницы имеет сразу 3 параметра: текущего пользователя,
            // дополнительную таблицу дат и статусов, а так же найденную выше строку нажатия кнопки
            this.NavigationService.Navigate(new RecordPage2(currentUser, table, row), UriKind.Relative);
            // Переход на второуя страницу записи -> RecordPage2
        }
    }
}
