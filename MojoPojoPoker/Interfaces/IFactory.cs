using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MojoPojoPoker.CLI
{
    public interface IFactory<T>
    {
        object GetInstance();
    }
}
