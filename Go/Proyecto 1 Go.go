// Server
package main

import (
	"fmt"
	"github.com/faiface/beep"
	"github.com/faiface/beep/mp3"
	"github.com/faiface/beep/speaker"
	"github.com/faiface/beep/wav"
	"net"
	"os"
	"strings"
	"sync"
	"time"
)

func main() {
	server := &Server{}
	listen, err := net.Listen("tcp", ":12345")
	if err != nil {
		fmt.Println("Error al iniciar el servidor:", err)
		os.Exit(1)
	}
	defer listen.Close()
	fmt.Println("Servidor en ejecuciÃ³n en localhost:12345")

	for {
		conn, err := listen.Accept()
		if err != nil {
			fmt.Println("Error al aceptar la conexiÃ³n:", err)
			continue
		}
		go handleConnection(conn, server)
	}
}

// Estructura para representar una canciÃ³n
type Song struct {
	Title    string
	Artist   string
	FileName string // Nombre del archivo de la canciÃ³n
}

// Estructura para el servidor
type Server struct {
	songs []Song
	mutex sync.Mutex
}

// Estructura para la reproducciÃ³n de canciones
type Player struct {
	streamer beep.StreamSeekCloser
	format   beep.Format
}

// FunciÃ³n para agregar una canciÃ³n al servidor
func (s *Server) AddSong(title, artist, fileName string) {
	s.mutex.Lock()
	defer s.mutex.Unlock()
	s.songs = append(s.songs, Song{Title: title, Artist: artist, FileName: fileName})
}

// FunciÃ³n para listar las canciones del servidor
func (s *Server) ListSongs() []Song {
	s.mutex.Lock()
	defer s.mutex.Unlock()
	return s.songs
}

// FunciÃ³n para buscar canciones por tÃ­tulo
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

// FunciÃ³n para buscar canciones por artista
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

// FunciÃ³n para buscar canciones por nombre de archivo
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

// FunciÃ³n para iniciar la reproducciÃ³n de una canciÃ³n
func (p *Player) StartPlayback() {
	speaker.Init(p.format.SampleRate, p.format.SampleRate.N(time.Second/10))
	speaker.Play(p.streamer)
}

// FunciÃ³n para detener la reproducciÃ³n de una canciÃ³n
func (p *Player) StopPlayback() {
	speaker.Clear()
}

func handleConnection(conn net.Conn, server *Server) {
	defer conn.Close()
	fmt.Println("Nueva conexiÃ³n establecida")

	buffer := make([]byte, 1024)
	player := &Player{} // Crear un reproductor para la canciÃ³n

	for {
		n, err := conn.Read(buffer)
		if err != nil {
			fmt.Println("Error al leer datos:", err)
			return
		}
		clientRequest := string(buffer[:n])
		response := processClientRequest(clientRequest, server, player)
		fmt.Println("Mensaje que va al cliente es: ", response)

		_, err = conn.Write([]byte(response + "\n"))
		if err != nil {
			fmt.Println("Error al enviar respuesta al cliente:", err)
		}
		conn.Close()
	}
}

func processClientRequest(request string, server *Server, player *Player) string {
	parts := strings.Split(request, "|")
	if len(parts) < 2 {
		return "Comando no vÃ¡lido"
	}

	command := parts[0]
	params := parts[1:]

	switch command {
	case "add":
		if len(params) != 3 {
			return "Comando 'add' requiere tres parÃ¡metros: tÃ­tulo, artista y nombre de archivo"
		}
		title := params[0]
		artist := params[1]
		fileName := params[2]

		fmt.Printf("Datos recibidos del cliente (add):\nTÃ­tulo: %s\nArtista: %s\nNombre de archivo: %s\n", title, artist, fileName)
		server.AddSong(title, artist, fileName)
		return "CanciÃ³n agregada con Ã©xito"
	case "list":
		songs := server.ListSongs()
		if len(songs) == 0 {
			return "No hay canciones registradas"
		}
		return formatSongsList(songs)
	case "searchTitle":
		if len(params) != 1 {
			return "Comando 'searchTitle' requiere un parÃ¡metro: tÃ­tulo"
		}
		title := params[0]
		matchingSongs := server.SearchByTitle(title)
		return formatSongsList(matchingSongs)
	case "searchArtist":
		if len(params) != 1 {
			return "Comando 'searchArtist' requiere un parÃ¡metro: artista"
		}
		artist := params[0]
		matchingSongs := server.SearchByArtist(artist)
		return formatSongsList(matchingSongs)
	case "searchFileName":
		if len(params) != 1 {
			return "Comando 'searchFileName' requiere un parÃ¡metro: nombre de archivo"
		}
		fileName := params[0]
		matchingSongs := server.SearchByFileName(fileName)
		return formatSongsList(matchingSongs)
	case "play":
		if len(params) != 1 {
			return "Comando 'play' requiere un parÃ¡metro: nombre de archivo de la canciÃ³n"
		}
		fileName := params[0]
		err := playSong(fileName, player)
		if err != nil {
			return fmt.Sprintf("Error al reproducir la canciÃ³n: %v", err)
		}
		return "Reproduciendo canciÃ³n"
	case "stop":
		stopSong(player)
		return "CanciÃ³n detenida"
	default:
		return "Comando no reconocido"
	}
}

// FunciÃ³n para detener la reproducciÃ³n de una canciÃ³n
func stopSong(player *Player) {
	player.StopPlayback()
}

func formatSongsList(songs []Song) string {
	var songList strings.Builder
	for i, song := range songs {
		songList.WriteString(fmt.Sprintf("%d. TÃ­tulo: %s, Artista: %s, Nombre de archivo: %s\n", i+1, song.Title, song.Artist, song.FileName))
	}
	return songList.String()
}

// FunciÃ³n para reproducir una canciÃ³n
func playSong(fileName string, player *Player) error {
	file, err := os.Open(fileName)
	if err != nil {
		return err
	}
	defer file.Close()

	var streamer beep.StreamSeekCloser
	var format beep.Format

	if strings.HasSuffix(fileName, ".mp3") {
		streamer, format, err = mp3.Decode(file)
		if err != nil {
			return err
		}
	} else if strings.HasSuffix(fileName, ".wav") {
		streamer, format, err = wav.Decode(file)
		if err != nil {
			return err
		}
	} else {
		return fmt.Errorf("Formato de archivo no compatible")
	}

	player.streamer = streamer
	player.format = format
	player.StartPlayback()

	return nil
}
