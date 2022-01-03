using HomeJok.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeJok.Services
{
    public class BaseService : IBaseService
    {
        public bool PublicFirst()
        {
            return true;
        }
    }
}
