﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkSourceSimulator;
using OOD_24L_01180689.src.dto.reports.reporters;
using OOD_24L_01180689.src.logging;

namespace OOD_24L_01180689.src.observers
{
    public interface IObserver
    {
        void Update(IDUpdateArgs args);
        void Update(PositionUpdateArgs args);
        void Update(ContactInfoUpdateArgs args);

    }
}
