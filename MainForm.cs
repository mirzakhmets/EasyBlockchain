
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#if TRIAL
using Microsoft.Win32;
#endif

namespace EasyBlockchain
{
  public class MainForm : Form
  {
    public Wallet wallet = (Wallet) null;
    public bool walletSaved = false;
    private IContainer components = (IContainer) null;
    private OpenFileDialog openFileDialog;
    private SaveFileDialog saveFileDialog;
    private Label labelWallet;
    private TextBox textBoxWallet;
    private Button buttonWalletOpen;
    private Label labelOperation;
    private TextBox textBoxOperation;
    private Button buttonAdd;
    private Button buttonSubtract;
    private SaveFileDialog saveFileDialogReport;
    private Label labelBalance;
    private TextBox textBoxBalance;
    private Button buttonReport;
    private Button buttonWalletNew;
    private Button buttonSave;
    private Button buttonExit;

    public MainForm() {
    	this.InitializeComponent();
    }

    private void ButtonWalletOpenClick(object sender, EventArgs e)
    {
      if (this.openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      this.wallet = new Wallet(new StreamReader((Stream) new FileStream(this.openFileDialog.FileName, FileMode.Open)));
      this.textBoxWallet.Text = this.openFileDialog.FileName;
      this.textBoxBalance.Text = string.Concat((object) this.wallet.Balance());
    }

    private void ButtonWalletNewClick(object sender, EventArgs e)
    {
      if (this.saveFileDialog.ShowDialog() != DialogResult.OK)
        return;
      this.wallet = new Wallet();
      this.textBoxWallet.Text = this.saveFileDialog.FileName;
      this.textBoxBalance.Text = "0";
      this.walletSaved = false;
    }

    private void Button1Click(object sender, EventArgs e)
    {
      if (this.wallet == null)
      {
        this.ButtonWalletNewClick(sender, e);
      }
      else
      {
        this.wallet.Save(new StreamWriter((Stream) new FileStream(this.textBoxWallet.Text, FileMode.Create)));
        this.walletSaved = true;
      }
    }

    private void ButtonAddClick(object sender, EventArgs e)
    {
      if (this.wallet == null)
        return;
      Blockchain start = this.wallet.Start;
      if (this.wallet.Chain.Count > 0)
        start = this.wallet.Chain[checked (this.wallet.Chain.Count - 1)];
      this.wallet.Chain.Add(start.Operation(float.Parse(this.textBoxOperation.Text.Replace('.', ','))));
      this.textBoxBalance.Text = string.Concat((object) this.wallet.Balance());
      this.walletSaved = false;
    }

    private void ButtonSubtractClick(object sender, EventArgs e)
    {
      if (this.wallet == null)
        return;
      Blockchain start = this.wallet.Start;
      if (this.wallet.Chain.Count > 0)
        start = this.wallet.Chain[checked (this.wallet.Chain.Count - 1)];
      this.wallet.Chain.Add(start.Operation(-float.Parse(this.textBoxOperation.Text.Replace('.', ','))));
      this.textBoxBalance.Text = string.Concat((object) this.wallet.Balance());
      this.walletSaved = false;
    }

    private void ButtonReportClick(object sender, EventArgs e)
    {
      if (this.saveFileDialogReport.ShowDialog() != DialogResult.OK)
        return;
      StreamWriter streamWriter = new StreamWriter((Stream) new FileStream(this.saveFileDialogReport.FileName, FileMode.CreateNew));
      streamWriter.WriteLine("Hash,Difference,Balance");
      streamWriter.WriteLine(this.wallet.Start.Value + ",0,0");
      Blockchain bc = this.wallet.Start;
      foreach (Blockchain blockchain in this.wallet.Chain)
      {
        streamWriter.WriteLine(blockchain.Value + "," + string.Concat((object) blockchain.Subtract(bc)).Replace(',', '.') + "," + string.Concat((object) blockchain.Subtract(this.wallet.Start)).Replace(',', '.'));
        bc = blockchain;
      }
      streamWriter.Close();
    }

    private void ButtonExitClick(object sender, EventArgs e)
    {
      if (this.walletSaved)
      {
        Application.Exit();
      }
      else
      {
        int num = (int) MessageBox.Show("Wallet not saved");
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
    	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
    	this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
    	this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
    	this.labelWallet = new System.Windows.Forms.Label();
    	this.textBoxWallet = new System.Windows.Forms.TextBox();
    	this.buttonWalletOpen = new System.Windows.Forms.Button();
    	this.labelOperation = new System.Windows.Forms.Label();
    	this.textBoxOperation = new System.Windows.Forms.TextBox();
    	this.buttonAdd = new System.Windows.Forms.Button();
    	this.buttonSubtract = new System.Windows.Forms.Button();
    	this.saveFileDialogReport = new System.Windows.Forms.SaveFileDialog();
    	this.labelBalance = new System.Windows.Forms.Label();
    	this.textBoxBalance = new System.Windows.Forms.TextBox();
    	this.buttonReport = new System.Windows.Forms.Button();
    	this.buttonWalletNew = new System.Windows.Forms.Button();
    	this.buttonSave = new System.Windows.Forms.Button();
    	this.buttonExit = new System.Windows.Forms.Button();
    	this.SuspendLayout();
    	// 
    	// openFileDialog
    	// 
    	this.openFileDialog.Filter = "Wallet files (*.wallet)|*.wallet|All files|*.*";
    	// 
    	// saveFileDialog
    	// 
    	this.saveFileDialog.Filter = "Wallet files (*.wallet)|*.wallet|All files|*.*";
    	// 
    	// labelWallet
    	// 
    	this.labelWallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    	this.labelWallet.Location = new System.Drawing.Point(16, 62);
    	this.labelWallet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
    	this.labelWallet.Name = "labelWallet";
    	this.labelWallet.Size = new System.Drawing.Size(81, 28);
    	this.labelWallet.TabIndex = 0;
    	this.labelWallet.Text = "Wallet:";
    	// 
    	// textBoxWallet
    	// 
    	this.textBoxWallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    	this.textBoxWallet.Location = new System.Drawing.Point(16, 94);
    	this.textBoxWallet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
    	this.textBoxWallet.Name = "textBoxWallet";
    	this.textBoxWallet.ReadOnly = true;
    	this.textBoxWallet.Size = new System.Drawing.Size(405, 26);
    	this.textBoxWallet.TabIndex = 1;
    	// 
    	// buttonWalletOpen
    	// 
    	this.buttonWalletOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    	this.buttonWalletOpen.Location = new System.Drawing.Point(207, 17);
    	this.buttonWalletOpen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
    	this.buttonWalletOpen.Name = "buttonWalletOpen";
    	this.buttonWalletOpen.Size = new System.Drawing.Size(100, 34);
    	this.buttonWalletOpen.TabIndex = 2;
    	this.buttonWalletOpen.Text = "Open...";
    	this.buttonWalletOpen.UseVisualStyleBackColor = true;
    	this.buttonWalletOpen.Click += new System.EventHandler(this.ButtonWalletOpenClick);
    	// 
    	// labelOperation
    	// 
    	this.labelOperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    	this.labelOperation.Location = new System.Drawing.Point(16, 188);
    	this.labelOperation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
    	this.labelOperation.Name = "labelOperation";
    	this.labelOperation.Size = new System.Drawing.Size(133, 28);
    	this.labelOperation.TabIndex = 3;
    	this.labelOperation.Text = "Operation:";
    	// 
    	// textBoxOperation
    	// 
    	this.textBoxOperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    	this.textBoxOperation.Location = new System.Drawing.Point(16, 220);
    	this.textBoxOperation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
    	this.textBoxOperation.Name = "textBoxOperation";
    	this.textBoxOperation.Size = new System.Drawing.Size(181, 26);
    	this.textBoxOperation.TabIndex = 4;
    	this.textBoxOperation.Text = "0.00";
    	this.textBoxOperation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
    	// 
    	// buttonAdd
    	// 
    	this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    	this.buttonAdd.Location = new System.Drawing.Point(207, 220);
    	this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
    	this.buttonAdd.Name = "buttonAdd";
    	this.buttonAdd.Size = new System.Drawing.Size(88, 28);
    	this.buttonAdd.TabIndex = 5;
    	this.buttonAdd.Text = "Add";
    	this.buttonAdd.UseVisualStyleBackColor = true;
    	this.buttonAdd.Click += new System.EventHandler(this.ButtonAddClick);
    	// 
    	// buttonSubtract
    	// 
    	this.buttonSubtract.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    	this.buttonSubtract.Location = new System.Drawing.Point(303, 220);
    	this.buttonSubtract.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
    	this.buttonSubtract.Name = "buttonSubtract";
    	this.buttonSubtract.Size = new System.Drawing.Size(120, 28);
    	this.buttonSubtract.TabIndex = 6;
    	this.buttonSubtract.Text = "Subtract";
    	this.buttonSubtract.UseVisualStyleBackColor = true;
    	this.buttonSubtract.Click += new System.EventHandler(this.ButtonSubtractClick);
    	// 
    	// saveFileDialogReport
    	// 
    	this.saveFileDialogReport.Filter = "CSV files (*.csv)|*.csv|All files| *.*";
    	// 
    	// labelBalance
    	// 
    	this.labelBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    	this.labelBalance.Location = new System.Drawing.Point(16, 142);
    	this.labelBalance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
    	this.labelBalance.Name = "labelBalance";
    	this.labelBalance.Size = new System.Drawing.Size(111, 28);
    	this.labelBalance.TabIndex = 7;
    	this.labelBalance.Text = "Balance:";
    	// 
    	// textBoxBalance
    	// 
    	this.textBoxBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    	this.textBoxBalance.Location = new System.Drawing.Point(135, 142);
    	this.textBoxBalance.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
    	this.textBoxBalance.Name = "textBoxBalance";
    	this.textBoxBalance.ReadOnly = true;
    	this.textBoxBalance.Size = new System.Drawing.Size(287, 30);
    	this.textBoxBalance.TabIndex = 8;
    	this.textBoxBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
    	// 
    	// buttonReport
    	// 
    	this.buttonReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    	this.buttonReport.Location = new System.Drawing.Point(156, 282);
    	this.buttonReport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
    	this.buttonReport.Name = "buttonReport";
    	this.buttonReport.Size = new System.Drawing.Size(139, 32);
    	this.buttonReport.TabIndex = 9;
    	this.buttonReport.Text = "Report...";
    	this.buttonReport.UseVisualStyleBackColor = true;
    	this.buttonReport.Click += new System.EventHandler(this.ButtonReportClick);
    	// 
    	// buttonWalletNew
    	// 
    	this.buttonWalletNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    	this.buttonWalletNew.Location = new System.Drawing.Point(99, 16);
    	this.buttonWalletNew.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
    	this.buttonWalletNew.Name = "buttonWalletNew";
    	this.buttonWalletNew.Size = new System.Drawing.Size(100, 34);
    	this.buttonWalletNew.TabIndex = 10;
    	this.buttonWalletNew.Text = "New...";
    	this.buttonWalletNew.UseVisualStyleBackColor = true;
    	this.buttonWalletNew.Click += new System.EventHandler(this.ButtonWalletNewClick);
    	// 
    	// buttonSave
    	// 
    	this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    	this.buttonSave.Location = new System.Drawing.Point(315, 17);
    	this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
    	this.buttonSave.Name = "buttonSave";
    	this.buttonSave.Size = new System.Drawing.Size(108, 34);
    	this.buttonSave.TabIndex = 11;
    	this.buttonSave.Text = "Save...";
    	this.buttonSave.UseVisualStyleBackColor = true;
    	this.buttonSave.Click += new System.EventHandler(this.Button1Click);
    	// 
    	// buttonExit
    	// 
    	this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    	this.buttonExit.Location = new System.Drawing.Point(303, 282);
    	this.buttonExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
    	this.buttonExit.Name = "buttonExit";
    	this.buttonExit.Size = new System.Drawing.Size(120, 32);
    	this.buttonExit.TabIndex = 12;
    	this.buttonExit.Text = "Exit";
    	this.buttonExit.UseVisualStyleBackColor = true;
    	this.buttonExit.Click += new System.EventHandler(this.ButtonExitClick);
    	// 
    	// MainForm
    	// 
    	this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
    	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    	this.ClientSize = new System.Drawing.Size(439, 329);
    	this.Controls.Add(this.buttonExit);
    	this.Controls.Add(this.buttonSave);
    	this.Controls.Add(this.buttonWalletNew);
    	this.Controls.Add(this.buttonReport);
    	this.Controls.Add(this.textBoxBalance);
    	this.Controls.Add(this.labelBalance);
    	this.Controls.Add(this.buttonSubtract);
    	this.Controls.Add(this.buttonAdd);
    	this.Controls.Add(this.textBoxOperation);
    	this.Controls.Add(this.labelOperation);
    	this.Controls.Add(this.buttonWalletOpen);
    	this.Controls.Add(this.textBoxWallet);
    	this.Controls.Add(this.labelWallet);
    	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
    	this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
    	this.MaximizeBox = false;
    	this.MinimizeBox = false;
    	this.Name = "MainForm";
    	this.Text = "EasyBlockchain";
    	this.Shown += new System.EventHandler(this.MainFormShown);
    	this.ResumeLayout(false);
    	this.PerformLayout();

    }
    
    #if TRIAL
    public void CheckRuns() {
		try {
			RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\OVG-Developers", true);
			
			int runs = -1;
			
			if (key != null && key.GetValue("Runs") != null) {
				runs = (int) key.GetValue("Runs");
			} else {
				key = Registry.CurrentUser.CreateSubKey("Software\\OVG-Developers");
			}
			
			runs = runs + 1;
			
			key.SetValue("Runs", runs);
			
			if (runs > 10) {
				System.Windows.Forms.MessageBox.Show("Number of runs expired.\n"
							+ "Please register the application (visit https://ovg-developers.mystrikingly.com/ for purchase).");
				
				Environment.Exit(0);
			}
		} catch (Exception e) {
			Console.WriteLine(e.Message);
		}
	}
	
	public bool IsRegistered() {
		try {
			RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\OVG-Developers");
			
			if (key != null && key.GetValue("Registered") != null) {
				return true;
			}
		} catch (Exception e) {
			Console.WriteLine(e.Message);
		}
		
		return false;
	}
    #endif
    
		void MainFormShown(object sender, EventArgs e)
		{
			#if TRIAL
			if (!IsRegistered()) {
    			CheckRuns();
    		}
			#endif
		}
  }
}
