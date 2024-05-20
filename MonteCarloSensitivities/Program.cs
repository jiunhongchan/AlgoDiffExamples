using AlgoDiff;
using MathNet.Numerics.Distributions;

public class Program
{
    //using AlgoDiff to compute the gradient and hessian (1st and 2nd order Greeks) of Black-Scholes Call Price via Monte-Carlo simultion
    static void Main(string[] args)
    {
        var calc = new AlgoDiffCalculator();

        //create Black-Scholes input variables
        var s0 = calc.CreateVariable("Spot", 100);
        var k = calc.CreateVariable("Strike", 80);
        var r = calc.CreateVariable("IR", 0.02);
        var vol = calc.CreateVariable("Vol", 0.4);
        var t = calc.CreateVariable("T", 2);

        //create standard normal parameter
        var standardNormal = calc.CreateParameter("Z");

        //construct the discounted payoff of a call option
        var st = s0 * MathFunc.Exp((r - 0.5 * vol * vol) * t + vol * MathFunc.Sqrt(t) * standardNormal);
        var df = MathFunc.Exp(-r * t);
        var moneyness = st - k;
        var payoff = moneyness * df * MathFunc.HeavisideSmoothed(moneyness, 1.0);
        //note: please refer to https://papers.ssrn.com/sol3/papers.cfm?abstract_id=1626547 for the rationale behind smoothing the payoff.

        //compile. To improve performance, set includeHessian=False if Hessian is not required.
        calc.Compile(includeHessian: true);

        //create a result collector to collect and aggregate simulated payoffs.
        var resultCollector = new ResultCollector(new[] { s0, k, r, vol, t });

        //run Monte-Carlo simulation
        var rng = new Random();
        var normal = new Normal(0, 1);
        var paths = 1000000;
        for (int i = 0; i < paths; ++i)
        {
            var uniform = rng.NextDouble();
            standardNormal.Value = normal.InverseCumulativeDistribution(uniform);

            var results = calc.Evaluate(payoff);
            resultCollector.AddResult(results);
        }

        //get simulated price (i.e. expected payoffs)
        var value = resultCollector.GetAverageValue();

        //get gradient (1st order Greeks) of simulated price wrt [s0, k, r, vol, t],
        //i.e. [dprice/ds, dprice/dk, dprice/dr, dprice/dvol, dprice/dt]
        var gradient = resultCollector.GetAverageGradient();

        //get hessian (2nd order cross Greeks) of simulated price, wrt [s0, k, r, vol, t]
        var hessian = resultCollector.GetAverageHessian();

        // print results
        Console.WriteLine("Value: " + value);
        Console.WriteLine("Gradient: ");
        Console.WriteLine("[" + string.Join(", ", gradient) + "]");
        Console.WriteLine("Hessian:");
        for (int i = 0; i < hessian.Length; i++) { Console.WriteLine("[" + string.Join(", ", hessian[i]) + "]"); }
    }
}