using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arenda
{
    class s_Building
    {
        private int id;
        private string cName;
        private string abbreviation;
        private bool isActive;

        public s_Building()
        {
            cName = "";
            abbreviation = "";
            isActive = false;
        }

        public s_Building(int id, string cName, string abbreviation,bool isActive)
        {
            this.id = id;
            this.cName = cName;
            this.abbreviation = abbreviation;
            this.isActive = isActive;
        }

        public string Cname
        {
            get { return cName; }
            set { cName = value;}
        }

        public string Abbreviation
        {
            get { return abbreviation; }
            set { abbreviation = value; }
        }

        public bool Isactive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public void getBuild (int id)
        {
        
        }
        
    }

    
}
