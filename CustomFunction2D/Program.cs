using AlgoDiff;
namespace CustomFunction2D;

public class Program
{
    //a simple example to demostrate user defined 2d operator/function.
    static void Main(string[] args)
    {
        var calc = new AlgoDiffCalculator();

        //create an user defined 2d operator (e.g. f(x, y) = x^2 + y^2 )
        var paraboloidFunc = Factory.Create(new Paraboloid());

        var x = calc.CreateVariable("x");
        var y = calc.CreateVariable("y");
        var z = paraboloidFunc(x, y);

        //compile. To improve performance, set includeHessian=False if Hessian is not required
        calc.Compile(includeHessian: true);

        //evaluate z 
        x.Value = 0.2;
        y.Value = 1.3;
        var result = calc.Evaluate(z);

        //get value, gradient and hessian of z wrt [x, y]
        var value = result.Value;
        var gradient = result.GetGradient(new[] { x, y });
        var hessian = result.GetHessian(new[] { x, y });

        //print results
        Console.WriteLine("Value: " + value);
        Console.WriteLine("Gradient: ");
        Console.WriteLine("[" + string.Join(", ", gradient) + "]");
        Console.WriteLine("Hessian:");
        for (int i = 0; i < hessian.Length; i++) { Console.WriteLine("[" + string.Join(", ", hessian[i]) + "]"); }
    }
}