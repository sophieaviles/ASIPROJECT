using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigiApi.Model
{
    public partial class OITM_Articles : BaseModel
    {
        private bool _isTemplate;

        public bool IsTemplate
        {
            get { return _isTemplate ; }
            set
            {

                _isTemplate = value;
                TemplateL = _isTemplate ? "Y" : "N";
                RaisePropertyChanged("IsTemplate");
            }
        }
    }
}
