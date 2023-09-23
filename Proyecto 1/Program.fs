//Proyecto 1 Lenguajes de programación
//Cliente
open System
open System.IO
open System.Net
open System.Net.Sockets
open System.Text
open System.Diagnostics

// Función para enviar una solicitud al servidor (sin cambios)
let sendRequestToServer (serverIP: string) (serverPort: int) (request: string) =
    // ...

// Función para reproducir una canción localmente
let playSong (filePath: string) =
    try
        let audioFilePath = Path.Combine(Environment.CurrentDirectory, filePath)
        if File.Exists(audioFilePath) then
            let psi = new ProcessStartInfo(audioFilePath)
            psi.UseShellExecute <- true
            Process.Start(psi) |> ignore
        else
            printfn "El archivo de audio no se encontró: %s" audioFilePath
    with
    | :? Exception as ex ->
        Console.WriteLine("Error durante la reproducción: " + ex.Message)

// Resto del código (sin cambios)

[<EntryPoint>]
let main argv =
    // ...

    while true do
        // ...

        let input = Console.ReadLine()
        match input with
        | "1" ->
            // ...
        | "2" ->
            // ...
        | "3" ->
            // ...
        | "4" ->
            // ...
        | "5" ->
            // ...
        | "6" ->
            printfn "Ingrese el nombre del archivo de la canción a reproducir localmente:"
            let fileName = Console.ReadLine()
            playSong fileName
        | "7" ->
            printfn "Saliendo del cliente."
            Environment.Exit(0)
        | _ ->
            printfn "Comando no válido."

    0