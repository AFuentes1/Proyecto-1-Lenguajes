// Server
package main

import (
	"fmt"
	"net"
	"os"
	"strings"
	"sync"
)

// Estructura para representar una canción
type Song struct {
	Title    string
	Artist   string
	FileName string // Nombre del archivo de la canción
}

// Estructura para el servidor
type Server struct {
	songs []Song
	mutex sync.Mutex
}

// Función para agregar una canción al servidor
func (s *Server) AddSong(title, artist, fileName string) {
	s.mutex.Lock()
	defer s.mutex.Unlock()
	s.songs = append(s.songs, Song{Title: title, Artist: artist, FileName: fileName})
}

// Función para listar las canciones del servidor
func (s *Server) ListSongs() []Song {
	s.mutex.Lock()
	defer s.mutex.Unlock()
	return s.songs
}

// Función para buscar canciones por título
func (s *Server) SearchByTitle(title string) []Song {
	s.mutex.Lock()
	defer s.mutex.Unlock()
	var result []Song
	for _, song := range s.songs {
		if strings.Contains(song.Title, title) {
			result = append(result, song)
		}
	}
	return result
}

// Función para buscar canciones por artista
func (s *Server) SearchByArtist(artist string) []Song {
	s.mutex.Lock()
	defer s.mutex.Unlock()
	var result []Song
	for _, song := range s.songs {
		if strings.Contains(song.Artist, artist) {
			result = append(result, song)
		}
	}
	return result
}

// Función para buscar canciones por nombre de archivo
func (s *Server) SearchByFileName(fileName string) []Song {
	s.mutex.Lock()
	defer s.mutex.Unlock()
	var result []Song
	for _, song := range s.songs {
		if strings.Contains(song.FileName, fileName) {
			result = append(result, song)
		}
	}
	return result
}

func main() {
	server := &Server{}
	listen, err := net.Listen("tcp", ":12345")
	if err != nil {
		fmt.Println("Error al iniciar el servidor:", err)
		os.Exit(1)
	}
	defer listen.Close()
	fmt.Println("Servidor en ejecución en localhost:12345")

	for {
		conn, err := listen.Accept()
		if err != nil {
			fmt.Println("Error al aceptar la conexión:", err)
			continue
		}
		go handleConnection(conn, server)
	}
}

func handleConnection(conn net.Conn, server *Server) {
	defer conn.Close()
	fmt.Println("Nueva conexión establecida")

	buffer := make([]byte, 1024)
	for {
		n, err := conn.Read(buffer)
		if err != nil {
			fmt.Println("Error al leer datos:", err)
			return
		}
		clientRequest := string(buffer[:n])
		response := processClientRequest(clientRequest, server)
		conn.Write([]byte(response))
	}
}

func processClientRequest(request string, server *Server) string {
	parts := strings.Split(request, "|")
	if len(parts) < 2 {
		return "Comando no válido"
	}

	command := parts[0]
	params := parts[1:]

	switch command {
	case "add":
		if len(params) != 3 {
			return "Comando 'add' requiere tres parámetros: título, artista y nombre de archivo"
		}
		title := params[0]
		artist := params[1]
		fileName := params[2]
		server.AddSong(title, artist, fileName)
		return "Canción agregada con éxito"
	case "list":
		songs := server.ListSongs()
		if len(songs) == 0 {
			return "No hay canciones registradas"
		}
		return formatSongsList(songs)
	case "searchTitle":
		if len(params) != 1 {
			return "Comando 'searchTitle' requiere un parámetro: título"
		}
		title := params[0]
		matchingSongs := server.SearchByTitle(title)
		return formatSongsList(matchingSongs)
	case "searchArtist":
		if len(params) != 1 {
			return "Comando 'searchArtist' requiere un parámetro: artista"
		}
		artist := params[0]
		matchingSongs := server.SearchByArtist(artist)
		return formatSongsList(matchingSongs)
	case "searchFileName":
		if len(params) != 1 {
			return "Comando 'searchFileName' requiere un parámetro: nombre de archivo"
		}
		fileName := params[0]
		matchingSongs := server.SearchByFileName(fileName)
		return formatSongsList(matchingSongs)
	default:
		return "Comando no reconocido"
	}
}

func formatSongsList(songs []Song) string {
	var songList strings.Builder
	for i, song := range songs {
		songList.WriteString(fmt.Sprintf("%d. Título: %s, Artista: %s, Nombre de archivo: %s\n", i+1, song.Title, song.Artist, song.FileName))
	}
	return songList.String()
}
