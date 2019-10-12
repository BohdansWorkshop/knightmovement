using KnightMovement.Interfaces;
using KnightMovement.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

class Program
{

    static void Main()
    {
        var kernel = ConfigureDependencies();
        var knightModel =kernel.Get<IKnightBehavior>();


        Console.WriteLine("Write coordinates for knight");
        var knight = Console.ReadLine();

        Console.WriteLine("Write count of figures");
        var figuresCount = int.Parse(Console.ReadLine());

        Console.WriteLine("Write figure settings : color position \n0 - black, 1 - white \nfor example 1 b6 - white figure on b6 square position ");
        string[] figuresConfiguration = new string[figuresCount];
        for(int i = 0; i <figuresCount; i++)
        {
            figuresConfiguration[i] = Console.ReadLine();
        }

        var result = knightModel.CaptureFigures($"1 {knight}", figuresConfiguration);

        foreach (var item in result)
        {
            Console.WriteLine(item);
        }
    }

    public static IKernel ConfigureDependencies()
    {
        IKernel kernel = new StandardKernel();
        kernel.Load(Assembly.GetExecutingAssembly());
        return kernel;
    }
}
