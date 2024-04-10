﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.reports
{
    public interface IIterator<T>
    {
        bool HasNext();
        T Next();

        void Reset();
    }
}