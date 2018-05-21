using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Markup;
namespace StockAdminContext
{
    public enum LogStatus
    {
        NoProcesado = 0,
        Procesado =1,
        Confirmado =2,
    }

    public enum LocalStatus
    { 
        Guardado = 0,
        Procesado = 1,
        Pendiente = 2,
    }

    public enum ClientCreditNoteType
    {
        Sale = 0,
        DownPayment = 1
    }

    public enum ProductsFor
    {
        Purchase,
        Sell,
        Inventory,
        SpecialOrders,
    }

    public enum CheckBookStatus
    {
        Ingresado = 0,
        Activo = 1,
        Anulado = 2
    }

    public enum SyncType
    {
        Add =0,
        Update =1,
        Closed =2,
        Ignore =3,
    }

    public enum Operations
    {
        Save, Process
    }

    public enum CanceledCheckBookCategories
    {
        [Description("Documento Dañado")]
        DocumentoDaniado = 0,
        [Description("Mala Administración")]
        MalaAdministracion = 1
    }

    public static class EnumHelper
    {        
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return attribute != null ? attribute.Description : value.ToString();
        }

        public static object GetEnumValue(Type enumType, StockAdminContext.EnumExtension.EnumMember value ){
            return Enum.Parse(enumType, value.Value.ToString());
        }
      
    }

    public class EnumExtension : MarkupExtension
    {
        private readonly Type _enumType;

        public EnumExtension(Type enumType)
        {
            _enumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return (from object enumValue in Enum.GetValues(_enumType)
                    select new EnumMember { Value = enumValue, Description = ((Enum)enumValue).GetDescription() }).ToArray();
        }

        public class EnumMember
        {
            public string Description { get; set; }
            public object Value { get; set; }
        }
    }
}
