﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Harjoitustyo_raha
{
    
    public interface ITallettava
    {
        void TalletaRahaa(double maara);
    }
}
