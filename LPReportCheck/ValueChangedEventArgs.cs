﻿using System;
namespace LPReportCheck
{
    public class ValueChangedEventArgs: EventArgs
    {
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
