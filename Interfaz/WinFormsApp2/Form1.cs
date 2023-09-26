using ClassLibrary1;
using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Forms;
using NAudio.Wave;

namespace WinFormsApp2
{

    public partial class Form1 : Form
    {
        private WaveOutEvent waveOutDevice;
        private AudioFileReader audioFileReader;
        private string currentlyPlayingFileName;
        private int selectedID = -1;
        private string selectedTitulo = string.Empty;
        private string selectedArtista = string.Empty;
        private string selectedFileName = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_buscar.Text == "")
            {

                var request = "add|Titulo|Artista|MP3";

                txt_buscar.Text = ClassLibrary1.lib.sendRequestToServer("127.0.0.1", 12345, request);

            }
            else
                textBox2.Text = "No se agregó";
        }

        private void btn_Restablecer_Click(object sender, EventArgs e)
        {
            string serverIP = "127.0.0.1"; // Reemplaza con la IP de tu servidor F#
            int serverPort = 12345;
            string responseJson = lib.sendRequestToServerList(serverIP, serverPort, "listar", "0");



            // Verificar si la respuesta no es nula o vacía antes de deserializarla
            if (!string.IsNullOrEmpty(responseJson))
            {
                // Deserializar la lista de canciones desde JSON a una lista de objetos SongInfo
                //textBox2.Text = JsonConvert.DeserializeObject<List<SongInfo>>(responseJson).ToString();
                List<SongInfo> songs = JsonConvert.DeserializeObject<List<SongInfo>>(responseJson);

                // Configurar el DataGridView
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = songs;
                dataGridView1.Columns["FileName"].Visible = false;
            }
            else
            {
                // Manejar el caso en el que la respuesta es nula o vacía
                MessageBox.Show("No se pudo obtener la lista de canciones desde el servidor.");
            }
        }
        //Tiene boton de play-
        private bool ImageEquals(Image img1, Image img2)
        {
            if (img1 == null || img2 == null)
            {
                return false;
            }

            // Convierte las imágenes en matrices de bytes
            byte[] bytes1, bytes2;
            using (System.IO.MemoryStream ms1 = new System.IO.MemoryStream())
            using (System.IO.MemoryStream ms2 = new System.IO.MemoryStream())
            {
                img1.Save(ms1, img1.RawFormat);
                img2.Save(ms2, img2.RawFormat);
                bytes1 = ms1.ToArray();
                bytes2 = ms2.ToArray();
            }

            // Compara las matrices de bytes
            return System.Linq.Enumerable.SequenceEqual(bytes1, bytes2);
        }
        private bool isPlaying = false;
        private TimeSpan pausePosition = TimeSpan.Zero;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (ImageEquals(pictureBox1.Image, Properties.Resources.pause))
            {
                // Cambia la imagen a Properties.Resources.Play
                pictureBox1.Image = Properties.Resources.play;
                // Llamar al método para pausar la reproducción
                PausePlayback();
            }
            else if (ImageEquals(pictureBox1.Image, Properties.Resources.play))
            {
                // Cambia la imagen a Properties.Resources.Pause
                pictureBox1.Image = Properties.Resources.pause;
                // Reanudar o iniciar la reproducción
                if (string.IsNullOrEmpty(selectedFileName))
                {
                    // No hay una canción seleccionada
                    return;
                }

                if (!isPlaying)
                {
                    // Si no está en reproducción, crear un nuevo dispositivo de salida
                    waveOutDevice = new WaveOutEvent();

                    // Abrir el archivo MP3
                    audioFileReader = new AudioFileReader(selectedFileName);

                    // Asignar el archivo al dispositivo de salida
                    waveOutDevice.Init(audioFileReader);

                    // Comenzar la reproducción
                    waveOutDevice.Play();

                    isPlaying = true;
                }
            }
        }

        private void btn_AgregarC_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos MP3 (*.mp3)|*.mp3|Todos los archivos (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string titulo = txtTitulo.Text; // Obtener el título del TextBox
                string artista = txtArtista.Text; // Obtener el artista del TextBox

                try
                {
                    // Carpeta donde deseas guardar los archivos MP3 en tu proyecto.
                    string carpetaProyecto = @"C:\Users\isaac\OneDrive\Escritorio\Git Analisis\Proyecto-1-Lenguajes\Music"; // Reemplaza con la ruta adecuada.

                    // Crear una carpeta si no existe.
                    if (!Directory.Exists(carpetaProyecto))
                    {
                        Directory.CreateDirectory(carpetaProyecto);
                    }

                    // Generar un nombre de archivo único basado en la fecha y hora actual.
                    string nombreArchivo = $"{DateTime.Now:yyyyMMddHHmmssfff}.mp3";

                    // Construir la ruta completa del archivo en la carpeta del proyecto.
                    string rutaDestino = Path.Combine(carpetaProyecto, nombreArchivo);

                    // Copiar el archivo MP3 seleccionado a la carpeta del proyecto.
                    File.Copy(filePath, rutaDestino);

                    // Luego, envía la ruta completa del archivo al servidor.
                    string serverIP = "127.0.0.1"; // Reemplaza con la IP de tu servidor F#
                    int serverPort = 12345; // Reemplaza con el puerto de tu servidor F#
                    string request = $"add|{titulo}|{artista}|{rutaDestino}"; // Incluye la ruta del archivo en la solicitud.

                    // Llama a la función de F# para enviar la solicitud al servidor.
                    string response = lib.sendRequestToServer(serverIP, serverPort, request);

                    // Puedes manejar la respuesta del servidor aquí si es necesario.
                    //txt_buscar.Text = response;
                    btn_Restablecer_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar el archivo MP3: " + ex.Message);
                }
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (waveOutDevice != null)
            {
                waveOutDevice.Stop();
                waveOutDevice.Dispose();
            }
            if (audioFileReader != null)
            {
                audioFileReader.Dispose();
            }
        }
        private void PausePlayback()
        {
            if (waveOutDevice != null && isPlaying)
            {
                waveOutDevice.Pause();
                isPlaying = false;
                // Cambiar la imagen del botón a Play
                pictureBox1.Image = Properties.Resources.play;
            }
        }

        private void btn_NuevaLista_Click(object sender, EventArgs e)
        {

        }

        private void btn_bTitulo_Click(object sender, EventArgs e)
        {
            string serverIP = "127.0.0.1"; // Reemplaza con la IP de tu servidor F#
            int serverPort = 12345;
            string responseJson = lib.sendRequestToServerList(serverIP, serverPort, "titulo", txt_buscar.Text);



            // Verificar si la respuesta no es nula o vacía antes de deserializarla
            if (!string.IsNullOrEmpty(responseJson))
            {
                // Deserializar la lista de canciones desde JSON a una lista de objetos SongInfo

                List<SongInfo> songs = JsonConvert.DeserializeObject<List<SongInfo>>(responseJson);

                // Configurar el DataGridView
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = songs;
            }
            else
            {
                // Manejar el caso en el que la respuesta es nula o vacía
                MessageBox.Show("No se pudo obtener la lista de canciones desde el servidor.");
            }
        }

        private void btn_bArtista_Click(object sender, EventArgs e)
        {
            string serverIP = "127.0.0.1"; // Reemplaza con la IP de tu servidor F#
            int serverPort = 12345;
            string responseJson = lib.sendRequestToServerList(serverIP, serverPort, "artista", txt_buscar.Text);



            // Verificar si la respuesta no es nula o vacía antes de deserializarla
            if (!string.IsNullOrEmpty(responseJson))
            {
                // Deserializar la lista de canciones desde JSON a una lista de objetos SongInfo

                List<SongInfo> songs = JsonConvert.DeserializeObject<List<SongInfo>>(responseJson);

                // Configurar el DataGridView
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = songs;
            }
            else
            {
                // Manejar el caso en el que la respuesta es nula o vacía
                MessageBox.Show("No se pudo obtener la lista de canciones desde el servidor.");
            }
        }

        private void btn_Archivo_Click(object sender, EventArgs e)
        {
            string serverIP = "127.0.0.1"; // Reemplaza con la IP de tu servidor F#
            int serverPort = 12345;
            string responseJson = lib.sendRequestToServerList(serverIP, serverPort, "archivo", txt_buscar.Text);



            // Verificar si la respuesta no es nula o vacía antes de deserializarla
            if (!string.IsNullOrEmpty(responseJson))
            {
                // Deserializar la lista de canciones desde JSON a una lista de objetos SongInfo

                List<SongInfo> songs = JsonConvert.DeserializeObject<List<SongInfo>>(responseJson);

                // Configurar el DataGridView
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = songs;
            }
            else
            {
                // Manejar el caso en el que la respuesta es nula o vacía
                MessageBox.Show("No se pudo obtener la lista de canciones desde el servidor.");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;

            if (e.RowIndex >= 0)
            {
                PausePlayback();
                pictureBox1.Image = Properties.Resources.play;
                selectedID = (int)dataGridView.Rows[e.RowIndex].Cells["ID"].Value;
                selectedTitulo = dataGridView.Rows[e.RowIndex].Cells["Title"].Value.ToString();
                selectedArtista = dataGridView.Rows[e.RowIndex].Cells["Artist"].Value.ToString();
                selectedFileName = dataGridView.Rows[e.RowIndex].Cells["FileName"].Value.ToString();

                lbl_rN.Text = selectedTitulo;
                lbl_Artista.Text = selectedArtista;
            }
        }
    }
}