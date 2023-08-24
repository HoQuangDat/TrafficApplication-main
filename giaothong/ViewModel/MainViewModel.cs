using giaothong.Model;
using QuanLyShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace giaothong.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private giaothongEntities db;

        private static USER _user;
        public static USER User { get => _user; set { _user = value; } }

        public ICommand Login { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand closeWindow { get; set; }


        public MainViewModel()
        {
            User = new USER();

            closeWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => {
                User.Password = p.Password;
            });

            Login = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                bool checkLogin = isLogin();
                if (!checkLogin)
                {
                    MessageBox.Show("Đăng nhập thất bại. Vui lòng xem lại tài khoản hoặc mật khẩu", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    p.Hide();
                    HomeWindow home = new HomeWindow();
                    home.ShowDialog();
                    p.ShowDialog();
                }
            });
        }

        public bool isLogin()
        {
            using (db = new giaothongEntities())
            {
                bool check = true;

                if (User.UserName == null || User.Password == null)
                {
                    check = false;
                }

                if (check)
                {
                    var user = (from c in db.USERS
                                join r in db.USERS_ROLE
                                on c.Role_ID equals r.ID
                                where c.UserName == User.UserName && c.Password == User.Password
                                select c).FirstOrDefault();

                    if (user == null)
                    {
                        check = false;
                        return check;
                    }
                    else
                    {
                        User.Name = user.Name;
                        User.Phone = user.Phone;
                        User.Role_ID = user.Role_ID;
                        User.Address = user.Address;
                        User.BirthDay = user.BirthDay;
                        User.Email = user.Email;
                        User.Password = null;
                        User.ID = user.ID;
                        User.USERS_ROLE = user.USERS_ROLE;

                        if (user.USERS_ROLE.ID == 1)
                        {
                            TeacherViewModel.IsEnable = false;
                        }
                        else if (user.USERS_ROLE.ID == 2)
                        {
                            TeacherViewModel.IsEnable = true;
                        }

                        check = true;
                    }
                }

                return check;
            }
        }
    }
}
