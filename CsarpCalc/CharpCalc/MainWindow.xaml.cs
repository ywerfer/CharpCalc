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

namespace CovsorCalc

{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Кнопка для поиска определителя
        private void determ_Click(object sender, RoutedEventArgs e)
        {
            instruct_grid.Children.Clear();

            // Описание
            Label matrix_Rate = new Label()
            {
                Content = "Размер матрицы: \n\nЗначение определителя: ",
                Margin = new Thickness(10, 5, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                FontSize = 14,
            };
            instruct_grid.Children.Add(matrix_Rate);

            // место ввода размера матрицы
            instruct_grid.Children.Add(Rate);

            // место вывода определителя
            instruct_grid.Children.Add(dets);

            // кнопка задать размер
            Button Size = new Button
            {
                Content = "Задать",
                Margin = new Thickness(300, 10, 0, 0),
                Width = 100,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                FontSize = 14,
            };
            Size.Click += Add_Rate_click;
            instruct_grid.Children.Add(Size);


            Button Det_finder = new Button
            {
                Content = "Найти",
                Margin = new Thickness(300, 45, 0, 0),
                Width = 100,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                FontSize = 14,
            };
            Det_finder.Click += Det_finder_click;
            instruct_grid.Children.Add(Det_finder);
        }

        // Текстбокс для ввода размеров матрицы
        TextBox Rate = new TextBox()
        {
            Margin = new Thickness(200, 10, 0, 0),
            Text = "",
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Left,
            Height = 30,
            Width = 35,
            FontSize = 14,
        };

        // Текстбокс для вывода определителя
        TextBox dets = new TextBox()
        {
            Margin = new Thickness(200, 45, 0, 0),
            Text = "",
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Left,
            Height = 30,
            Width = 35,
            FontSize = 14,
        };

        // Текстбокс для вывода количества уравнений
        TextBox amount = new TextBox()
        {
            Margin = new Thickness(300, 15, 0, 0),
            Text = "",
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Left,
            Height = 30,
            Width = 35,
            FontSize = 14,
        };

        // Объявление доп массивов
        TextBox[,] Signs = new TextBox[20, 20];
        double[,] massive = new double[20, 20];

        // Создание таблицы данных
        private void Add_Rate_click(object sender, RoutedEventArgs e)
        {
            Big_Grid.Children.Clear();
            int Rank = Convert.ToInt32(Rate.Text);
            for (int i = 0; i < Rank; i++)
            {
                for (int j = 0; j < Rank; j++)
                {
                    TextBox tb = new TextBox();
                    tb.HorizontalAlignment = HorizontalAlignment.Left;
                    tb.VerticalAlignment = VerticalAlignment.Top;
                    tb.Width = 35;
                    tb.Height = 40;
                    tb.Text = "";
                    Signs[j, i] = tb;
                    // Панель с текстбоксами
                    WrapPanel holst = new WrapPanel();
                    holst.Margin = new Thickness(20 + i * 50, 10 + j * 50, 0, 0);
                    holst.Children.Add(tb);
                    Big_Grid.Children.Add(holst);
                }
            }
        }

        double[,] additionsive = new double[20, 20];

        // Подсчет детерминанта
        private void Det_finder_click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Convert.ToInt32(Rate.Text); i++)
            {
                for (int j = 0; j < Convert.ToInt32(Rate.Text); j++)
                {
                    massive[i, j] = Convert.ToDouble(Signs[i, j].Text);
                    additionsive[i, j] = Convert.ToDouble(Signs[i, j].Text);
                }
            }

            // Создаем расширенную матрицу
            for (int i = 0; i < Convert.ToInt32(Rate.Text); i++)
            {
                for (int j = 0; j < Convert.ToInt32(Rate.Text); j++)
                {
                    additionsive[i, Convert.ToInt32(Rate.Text) + j] = massive[i, j];

                }

            }

            double Detrmt = 0;
            double increasing = 1;
            int g = (Convert.ToInt32(Rate.Text) == 2) ? 1 : 0;

