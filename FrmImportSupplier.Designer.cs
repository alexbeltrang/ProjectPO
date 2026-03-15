namespace ProjectPO
{
    partial class FrmImportSupplier
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Label lblArchivo;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblEstado;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.btnImportar = new System.Windows.Forms.Button();
            this.lblArchivo = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblEstado = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Location = new System.Drawing.Point(20, 20);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(160, 35);
            this.btnSeleccionar.TabIndex = 0;
            this.btnSeleccionar.Text = "Seleccionar Archivo";
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // btnImportar
            // 
            this.btnImportar.Location = new System.Drawing.Point(200, 20);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(160, 35);
            this.btnImportar.TabIndex = 1;
            this.btnImportar.Text = "Importar";
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // lblArchivo
            // 
            this.lblArchivo.Location = new System.Drawing.Point(20, 70);
            this.lblArchivo.Name = "lblArchivo";
            this.lblArchivo.Size = new System.Drawing.Size(600, 25);
            this.lblArchivo.TabIndex = 2;
            this.lblArchivo.Text = "Archivo no seleccionado";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(20, 110);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(600, 25);
            this.progressBar1.TabIndex = 3;
            // 
            // lblEstado
            // 
            this.lblEstado.Location = new System.Drawing.Point(20, 145);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(600, 25);
            this.lblEstado.TabIndex = 4;
            // 
            // FrmImportSupplier
            // 
            this.ClientSize = new System.Drawing.Size(650, 200);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.btnImportar);
            this.Controls.Add(this.lblArchivo);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblEstado);
            this.Name = "FrmImportSupplier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importar Suppliers";
            this.ResumeLayout(false);

        }

        #endregion
    }
}