namespace ViewModelOppgave.Frontend
{
	partial class MembersView
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
			this.components = new System.ComponentModel.Container();
			this._addBtn = new System.Windows.Forms.Button();
			this._closeBtn = new System.Windows.Forms.Button();
			this._lblMembers = new System.Windows.Forms.Label();
			this._gridMember = new System.Windows.Forms.DataGridView();
			this._colFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._colLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._colAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._colSex = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._bsAllMembers = new System.Windows.Forms.BindingSource(this.components);
			this._memberDetailsView = new ViewModelOppgave.Frontend.MemberDetailsView();
			this._bsViewModel = new System.Windows.Forms.BindingSource(this.components);
			this.menuBar = new System.Windows.Forms.ToolStrip();
			this._newMemberBtn = new System.Windows.Forms.ToolStripButton();
			((System.ComponentModel.ISupportInitialize)(this._gridMember)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._bsAllMembers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._bsViewModel)).BeginInit();
			this.menuBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// _addBtn
			// 
			this._addBtn.Location = new System.Drawing.Point(380, 83);
			this._addBtn.Name = "_addBtn";
			this._addBtn.Size = new System.Drawing.Size(75, 23);
			this._addBtn.TabIndex = 1;
			this._addBtn.Text = "&Legg til";
			this._addBtn.UseVisualStyleBackColor = true;
			// 
			// _closeBtn
			// 
			this._closeBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._closeBtn.Location = new System.Drawing.Point(646, 458);
			this._closeBtn.Name = "_closeBtn";
			this._closeBtn.Size = new System.Drawing.Size(75, 23);
			this._closeBtn.TabIndex = 4;
			this._closeBtn.Text = "&Lukk";
			this._closeBtn.UseVisualStyleBackColor = true;
			this._closeBtn.Click += new System.EventHandler(this.Close_Click);
			// 
			// _lblMembers
			// 
			this._lblMembers.AutoSize = true;
			this._lblMembers.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._lblMembers.Location = new System.Drawing.Point(12, 118);
			this._lblMembers.Name = "_lblMembers";
			this._lblMembers.Size = new System.Drawing.Size(136, 26);
			this._lblMembers.TabIndex = 2;
			this._lblMembers.Text = "Medlemmer";
			// 
			// _gridMember
			// 
			this._gridMember.AllowUserToAddRows = false;
			this._gridMember.AllowUserToDeleteRows = false;
			this._gridMember.AutoGenerateColumns = false;
			this._gridMember.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._gridMember.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._colFirstName,
            this._colLastName,
            this._colAge,
            this._colSex});
			this._gridMember.DataSource = this._bsAllMembers;
			this._gridMember.Location = new System.Drawing.Point(12, 147);
			this._gridMember.Name = "_gridMember";
			this._gridMember.ReadOnly = true;
			this._gridMember.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._gridMember.Size = new System.Drawing.Size(709, 305);
			this._gridMember.TabIndex = 3;
			// 
			// _colFirstName
			// 
			this._colFirstName.DataPropertyName = "FirstName";
			this._colFirstName.HeaderText = "FirstName";
			this._colFirstName.Name = "_colFirstName";
			this._colFirstName.ReadOnly = true;
			// 
			// _colLastName
			// 
			this._colLastName.DataPropertyName = "LastName";
			this._colLastName.HeaderText = "LastName";
			this._colLastName.Name = "_colLastName";
			this._colLastName.ReadOnly = true;
			// 
			// _colAge
			// 
			this._colAge.DataPropertyName = "Age";
			this._colAge.HeaderText = "Age";
			this._colAge.Name = "_colAge";
			this._colAge.ReadOnly = true;
			// 
			// _colSex
			// 
			this._colSex.DataPropertyName = "Sex";
			this._colSex.HeaderText = "Sex";
			this._colSex.Name = "_colSex";
			this._colSex.ReadOnly = true;
			// 
			// _memberDetailsView
			// 
			this._memberDetailsView.Location = new System.Drawing.Point(3, 28);
			this._memberDetailsView.Name = "_memberDetailsView";
			this._memberDetailsView.Size = new System.Drawing.Size(371, 87);
			this._memberDetailsView.TabIndex = 0;
			// 
			// _bsViewModel
			// 
			this._bsViewModel.DataSource = typeof(ViewModelOppgave.Frontend.MemberDetailsViewModel);
			// 
			// menuBar
			// 
			this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newMemberBtn});
			this.menuBar.Location = new System.Drawing.Point(0, 0);
			this.menuBar.Name = "menuBar";
			this.menuBar.Size = new System.Drawing.Size(733, 25);
			this.menuBar.TabIndex = 5;
			this.menuBar.Text = "toolStrip1";
			// 
			// _newMemberBtn
			// 
			this._newMemberBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this._newMemberBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._newMemberBtn.Name = "_newMemberBtn";
			this._newMemberBtn.Size = new System.Drawing.Size(81, 22);
			this._newMemberBtn.Text = "&Nytt medlem";
			this._newMemberBtn.Click += new System.EventHandler(this.NewMemberBtn_Click);
			// 
			// MembersView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(733, 487);
			this.Controls.Add(this.menuBar);
			this.Controls.Add(this._lblMembers);
			this.Controls.Add(this._gridMember);
			this.Controls.Add(this._closeBtn);
			this.Controls.Add(this._addBtn);
			this.Controls.Add(this._memberDetailsView);
			this.Name = "MembersView";
			this.Text = "Bokklubben";
			((System.ComponentModel.ISupportInitialize)(this._gridMember)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._bsAllMembers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._bsViewModel)).EndInit();
			this.menuBar.ResumeLayout(false);
			this.menuBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MemberDetailsView _memberDetailsView;
		private System.Windows.Forms.Button _addBtn;
		private System.Windows.Forms.Button _closeBtn;
		private System.Windows.Forms.Label _lblMembers;
		private System.Windows.Forms.DataGridView _gridMember;
		private System.Windows.Forms.BindingSource _bsViewModel;
		private System.Windows.Forms.BindingSource _bsAllMembers;
		private System.Windows.Forms.DataGridViewTextBoxColumn _colFirstName;
		private System.Windows.Forms.DataGridViewTextBoxColumn _colLastName;
		private System.Windows.Forms.DataGridViewTextBoxColumn _colAge;
		private System.Windows.Forms.DataGridViewTextBoxColumn _colSex;
		private System.Windows.Forms.ToolStrip menuBar;
		private System.Windows.Forms.ToolStripButton _newMemberBtn;
	}
}