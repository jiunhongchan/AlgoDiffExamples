using AlgoDiff;

namespace CustomFunction1D
{
    internal class QuadraticPolynomial : IFunction1D
    {
        // y = a*x^2 + b*x + c
        public QuadraticPolynomial(double a, double b, double c)
        {
            Name = "QuadPoly";
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public void ComputeValueAndDerivatives(double x)
        {
            y = a * x * x + b * x + c;
            dy_dx = 2 * a * x + b;
            d2y_dxdx = 2 * a;
        }

        public string Name { get; private set; }

        public double y { get; private set; }

        public double dy_dx { get; private set; }

        public double d2y_dxdx { get; private set; }

        private double a;
        private double b;
        private double c;
    }
}

