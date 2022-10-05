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
using System.Drawing;
using System.Reflection;
using System.Windows.Media.Media3D;
using System.Globalization;
namespace CourseChem
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MethodInfo MainFunc;
        public MethodInfo Condit;
        public MainWindow()
        {
            InitializeComponent();
            TypeSh.Items.Add("Нахлёсточный");
            TypeSh.Items.Add("Стыковый");
            TypeSh.Items.Add("Угловой");
            TypeSh.Items.Add("Тавровый");
            TypeSh.Items.Add("Торцовый");
            TypeSh.SelectedIndex = 0;



            
            Material.Items.Add("Углеродистая сталь СТ.3 (σ = 180 н/мм^2)");
            Material.Items.Add("Углеродистая сталь СТ.4 (σ = 240 н/мм^2)");
            Material.Items.Add("Углеродистая сталь СТ.6 (σ = 280 н/мм^2)");
            Material.SelectedIndex = 0;
            TypeEl.Items.Add("342А с флюсом (0.65 *σ)");
            TypeEl.Items.Add("Э34 с толстым покрытием (0.60*σ)");
            TypeEl.Items.Add("Э34 с тонким покрытием (0.50*σ)");
            TypeEl.SelectedIndex = 0;

     
           
        }

        private void InputData()
        {
            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            Calc.k.Clear();
            Calc.P.Clear();
            Calc.LList.Clear();
            Calc.LListForChart.Clear();
            Calc.b = Convert.ToDouble(b.Text);
            Calc.kmin = Convert.ToDouble(kmin.Text);
            Calc.kmax = Convert.ToDouble(kmax.Text);
            Calc.dk = Convert.ToDouble(dk.Text);
            Calc.Pmin = Convert.ToDouble(Pmin.Text);
            Calc.Pmax = Convert.ToDouble(Pmax.Text);
            Calc.dP = Convert.ToDouble(dP.Text);
            Calc.TypeF = Convert.ToInt32(TypeForse.SelectedIndex);
            Calc.TypeSh = Convert.ToInt32(TypeSh.SelectedIndex);
            Calc.n1 = double.Parse(n1.Text.Replace(".", ","));
            Calc.n2 = double.Parse(n2.Text.Replace(".", ","));
            Calc.n3 = double.Parse(n3.Text.Replace(".", ","));
            Calc.sinb = Convert.ToInt32(sinb.Text);

            if (Material.SelectedIndex == 0)
            {
                Calc.sigma = Convert.ToDouble(180);
            }
            else if (Material.SelectedIndex == 1)
            {
                Calc.sigma = Convert.ToDouble(240);
            }
            else if (Material.SelectedIndex == 2)
            {
                Calc.sigma = Convert.ToDouble(280);
            }

            if (TypeEl.SelectedIndex == 0)
            {
                Calc.forElec = Convert.ToDouble(0.65);
            }
            else if (TypeEl.SelectedIndex == 1)
            {
                Calc.forElec = Convert.ToDouble(0.60);
            }
            else if (TypeEl.SelectedIndex == 2)
            {
                Calc.forElec = Convert.ToDouble(0.5);
            }

            Calc.RunCalc();
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Checking())
            {

                InputData();


                for (int i = 0; i < Calc.LList.Count; i++)
                {

                    var res = MainFunc.Invoke(null, new object[] { Calc.LList[i], 0 });

                }


            (new _3DGraph(MainFunc, Calc.kmin, Calc.kmax, Calc.Pmin, Calc.Pmax)).ShowDialog();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Help_Click_info(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данная программа выполнена в рамках курса:\nКомпьютерное моделирование в химии и химической технологии\nСтудент СПбГТИ(ТУ) группы 475: Роман Сергеевич Овчинников\nРуководитель проекта: Лариса Владимировна Гольцева","Информация о разработчике");
        }

        private void Help_Click_fun(object sender, RoutedEventArgs e)
        {
            Form1 win = new Form1("/1.htm");
            win.ShowDialog();
        }


        private bool Checking()
        {
            bool flag = true;
            int b = 0;
            int Kmin = 0;
            int Kmax = 0;
            int ШагК = 0;
            int Pmin = 0;
            int Pmax = 0;
            int ШагP = 0;
            int Beta = 0;

            try
            {
                if (int.TryParse(this.b.Text, out b) && int.TryParse(kmin.Text, out Kmin) && int.TryParse(kmax.Text, out Kmax)
                    && int.TryParse(dk.Text, out ШагК) && int.TryParse(this.Pmin.Text, out Pmin) && int.TryParse(this.Pmax.Text, out Pmax)
                    && int.TryParse(dP.Text, out ШагP) && int.TryParse(sinb.Text, out Beta) ) {
                    if(b > 0 && Kmin > 0 && Kmax > 2 && ШагК > 0 && Pmin >= 100 && Pmax >= 1000 && ШагP >= 100 && Beta > 0)
                    {
                        if(b <= 750 && Kmin <= 140 && Kmax <= 150 && ШагК <= Kmax / 3 && Pmin <= 500000 && Pmax <= 1000000 && ШагP <= Pmax / 3 && Beta <= 360)
                        {
                            if (Kmin <  Kmax && Pmin < Pmax)
                            {

                            }
                            else
                            {
                                MessageBox.Show("Левая граница не может быть больше правой!\nДиапазон можно узнать наведя на поле ввода ", "Ввод некоректных данных!");
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Вы вышли за предел диапазона!\nДиапазон можно узнать наведя на поле ввода ", "Ввод некоректных данных!");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Вводите числа больше минимума!\nДиапазон можно узнать наведя на поле ввода", "Ввод некоректных данных!");
                        return false;
                    }

                }
                else
                {
                    MessageBox.Show("Вводите только целые числа!\nДиапазон можно узнать наведя на поле ввода", "Ввод некоректных данных!");
                    return false;
                }

                try
                {

                    double nx1 = double.Parse(n1.Text.Replace(".", ","));
                    double nx2 = double.Parse(n2.Text.Replace(".", ","));
                    double nx3 = double.Parse(n3.Text.Replace(".", ","));
                    if ((nx1 >= 1.1 && nx1 <= 3.0) && (nx2 >= 1.2 && nx2 <= 2.0) && (nx3 >= 1.0 && nx3 <= 1.5))
                    {
                        flag = true;
                    }
                    else
                    {
                        MessageBox.Show("Вводите в n только числа больше в указаных диапазонах");
                        return false;
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Вводите в n только числа в указаных диапазонах");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Вводите только целые числа больше 0");
                return false;
            }
            


            return flag;
        }


        private void ButtonTabel_Click(object sender, RoutedEventArgs e)
        {


            if (Checking())
            {
                InputData();

                Table win = new Table();
                this.Hide();
                win.ShowDialog();
                this.Show();
            }
        }

        private void TypeSh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (TypeSh.SelectedIndex > 1)
            {
                MessageBox.Show("Нереализовано!");

                _3dd.IsEnabled = false;
                _2dd.IsEnabled = false;
                _tab.IsEnabled = false;
            }
            else
            {
                _3dd.IsEnabled = true;
                _2dd.IsEnabled = true;
                _tab.IsEnabled = true;
            }
            if (TypeSh.SelectedIndex >= 0)
            {
             
                BitmapImage src = new BitmapImage();

                src.BeginInit();
                src.UriSource = new Uri("images/sh" + TypeSh.SelectedIndex + ".jpg", UriKind.Relative);
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();
                imgSh.Source = src;
                imgSh.Stretch = Stretch.Uniform;
            }
            if (TypeSh.SelectedIndex == 0)
            {
                TypeForse.Items.Clear();
                TypeForse.Items.Add("Фланговые швы (Lш=b)");
                TypeForse.Items.Add("Лобовые швы (Lш=2*lф)");
                TypeForse.Items.Add("Косой шов (Lш=b/sinβ)");
                TypeForse.Items.Add("Комбинированный шов (Lш=b+2*lф)");
                TypeForse.SelectedIndex = 0;
            }
            if (TypeSh.SelectedIndex == 1)
            {
                TypeForse.Items.Clear();
                TypeForse.Items.Add("На продольную силу (Lш=b)");
                TypeForse.Items.Add("На продольную силу стыка с косым швом (Lш = b/sinβ)");
                TypeForse.Items.Add("На изгиб (Lш=k)");
                TypeForse.SelectedIndex = 0;
            }
        }

        private void TypeForse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tasks tasks = new Tasks();
            Type TestType = Type.GetType("CourseChem.Tasks", false, true);
            if (TypeSh.SelectedIndex == 0)
            {
                if (TypeForse.SelectedIndex >= 0)
                {



                    Condit = TestType.GetMethod("Contidions3");

                    BitmapImage src = new BitmapImage();

                    src.BeginInit();
                    src.UriSource = new Uri("images/f" + TypeForse.SelectedIndex + ".jpg", UriKind.Relative);
                    src.CacheOption = BitmapCacheOption.OnLoad;
                    src.EndInit();
                    imgF.Source = src;
                    imgF.Stretch = Stretch.Uniform;
                }
                if (TypeForse.SelectedIndex == 0)
                {
                    MainFunc = TestType.GetMethod("MainFunction0");
                }
                if (TypeForse.SelectedIndex == 1)
                {
                    MainFunc = TestType.GetMethod("MainFunction1");

                }
                if (TypeForse.SelectedIndex == 2)
                {
                    MainFunc = TestType.GetMethod("MainFunction2");
                    sinl.Visibility = Visibility.Visible;
                    sinb.Visibility = Visibility.Visible;
                    grad.Visibility = Visibility.Visible;
                }
                else
                {
                    sinl.Visibility = Visibility.Hidden;
                    sinb.Visibility = Visibility.Hidden;
                    grad.Visibility = Visibility.Hidden;
                }
                if (TypeForse.SelectedIndex == 3)
                {
                    MainFunc = TestType.GetMethod("MainFunction3");
                }
            }
            if (TypeSh.SelectedIndex == 1)
            {
                if (TypeForse.SelectedIndex >= 0)
                {
                    sinl.Visibility = Visibility.Hidden;
                    sinb.Visibility = Visibility.Hidden;
                    grad.Visibility = Visibility.Hidden;


                    Condit = TestType.GetMethod("Contidions3");

                    BitmapImage src = new BitmapImage();

                    src.BeginInit();
                    src.UriSource = new Uri("images/fs" + TypeForse.SelectedIndex + ".png", UriKind.Relative);
                    src.CacheOption = BitmapCacheOption.OnLoad;
                    src.EndInit();
                    imgF.Source = src;
                    imgF.Stretch = Stretch.Uniform;
                }
                if (TypeForse.SelectedIndex == 0)
                {
                    MainFunc = TestType.GetMethod("MFunction0");
                }
                if (TypeForse.SelectedIndex == 1)
                {
                    MainFunc = TestType.GetMethod("MFunction1");
                    sinl.Visibility = Visibility.Visible;
                    sinb.Visibility = Visibility.Visible;
                    grad.Visibility = Visibility.Visible;
                }
                if (TypeForse.SelectedIndex == 2)
                {
                    MainFunc = TestType.GetMethod("MFunction2");
                }
            }
           
        }

        private void Material_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Material.SelectedIndex >= 0)
            {

                BitmapImage src = new BitmapImage();

                src.BeginInit();
                src.UriSource = new Uri("images/m" + Material.SelectedIndex + ".jpg", UriKind.Relative);
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();
                imgM.Source = src;
                imgM.Stretch = Stretch.Uniform;
            }
        }

        private void TypeEl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeEl.SelectedIndex >= 0)
            {
                BitmapImage src = new BitmapImage();

                src.BeginInit();
                src.UriSource = new Uri("images/el" + TypeEl.SelectedIndex + ".jpg", UriKind.Relative);
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();
                imgEl.Source = src;
                imgEl.Stretch = Stretch.Uniform;
            }
           
        }

        private void Button2d_Click(object sender, RoutedEventArgs e)
        {
            if (Checking())
            {
                InputData();

                _2d win = new _2d();
                this.Hide();
                win.ShowDialog();
                this.Show();
            }
               
        }

        private void Help_Block(object sender, RoutedEventArgs e)
        {
            Form1 win = new Form1("/2.htm");
            win.ShowDialog();
        }
    }
}
