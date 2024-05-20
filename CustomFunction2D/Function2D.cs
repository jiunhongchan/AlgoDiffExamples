using AlgoDiff;

namespace CustomFunction2D
{
    internal class Paraboloid : IFunction2D
    {
        // y = x_1^2 + x_2^2 
        public Paraboloid()
        {
            Name = "Paraboloid";
        }

        public void ComputeValueAndDerivatives(double x1, double x2)
        {
            y = x1 * x1 + x2 * x2;

            dy_dx1 = 2 * x1;
            dy_dx2 = 2 * x2;

            d2y_dx1dx1 = 2;
            d2y_dx1dx2 = 0;
            d2y_dx2dx2 = 2;
        }

        public string Name { get; private set; }

        public double y { get; private set; }

        public double dy_dx1 { get; private set; }
        public double dy_dx2 { get; private set; }

        public double d2y_dx1dx1 { get; private set; }
        public double d2y_dx1dx2 { get; private set; }
        public double d2y_dx2dx2 { get; private set; }
    }
}
