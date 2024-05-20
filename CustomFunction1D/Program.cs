using AlgoDiff;
using CustomFunction1D;

public class Program
{
    //a simple example to demostrate user defined 1d operator/function.
    static void Main(string[] args)
    {
        var calc = new AlgoDiffCalculator();

        //create an user defined 1d operator (e.g. f(x) = 2x^2 + 3x + 5 )
        var quadPolyFunc = Factory.Create(new QuadraticPolynomial(2, 3, 5));

        var x = calc.CreateVariable("x");
        var y = quadPolyFunc(x);
        var z = MathFunc.Exp(y);

        //compile. To improve performance, set includeHessian=False if Hessian is not required
        calc.Compile(includeHessian: true);

        //evaluate z 
        x.Value = 0.2;
        var result = calc.Evaluate(z);

        //get value, gradient and hessian of z
        var value = result.Value;
        var gradient = result.GetGradient(new[] { x });
        var hessian = result.GetHessian(new[] { x });

        //print results
        Console.WriteLine("Value: " + value);
        Console.WriteLine("Gradient: ");
        Console.WriteLine("[" + string.Join(", ", gradient) + "]");
        Console.WriteLine("Hessian:");
        for (int i = 0; i < hessian.Length; i++) { Console.WriteLine("[" + string.Join(", ", hessian[i]) + "]"); }
    }
}
