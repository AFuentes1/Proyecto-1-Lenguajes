namespace ClassLibrary1
open System
open System.IO
open System.Net
open System.Net.Sockets
open System.Text
open System.Net.Http
open System.Net.Http.Headers

module lib =

    let sendRequestToServer (serverIP: string) (serverPort: int) (request: string) =
        try
            let client = new TcpClient(serverIP, serverPort)
            let stream = client.GetStream()
            let writer = new StreamWriter(stream, Encoding.ASCII)
            let reader = new StreamReader(stream, Encoding.UTF8)

            writer.WriteLine(request)
            writer.Flush()

            let response = reader.ReadToEnd() // Obtener la respuesta del servidor

            client.Close()
            
            // Devolver la respuesta como resultado de la función
            response
        with
        | :? SocketException as ex ->
            printfn "Error de conexión al servidor: %s" ex.Message
            // Puedes manejar el error aquí si es necesario
            ""
        | ex ->
            printfn "Error inesperado: %s" ex.Message
            // Puedes manejar el error aquí si es necesario
            ""
    

    let sendRequestWithFileToServer (serverIP: string) (serverPort: int) (request: string) (filePath: string) =
        try
            use httpClient = new HttpClient()
        
            // Construir un objeto MultipartFormDataContent para enviar la solicitud con un archivo adjunto
            let content = new MultipartFormDataContent()
            let fileContent = new ByteArrayContent(File.ReadAllBytes(filePath))
            fileContent.Headers.ContentDisposition <- new ContentDispositionHeaderValue("form-data")
            fileContent.Headers.ContentDisposition.Name <- "\"file\""
            fileContent.Headers.ContentDisposition.FileName <- "\"" + Path.GetFileName(filePath) + "\""
            content.Add(fileContent)

            // Agregar otros datos de la solicitud, como el comando y los detalles de la canción
            content.Add(new StringContent(request), "command")

            // Realizar la solicitud POST al servidor Go
            let response = httpClient.PostAsync($"http://{serverIP}:{serverPort}/endpoint", content).Result
        
            // Leer la respuesta del servidor Go
            let responseBody = response.Content.ReadAsStringAsync().Result
            responseBody
        with
        | ex ->
            printfn "Error inesperado: %s" ex.Message
            // Puedes manejar el error aquí si es necesarios
            ""

