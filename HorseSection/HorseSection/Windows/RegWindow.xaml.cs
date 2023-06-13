using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using HorseSection.Windows;

namespace HorseSection.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        public RegWindow()
        {
            InitializeComponent();
        }

        // Конструктор принимающий номер телефона, этот конструктор используется в коде окна входа,
        // он устанавливает номер телефона набранный в окне входа в нужную строку окна регистрации
        public RegWindow(string phone)
        {
            InitializeComponent();
            PhoneBox.Text = phone;
        }

        // Вспомогательная функция проверки пароля, принимает строку пароля и возвращает да или нет (пароль соответствует или не соответствует заданным параметрам)
        static bool ValidatePassword(string password)
        {
            // Данная функция проверки в случае несоответсвия сразу сообщает об этом (return  false;)
            // и только в самом конце после окончания последней проверки сообщает о соответствии (return true;)

            // Проверка длинны
            if (password.Length < 8 || password.Length > 15)
                return false;

            // Проверка на присутствие букв верхнего регистра
            if (!password.Any(char.IsUpper))
                return false;

            // Проверка на присутствие букв нижнего регистра
            if (!password.Any(char.IsLower))
                return false;

            // Проверка на присутствие чисел
            if (!password.Any(char.IsDigit))
                return false;

            // Проверка на отсутствие пробелов
            if (password.Contains(" "))
                return false;

            // Проверка на присутствие спец. символов (Из списка)
            string specialCh = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialChArray = specialCh.ToCharArray();
            foreach (char ch in specialChArray)
            {
                if (password.Contains(ch))
                    return true;
            }

            // Если спец. символы не найдены
            return false;
        }

        // При нажатии на кнопку регистрации
        private void buttonReg_Click(object sender, RoutedEventArgs e)
        {
            // Заготовка для создания нового пользователя, по факту уже созданный пользователь, но не заполненный никакими данными
            user people = new user();

            // Если нет пустых полей, кроме поля отчества (его может не быть и это учтено в проверках), начинаем проверку всех полей поочередно
            if (NameBox.Text != "" && SurnameBox.Text != "" && PhoneBox.Text != "" && passwordBox.Password != "" && passwordDubleBox.Password != "")
            {
                // Заготовка для проверки номера телефона, в случае если пользователь с таки номером телефона будет найден в БД
                // переменная currentNum будет содержать в себе данные этого пользователя, в противном случае она будет содержать в себе null, тоесть ничего (будет пустой)
                var currentNum = BD.db.user.FirstOrDefault(u => u.phone == PhoneBox.Text);

                // Первые три проверки (if) - это проверки ФИО, включают в себя проверки на длинну и проверку на только русские символы,
                // без цифр, спец. символов и тд. выполняется это проверка при помощи Regex - Регулярных выражений языка C# для работы с текстом по шаблону
                // Также возможное отсутствие отчества учтено специальной проверкой (PatronomicBox.Text != "" &&), с помощью нее были разделены два случая,
                // Случай когда его нет и все остальные проверки пропускаются, или случай когда оно есть и все проверки выполняются
                if (!(SurnameBox.Text.Length >= 2 && SurnameBox.Text.Length < 40 && Regex.IsMatch(SurnameBox.Text, @"^[А-Яа-я]+$")))
                    MessageBox.Show("Фамилия не соответствует правилам заполнения", "Ошибка регистрации!");
                else if (!(NameBox.Text.Length >= 2 && NameBox.Text.Length < 40 && Regex.IsMatch(NameBox.Text, @"^[А-Яа-я]+$")))
                    MessageBox.Show("Имя не соответствует правилам заполнения", "Ошибка регистрации!");
                else if (PatronomicBox.Text != "" && !(PatronomicBox.Text.Length >= 2 && PatronomicBox.Text.Length < 40 && Regex.IsMatch(PatronomicBox.Text, @"^[А-Яа-я]+$")))
                    MessageBox.Show("Отчество не соответствует правилам заполнения", "Ошибка регистрации!");
                // Проверка на совпадение нового номера телефона и номеров в БД, при помощи заготовки выше в коде, если она не пуста, то такой номер был найден и обратное от этого
                else if (currentNum != null)
                    MessageBox.Show("Этот номер телефона уже используется", "Ошибка регистрации!");
                // Проверка номера по шаблону Regex - первая цифра обязательно семерка, остальные символы обязательно 10 цифр, но уже не важно каких 
                else if (!Regex.IsMatch(PhoneBox.Text, @"^(7)\d{10}$"))
                    MessageBox.Show("Номер телефона не соответствует правилам.\nНомер должен состоять из индекса страны (7) и 10 цифр.", "Ошибка регистрации!");
                // Проверка пароля при помощи вспомогательной функции выше в коде, мы просто вызываем ее и даем ей запрашиваемые данные (строку пароля)
                else if (!ValidatePassword(passwordBox.Password))
                    MessageBox.Show("Пароль не соответствует правилам безопасности. \nРазмер пароля не менее 8 и не более 15 символов.\nОн должен содержать в себе буквы нижнего и верхнего регистра,\nа так же цифры и специальные символы.\nНе может содержать пробелов.", "Ошибка регистрации!");
                else if (passwordBox.Password != passwordDubleBox.Password) // Проверка повторения пароля, в случае неверного повторения очищаем это поле
                {
                    MessageBox.Show("Неверный повтор пароля.", "Ошибка регистрации!");
                    passwordDubleBox.Password = "";
                }
                else // и наконец после проведения всех этих проверок можем создовать нового пользователя
                {
                    // В заготовку people вносим данные из строк
                    people.name = NameBox.Text;
                    people.surname = SurnameBox.Text;
                    people.patronymic = PatronomicBox.Text;
                    people.phone = PhoneBox.Text;
                    people.password = passwordBox.Password;

                    // При помощи функции Add в таблице user добовляем новую строку пользователя в бд
                    // и сохраняем все измененияпри помощи функции SaveChanges
                    BD.db.user.Add(people);
                    BD.db.SaveChanges();

                    MessageBox.Show("Ваш аккаунт был создан, осталось только войти в него.", "Уведомление");

                    // Возвращаемся в окно входа в аккаунт, передовая в его конструктор строку номера телефона для упрощения жизни пользователя
                    new EnterWindow(PhoneBox.Text).Show();
                    this.Close();
                    // Возврат на страницу кода входа -> EnterWindow
                }
            }
            else // Если есть пустые поля
                MessageBox.Show("Заполните все поля", "Ошибка регистрации!");

        }

        // При нажатии на кнопку назад возвращаемся на страницу входа
        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            new EnterWindow().Show();
            this.Close();
            // Возврат на страницу кода входа -> EnterWindow
        }
    }
}
