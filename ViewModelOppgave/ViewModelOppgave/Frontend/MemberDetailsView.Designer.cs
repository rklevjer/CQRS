namespace ViewModelOppgave.Frontend
{
	partial class MemberDetailsView
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
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
				if (_viewModel != null)
				{
					_viewModel.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._rbWoman = new System.Windows.Forms.RadioButton();
			this._rbMan = new System.Windows.Forms.RadioButton();
			this._intAge = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this._txtLastName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this._txtFirstName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this._memberBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._memberBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this._rbWoman);
			this.groupBox1.Controls.Add(this._rbMan);
			this.groupBox1.Location = new System.Drawing.Point(161, 11);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 69);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "&Kjønn";
			// 
			// _rbWoman
			// 
			this._rbWoman.AutoSize = true;
			this._rbWoman.Location = new System.Drawing.Point(7, 43);
			this._rbWoman.Name = "_rbWoman";
			this._rbWoman.Size = new System.Drawing.Size(58, 17);
			this._rbWoman.TabIndex = 1;
			this._rbWoman.TabStop = true;
			this._rbWoman.Text = "&Kvinne";
			this._rbWoman.UseVisualStyleBackColor = true;
			// 
			// _rbMan
			// 
			this._rbMan.AutoSize = true;
			this._rbMan.Location = new System.Drawing.Point(7, 20);
			this._rbMan.Name = "_rbMan";
			this._rbMan.Size = new System.Drawing.Size(52, 17);
			this._rbMan.TabIndex = 0;
			this._rbMan.TabStop = true;
			this._rbMan.Text = "&Mann";
			this._rbMan.UseVisualStyleBackColor = true;
			// 
			// _intAge
			// 
			this._intAge.Location = new System.Drawing.Point(55, 60);
			this._intAge.Name = "_intAge";
			this._intAge.Size = new System.Drawing.Size(100, 20);
			this._intAge.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 63);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(31, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "&Alder";
			// 
			// _txtLastName
			// 
			this._txtLastName.Location = new System.Drawing.Point(55, 34);
			this._txtLastName.Name = "_txtLastName";
			this._txtLastName.Size = new System.Drawing.Size(100, 20);
			this._txtLastName.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 37);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "&Etternavn";
			// 
			// _txtFirstName
			// 
			this._txtFirstName.Location = new System.Drawing.Point(55, 8);
			this._txtFirstName.Name = "_txtFirstName";
			this._txtFirstName.Size = new System.Drawing.Size(100, 20);
			this._txtFirstName.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "&Fornavn";
			// 
			// _errorProvider
			// 
			this._errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this._errorProvider.ContainerControl = this;
			this._errorProvider.DataSource = this._memberBindingSource;
			// 
			// _memberBindingSource
			// 
			this._memberBindingSource.DataSource = typeof(ViewModelOppgave.Frontend.MemberDetailsViewModel);
			// 
			// MemberView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._intAge);
			this.Controls.Add(this.label3);
			this.Controls.Add(this._txtLastName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this._txtFirstName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Name = "MemberView";
			this.Size = new System.Drawing.Size(371, 87);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._memberBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton _rbWoman;
		private System.Windows.Forms.RadioButton _rbMan;
		private System.Windows.Forms.TextBox _intAge;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox _txtLastName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox _txtFirstName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.BindingSource _memberBindingSource;
		private System.Windows.Forms.ErrorProvider _errorProvider;
	}
}
