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
            let reader = new StreamReader(stream, Encoding.ASCII)

            writer.WriteLine(request)
            writer.Flush()

            let response = reader.ReadToEnd()
            printfn "Respuesta del servidor:\n%s" response

            client.Close()
        with
        | :? SocketException as ex ->
            printfn "Error de conexión al servidor: %s" ex.Message
        | ex ->
            printfn "Error inesperado: %s" ex.Message
  
    let rec invertir lista =
        match lista with
        | [] -> []
        | cabeza::cola -> (invertir cola) @ [cabeza]

    let invertirString lista =
        let listaInvertida = invertir lista
        let resultado = System.String( listaInvertida |> List.toArray)
        resultado
