using PdfSharp.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.WebApp.Helpers
{
    public class PdfSharpFontResolver : IFontResolver
    {
        // Mapeia o nome da família da fonte para o nome do arquivo .ttf
        // Adicione todas as fontes que seu projeto pode precisar aqui.
        public string DefaultFontName => "Arial";

        public byte[] GetFont(string faceName)
        {
            // O faceName é o que retornamos no método ResolveTypeface
            var fontPath = Path.Combine(Directory.GetCurrentDirectory(), "Fonts", faceName);

            if (File.Exists(fontPath))
            {
                return File.ReadAllBytes(fontPath);
            }

            // Se a fonte específica não for encontrada, retorna a padrão.
            // Isso evita que a aplicação quebre se uma fonte rara for solicitada.
            var defaultFontPath = Path.Combine(Directory.GetCurrentDirectory(), "Fonts", "arial.ttf");
            return File.ReadAllBytes(defaultFontPath);
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            // Lógica para determinar qual arquivo de fonte usar baseado no nome e estilo
            var normalizedFamilyName = familyName.ToLowerInvariant().Trim();

            switch (normalizedFamilyName)
            {
                case "arial":
                    if (isBold && isItalic) return new FontResolverInfo("arialbi.ttf"); // Supondo que você tenha o arquivo
                    if (isBold) return new FontResolverInfo("arialbd.ttf");
                    if (isItalic) return new FontResolverInfo("ariali.ttf"); // Supondo que você tenha o arquivo
                    return new FontResolverInfo("arial.ttf");

                case "courier new":
                    if (isBold && isItalic) return new FontResolverInfo("courbi.ttf"); // Supondo que você tenha o arquivo
                    if (isBold) return new FontResolverInfo("courbd.ttf");
                    if (isItalic) return new FontResolverInfo("couri.ttf"); // Supondo que você tenha o arquivo
                    return new FontResolverInfo("cour.ttf");

                case "times new roman":
                    if (isBold && isItalic) return new FontResolverInfo("timesbi.ttf");
                    if (isBold) return new FontResolverInfo("timesbd.ttf");
                    if (isItalic) return new FontResolverInfo("timesi.ttf");
                    return new FontResolverInfo("times.ttf");

                // Caso não encontre a família, retorna a fonte padrão (Arial)
                default:
                    return new FontResolverInfo("arial.ttf");
            }
        }
    }
}
