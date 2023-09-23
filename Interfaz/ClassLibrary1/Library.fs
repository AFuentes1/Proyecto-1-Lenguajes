namespace ClassLibrary1
open System
open System.IO
open System.Net
open System.Net.Sockets
open System.Text

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
