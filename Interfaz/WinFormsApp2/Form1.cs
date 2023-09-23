using ClassLibrary1;
using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Forms;


namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {

                var request = "add|Titulo|Artista|MP3";

                textBox1.Text = ClassLibrary1.lib.sendRequestToServer("127.0.0.1", 12345, request);

            }
            else
                textBox2.Text = "No se agregó";
        }

        private void btn_Restablecer_Click(object sender, EventArgs e)
        {
            string serverIP = "127.0.0.1"; // Reemplaza con la IP de tu servidor Go
            int serverPort = 12345; // Reemplaza con el puerto de tu servidor Go
            string request = "list"; // Comando para listar canciones

            string responseJson = lib.sendRequestToServer(serverIP, serverPort, request);

            // Deserializa la respuesta JSON en una lista de canciones
            List<SongInfo> songs = JsonConvert.DeserializeObject<List<SongInfo>>(responseJson);

            // Enlaza la lista de canciones al DataGridView
            dataGridView1.DataSource = songs;
        }
        //Tiene boton de play-
        bool PlayPausa = true;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (PlayPausa == true)
            {
                pictureBox1.Image = Properties.Resources.pause;
                bool PlayPausa = false;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.play;
                bool PlayPausa = true;
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
                    textBox1.Text = response;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar el archivo MP3: " + ex.Message);
                }
            }

        }
    }
}