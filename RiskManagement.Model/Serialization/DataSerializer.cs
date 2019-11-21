using System;
using System.IO;
using System.Runtime.Serialization;

namespace RiskManagement.Model.Serialization
{
    public class DataSerializer
    {
        public static void SerializeData<DataType>(string fileName, DataType data)
        {
            FileStream fStream = null;
            try
            {
                var formatter = new DataContractSerializer(typeof(DataType));
                fStream = new FileStream(fileName, FileMode.Create);
                formatter.WriteObject(fStream, data);
                fStream.Close();
            }
            finally
            {
                fStream.Close();
            }
        }

        public static DataType DeserializeData<DataType>(string fileName)
        {
            try
            {
                var fStream = new FileStream(fileName, FileMode.Open);
                var formatter = new DataContractSerializer(typeof(DataType));
                var result = (DataType)formatter.ReadObject(fStream);
                fStream.Close();
                return result;
            }
            catch (Exception)
            {
                throw new Exception("Error while loading \"recent.dat\" file");
            }
        }
    }
}
