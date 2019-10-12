using KnightMovement.Interfaces;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace KnightMovement.Models
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IDeskBehavior>().To<ChessDeskModel>();
            Bind<IKnightBehavior>().To<KnightFigureModel>();
        }
    }
}
