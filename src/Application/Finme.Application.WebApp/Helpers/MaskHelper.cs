using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.WebApp.Helpers
{
    public static class MaskHelper
    {
        public static string MaskContact(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Verifica se é um e-mail
            if (input.Contains("@"))
            {
                return MaskEmail(input);
            }
            // Presume que é um telefone
            else if (input.All(char.IsDigit))
            {
                return MaskPhone(input);
            }

            return input; // Retorna original se não for nem e-mail nem telefone
        }

        private static string MaskEmail(string email)
        {
            string[] parts = email.Split('@');
            if (parts.Length != 2)
                return email;

            string username = parts[0];
            string domain = parts[1];

            if (username.Length <= 6)
                return $"{username.Substring(0, 2)}****@{domain}";

            return $"{username.Substring(0, 6)}****@{domain}";
        }

        private static string MaskPhone(string phone)
        {
            if (phone.Length <= 6)
                return phone;

            int visibleStart = 5;  // 55119 (DDI + DDD)
            int visibleEnd = 2;    // últimos 2 dígitos
            int totalVisible = visibleStart + visibleEnd;

            if (phone.Length <= totalVisible)
                return phone;

            string start = phone.Substring(0, visibleStart);
            string end = phone.Substring(phone.Length - visibleEnd);
            int maskedLength = phone.Length - totalVisible;

            return $"{start}{new string('*', maskedLength)}{end}";
        }
    }
}
