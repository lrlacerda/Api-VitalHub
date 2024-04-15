using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace WebAPI.Utils.OCR
{
    public class OcrService
    {
        private readonly string _subscriptionKey = "c81625c5eb034860bd09004f7074d2e5";

        private readonly string _endpoint = "https://cvvitalhublucas.cognitiveservices.azure.com/";

        //método para reconhecer os caracteres(texto) a partir de uma imagem
        public async Task<string> RecognizeTextAsync(Stream imageStream)
        {
            try
            {
                //criar um cliente para API de computer vision
                var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(_subscriptionKey))
                {
                    Endpoint = _endpoint
                };

                //faz a chamada para a API
                var ocrResult = await client.RecognizePrintedTextInStreamAsync(true, imageStream);

                //retorna o resultado e o texto reconhecido
                return ProcessRecognitionResult(ocrResult);
            }
            catch (Exception ex) 
            {
                return "Erro ao reconhecer o texto" + ex.Message;
            }
        }

        private static string ProcessRecognitionResult(OcrResult result)
        {
            string recognizedText = "";

            //percorre todas as regiões 
            foreach (var region in result.Regions)
            {
                //para cada região percorre as linhas
                foreach (var line in region.Lines)
                {
                    //para caa linha percorre as palavras 
                    foreach (var word in line.Words)
                    {
                        //add cada palavra ao texto separando com espaço
                        recognizedText += word.Text + " ";
                    }
                    
                    //quebra de linha ao final de cada linha
                    recognizedText += "\n";
                }
            }

            //retorna o texto
            return recognizedText;
        }
    }
}
