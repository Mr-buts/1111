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
using System.Windows.Shapes;
using HorseSection.Models;
using HorseSection.Windows.Pages;

namespace HorseSection.Windows
{
    /// <summary>
    /// Логика взаимодействия для PersonalWindow.xaml
    /// </summary>
    public partial class PersonalWindow : Window
    {
        public PersonalWindow()
        {
            InitializeComponent();
        }

        // Глобальная для данного окна переменная, с типом пользователь (user) создержит в себе все данные пользователя
        // Эта переменная находиться в глобальной области видимости и видна сразу всем функциям в этом окне, для удобства ее использования
        private user currentUser;

        // Конструктор принимающий переменную пользователя из окна входа, нужна для понимания того, какой пользователь зашел в личный кабинет
        public PersonalWindow(user currentUser)
        {
            InitializeComponent();

            // копируем в глобальную переменную окна currentUser все данные переданные из окна входа
            // и сразу после этого устанавливаем полученные данные в строки ФИО и номера телефона
            this.currentUser = currentUser;
            FIO.Text = currentUser.surname + " " + currentUser.name + " " + currentUser.patronymic;
            Phone.Text = "+" + currentUser.phone;
        }

        // При нажатии кнопки выхода возвращаемся в окно входа передавая номер телефона вышедшего пользователя
        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            new EnterWindow(currentUser.phone).Show();
            this.Close();
            // Возврат на страницу кода входа -> EnterWindow
        }

        // При нажатии на кнопку записи убираем текст Инфо, делаем кнопку Инфо активной, а кнопку записи и frame наоборот, открываем страницу записи
        private void buttonEnroll_Click(object sender, RoutedEventArgs e)
        {
            label2.Visibility = Visibility.Collapsed;
            label3.Visibility = Visibility.Collapsed;

            buttonEnroll.IsEnabled = false;
            buttonText.IsEnabled = true;

            // В данном окне используется такой обьект как frame, он является рамкой для запуска страницы (page) и может занимать в окне произвольное место
            // для переключения между страницами в frame существует функция Navigate или же в коде самих страниц обращение к Frame через NavigateService
            this.frame.Visibility = Visibility.Visible;
            // В функцию Navigate передается два параметра, сама страница (или через создание Uri ссылки или через создание обьекта new как здесь) и способ запуска через запятую
            this.frame.Navigate(new RecordPage(currentUser), UriKind.Relative);
            // Открытие страницы записи -> RecordPage
        }

        // При нажатии на кнопку Инфо делаем обратное от нажатия на кнопку записи
        private void buttonText_Click(object sender, RoutedEventArgs e)
        {
            label2.Visibility = Visibility.Visible;
            label3.Visibility = Visibility.Visible;

            buttonEnroll.IsEnabled = true;
            buttonText.IsEnabled = false;

            this.frame.Visibility = Visibility.Collapsed;
        }
    }
}
