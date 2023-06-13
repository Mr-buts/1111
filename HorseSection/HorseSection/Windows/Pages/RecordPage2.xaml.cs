using HorseSection.Models;
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

namespace HorseSection.Windows.Pages
{
    /// <summary>
    /// Логика взаимодействия для RecordPage2.xaml
    /// </summary>
    public partial class RecordPage2 : Page
    {
        // Глобальные переменный страницы для упрощения работы
        // Индекс выбранной на предыдущей странице строки, текущий пользователь и дополнительная таблица дат и статусов
        int index;
        user currentUser;
        List<dopTable> dops;

        // Вспомогательная функция для проверки статуса выбранной даты, чтобы не повторять код
        private void statusCheck()
        {
            // если статус Прошло - убираем функционал
            if (dops[comboBoxDate.SelectedIndex].status == "Прошло")
            {
                buttonReg.Visibility = Visibility.Hidden;
                buttonDel.Visibility = Visibility.Hidden;
                comboBoxNum.IsEnabled = false;
            }
            // если статус Предстоит - добовляем функционал записи
            else if (dops[comboBoxDate.SelectedIndex].status == "Предстоит")
            {
                buttonReg.Visibility = Visibility.Visible;
                buttonDel.Visibility = Visibility.Hidden;
                comboBoxNum.IsEnabled = true;
            }
            // если статус Записаны - добавляем функционал отмены записи
            else
            {
                buttonReg.Visibility = Visibility.Hidden;
                buttonDel.Visibility = Visibility.Visible;
                comboBoxNum.IsEnabled = false;
            }
        }

        // Конструктор страницы принимает данные глобально описанные выше и сразу их инициализирует
        public RecordPage2(user user, List<dopTable> dops, int index)
        {
            InitializeComponent();

            this.index = index;
            this.currentUser = user;
            this.dops = dops;

            // устанавливаем контекст данных для комбобокса даты (элемента с выбором) и сразу выбираем строку по индексу
            comboBoxDate.ItemsSource = dops;
            comboBoxDate.SelectedIndex = index;

            // все по тому же индексу строки с предыдущей страницы пишем текст статуса
            text_4.Text = dops[index].status;

            // использование вспомогательной функции проверки статуса
            statusCheck();
            
            // заполняем только созданный лист цифрами от 1 до 50 и используем этот массив как контекст для комбобокса количества человек
            List<int> numPeople = new List<int>();
            for (int i = 1; i < 51; i++)
                numPeople.Add(i);

            comboBoxNum.ItemsSource = numPeople;
            comboBoxNum.SelectedIndex = 0;
        }

        // Нажатие на кнопку назад возвращает на предыдущуюю страницу через Navigate.GoBack()
        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            // Возвращение на первую страницу записи -> RecordPage
        }

        // Нажатие на кнопку записи
        private void buttonReg_Click(object sender, RoutedEventArgs e)
        {
            // Создание новой записи и внесение данных по выбранным в данный момент в комбобоксах, затем добавление в бд и сохранение 
            record rec = new record();
            rec.userid = currentUser.userid;
            rec.workdayid = comboBoxDate.SelectedIndex + 1;
            rec.numperson = comboBoxNum.SelectedIndex + 1;
            BD.db.record.Add(rec);
            BD.db.SaveChanges();
            
            // Сообщение о записи и возвращение на первую страницу регистрации, но не через GoBack, а через прямую ссылку, для обновления данных
            MessageBox.Show("Запись на " + dops[comboBoxDate.SelectedIndex].date + " была успешно произведена.", "Уведомление");
            NavigationService.Navigate(new RecordPage(currentUser), UriKind.Relative);

        }

        // Обновление статуса при изменениях в комбобоксе даты
        private void comboBoxDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            text_4.Text = dops[comboBoxDate.SelectedIndex].status;

            // использование вспомогательной функции проверки статуса
            statusCheck();
        }

        // Нажатие на кнопку отмены записи
        private void buttonDel_Click(object sender, RoutedEventArgs e)
        {
            // Находим в БД запись на текущую выбранную в комбобоксе дату и при помощи функции Remove удаляем ее из бд, сохраняем изменение
            var currentRecord = BD.db.record.FirstOrDefault(r => r.workdayid == comboBoxDate.SelectedIndex + 1 && r.userid == currentUser.userid);
            BD.db.record.Remove(currentRecord);
            BD.db.SaveChanges();

            // Сообщение об отмене записи и возвращение на первую страницу регистрации, но не через GoBack, а через прямую ссылку, для обновления данных 
            MessageBox.Show("Запись на " + dops[comboBoxDate.SelectedIndex].date + " была успешно отменена", "Уведомление");
            NavigationService.Navigate(new RecordPage(currentUser), UriKind.Relative);
        }
    }
}
