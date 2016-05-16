using RFEM_Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RFEM_Software
{
    public static class FormBuilder
    {
        public static UserControl Build(IHasDataFile formData, Program type)
        {
            switch (type)
            {
                case Program.RBear2D:
                    return new Rbear2dForm((RBear2D)formData);
                default:
                    throw new NotImplementedException();
            }

        }
    }
}
