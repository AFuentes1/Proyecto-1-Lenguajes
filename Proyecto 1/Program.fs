﻿//Proyecto 1 Lenguajes de programación
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
        let reader = new StreamReader(stream, Encoding.UTF8)

        writer.WriteLine(request)
        writer.Flush()

        if request = "list" then
            // Si es una solicitud de lista, recibir la lista del servidor
            let response = reader.ReadToEnd()
            printfn "Lista de canciones:\n%s" response
        else
            // Si no es una solicitud de lista, mostrar la respuesta del servidor
            let response = reader.ReadLine()
            printfn "Respuesta del servidor: %s" response

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
        printfn "1. Agregar canción (add|título|artista|nombre de archivo)"
        printfn "2. Listar canciones (list)"
        printfn "3. Búsqueda por título"
        printfn "4. Búsqueda por artista"
        printfn "5. Búsqueda por nombre de archivo"
        printfn "6. Salir"
        printf "> "

        let input = Console.ReadLine()
        match input with
        | "1" ->
            printfn "Ingrese el título de la canción:"
            let title = Console.ReadLine()
            printfn "Ingrese el artista de la canción:"
            let artist = Console.ReadLine()
            printfn "Ingrese el nombre de archivo de la canción:"
            let fileName = Console.ReadLine()
            let request = sprintf "add|%s|%s|%s" title artist fileName
            sendRequestToServer serverIP serverPort request
        | "2" ->
            let request = "list"
            sendRequestToServer serverIP serverPort request
        | "3" ->
            printfn "Búsqueda por título (Ingrese el título):"
            let title = Console.ReadLine()
            let request = sprintf "searchTitle|%s" title
            sendRequestToServer serverIP serverPort request
        | "4" ->
            printfn "Búsqueda por artista (Ingrese el artista):"
            let artist = Console.ReadLine()
            let request = sprintf "searchArtist|%s" artist
            sendRequestToServer serverIP serverPort request
        | "5" ->
            printfn "Búsqueda por nombre de archivo (Ingrese el nombre de archivo):"
            let fileName = Console.ReadLine()
            let request = sprintf "searchFileName|%s" fileName
            sendRequestToServer serverIP serverPort request
        | "6" ->
            printfn "Saliendo del cliente."
            Environment.Exit(0)
        | _ ->
            printfn "Comando no válido."

    0