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
using SharpGL;

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
            if (left_bound_box != null)
            {
                if (left_box.Text.Contains("∞"))
                {
                    left_bound_box.IsEnabled = false;
                    first_left_bound_label.IsEnabled = false;
                    two_left_bound_label.IsEnabled = false;
                    first_left_radio_button.IsEnabled = false;
                    two_left_radio_button.IsEnabled = false;
                }
                else
                {
                    left_bound_box.IsEnabled = true;
                    first_left_bound_label.IsEnabled = true;
                    two_left_bound_label.IsEnabled = true;
                    first_left_radio_button.IsEnabled = true;
                    two_left_radio_button.IsEnabled = true;
                }
            }
            if(begin_cond_left_bound_box!= null)
            {
                if (left_box.Text.Contains("∞")) {
                    begin_cond_left_bound_box.IsEnabled = true;   
                }
                else
                {
                    begin_cond_left_bound_box.IsEnabled = false;
                    begin_cond_left_bound_box.Text = left_box.Text;
                }
            }
        }

        private void right_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (first_right_bound_label != null)
                first_right_bound_label.Content = "U(" + right_box.Text + ",t):";
            if (two_right_bound_label != null)
                two_right_bound_label.Content = "Ux(" + right_box.Text + ",t):";
            if (right_bound_box != null)
            {
                if (right_box.Text.Contains("∞"))
                {
                    right_bound_box.IsEnabled = false;
                    first_right_bound_label.IsEnabled = false;
                    two_right_bound_label.IsEnabled = false;
                    first_right_radio_button.IsEnabled = false;
                    two_right_radio_button.IsEnabled = false;
                }
                else
                {
                    right_bound_box.IsEnabled = true;
                    first_right_bound_label.IsEnabled = true;
                    two_right_bound_label.IsEnabled = true;
                    first_right_radio_button.IsEnabled = true;
                    two_right_radio_button.IsEnabled = true;
                }
            }
            if (begin_cond_right_bound_box != null)
            {
                if (right_box.Text.Contains("∞"))
                {                                                
                    begin_cond_right_bound_box.IsEnabled = true;
                }
                else
                {                                                 
                    begin_cond_right_bound_box.IsEnabled = false;
                    begin_cond_right_bound_box.Text = right_box.Text;
                }
            }
        }

        Func<double, double, double> U = (x, t) => 0;
        double time = -1;
        long ticks;



        private void OpenGLControl_OpenGLInitialized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;
            gl.ClearColor(1, 1, 1, 1);
            gl.Disable(OpenGL.GL_LIGHTING);
        }

        private void OpenGLControl_Resized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;
            gl.Viewport(0, 0, (int)gl_view.ActualWidth, (int)gl_view.ActualHeight);
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            double ratio = gl_view.ActualWidth / Math.Max(1, gl_view.ActualHeight);

            //gl.Frustum(-0.1 * ratio, 0.1 * ratio, -0.1, 0.1, 0.1, 10);
            gl.Ortho(-10 , 10 , -10/ratio, 10/ratio, 0.5, 10);
        }

        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            if (time == -1)
            {
                ticks = DateTime.Now.Ticks;
                time = 0;
            }
            else
            {
                long newTicks = DateTime.Now.Ticks;
                time += 1 * (double)(newTicks - ticks) / 10000000;
                ticks = newTicks;
                while (time > 100)
                {
                    time -= 100;
                }
            }
            OpenGL gl = args.OpenGL;
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(0, 0, -1);

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Color(0, 0, 0, 1.0);
            gl.Begin(OpenGL.GL_LINE_STRIP);

            for(double x = -10; x <= 10; x += 0.01)
            {
                gl.Vertex4d(x,U(x, time),0,1);
            }

            gl.End();
            gl.Flush();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            double begin_cond_left = FunctionsAndParsing.Parser.ParseExpression(begin_cond_left_bound_box.Text, null)(null);
            double begin_cond_right = FunctionsAndParsing.Parser.ParseExpression(begin_cond_right_bound_box.Text, null)(null);
            double a = FunctionsAndParsing.Parser.ParseExpression(a_box.Text, null)(null);
                                                                                                  
            if (left_box.Text == "-∞" && right_box.Text == "+∞")
            {
                Func<double[], double> pre_phi = FunctionsAndParsing.Parser.ParseExpression(phi_box.Text, new string[] { "x" });
                Func<double, double> phi = x =>
                {
                    if (x >= begin_cond_left&&x<=begin_cond_right)
                        return pre_phi(new double[] { x });
                    else
                        return 0;
                };
                Func<double[],double> pre_psi = FunctionsAndParsing.Parser.ParseExpression(psi_box.Text, new string[] { "x" });
                Func<double, double> psi = x =>
                {
                    if (x >= begin_cond_left && x <= begin_cond_right)
                        return pre_psi(new double[] { x });
                    else
                        return 0;
                };

                U = (x, t) => 0.5 * (phi(x + a * t) + phi(x - a * t)) + 0.5 / a * MathNet.Numerics.Integrate.OnClosedInterval(psi, x - a * t, x + a * t, 1e-4);
                time = 0;
            }
            else
            {
                if (left_box.Text == "-∞")
                {

                }
                else
                {
                    if(right_box.Text == "+∞")
                    {

                    }
                    else
                    {

                    }
                }            
            }
        }
    }
}
