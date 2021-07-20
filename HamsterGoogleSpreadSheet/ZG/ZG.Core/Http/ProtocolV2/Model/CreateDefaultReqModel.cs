﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterGoogleSpreadSheet.ZG.ZG.Core.Http.ProtocolV2.Req
{
    public class CreateDefaultReqModel : Model
    {
        public string folderID;
        public string fileName;

        public CreateDefaultReqModel(string folderID, string fileName)
        {
            this.instruction = (int)EInstruction.CREATE_DEFAULT_TABLE;
            this.folderID = folderID;
            this.fileName = fileName;
        }
    }
}
