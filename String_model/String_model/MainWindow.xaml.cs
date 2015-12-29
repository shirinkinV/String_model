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

namespace String_model
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

        private void a_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (equation_label != null)
                equation_label.Content = "Utt=(" + a_box.Text + ")^2*Uxx+(" + f_box.Text + ")";
        }

        private void f_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (equation_label != null)
                equation_label.Content = "Utt=(" + a_box.Text + ")^2*Uxx+(" + f_box.Text + ")";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            left_box.Text = "-∞";
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            right_box.Text = "+∞";
        }

        private void left_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (first_left_bound_label != null)
                first_left_bound_label.Content = "U(" + left_box.Text + ",t):";
            if (two_left_bound_label != null)
                two_left_bound_label.Content = "Ux(" + left_box.Text + ",t):";
        }

        private void right_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (first_right_bound_label != null)
                first_right_bound_label.Content = "U(" + right_box.Text + ",t):";
            if (two_right_bound_label != null)
                two_right_bound_label.Content = "Ux(" + right_box.Text + ",t):";
        }

        private void OpenGLControl_OpenGLInitialized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {

        }

        private void OpenGLControl_Resized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {

        }

        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {

        }                             
    }
}
