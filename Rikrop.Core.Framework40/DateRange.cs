using System;
using System.Runtime.Serialization;

namespace Rikrop.Core.Framework
{
    [DataContract(IsReference = true)]
    public class DateRange
    {
        [DataMember]
        private DateTime? _start; 

        [DataMember]
        private DateTime? _end;

        public DateTime? Start
        {
            get { return _start; }
        }

        public DateTime? End
        {
            get { return _end; }
        }

        public DateRange(DateTime? start, DateTime? end)
        {
            _start = start;
            _end = end;
        }

        public override string ToString()
        {
            return string.Format("From: {0}, To: {1}", Start.ConvertTo<DateTime, string>(a => a.ToString(), "null"), End.ConvertTo<DateTime, string>(a => a.ToString(), "null"));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((DateRange) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_start.GetHashCode()*397) ^ _end.GetHashCode();
            }
        }

        public DateRange RoundToDaysInterval()
        {
            var start = Start.ToDate();
            var end = End.ConvertTo(a => a.Date.AddDays(1));

            return new DateRange(start, end);
        }

        public DateRange NormalizeRange()
        {
            var min = Start.ToDate();
            var max = End.ToDate();

            if (min.HasValue && max.HasValue)
            {
                if (max < min)
                {
                    var t = max;
                    max = min;
                    min = t;
                }
            }
            return new DateRange(min, max);
        }

        public DateRange ToDates()
        {
            var start = Start.ToDate();
            var end = End.ToDate();

            return new DateRange(start, end);
        }

        protected bool Equals(DateRange other)
        {
            return _start.Equals(other._start) && _end.Equals(other._end);
        }

        public static bool operator ==(DateRange left, DateRange right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DateRange left, DateRange right)
        {
            return !Equals(left, right);
        }
    }
}