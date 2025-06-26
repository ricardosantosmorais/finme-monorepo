using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Finme.Application.Admin.Helpers
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string phrase)
        {
            if (string.IsNullOrEmpty(phrase))
            {
                return string.Empty;
            }

            // 1. Normaliza para remover acentos e caracteres especiais
            string str = RemoveAccents(phrase).ToLower();

            // 2. Remove caracteres inválidos que não sejam letras, números, espaços ou hífens
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

            // 3. Substitui espaços por hífens
            str = Regex.Replace(str, @"\s+", "-").Trim();

            // 4. Garante que não haja múltiplos hífens em sequência
            str = Regex.Replace(str, @"-{2,}", "-");

            // 5. Remove hífens do início ou do fim
            str = str.Trim('-');

            return str;
        }

        private static string RemoveAccents(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
