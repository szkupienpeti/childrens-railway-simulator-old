using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Gyermekvasút
{
    public class AzonosithatoObj
    {
        public AzonosithatoObj()
        {
            Id = idCount++;
        }

        static int idCount = 0;

        public int Id { get; set; }

        private List<string> methods = new List<string>();
        public List<string> Methods
        {
            get
            {
                methods.Clear();
                for (int i = 0; i < this.GetType().GetMethods().Length; i++)
                {
                    methods.Add(this.GetType().GetMethods()[i].Name);
                }
                return methods;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is AzonosithatoObj)
            {
                if ((obj as AzonosithatoObj).Id == Id)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
