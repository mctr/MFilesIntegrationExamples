namespace MFilesExamples.REST
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.btn_Authenticate = new System.Windows.Forms.Button();
            this.btn_Upload = new System.Windows.Forms.Button();
            this.btn_Download = new System.Windows.Forms.Button();
            this.btn_GetListOfDocuments = new System.Windows.Forms.Button();
            this.btn_Search = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Authenticate
            // 
            this.btn_Authenticate.Location = new System.Drawing.Point(98, 40);
            this.btn_Authenticate.Name = "btn_Authenticate";
            this.btn_Authenticate.Size = new System.Drawing.Size(95, 23);
            this.btn_Authenticate.TabIndex = 0;
            this.btn_Authenticate.Text = "Authentication";
            this.btn_Authenticate.UseVisualStyleBackColor = true;
            this.btn_Authenticate.Click += new System.EventHandler(this.btn_Authenticate_Click);
            // 
            // btn_Upload
            // 
            this.btn_Upload.Location = new System.Drawing.Point(80, 85);
            this.btn_Upload.Name = "btn_Upload";
            this.btn_Upload.Size = new System.Drawing.Size(136, 23);
            this.btn_Upload.TabIndex = 1;
            this.btn_Upload.Text = "Upload Document";
            this.btn_Upload.UseVisualStyleBackColor = true;
            this.btn_Upload.Click += new System.EventHandler(this.btn_Upload_Click);
            // 
            // btn_Download
            // 
            this.btn_Download.Location = new System.Drawing.Point(80, 124);
            this.btn_Download.Name = "btn_Download";
            this.btn_Download.Size = new System.Drawing.Size(136, 23);
            this.btn_Download.TabIndex = 2;
            this.btn_Download.Text = "Download Document";
            this.btn_Download.UseVisualStyleBackColor = true;
            this.btn_Download.Click += new System.EventHandler(this.btn_Download_Click);
            // 
            // btn_GetListOfDocuments
            // 
            this.btn_GetListOfDocuments.Location = new System.Drawing.Point(80, 165);
            this.btn_GetListOfDocuments.Name = "btn_GetListOfDocuments";
            this.btn_GetListOfDocuments.Size = new System.Drawing.Size(136, 23);
            this.btn_GetListOfDocuments.TabIndex = 3;
            this.btn_GetListOfDocuments.Text = "Get List Of Documents";
            this.btn_GetListOfDocuments.UseVisualStyleBackColor = true;
            this.btn_GetListOfDocuments.Click += new System.EventHandler(this.btn_GetListOfDocuments_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(80, 203);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(136, 23);
            this.btn_Search.TabIndex = 4;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.btn_GetListOfDocuments);
            this.Controls.Add(this.btn_Download);
            this.Controls.Add(this.btn_Upload);
            this.Controls.Add(this.btn_Authenticate);
            this.Name = "MainForm";
            this.Text = "Select Example";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Authenticate;
        private System.Windows.Forms.Button btn_Upload;
        private System.Windows.Forms.Button btn_Download;
        private System.Windows.Forms.Button btn_GetListOfDocuments;
        private System.Windows.Forms.Button btn_Search;
    }
}

