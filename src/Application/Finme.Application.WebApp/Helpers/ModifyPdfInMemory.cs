using Finme.Domain.Entities;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Finme.Application.WebApp.Helpers
{
    public static class ModifyPdfInMemory
    {
        public static MemoryStream Execute(MemoryStream inputStream, Operation operation)
        {
            // Carrega o documento PDF a partir do stream de entrada
            PdfDocument pdfDocument = PdfReader.Open(inputStream, PdfDocumentOpenMode.Modify);

            // Pega a primeira página para desenhar
            PdfPage firstPage = pdfDocument.Pages[0];

            // Cria o "pincel" para desenhar na página
            using (XGraphics gfx = XGraphics.FromPdfPage(firstPage))
            {
                // Define as fontes e cores que você quer usar.
                // O FontResolver configurado no Program.cs será usado aqui.
                XFont fontNormal = new XFont("Arial", 10, XFontStyleEx.Regular);
                XFont fontBold = new XFont("Arial", 10, XFontStyleEx.Bold);
                XBrush brush = XBrushes.Black;

                // "Carimbando" as informações nas coordenadas X, Y que você encontrou.
                // A coordenada (0, 0) é o canto SUPERIOR esquerdo.

                // Exemplo: Nome do usuário a 150px da esquerda e 212px do topo
                gfx.DrawString(operation.Name, fontBold, brush, new XPoint(150, 212));

                // Exemplo: Valor do investimento
                gfx.DrawString(
                    operation.Name,
                    fontBold,
                    brush,
                    new XPoint(150, 245)
                );

                // Exemplo: Dados do investidor
                //var dados = $"CPF: {operation.User.Document} | Endereço: {operation.User.Address}, {operation.User.City} - {operation.User.State}";
                gfx.DrawString(operation.Name, fontBold, brush, new XPoint(150, 278));
            }

            // Salva o documento modificado em um NOVO MemoryStream de saída
            var outputStream = new MemoryStream();
            pdfDocument.Save(outputStream);

            // PDFsharp não fecha o stream ao salvar, mas a posição fica no final.
            // É crucial resetar para o início antes de retornar.
            outputStream.Position = 0;

            return outputStream;
        }
    }
}
