//Proyecto 1 Lenguajes de programación
//Cliente
open System
open System.IO
open System.Net
open System.Net.Sockets
open System.Text

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

[<EntryPoint>]
let main argv =
    let serverIP = "127.0.0.1" // Cambia a la dirección IP del servidor si es necesario
    let serverPort = 12345 // El puerto del servidor

    printfn "Cliente en ejecución"
    while true do
        printfn "Comandos disponibles:"
        printfn "1. Agregar canción (add|título|artista)"
        printfn "2. Listar canciones (list)"
        printfn "3. Salir"
        printf "> "

        let input = Console.ReadLine()
        match input with
        | "1" ->
            printfn "Ingrese el título de la canción:"
            let title = Console.ReadLine()
            printfn "Ingrese el artista de la canción:"
            let artist = Console.ReadLine()
            let request = sprintf "add|%s|%s" title artist
            sendRequestToServer serverIP serverPort request
        | "2" ->
            let request = "list"
            sendRequestToServer serverIP serverPort request
        | "3" ->
            printfn "Saliendo del cliente."
            Environment.Exit(0)
        | _ ->
            printfn "Comando no válido."

    0