using AlgoDiff;

public class Program
{
    //a simple example to demostrate the usage of AlgoDiff package
    static void Main(string[] args)
    {
        //create an algo diff calculator
        var calc = new AlgoDiffCalculator();

        //create input variables i.e. we want to compute derivatives w.r.t these variables.
        var x = calc.CreateVariable("x");
        var y = calc.CreateVariable("y");

        //create parameter 
        var a = calc.CreateParameter("a");

        //construct output variable 
        var z = a * MathFunc.Exp(-2.0 * x * (x + y));

        //compile. To improve performance, set includeHessian=False if Hessian is not required
        calc.Compile(includeHessian: true);

        //evaluate z at x = 0.2, y = 0.3, a = 1.5
        x.Value = 0.2; y.Value = 0.3; a.Value = 1.5;
        var result = calc.Evaluate(z);

        //get value of z
        var value = result.Value;

        //get gradient of z wrt [x, y] i.e. [dz/dx, dz/dy]
        var gradient = result.GetGradient(new[] { x, y });

        //get hessian of z wrt [x, y] , i.e.
        // [[d^2z/dx^2, d^2z/dxdy],
        //  [d^2z/dydx, d^2z/dy^2]]
        var hessian = result.GetHessian(new[] { x, y });

        //print results
        Console.WriteLine("Value: " + value);
        Console.WriteLine("Gradient: ");
        Console.WriteLine("[" + string.Join(", ", gradient) + "]");
        Console.WriteLine("Hessian:");
        for (int i = 0; i < hessian.Length; i++) { Console.WriteLine("[" + string.Join(", ", hessian[i]) + "]"); }
    }
}
