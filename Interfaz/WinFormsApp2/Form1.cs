using ClassLibrary1;
using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;

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

        }
        //Tiene boton de play
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

        
    }
}