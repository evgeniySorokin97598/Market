using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.ConfigLib.Interfaces
{
    public interface IConfigLoader
    {
        public object Config { set; }
        public object Load<T>();
        public void Save();
    }
}
