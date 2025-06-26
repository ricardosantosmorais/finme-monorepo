using Finme.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Domain.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Retorna o valor do atributo [StringValue] de um membro do enum.
        /// Se não encontrar, retorna o nome do membro do enum.
        /// </summary>
        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Tenta obter o atributo StringValueAttribute
            if (fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) is StringValueAttribute[] attrs && attrs.Length > 0)
            {
                return attrs[0].Value;
            }

            // Se não encontrar o atributo, retorna o nome do próprio membro do enum
            return value.ToString();
        }
    }
}
