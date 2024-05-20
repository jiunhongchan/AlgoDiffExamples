using AlgoDiff;

public class Program
{
    //using AlgoDiff to compute the gradient and hessian (1st and 2nd order Greeks) of Black-Scholes Call Price.
    static void Main(string[] args)
    {
        var calc = new AlgoDiffCalculator();

        //create Black-Scholes input variables
        var s = calc.CreateVariable("Spot");
        var k = calc.CreateVariable("Strike");
        var r = calc.CreateVariable("IR");
        var vol = calc.CreateVariable("Vol");
        var t = calc.CreateVariable("T");

        //Black-Scholes price formula for call option
        var d1 = (MathFunc.Log(s / k) + (r + 0.5 * vol * vol) * t) / (vol * MathFunc.Sqrt(t));
        var d2 = d1 - vol * MathFunc.Sqrt(t);
        var price = s * MathFunc.NormalCDF(d1) - k * MathFunc.Exp(-r * t) * MathFunc.NormalCDF(d2);

        //compile. To improve performance, set includeHessian=False if Hessian is not required
        calc.Compile(includeHessian: true);

        //evaluate price
        s.Value = 100; k.Value = 80; r.Value = 0.02; vol.Value = 0.4; t.Value = 2;
        var result = calc.Evaluate(price);

        //get value of price
        var value = result.Value;

        //get gradient (1st order Greeks) of price wrt [s, k, r, vol, t],
        //i.e. [dprice/ds, dprice/dk, dprice/dr, dprice/dvol, dprice/dt]
        var gradient = result.GetGradient(new[] { s, k, r, vol, t });

        //get hessian (2nd order cross Greeks) of price wrt [s, k, r, vol, t]
        var hessian = result.GetHessian(new[] { s, k, r, vol, t });

        //print results
        Console.WriteLine("Value: " + value);
        Console.WriteLine("Gradient: ");
        Console.WriteLine("[" + string.Join(", ", gradient) + "]");
        Console.WriteLine("Hessian:");
        for (int i = 0; i < hessian.Length; i++) { Console.WriteLine("[" + string.Join(", ", hessian[i]) + "]"); }
    }
}