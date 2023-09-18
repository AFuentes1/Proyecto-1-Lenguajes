// Server
package main

import (
	"fmt"
	"net"
	"os"
	"strings"
)

// Mapa para almacenar las canciones
var songs = make(map[string]string)

func main() {
	// Crear un servidor TCP que escuche en el puerto 12345
	ln, err := net.Listen("tcp", ":12345")
	if err != nil {
		fmt.Println("Error al iniciar el servidor:", err)
		os.Exit(1)
	}
	defer ln.Close()
	fmt.Println("Servidor en ejecución en localhost:12345")

	for {
		// Aceptar conexiones de clientes
		conn, err := ln.Accept()
		if err != nil {
			fmt.Println("Error al aceptar la conexión:", err)
			continue
		}

		// Manejar la conexión en una gorutina separada
		go handleConnection(conn)
	}
}

func handleConnection(conn net.Conn) {
	defer conn.Close()
	fmt.Println("Nueva conexión establecida")

	// Búfer para almacenar los datos recibidos del cliente
	buf := make([]byte, 1024)

	for {
		// Leer datos del cliente
		n, err := conn.Read(buf)
		if err != nil {
			fmt.Println("Error al leer datos:", err)
			return
		}

		// Convertir los datos en una cadena y procesar la solicitud
		request := string(buf[:n])
		response := processRequest(request)
		conn.Write([]byte(response))
	}
}

func processRequest(request string) string {
	parts := strings.Split(request, "|")
	if len(parts) < 2 {
		return "Comando no válido"
	}

	command := parts[0]
	params := parts[1:]

	switch command {
	case "add":
		if len(params) != 2 {
			return "Comando 'add' requiere dos parámetros: título y artista"
		}
		title := params[0]
		artist := params[1]
		songs[title] = artist
		return "Canción agregada con éxito"
	case "list":
		var songList strings.Builder
		for title, artist := range songs {
			songList.WriteString(fmt.Sprintf("Título: %s, Artista: %s\n", title, artist))
		}
		if songList.Len() == 0 {
			return "No hay canciones registradas"
		}
		return songList.String()
	default:
		return "Comando no reconocido"
	}
}
