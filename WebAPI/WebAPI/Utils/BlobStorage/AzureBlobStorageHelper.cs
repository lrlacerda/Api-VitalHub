using Azure.Storage.Blobs;
using System.ComponentModel;

namespace WebAPI.Utils.BlobStorage
{
    public class AzureBlobStorageHelper
    {
        public static async Task<string> UploadImageBlobAsync(IFormFile arquivo, string stringConexao, string nomeContainer)
        {
            try
            {
                //verifica se existe o arquivo
                if (arquivo != null)
                {
                    //retorna a URI com imagem salva
                    //gerar um nome único para a imagem
                    var blobName = Guid.NewGuid().ToString().Replace("-","") + Path.GetExtension(arquivo.FileName);

                    //cria uma instância do BlobServiceCliente passando a string de conexâo com o blob da azure
                    var blobServiceClient = new BlobServiceClient(stringConexao);
                    
                    //obtem dados do container client
                    var blobContainerClient = blobServiceClient.GetBlobContainerClient(nomeContainer);

                    //obtem um blobClient usando o blob name
                    var blobClient = blobContainerClient.GetBlobClient(blobName);

                    //abre o fluxo de entrada do arquivo
                    using (var stream = arquivo.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream, true);
                    }

                    return blobClient.Uri.ToString();
                }
                else
                {
                    //retorna a URI de uma imagem padrão caso nenhuma imagem seja enviada na requisição
                    return "https://blogvitalhub3dm.blob.core.windows.net/blogvitalcontainerlucas/imageProfile.png";
                }
            }
            catch (Exception) 
            {
                throw;
            }
        }
    }
}
