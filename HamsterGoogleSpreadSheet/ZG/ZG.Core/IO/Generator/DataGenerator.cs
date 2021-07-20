﻿
using HamsterGoogleSpreadSheet.ZG.ZG.Core.Http.ProtocolV2.Res;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamster.ZG.IO.Generator
{
    public class DataGenerator : ICodeGenerator
    {
        ReadSpreadSheetResult info ;
        public DataGenerator(ReadSpreadSheetResult info)
        {
             this.info = info;
        }
        /// <summary>
        /// Vertical Data To Horizontal 
        /// </summary>
        /// <returns></returns>
        public string Generate()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(info); 
        }
    }
}
