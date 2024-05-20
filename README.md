# Project Summary

This repository contains example applications for the AlgoDiff NuGet package.

# A Simple Example
```c#
using AlgoDiff;

public class Program
{
     static void Main(string[] args)
      {
          var calc = new AlgoDiffCalculator();

          //create input variables
          var x = calc.CreateVariable("x");
          var y = calc.CreateVariable("y");

          //define output variable 
          var z = MathFunc.Exp(-2.0 * x * (x + y));

          //compile
          calc.Compile(includeHessian: true);

          //evaluate z at x = 0.2, y = 0.3
          x.Value = 0.2;
          y.Value = 0.3;
          var result = calc.Evaluate(z);

          //get value, gradient and hessian of z wrt [x, y] 
          var value = result.Value;
          var gradient = result.GetGradient(new[] { x, y });
          var hessian = result.GetHessian(new[] { x, y });

          //print results
          Console.WriteLine("z = {0}", value);
          Console.WriteLine("[dz/dx, dz/dy] = [{0}, {1}]", gradient[0], gradient[1]);
          Console.WriteLine("[d^2z/dxdx, d^2z/dxdy] = [{0}, {1}]", hessian[0][0], hessian[0][1]);
          Console.WriteLine("[d^2z/dydx, d^2z/dydy] = [{0}, {1}]", hessian[1][0], hessian[1][1]);

      }
}
```
