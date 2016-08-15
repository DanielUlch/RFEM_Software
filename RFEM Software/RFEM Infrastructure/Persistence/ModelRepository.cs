using RFEMSoftware.Simulation.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Infrastructure.Persistence
{
    public static class ModelRepository
    {
        public static void Store(ISimModel model)
        {
            FileWriter.Write(model);
        }
        public static void Store(ISimModel model, string filePath)
        {
            FileWriter.Write(model, filePath);
        }
        public static ISimModel Retrieve(string filePath, Program type)
        {
            return FileReader.Read(type, filePath);
        }
        //static string GetContainingFolder(ISimModel model)
        //{

        //}
    }
}
