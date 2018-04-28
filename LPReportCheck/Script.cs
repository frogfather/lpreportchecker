using System;
using System.Collections.Generic;
using System.Collections;

namespace LPReportCheck
{
    public class Script
    {
        public Script(string scriptName, int facilityId)

        {

            Name = scriptName;

            FacId = facilityId;

            _dashSuccess = new List<string>();

            _dashFail = new List<string>();

            _emailSuccess = new List<string>();

            _emailFail = new Dictionary<string, string>();

        }

        public string Name { get; set; }

        public int FacId { get; set; }

        public string ScriptStart { get; set; }

        public string ScriptEnd { get; set; }

        public int RecCount 
        { 
            get
            {
                return _recCount;   
            } 
            set
            {
               if(_recCount != value)
                {
                    ValueChangedEventArgs args = new ValueChangedEventArgs
                    {
                        OldValue = _recCount.ToString(),
                        NewValue = value.ToString()
                    };
                    if (ValueChanged != null)
                    {
                        ValueChanged(this, args);
                    }
                    _recCount = value;
                }
            } 
        }

        public int SuccessCount 
        { 
            get
            {
                return _successCount;
            }
            set
            {
                if (_successCount != value)
                {
                    ValueChangedEventArgs args = new ValueChangedEventArgs
                    {
                        OldValue = _successCount.ToString(),
                        NewValue = value.ToString()
                    };

                    ValueChanged?.Invoke(this, args);
                    _successCount = value;

                }
            }
        }

        public int FailCount 
        { 
            get
            {
                return _failCount;        
            }
            set
            {
                if (_failCount != value)
                {
                    ValueChangedEventArgs args = new ValueChangedEventArgs
                    {
                        OldValue = _failCount.ToString(),
                        NewValue = value.ToString()
                    };
                    ValueChanged?.Invoke(this, args);
                    _failCount = value;

                }
            }
        }



        public void AddEmailSuccess(string serviceCode)

        {            
            ValueChangedEventArgs args = new ValueChangedEventArgs
            {
                OldValue = _emailSuccess.Count.ToString(),
                NewValue = (_emailSuccess.Count+1).ToString()
            };
            ValueChanged?.Invoke(this, args);
            _emailSuccess.Add(serviceCode);
        }

        public string GetEmailSuccess(int index)

        {
            if (index < 0 || index >= _emailSuccess.Count) { return ""; }

            return _emailSuccess[index];
        }

        public void AddEmailFail(string serviceCode, string errorMsg)
        {
            ValueChangedEventArgs args = new ValueChangedEventArgs
            {
                OldValue = _emailFail.Count.ToString(),
                NewValue = (_emailFail.Count + 1).ToString()
            };
            ValueChanged?.Invoke(this, args);

            _emailFail[serviceCode] = errorMsg;
            //add delegate
        }

        public string GetEmailFail(string serviceCode)
        {
            return _emailFail[serviceCode];
        }

        public void AddDashSuccess(string serviceCode)
        {
            ValueChangedEventArgs args = new ValueChangedEventArgs
            {
                OldValue = _dashSuccess.Count.ToString(),
                NewValue = (_dashSuccess.Count + 1).ToString()
            };
            ValueChanged?.Invoke(this, args);

            _dashSuccess.Add(serviceCode);
            //add delegate
        }

        public string GetDashSuccess(int index)
        {
            if (index < 0 || index >= _dashSuccess.Count) { return ""; }
            return _dashSuccess[index];
        }

        public void AddDashFail(string serviceCode)
        {
            ValueChangedEventArgs args = new ValueChangedEventArgs
            {
                OldValue = _dashFail.Count.ToString(),
                NewValue = (_dashFail.Count + 1).ToString()
            };
            ValueChanged?.Invoke(this, args);

            _dashFail.Add(serviceCode);
            //add delegate
        }

        public string GetDashFail(int index)
        {
            if (index < 0 || index >= _dashFail.Count) { return ""; }
            return _dashFail[index];
        }

        public event ValueChangedDelegate ValueChanged;
        int _recCount;
        int _successCount;
        int _failCount;
        List<string> _dashSuccess;
        List<string> _dashFail;
        List<string> _emailSuccess;
        Dictionary<string, string> _emailFail;
    }
}
