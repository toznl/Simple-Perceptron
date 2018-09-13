using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Perceptron
{
    using System.Collections.Generic;

    using OxyPlot;




    public class MainViewModel
    {
        public MainViewModel()
        {
            this.Title = "Example 2";
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

    public class SinglePerceptron
    {
        double bias = 1;
        double weight1 = 0;
        double weight2 = 0;
        double eta = 0.001;

        public double net(double w0, double w1, double w2, double x1, double x2)
        {
            double result;

            result = x1 * w1 + x2 * w2 + w0;

            return result;
        }

        public double activation_linear(double x)
        {
            double result;

            result = x;

            return result;
        }

        public double cost_Sigmoid(double x1, double x2)
        {
            double result;

            result = 1 / (1 + (Math.Exp(net(bias, weight1, weight2, x1, x2))));

            return result;

        }

        public double errorFunc_Batch()
        {
            double result;
            Dataset_And dataset_and = new Dataset_And(1);

            result = dataset_and.d1.output-;

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