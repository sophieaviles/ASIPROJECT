using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigiApi.Model
{
    public partial class OWHS_Branch : BaseModel
    {
        public string WhsTitle
        {
            get
            {
                return string.Format("{0} {1}", WhsCode, WhsName);
            }
        }
            
    }
}
