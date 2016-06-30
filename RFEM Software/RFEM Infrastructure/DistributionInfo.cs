using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace RFEM_Infrastructure
{
    public class DistributionInfo: INotifyPropertyChanged, IDataErrorInfo
    {
        private Distribution _Dist;
        private double? _Mean;
        private double? _StandardDev;
        private double? _LowerBound;
        private double? _UpperBound;
        private double? _Location;
        private double? _Scale;

        private List<ValidationDelegate> _validationDelegates = new List<ValidationDelegate>();

        public delegate string ValidationDelegate(object sender, string propertyName);

        public SoilProperty SoilProp { get; private set; }
        public DistributionType Type
        {
            get { return _Dist.Value; }
            set
            {
                if(_Dist.Value != value)
                {
                    _Dist.Value = value;
                    NotifyPropertyChanged("Mean");
                    NotifyPropertyChanged("StandardDev");
                    NotifyPropertyChanged("LowerBound");
                    NotifyPropertyChanged("UpperBound");
                    NotifyPropertyChanged("Location");
                    NotifyPropertyChanged("Scale");
                }
            }
        }


        public double? Mean
        {
            get { return _Mean; }
            set
            {
                if (_Mean != value)
                {
                    _Mean = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double? StandardDev
        {
            get { return _StandardDev; }
            set
            {
                if (_StandardDev != value)
                {
                    _StandardDev = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double? LowerBound
        {
            get { return _LowerBound; }
            set
            {
                if(_LowerBound != value)
                {
                    _LowerBound = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double? UpperBound
        {
            get { return _UpperBound; }
            set
            {
                if(_UpperBound != value)
                {
                    _UpperBound = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double? Location
        {
            get { return _Location; }
            set
            {
                if(_Location != value)
                {
                    _Location = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double? Scale
        {
            get { return _Scale; }
            set
            {
                if(_Scale != value)
                {
                    _Scale = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public Distribution Dist
        {
            get { return _Dist; }
        }

        public DistributionInfo(SoilProperty property)
        {
            SoilProp = property;
            _Dist = new Distribution(DistributionType.Deterministic);
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }


        public void AddValidationDelegate(ValidationDelegate func)
        {
            _validationDelegates.Add(func);
        }
        public void RemoveValidationDelegate(ValidationDelegate func)
        {
            if (_validationDelegates.Contains(func))
            {
                _validationDelegates.Remove(func);
            }
        }
        public string Validate(string propertyName)
        {
            string validationMessage = null;

            foreach( var func in _validationDelegates)
            {
                validationMessage = func(this, propertyName);
                if (validationMessage != null)
                    return validationMessage;
            }



            return validationMessage;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
