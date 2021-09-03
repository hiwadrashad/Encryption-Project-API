﻿using Encryption_Project_LIB.DTOs;
using Encryption_Project_LIB.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_Project_LIB.BLL
{
    public class Converters : IConverter
    {
        public List<Root> ConvertJSON()
        {
            string path = Directory.GetCurrentDirectory() + @"\..\..\..\Repos\Encryption-Project-API\Encryption-Project-API\JSON\AdminData.json";
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                List<Root> values = JsonConvert.DeserializeObject<List<Root>>(json);
                return values;
            }
        }
    }
}