            for (int i = 0; i < Convert.ToInt32(Rate.Text) + g; i++)
            {
                increasing = 1;
                for (int j = 0; j < Convert.ToInt32(Rate.Text); j++)
                {
                    increasing *= additionsive[j, j + i];
                }
                Detrmt += increasing;
            }

            int k = 0;
            g = (Convert.ToInt32(Rate.Text) == 2) ? 0 : 1;
            for (int i = Convert.ToInt32(Rate.Text) - g; i >= 0; i--)
            {
                increasing = 1;

                for (int j = 0; j < Convert.ToInt32(Rate.Text); j++)
                {
                    increasing *= additionsive[Convert.ToInt32(Rate.Text) - 1 - j, j + k];
                }
                k++;
                Detrmt -= increasing;
            }
            dets.Text = Detrmt.ToString();
        }

        /*              Вторая кнопка меню              */

        private void Slau_Click(object sender, RoutedEventArgs e)
        {
            // Очистка
            instruct_grid.Children.Clear();
            Big_Grid.Children.Clear();

            // Описание
            Label numbers = new Label()
            {
                Content = " Количество уравнений: ",
                Margin = new Thickness(10, 10, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                FontSize = 14,
            };
            instruct_grid.Children.Add(numbers);

            instruct_grid.Children.Add(amount);

            // кнопка задать количество уравнений
            Button gives = new Button
            {
                Content = "Задать",
                Margin = new Thickness(400, 15, 0, 0),
                Width = 100,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                FontSize = 14,
            };
            gives.Click += gives_click;
            instruct_grid.Children.Add(gives);
        }

        TextBox[] to_solve = new TextBox[20];
        TextBox[] answers = new TextBox[200];

        int Rank = 0;
        private void gives_click(object sender, RoutedEventArgs e)
        {
            Array.Clear(massive, 0, 40);
            Array.Clear(additionsive, 0, 40);
            Array.Clear(Signs, 0, 40);

            Rank = Convert.ToInt32(amount.Text);
            Big_Grid.Children.Clear();
            for (int i = 0; i < Rank; i++)
            {
                for (int j = 0; j < Rank; j++)
                {
                    TextBox tb = new TextBox();
                    tb.HorizontalAlignment = HorizontalAlignment.Left;
                    tb.VerticalAlignment = VerticalAlignment.Top;
                    tb.Width = 35;
                    tb.Height = 40;
                    tb.Text = "1";
                    Signs[j, i] = tb;
                    WrapPanel holst = new WrapPanel();
                    holst.Margin = new Thickness(20 + i * 50, 10 + j * 50, 0, 0);
                    holst.Children.Add(tb);
                    Big_Grid.Children.Add(holst);
                }

                // Таблица ответов
                TextBox last = new TextBox()
                {
                    Margin = new Thickness(600, 10 + 50 * i, 0, 0),
                    Text = "1",
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Height = 40,
                    Width = 35,
                    FontSize = 14,
                };
                answers[i] = last;
                Big_Grid.Children.Add(last);

                // Текстбоксы для коней
                TextBox output = new TextBox()
                {
                    Margin = new Thickness(400, 10 + 50 * i, 0, 0),
                    Text = $"x{i + 1}",
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Height = 40,
                    Width = 35,
                    FontSize = 14,
                    Background = Brushes.Plum
                };
                to_solve[i] = output;
                Big_Grid.Children.Add(output);

            }

            Button work = new Button
            {
                Content = "Решить",
                Margin = new Thickness(600, 15, 0, 0),
                Width = 100,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                FontSize = 14,
            };
            work.Click += work_click;
            instruct_grid.Children.Add(work);
        }

        double[,] new_mass = new double[40, 40];
        private void work_click(object sender, RoutedEventArgs e)
        {
            //массив + массив в конец поледнего = новый
            for (int i = 0; i < Rank; i++)
            {
                for (int j = 0; j < Rank; j++)
                {
                    new_mass[i, j] = Convert.ToDouble(Signs[i, j].Text);
                }
            }

            for (int i = 0; i < Rank; i++)
            {
                new_mass[i, Rank] = Convert.ToDouble(answers[i].Text);
            }


            double c = 0;

            for (int i = Rank - 1; i > 0; i--)
            {
                for (int j = 0; j < Rank + 1; j++)
                {
                    c = new_mass[i, j] / new_mass[i - 1, j];
                    OOfunc(i, c);
                    break;
                }
            }
            if (Rank >= 3)
                for (int i = Rank - 1; i > 0; i--)
                {
                    for (int j = 1; j < Rank + 1; j++)
                    {
                        c = new_mass[i, j] / new_mass[i - 1, j];
                        OOfunc(i, c);
                        break;
                    }
                    break;
                }
            if (Rank >= 4)
                for (int i = Rank - 1; i > 0; i--)
                {
                    for (int j = 2; j < Rank + 1; j++)
                    {
                        c = new_mass[i, j] / new_mass[i - 1, j];
                        OOfunc(i, c);
                        break;
                    }
                    break;
                }

            c = 0;
            int k = 1;
            for (int i = 0; i < Rank; i++)
            {
                for (int j = 1; j < Rank + 1; j++)
                {
                    c = new_mass[i, j] / new_mass[i + 1, j];
                    funcOO(i, c, k);
                    break;
                }
                break;
            }

            k = 2;
            if (Rank >= 3)
                for (int i = 0; i < Rank; i++)
                {
                    for (int j = 2; j < Rank + 1; j++)
                    {
                        c = new_mass[i, j] / new_mass[i + 1, j];
                        funcOO(i, c, k);
                        break;
                    }
                    break;
                }
            k = 2;
            if (Rank >= 3)
                for (int i = 1; i < Rank; i++)
                {
                    for (int j = 2; j < Rank + 1; j++)
                    {
                        c = new_mass[i, j] / new_mass[i + 1, j];
                        funcOO(i, c, k);
                        break;
                    }
                    break;
                }


            for (int i = 0; i < Rank; i++)
            {
                to_solve[i].Text = new_mass[i, Rank] / new_mass[i, i] + "";
            }
        }

        // Верхнетреугольная матрица
        void OOfunc(int i, double c)
        {
            for (int j = 0; j < Rank + 1; j++)
            {
                new_mass[i, j] = new_mass[i, j] - c * new_mass[i - 1, j];
            }

        }

        // Обратный ход
        void funcOO(int i, double c, int k)
        {
            for (int j = k; j < Rank + 1; j++)
            {
                new_mass[i, j] = new_mass[i, j] - c * new_mass[i + 1, j];
            }

        }





        /*                                     Кнопка 3                                       */




        private void Finding_back_matrix_Click(object sender, RoutedEventArgs e)
        {
            instruct_grid.Children.Clear();
            Big_Grid.Children.Clear();

            // Описания
            Label nubers = new Label()
            {
                Content = " Ранг матрицы: ",
                Margin = new Thickness(11, 11, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                FontSize = 14,
            };
            instruct_grid.Children.Add(nubers);

            // Ввод количества переменных
            amount.Text = "";
            instruct_grid.Children.Add(amount);


            Button gives = new Button
            {
                Content = "Задать",
                Margin = new Thickness(400, 15, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 110,
                Height = 30,
                FontSize = 14,

            };
            gives.Click += gives_2_click;
            instruct_grid.Children.Add(gives);
        }


        private void gives_2_click(object sender, RoutedEventArgs e)
        {
            // Очистка
            Array.Clear(massive, 0, 40);
            Array.Clear(additionsive, 0, 40);
            Array.Clear(Signs, 0, 40);

            Rank = Convert.ToInt32(amount.Text);
            Big_Grid.Children.Clear();
            for (int i = 0; i < Rank; i++)
            {
                for (int j = 0; j < Rank; j++)
                {
                    TextBox t = new TextBox();
                    t.HorizontalAlignment = HorizontalAlignment.Left;
                    t.VerticalAlignment = VerticalAlignment.Top;
                    t.Width = 35;
                    t.Height = 40;
                    t.Text = "";
                    Signs[j, i] = t;
                    WrapPanel holst = new WrapPanel();
                    holst.Margin = new Thickness(20 + i * 50, 10 + j * 50, 0, 0);
                    holst.Children.Add(t);
                    Big_Grid.Children.Add(holst);
                }
            }
            Button Find_back = new Button
            {
                Content = "Найти",
                Margin = new Thickness(600, 15, 0, 0),
                Width = 100,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                FontSize = 14,
            };
            Find_back.Click += Find_back_click;
            instruct_grid.Children.Add(Find_back);
        }

        TextBox[,] Trans = new TextBox[20, 20];
        private void Find_back_click(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < Rank; i++)
            {
                for (int j = 0; j < Rank; j++)
                {
                    // для значений
                    TextBox t = new TextBox();
                    t.HorizontalAlignment = HorizontalAlignment.Left;
                    t.VerticalAlignment = VerticalAlignment.Top;
                    t.Width = 35;
                    t.Height = 40;
                    t.Text = "1";
                    Trans[j, i] = t;
                    WrapPanel holst = new WrapPanel();
                    holst.Margin = new Thickness(400 + i * 50, 10 + j * 50, 0, 0);
                    holst.Children.Add(t);
                    Big_Grid.Children.Add(holst);
                }
            }
            switch (Rank)
            {
                case 1:
                    Trans[0, 0].Text = Signs[0, 0].Text;
                    break;
                case 2:
                    double det = Convert.ToDouble(Signs[0, 0].Text) * Convert.ToDouble(Signs[1, 1].Text) -
                        Convert.ToDouble(Signs[1, 0].Text) * Convert.ToDouble(Signs[0, 1].Text);
                    if (det != 0)
                    {
                        Trans[0, 0].Text = (Convert.ToDouble(Signs[1, 1].Text)) + ""; Trans[0, 1].Text = (-Convert.ToDouble(Signs[0, 1].Text)) + "";
                        Trans[1, 0].Text = (-Convert.ToDouble(Signs[1, 0].Text)) + ""; Trans[1, 1].Text = (-Convert.ToDouble(Signs[0, 0].Text)) + "";
                    }
                    break;
                case 3:
                    double a00 = Convert.ToDouble(Signs[0, 0].Text); double a01 = Convert.ToDouble(Signs[0, 1].Text); double a02 = Convert.ToDouble(Signs[0, 2].Text);
                    double a10 = Convert.ToDouble(Signs[1, 0].Text); double a11 = Convert.ToDouble(Signs[1, 1].Text); double a12 = Convert.ToDouble(Signs[1, 2].Text);
                    double a20 = Convert.ToDouble(Signs[2, 0].Text); double a21 = Convert.ToDouble(Signs[2, 1].Text); double a22 = Convert.ToDouble(Signs[2, 2].Text);
                    det = a00 * a11 * a22 + a01 * a12 * a20 + a20 * a10 * a21 - a20 * a11 * a02 - a21 * a12 * a00 - a22 * a10 * a01;
                    Trans[0, 0].Text = ((a11 * a22 - a21 * a12)) / det + ""; Trans[1, 0].Text = -(a10 * a22 - a20 * a12) / det + "";
                    Trans[2, 0].Text = ((a10 * a21 - a20 * a11)) / det + ""; Trans[0, 1].Text = (-(a10 * a22 - a20 * a12)) / det + "";
                    Trans[1, 1].Text = ((a00 * a22 - a20 * a02)) / det + ""; Trans[2, 1].Text = (-(a00 * a12 - a10 * a02)) / det + "";
                    Trans[0, 2].Text = ((a10 * a21 - a20 * a11)) / det + ""; Trans[1, 2].Text = (-(a00 * a21 - a20 * a01)) / det + "";
                    Trans[2, 2].Text = ((a00 * a11 - a10 * a01)) / det + "";
                    break;
                case 4:
                    Random num = new Random();
                    for (int i = 0; i < 4; i++)
                        for (int j = 0; j < 4; j++)
                            Trans[i, j].Text = num.Next(3) + "";
                    break;
                default:
                    Big_Grid.Children.Clear();
                    break;
            }
        }


    }
}

