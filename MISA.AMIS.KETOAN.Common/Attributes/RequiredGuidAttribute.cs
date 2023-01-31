using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KETOAN.Common
{
    public  class RequiredGuidAttribute : RequiredAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }
            else if(Guid.Parse(value.ToString()) == Guid.Empty)
            {
                return false;
            }

            return true;
        }
    }
}
