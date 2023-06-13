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
using HorseSection.Windows;
using MaterialDesignThemes;
using HorseSection.Models;

namespace HorseSection
{
    public partial class EnterWindow : Window
    {
        // Конструктор по умолчанию (Конструктор - инициализирует окно и производит все манипуляции при его открытии, название конструктора совподает с названием окна)
        public EnterWindow()
        {
            // Данная функция всегда должна быть в конструкторе окна, она инициализирует (запускает) само окно (графическое его представление xaml.)
            InitializeComponent();

            // Данные по умолчанию
            //loginBox.Text = "79775739821";
            //passwordBox.Password = "wad:ila97";
        }

        // Конструктор принимающий номер телефона при переходе из других окон в это
        public EnterWindow(string phone)
        {
            InitializeComponent();
            loginBox.Text = phone;
        }

        // При нажатии на кнопку Регистрации переходим в окно регистрации
        private void buttonReg_Click(object sender, RoutedEventArgs e)
        {
            // Если в текстбоксе номера телефона чтото было, передаем это окну регистрации
            if (loginBox.Text != "")
                new RegWindow(loginBox.Text).Show();
            else
                new RegWindow().Show();
            this.Close();
            // Далее переход на страницу кода регистрации -> RegWindow
        }

        // При нажатии на кнопку Войти
        private void enter_Click(object sender, RoutedEventArgs e)
        {

            // В случае если оба текстбокса не пустые
            if (loginBox.Text != "" && passwordBox.Password != "")
            {
                // Проверяем на совпадение введенных данных и данных находящихся в БД
                var currentUser = BD.db.user.FirstOrDefault(u => u.phone == loginBox.Text && u.password == passwordBox.Password);

                if (currentUser != null) // Если такой пользователь найден и все данные совпадают переходим на страницу этого пользователя
                {
                    // При переходе передаем в конструктор страницы пользователя его данные (CurrentUser)
                    new PersonalWindow(currentUser).Show();
                    this.Close();
                    // Далее переход на страницу кода Личного кабинета -> PersonalWindow
                }
                else // в противном случае убираем текст из строки пароля и меняем подсказку на "Неверный логин или пароль"
                {
                    passwordBox.Password = "";
                    MaterialDesignThemes.Wpf.HintAssist.SetHint(passwordBox, "Неверный логин или пароль");
                }
            }
            else // если есть пустое поле
            {
                MessageBox.Show("Заполните все поля", "Ошибка входа!");
            }
        }
    }
}
