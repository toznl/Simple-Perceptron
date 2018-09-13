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
using System.Runtime.InteropServices;

using OxyPlot;

namespace Simple_Perceptron
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        #region ConsoleWindows
        //For using console windows
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        #endregion
        public MainWindow()
        {
            AllocConsole();
            InitializeComponent();
            SinglePerceptron singlePerceptron = new SinglePerceptron();
            singlePerceptron.errorFunc_Batch();
            singlePerceptron.errorFunc_Online();
            
        }

        public class MainViewModel
        {
            public MainViewModel()
            {
                this.Title = "Batch";
                int t = 0;
                while (t < 100)
                {
                    int x = 1;
                    int y = 1;
                    x++;
                    y++;
                    this.Points = new List<DataPoint>
                              {
                                  new DataPoint(x, y),

                              };
                    t++;
                }
            }

            public string Title { get; private set; }

            public IList<DataPoint> Points { get; private set; }
        }

        public struct Data_And
        {
            public int x1;
            public int x2;
            public int output;

            public Data_And(int x, int y, int z)
            {
                x1 = x;
                x2 = y;
                output = z;

            }
        }

        public struct Dataset_And
        {
            public Data_And d1;
            public Data_And d2;
            public Data_And d3;
            public Data_And d4;

            public Dataset_And(int x)
            {
                d1 = new Data_And(1, 1, 1);
                d2 = new Data_And(1, 0, 0);
                d3 = new Data_And(0, 1, 0);
                d4 = new Data_And(0, 0, 0);
            }

        }

        public struct result_var
        {
            public double w0;
            public double w1;
            public double bias;
            public result_var(double x, double y, double z)
            {
                w0 = x;
                w1 = y;
                bias = z;
            }

        }

        public class SinglePerceptron
        {
            double bias = 0;
            double weight1 = 0.5;
            double weight2 = 0.5;
            double eta = 0.0001;

            public double net(double w0, double w1, double w2, double x1, double x2)
            {
                double result;

                result = x1 * w1 + x2 * w2 + w0;

                return result;
            }

            public double cost_linear(double x)
            {
                double result;

                if (x > 0)
                {
                    result = x;
                    return result;
                }
                else
                {
                    result = 0;
                    return result;
                }
            }

            public double cost_Sigmoid(double x1, double x2)
            {
                double result;

                result = 1 / (1 + (Math.Exp(net(bias, weight1, weight2, x1, x2))));

                return result;

            }


            public result_var errorFunc_Batch()
            {
                result_var result;
                double lostFunc;
                Dataset_And dataset_and = new Dataset_And(1);
                double temp0=0;
                int epoch = 0;

                lostFunc = Math.Abs(dataset_and.d1.output - cost_Sigmoid(dataset_and.d1.x1, dataset_and.d1.x2) +
                    dataset_and.d2.output - cost_Sigmoid(dataset_and.d2.x1, dataset_and.d2.x2) +
                    dataset_and.d3.output - cost_Sigmoid(dataset_and.d3.x1, dataset_and.d3.x2) +
                    dataset_and.d4.output - cost_Sigmoid(dataset_and.d4.x1, dataset_and.d4.x2))
                    ;

                while (lostFunc > 0.01)
                {
                    bias = bias + (eta * (
                        (-(1 / Math.Exp((bias + weight1 * dataset_and.d1.x1 + weight2 * dataset_and.d1.x2))
                        * Math.Sin(dataset_and.d1.output + 1 / Math.Exp((bias + weight1 * dataset_and.d1.x1 + weight2 * dataset_and.d1.x2)) - 1)) / 2) +
                        (-(1 / Math.Exp((bias + weight1 * dataset_and.d2.x1 + weight2 * dataset_and.d2.x2))
                        * Math.Sin(dataset_and.d2.output + 1 / Math.Exp((bias + weight1 * dataset_and.d2.x1 + weight2 * dataset_and.d2.x2)) - 1)) / 2) +
                        (-(1 / Math.Exp((bias + weight1 * dataset_and.d3.x1 + weight2 * dataset_and.d3.x2))
                        * Math.Sin(dataset_and.d3.output + 1 / Math.Exp((bias + weight1 * dataset_and.d3.x1 + weight2 * dataset_and.d3.x2)) - 1)) / 2) +
                        (-(1 / Math.Exp((bias + weight1 * dataset_and.d4.x1 + weight2 * dataset_and.d4.x2))
                        * Math.Sin(dataset_and.d4.output + 1 / Math.Exp((bias + weight1 * dataset_and.d4.x1 + weight2 * dataset_and.d4.x2)) - 1)) / 2)
                        ));

                    weight1 = weight1 + (eta * (
                         (-(1 / Math.Exp(bias + weight1 * dataset_and.d1.x1 + weight2 * dataset_and.d1.x2) * dataset_and.d1.x1 * Math.Sin(dataset_and.d1.output + 1 / Math.Exp(bias + weight1 * dataset_and.d1.x1 + weight2 * dataset_and.d1.x2) - 1)) / 2) +
                        (-(1 / Math.Exp(bias + weight1 * dataset_and.d2.x1 + weight2 * dataset_and.d2.x2) * dataset_and.d2.x1 * Math.Sin(dataset_and.d2.output + 1 / Math.Exp(bias + weight1 * dataset_and.d2.x1 + weight2 * dataset_and.d2.x2) - 1)) / 2) +
                        (-(1 / Math.Exp(bias + weight1 * dataset_and.d3.x1 + weight2 * dataset_and.d3.x2) * dataset_and.d3.x1 * Math.Sin(dataset_and.d3.output + 1 / Math.Exp(bias + weight1 * dataset_and.d3.x1 + weight2 * dataset_and.d3.x2) - 1)) / 2) +
                        (-(1 / Math.Exp(bias + weight1 * dataset_and.d4.x1 + weight2 * dataset_and.d4.x2) * dataset_and.d4.x1 * Math.Sin(dataset_and.d4.output + 1 / Math.Exp(bias + weight1 * dataset_and.d4.x1 + weight2 * dataset_and.d4.x2) - 1)) / 2)));

                    weight2 = weight2 + (eta * (
                         (-(1 / Math.Exp(bias + weight1 * dataset_and.d1.x1 + weight2 * dataset_and.d1.x2) * dataset_and.d1.x2 * Math.Sin(dataset_and.d1.output + 1 / Math.Exp(bias + weight1 * dataset_and.d1.x1 + weight2 * dataset_and.d1.x2) - 1)) / 2) +
                        (-(1 / Math.Exp(bias + weight1 * dataset_and.d2.x1 + weight2 * dataset_and.d2.x2) * dataset_and.d2.x2 * Math.Sin(dataset_and.d2.output + 1 / Math.Exp(bias + weight1 * dataset_and.d2.x1 + weight2 * dataset_and.d2.x2) - 1)) / 2) +
                        (-(1 / Math.Exp(bias + weight1 * dataset_and.d3.x1 + weight2 * dataset_and.d3.x2) * dataset_and.d3.x2 * Math.Sin(dataset_and.d3.output + 1 / Math.Exp(bias + weight1 * dataset_and.d3.x1 + weight2 * dataset_and.d3.x2) - 1)) / 2) +
                        (-(1 / Math.Exp(bias + weight1 * dataset_and.d4.x1 + weight2 * dataset_and.d4.x2) * dataset_and.d4.x2 * Math.Sin(dataset_and.d4.output + 1 / Math.Exp(bias + weight1 * dataset_and.d4.x1 + weight2 * dataset_and.d4.x2) - 1)) / 2)));

                    lostFunc = Math.Abs(dataset_and.d1.output - cost_Sigmoid(dataset_and.d1.x1, dataset_and.d1.x2) +
                    dataset_and.d2.output - cost_Sigmoid(dataset_and.d2.x1, dataset_and.d2.x2) +
                    dataset_and.d3.output - cost_Sigmoid(dataset_and.d3.x1, dataset_and.d3.x2) +
                    dataset_and.d4.output - cost_Sigmoid(dataset_and.d4.x1, dataset_and.d4.x2));

                    epoch++;
                    Console.WriteLine(lostFunc);
                    if (lostFunc == temp0)
                    {

                        break;

                    }

                    temp0 = lostFunc;


                    //System.Console.WriteLine(bias);
                    //System.Console.WriteLine(weight1);
                    //System.Console.WriteLine(weight2);
                    //System.Console.WriteLine("Lost : {0}", lostFunc);
                    //System.Console.WriteLine("Net : {0}==1", net(bias, weight1, weight2, dataset_and.d1.x1, dataset_and.d1.x2));
                    //System.Console.WriteLine(epoch);
                    
                   

                }
                Console.WriteLine("W0 : {0} | W1 : {1} | W2 : {2} | Error : {3} | Epoch : {4} ", bias, weight1, weight2, lostFunc, epoch);
               

                result.bias = bias;
                result.w0 = weight1;
                result.w1 = weight2;


                return result;
            }

            public result_var errorFunc_Online()
            {
                result_var result;
                double lostFunc1;
                double lostFunc2;
                double lostFunc3;
                double lostFunc4;
                double lostFunc;
                double temp = 0;
                double temp0 = 0;
                Dataset_And dataset_and = new Dataset_And(1);

                int epoch = 0;

                lostFunc1 = Math.Abs(dataset_and.d1.output - cost_Sigmoid(dataset_and.d1.x1, dataset_and.d1.x2));
                lostFunc2 = Math.Abs(dataset_and.d2.output - cost_Sigmoid(dataset_and.d2.x1, dataset_and.d2.x2));
                lostFunc3 = Math.Abs(dataset_and.d3.output - cost_Sigmoid(dataset_and.d3.x1, dataset_and.d3.x2));
                lostFunc4 = Math.Abs(dataset_and.d4.output - cost_Sigmoid(dataset_and.d4.x1, dataset_and.d4.x2));
                lostFunc = Math.Abs(dataset_and.d1.output - cost_Sigmoid(dataset_and.d1.x1, dataset_and.d1.x2) +
            dataset_and.d2.output - cost_Sigmoid(dataset_and.d2.x1, dataset_and.d2.x2) +
            dataset_and.d3.output - cost_Sigmoid(dataset_and.d3.x1, dataset_and.d3.x2) +
            dataset_and.d4.output - cost_Sigmoid(dataset_and.d4.x1, dataset_and.d4.x2))
            ;
                while (lostFunc > 0.01)
                {
                    while (lostFunc1 > 0.01)
                    {
                        

                        
                        bias = bias + (eta * (
                            (-(1 / Math.Exp((bias + weight1 * dataset_and.d1.x1 + weight2 * dataset_and.d1.x2))
                            * Math.Sin(dataset_and.d1.output + 1 / Math.Exp((bias + weight1 * dataset_and.d1.x1 + weight2 * dataset_and.d1.x2)) - 1)) / 2)));
                        weight1 = weight1 + (eta * (
                             (-(1 / Math.Exp(bias + weight1 * dataset_and.d1.x1 + weight2 * dataset_and.d1.x2) * dataset_and.d1.x1 * Math.Sin(dataset_and.d1.output + 1 / Math.Exp(bias + weight1 * dataset_and.d1.x1 + weight2 * dataset_and.d1.x2) - 1)) / 2)));

                        weight2 = weight2 + (eta * (
                             (-(1 / Math.Exp(bias + weight1 * dataset_and.d1.x1 + weight2 * dataset_and.d1.x2) * dataset_and.d1.x2 * Math.Sin(dataset_and.d1.output + 1 / Math.Exp(bias + weight1 * dataset_and.d1.x1 + weight2 * dataset_and.d1.x2) - 1)) / 2)));

                        lostFunc1 = Math.Abs(dataset_and.d1.output - cost_Sigmoid(dataset_and.d1.x1, dataset_and.d1.x2));
                        
                        epoch++;
                        if (lostFunc1 == temp)
                        {

                            break;

                        }

                        temp = lostFunc1;
                        

                        //System.Console.WriteLine(bias);
                        //System.Console.WriteLine(weight1);
                        //System.Console.WriteLine(weight2);
                        //System.Console.WriteLine(epoch);
                        //System.Console.WriteLine(lostFunc1);
                        //System.Console.WriteLine(temp);
                        

                    }
                    Console.WriteLine("1 COMPLETE");
                    while (lostFunc2 > 0.01)
                    {
                        bias = bias + (eta * (
                            (-(1 / Math.Exp((bias + weight1 * dataset_and.d2.x1 + weight2 * dataset_and.d2.x2))
                            * Math.Sin(dataset_and.d2.output + 1 / Math.Exp((bias + weight1 * dataset_and.d2.x1 + weight2 * dataset_and.d2.x2)) - 1)) / 2)));
                        weight1 = weight1 + (eta * (
                             (-(1 / Math.Exp(bias + weight1 * dataset_and.d2.x1 + weight2 * dataset_and.d2.x2) * dataset_and.d2.x1 * Math.Sin(dataset_and.d2.output + 1 / Math.Exp(bias + weight1 * dataset_and.d2.x1 + weight2 * dataset_and.d2.x2) - 1)) / 2)));

                        weight2 = weight2 + (eta * (
                             (-(1 / Math.Exp(bias + weight1 * dataset_and.d2.x1 + weight2 * dataset_and.d2.x2) * dataset_and.d2.x2 * Math.Sin(dataset_and.d2.output + 1 / Math.Exp(bias + weight1 * dataset_and.d2.x1 + weight2 * dataset_and.d2.x2) - 1)) / 2)));

                        lostFunc2 = Math.Abs(dataset_and.d2.output - cost_Sigmoid(dataset_and.d2.x1, dataset_and.d2.x2));
                        epoch++;
                        
                        if (lostFunc2 == temp)
                        {

                            break;

                        }

                        temp = lostFunc2;
                        //System.Console.WriteLine(bias);
                        //System.Console.WriteLine(weight1);
                        //System.Console.WriteLine(weight2);
                        //System.Console.WriteLine(epoch);
                        //System.Console.WriteLine(lostFunc2);

                    }
                    Console.WriteLine("2 COMPLETE");
                    while (lostFunc3 > 0.01)
                    {
                        bias = bias + (eta * (
                            (-(1 / Math.Exp((bias + weight1 * dataset_and.d3.x1 + weight2 * dataset_and.d3.x2))
                            * Math.Sin(dataset_and.d3.output + 1 / Math.Exp((bias + weight1 * dataset_and.d3.x1 + weight2 * dataset_and.d3.x2)) - 1)) / 2)));
                        weight1 = weight1 + (eta * (
                             (-(1 / Math.Exp(bias + weight1 * dataset_and.d3.x1 + weight2 * dataset_and.d3.x2) * dataset_and.d3.x1 * Math.Sin(dataset_and.d3.output + 1 / Math.Exp(bias + weight1 * dataset_and.d3.x1 + weight2 * dataset_and.d3.x2) - 1)) / 2)));

                        weight2 = weight2 + (eta * (
                             (-(1 / Math.Exp(bias + weight1 * dataset_and.d3.x1 + weight2 * dataset_and.d3.x2) * dataset_and.d3.x2 * Math.Sin(dataset_and.d3.output + 1 / Math.Exp(bias + weight1 * dataset_and.d3.x1 + weight2 * dataset_and.d3.x2) - 1)) / 2)));

                        lostFunc3 = Math.Abs(dataset_and.d3.output - cost_Sigmoid(dataset_and.d3.x1, dataset_and.d3.x2));


                        //System.Console.WriteLine(bias);
                        //System.Console.WriteLine(weight1);
                        //System.Console.WriteLine(weight2);
                        //System.Console.WriteLine(epoch);
                        epoch++;

                        if (lostFunc3 == temp)
                        {

                            break;

                        }

                        temp = lostFunc3;

                    }
                    Console.WriteLine("3 COMPLETE");
                    while (lostFunc4 > 0.01)
                    {
                        bias = bias + (eta * (
                            (-(1 / Math.Exp((bias + weight1 * dataset_and.d4.x1 + weight2 * dataset_and.d4.x2))
                            * Math.Sin(dataset_and.d4.output + 1 / Math.Exp((bias + weight1 * dataset_and.d4.x1 + weight2 * dataset_and.d4.x2)) - 1)) / 2)));
                        weight1 = weight1 + (eta * (
                             (-(1 / Math.Exp(bias + weight1 * dataset_and.d4.x1 + weight2 * dataset_and.d4.x2) * dataset_and.d4.x1 * Math.Sin(dataset_and.d4.output + 1 / Math.Exp(bias + weight1 * dataset_and.d4.x1 + weight2 * dataset_and.d4.x2) - 1)) / 2)));

                        weight2 = weight2 + (eta * (
                             (-(1 / Math.Exp(bias + weight1 * dataset_and.d4.x1 + weight2 * dataset_and.d4.x2) * dataset_and.d4.x2 * Math.Sin(dataset_and.d4.output + 1 / Math.Exp(bias + weight1 * dataset_and.d4.x1 + weight2 * dataset_and.d4.x2) - 1)) / 2)));

                        lostFunc4 = Math.Abs(dataset_and.d4.output - cost_Sigmoid(dataset_and.d4.x1, dataset_and.d4.x2));


                        //System.Console.WriteLine(bias);
                        //System.Console.WriteLine(weight1);
                        //System.Console.WriteLine(weight2);
                        //System.Console.WriteLine(epoch);
                        epoch++;

                        if (lostFunc4 == temp)
                        {

                            break;

                        }

                        temp = lostFunc4;

                    }
                    Console.WriteLine("4 COMPLETE");


                    lostFunc = Math.Abs(dataset_and.d1.output - cost_Sigmoid(dataset_and.d1.x1, dataset_and.d1.x2) +
           dataset_and.d2.output - cost_Sigmoid(dataset_and.d2.x1, dataset_and.d2.x2) +
           dataset_and.d3.output - cost_Sigmoid(dataset_and.d3.x1, dataset_and.d3.x2) +
           dataset_and.d4.output - cost_Sigmoid(dataset_and.d4.x1, dataset_and.d4.x2))
           ;
                    epoch++;

                    if (lostFunc == temp0)
                    {

                        break;

                    }

                    temp0 = lostFunc;
                }

                Console.WriteLine("ALL Complete");


                result.bias = bias;
                result.w0 = weight1;
                result.w1 = weight2;

                Console.WriteLine("W0 : {0} | W1 : {1} | W2 : {2} | Error : {3} | Epoch : {4} ", bias, weight1, weight2, lostFunc, epoch);


                return result;
            }

            //public double errorFunc_On-line()
            //{

            //}

            //public double errorFunc_StochasticBatch()
            //{

            //}
        }

    }
}