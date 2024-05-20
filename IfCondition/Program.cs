using AlgoDiff;

public class Program
{
    //a simple example to demostrate the use of if condition.
    static void Main(string[] args)
    {
        //create an algo diff calculator
        var calc = new AlgoDiffCalculator();

        //create input variables and conditional parameter
        var x = calc.CreateVariable("x", 4.0);
        var y = calc.CreateVariable("y", 2.1);
        var a = calc.CreateParameter("a");

        //construct conditional output variable
        var trueStatement = () => { return y * MathFunc.Sqrt(x); };
        var falseStatement = () => { return x * x; };
        var z = Condition.If(a > 0, trueStatement, falseStatement);

        //compile. To improve performance, set includeHessian=False if Hessian is not required
        calc.Compile(includeHessian: true);

        //evaluate z at a=1.5 i.e. true condition
        a.Value = 1.5;
        var result = calc.Evaluate(z);

        //get value, gradient and hessian of z wrt [x, y]
        var value = result.Value;
        var gradient = result.GetGradient(new[] { x, y });
        var hessian = result.GetHessian(new[] { x, y });

        //print results
        Console.WriteLine("True Condition");
        Console.WriteLine("Value: " + value);
        Console.WriteLine("Gradient: ");
        Console.WriteLine("[" + string.Join(", ", gradient) + "]");
        Console.WriteLine("Hessian:");
        for (int i = 0; i < hessian.Length; i++) { Console.WriteLine("[" + string.Join(", ", hessian[i]) + "]"); }
        Console.WriteLine();

        //evaluate z at a= 0.5 i.e. false condition 
        a.Value = -0.5;
        result = calc.Evaluate(z);

        //get value, gradient and hessian of z wrt [x, y]
        value = result.Value;
        gradient = result.GetGradient(new[] { x, y });
        hessian = result.GetHessian(new[] { x, y });

        //print results
        Console.WriteLine("False Condition");
        Console.WriteLine("Value: " + value);
        Console.WriteLine("Gradient: ");
        Console.WriteLine("[" + string.Join(", ", gradient) + "]");
        Console.WriteLine("Hessian:");
        for (int i = 0; i < hessian.Length; i++) { Console.WriteLine("[" + string.Join(", ", hessian[i]) + "]"); }
    }
}