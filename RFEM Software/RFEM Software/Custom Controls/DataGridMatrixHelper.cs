using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEM_Software.Custom_Controls
{
    //I got this code almost directly from http://stackoverflow.com/questions/276808/how-to-populate-a-wpf-grid-based-on-a-2-dimensional-array
    //so i thought I would cite
    public static class DataGridMatrixHelper
    {
        public static DataView GetBindable2DArray<T>(T[,] array)
        {
            DataTable dataTable = new DataTable();
            for (int i = 0; i < array.GetLength(1); i++)
            {
                dataTable.Columns.Add(i.ToString(), typeof(Ref<T>));
            }
            for (int i = 0; i < array.GetLength(0); i++)
            {
                DataRow dataRow = dataTable.NewRow();
                dataTable.Rows.Add(dataRow);
            }
            DataView dataView = new DataView(dataTable);
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j <=i; j++)
                {
                    int a = i;
                    int b = j;
                    Ref<T> refT = new Ref<T>(() => array[a, b], z =>
                    {
                        array[a, b] = z;
                        array[b, a] = z; //Added this to reflect the symmetry of the matrix
                    });
                    dataView[i][j] = refT;
                    dataView[j][i] = refT; //Added this to reflect the symmetry of the matrix
                }
            }
            return dataView;
        }
    }

    public class Ref<T>
    {
        private readonly Func<T> getter;
        private readonly Action<T> setter;
        public Ref(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }
        public T Value { get { return getter(); } set { setter(value); } }
    }
}
