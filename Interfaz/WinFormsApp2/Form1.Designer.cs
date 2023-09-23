namespace WinFormsApp2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            saveFileDialog1 = new SaveFileDialog();
            panel1 = new Panel();
            lbl_Busqueda = new Label();
            btn_Archivo = new Button();
            btn_bArtista = new Button();
            btn_bTitulo = new Button();
            btn_Restablecer = new Button();
            textBox1 = new TextBox();
            btn_AgregarC = new Button();
            textBox2 = new TextBox();
            panel2 = new Panel();
            lbl_Artista = new Label();
            label1 = new Label();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            panel3 = new Panel();
            button1 = new Button();
            lbl_lReproduccion = new Label();
            panel4 = new Panel();
            button2 = new Button();
            lbl_sReproduccion = new Label();
            textBox3 = new TextBox();
            panel5 = new Panel();
            label3 = new Label();
            label2 = new Label();
            txtTitulo = new TextBox();
            txtArtista = new TextBox();
            dataGridView1 = new DataGridView();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(242, 249, 255);
            panel1.Controls.Add(dataGridView1);
            panel1.Controls.Add(lbl_Busqueda);
            panel1.Controls.Add(btn_Archivo);
            panel1.Controls.Add(btn_bArtista);
            panel1.Controls.Add(btn_bTitulo);
            panel1.Controls.Add(btn_Restablecer);
            panel1.Controls.Add(textBox1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(537, 753);
            panel1.TabIndex = 5;
            // 
            // lbl_Busqueda
            // 
            lbl_Busqueda.AutoSize = true;
            lbl_Busqueda.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbl_Busqueda.Location = new Point(10, 10);
            lbl_Busqueda.Name = "lbl_Busqueda";
            lbl_Busqueda.Size = new Size(97, 28);
            lbl_Busqueda.TabIndex = 7;
            lbl_Busqueda.Text = "Búsqueda";
            // 
            // btn_Archivo
            // 
            btn_Archivo.Location = new Point(438, 49);
            btn_Archivo.Name = "btn_Archivo";
            btn_Archivo.Size = new Size(90, 29);
            btn_Archivo.TabIndex = 6;
            btn_Archivo.Text = "Archivo";
            btn_Archivo.UseVisualStyleBackColor = true;
            // 
            // btn_bArtista
            // 
            btn_bArtista.Location = new Point(342, 49);
            btn_bArtista.Name = "btn_bArtista";
            btn_bArtista.Size = new Size(90, 29);
            btn_bArtista.TabIndex = 5;
            btn_bArtista.Text = "Artista";
            btn_bArtista.UseVisualStyleBackColor = true;
            // 
            // btn_bTitulo
            // 
            btn_bTitulo.Location = new Point(246, 49);
            btn_bTitulo.Name = "btn_bTitulo";
            btn_bTitulo.Size = new Size(90, 29);
            btn_bTitulo.TabIndex = 4;
            btn_bTitulo.Text = "Titulo";
            btn_bTitulo.UseVisualStyleBackColor = true;
            // 
            // btn_Restablecer
            // 
            btn_Restablecer.Location = new Point(113, 10);
            btn_Restablecer.Name = "btn_Restablecer";
            btn_Restablecer.Size = new Size(94, 29);
            btn_Restablecer.TabIndex = 2;
            btn_Restablecer.Text = "Restablecer";
            btn_Restablecer.UseVisualStyleBackColor = true;
            btn_Restablecer.Click += btn_Restablecer_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(10, 50);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(230, 27);
            textBox1.TabIndex = 0;
            // 
            // btn_AgregarC
            // 
            btn_AgregarC.Location = new Point(392, 84);
            btn_AgregarC.Name = "btn_AgregarC";
            btn_AgregarC.Size = new Size(136, 29);
            btn_AgregarC.TabIndex = 3;
            btn_AgregarC.Text = "Agregar Cancion";
            btn_AgregarC.UseVisualStyleBackColor = true;
            btn_AgregarC.Click += btn_AgregarC_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(10, 40);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(152, 27);
            textBox2.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(242, 249, 255);
            panel2.Controls.Add(lbl_Artista);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(pictureBox3);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(pictureBox1);
            panel2.Location = new Point(555, 771);
            panel2.Name = "panel2";
            panel2.Size = new Size(631, 125);
            panel2.TabIndex = 6;
            // 
            // lbl_Artista
            // 
            lbl_Artista.AutoSize = true;
            lbl_Artista.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lbl_Artista.Location = new Point(10, 68);
            lbl_Artista.Name = "lbl_Artista";
            lbl_Artista.Size = new Size(52, 20);
            lbl_Artista.TabIndex = 11;
            lbl_Artista.Text = "Artista";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(10, 40);
            label1.Name = "label1";
            label1.Size = new Size(85, 28);
            label1.TabIndex = 10;
            label1.Text = "Nombre";
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Avanzar;
            pictureBox3.Location = new Point(533, 24);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(75, 75);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 2;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Retroceder;
            pictureBox2.Location = new Point(321, 24);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(75, 75);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.pause;
            pictureBox1.Location = new Point(431, 24);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(75, 75);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(242, 249, 255);
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(button1);
            panel3.Controls.Add(lbl_lReproduccion);
            panel3.Controls.Add(textBox2);
            panel3.Location = new Point(555, 12);
            panel3.Name = "panel3";
            panel3.Size = new Size(313, 753);
            panel3.TabIndex = 6;
            // 
            // button1
            // 
            button1.Location = new Point(168, 40);
            button1.Name = "button1";
            button1.Size = new Size(136, 29);
            button1.TabIndex = 9;
            button1.Text = "Nueva Lista";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // lbl_lReproduccion
            // 
            lbl_lReproduccion.AutoSize = true;
            lbl_lReproduccion.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbl_lReproduccion.Location = new Point(3, 4);
            lbl_lReproduccion.Name = "lbl_lReproduccion";
            lbl_lReproduccion.Size = new Size(212, 28);
            lbl_lReproduccion.TabIndex = 8;
            lbl_lReproduccion.Text = "Listas de Reproducción";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(242, 249, 255);
            panel4.Controls.Add(button2);
            panel4.Controls.Add(lbl_sReproduccion);
            panel4.Controls.Add(textBox3);
            panel4.Location = new Point(874, 12);
            panel4.Name = "panel4";
            panel4.Size = new Size(312, 753);
            panel4.TabIndex = 6;
            // 
            // button2
            // 
            button2.Location = new Point(166, 40);
            button2.Name = "button2";
            button2.Size = new Size(136, 29);
            button2.TabIndex = 11;
            button2.Text = "Nueva Lista";
            button2.UseVisualStyleBackColor = true;
            // 
            // lbl_sReproduccion
            // 
            lbl_sReproduccion.AutoSize = true;
            lbl_sReproduccion.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbl_sReproduccion.Location = new Point(3, 4);
            lbl_sReproduccion.Name = "lbl_sReproduccion";
            lbl_sReproduccion.Size = new Size(170, 28);
            lbl_sReproduccion.TabIndex = 9;
            lbl_sReproduccion.Text = "Lista Seleccionada";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(10, 40);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(150, 27);
            textBox3.TabIndex = 10;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(242, 249, 255);
            panel5.Controls.Add(label3);
            panel5.Controls.Add(label2);
            panel5.Controls.Add(txtTitulo);
            panel5.Controls.Add(txtArtista);
            panel5.Controls.Add(btn_AgregarC);
            panel5.Location = new Point(12, 771);
            panel5.Name = "panel5";
            panel5.Size = new Size(537, 125);
            panel5.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(171, 51);
            label3.Name = "label3";
            label3.Size = new Size(69, 28);
            label3.TabIndex = 12;
            label3.Text = "Artista";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(171, 13);
            label2.Name = "label2";
            label2.Size = new Size(62, 28);
            label2.TabIndex = 8;
            label2.Text = "Título";
            // 
            // txtTitulo
            // 
            txtTitulo.Location = new Point(274, 13);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(254, 27);
            txtTitulo.TabIndex = 11;
            // 
            // txtArtista
            // 
            txtArtista.Location = new Point(274, 51);
            txtArtista.Name = "txtArtista";
            txtArtista.Size = new Size(254, 27);
            txtArtista.TabIndex = 10;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.FromArgb(5, 15, 34);
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(10, 84);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(518, 666);
            dataGridView1.TabIndex = 8;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(114, 120, 140);
            ClientSize = new Size(1198, 908);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "SpotyCry";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private SaveFileDialog saveFileDialog1;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private TextBox textBox2;
        private TextBox textBox1;
        private Button btn_AgregarC;
        private Button btn_Restablecer;
        private Label lbl_Busqueda;
        private Button btn_Archivo;
        private Button btn_bArtista;
        private Button btn_bTitulo;
        private Label lbl_lReproduccion;
        private Button button1;
        private Label lbl_sReproduccion;
        private Button button2;
        private TextBox textBox3;
        private PictureBox pictureBox1;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private Label lbl_Artista;
        private Label label1;
        private Panel panel5;
        private Label label3;
        private Label label2;
        private TextBox txtTitulo;
        private TextBox txtArtista;
        private DataGridView dataGridView1;
    }
}