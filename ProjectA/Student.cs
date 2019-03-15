using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA
{
    class Student
    {
        private int id;
        private string regNo;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string RegNo
        {
            get
            {
                return regNo;
            }

            set
            {
                if (!invalidRegisteration(value))
                {
                    regNo = value;
                }
            }
        }
        
        public bool invalidRegisteration(string registeration)
        {
            bool isInvalid = false;

            if (registeration.Count() != 11 || !registeration.Split('-')[0].All(Char.IsDigit) || registeration[4] != '-' || registeration[7] != '-' ||
                !registeration.Split('-')[1].All(Char.IsLetter) || !registeration.Split('-')[2].All(Char.IsDigit) ||
            !registeration.Split('-')[1].All(Char.IsUpper))
            {
                isInvalid = true;
            }
            return isInvalid;
        }
    }
}
