using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;

namespace SigiApi.Model
{
    public static class ContextFactory
    {
        private static SigiEntities Context
        {
            get; set;
        }

        public static SigiEntities GetDBContext()
        {
            return Context ?? (Context = new SigiEntities());
        }
    }
}
