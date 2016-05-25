﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEM_Infrastructure
{
    public static class FileWriter
    {
        public static void Write(IHasDataFile formData)
        {
            using (var fileWriter = new System.IO.StreamWriter(formData.DataFileLocation(), false))
            {
                fileWriter.Write(formData.DataFileString());
            }

            if (!System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                            "\\RFEM_Software"))
                System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.
                                                        LocalApplicationData) + "\\RFEM_Software");


            using (var fileWriter = new System.IO.StreamWriter(formData.AppDataFileLocation, false))
            {
                fileWriter.Write(formData.DataFileString());
            }

        }
    }
}
