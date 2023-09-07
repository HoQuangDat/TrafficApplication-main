using QuanLyShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace giaothong.ViewModel
{
    public class ArrangTeacherViewModel : BaseViewModel
    {
        public ICommand closeArrangeTeacherWindow { get; set; }
        public ICommand openInsertTheoryTeacherWindow { get; set; }
        public ICommand openInsertPraticeTeacherWindow { get; set; }

        public ArrangTeacherViewModel()
        {
            closeArrangeTeacherWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            openInsertTheoryTeacherWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                InsertTheoryTeacherTrainingWindow insertTheoryTeacher = new InsertTheoryTeacherTrainingWindow();
                insertTheoryTeacher.ShowDialog();
                p.ShowDialog();
            });

            openInsertPraticeTeacherWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                InsertPracticeTeacherTrainingrWindow insertPractieTeacher = new InsertPracticeTeacherTrainingrWindow();
                insertPractieTeacher.ShowDialog();
                p.ShowDialog();
            });
        }
    }
}
