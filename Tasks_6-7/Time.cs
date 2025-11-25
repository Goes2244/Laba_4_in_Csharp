using System;

namespace lab4_2
{
    public class Time
    {
        private byte _hours;
        private byte _minutes;
        
        public byte Hours 
        { 
            get => _hours;
            private set
            {
                if (value >= 24)
                    throw new ArgumentException("Часы должны быть в диапазоне 0-23");
                _hours = value;
            }
        }

        public byte Minutes 
        { 
            get => _minutes;
            private set
            {
                if (value >= 60)
                    throw new ArgumentException("Минуты должны быть в диапазоне 0-59");
                _minutes = value;
            }
        }
        
        public Time(byte hours, byte minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }

        public Time() : this(0, 0) { }

        // ЗАДАНИЕ 6.7 - Метод вычитания
        public Time Subtract(Time other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            int totalMinutes = (Hours * 60 + Minutes) - (other.Hours * 60 + other.Minutes);
            
            if (totalMinutes < 0)
                totalMinutes += 24 * 60;
            
            return new Time((byte)(totalMinutes / 60), (byte)(totalMinutes % 60));
        }

        // ЗАДАНИЕ 7.7 - Перегрузка операторов
        
        public static Time operator ++(Time t)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            
            Time original = new Time(t.Hours, t.Minutes);
            
            int totalMinutes = (t.Hours * 60 + t.Minutes) + 1;
            totalMinutes %= (24 * 60);
    
            t.Hours = (byte)(totalMinutes / 60);
            t.Minutes = (byte)(totalMinutes % 60);
            
            return original;
        }

        public static Time operator --(Time t)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            
            Time original = new Time(t.Hours, t.Minutes);
            
            int totalMinutes = (t.Hours * 60 + t.Minutes) - 1;
            if (totalMinutes < 0)
                totalMinutes += 24 * 60;
    
            t.Hours = (byte)(totalMinutes / 60);
            t.Minutes = (byte)(totalMinutes % 60);
            
            return original;
        }
        
        public static explicit operator bool(Time t)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            return t.Hours != 0 || t.Minutes != 0;
        }

        public static implicit operator int(Time t)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            return t.Hours * 60 + t.Minutes;
        }
        
        public static bool operator <(Time t1, Time t2)
        {
            if (t1 == null || t2 == null) 
                throw new ArgumentNullException();
            return (int)t1 < (int)t2;
        }

        public static bool operator >(Time t1, Time t2)
        {
            if (t1 == null || t2 == null) 
                throw new ArgumentNullException();
            return (int)t1 > (int)t2;
        }
        
        public static Time operator +(Time t, uint minutes)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            
            int totalMinutes = (int)t + (int)minutes;
            totalMinutes %= (24 * 60);
            if (totalMinutes < 0) totalMinutes += 24 * 60;
            
            return new Time((byte)(totalMinutes / 60), (byte)(totalMinutes % 60));
        }

        public static Time operator -(Time t, uint minutes)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            
            int totalMinutes = (int)t - (int)minutes;
            totalMinutes %= (24 * 60);
            if (totalMinutes < 0) totalMinutes += 24 * 60;
            
            return new Time((byte)(totalMinutes / 60), (byte)(totalMinutes % 60));
        }

        public static Time operator +(Time t1, Time t2)
        {
            if (t1 == null || t2 == null) 
                throw new ArgumentNullException();
            return t1 + (uint)((int)t2);
        }

        public static Time operator -(Time t1, Time t2)
        {
            if (t1 == null || t2 == null) 
                throw new ArgumentNullException();
            return t1 - (uint)((int)t2);
        }
        
        public override string ToString()
        {
            return $"{Hours:D2}:{Minutes:D2}";
        }
        
        public override bool Equals(object obj)
        {
            return obj is Time time &&
                   Hours == time.Hours &&
                   Minutes == time.Minutes;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Hours, Minutes);
        }
    }
}