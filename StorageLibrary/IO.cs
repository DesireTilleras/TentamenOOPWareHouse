using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StorageLibrary
{
    [Serializable]
    public class IOBinary
    {       

        public void BinarySerialize(object data, string filePath)
        {
            FileStream fileStream;
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(filePath)) File.Delete(filePath);
            fileStream = File.Create(filePath);
            bf.Serialize(fileStream, data);
            fileStream.Flush();
            fileStream.Close();
        }

        public WareHouse BinaryDeserialize(string filePath)
        {
            WareHouse obj = null;
            FileStream fileStream;
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(filePath))
            {
                fileStream = File.OpenRead(filePath);
                obj = bf.Deserialize(fileStream) as WareHouse;
                fileStream.Flush();
                fileStream.Close();
            }
            return obj;
        }

       







    }
}
