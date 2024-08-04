
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

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
      ComponentResourceManager resources = new ComponentResourceManager(typeof (MainForm));
      this.openFileDialog = new OpenFileDialog();
      this.saveFileDialog = new SaveFileDialog();
      this.labelWallet = new Label();
      this.textBoxWallet = new TextBox();
      this.buttonWalletOpen = new Button();
      this.labelOperation = new Label();
      this.textBoxOperation = new TextBox();
      this.buttonAdd = new Button();
      this.buttonSubtract = new Button();
      this.saveFileDialogReport = new SaveFileDialog();
      this.labelBalance = new Label();
      this.textBoxBalance = new TextBox();
      this.buttonReport = new Button();
      this.buttonWalletNew = new Button();
      this.buttonSave = new Button();
      this.buttonExit = new Button();
      this.SuspendLayout();
      this.openFileDialog.Filter = "Wallet files (*.wallet)|*.wallet|All files|*.*";
      this.saveFileDialog.Filter = "Wallet files (*.wallet)|*.wallet|All files|*.*";
      this.labelWallet.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.labelWallet.Location = new Point(12, 50);
      this.labelWallet.Name = "labelWallet";
      this.labelWallet.Size = new Size(61, 23);
      this.labelWallet.TabIndex = 0;
      this.labelWallet.Text = "Wallet:";
      this.textBoxWallet.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.textBoxWallet.Location = new Point(12, 76);
      this.textBoxWallet.Name = "textBoxWallet";
      this.textBoxWallet.ReadOnly = true;
      this.textBoxWallet.Size = new Size(305, 23);
      this.textBoxWallet.TabIndex = 1;
      this.buttonWalletOpen.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonWalletOpen.Location = new Point(155, 14);
      this.buttonWalletOpen.Name = "buttonWalletOpen";
      this.buttonWalletOpen.Size = new Size(75, 28);
      this.buttonWalletOpen.TabIndex = 2;
      this.buttonWalletOpen.Text = "Open...";
      this.buttonWalletOpen.UseVisualStyleBackColor = true;
      this.buttonWalletOpen.Click += new EventHandler(this.ButtonWalletOpenClick);
      this.labelOperation.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.labelOperation.Location = new Point(12, 153);
      this.labelOperation.Name = "labelOperation";
      this.labelOperation.Size = new Size(100, 23);
      this.labelOperation.TabIndex = 3;
      this.labelOperation.Text = "Operation:";
      this.textBoxOperation.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.textBoxOperation.Location = new Point(12, 179);
      this.textBoxOperation.Name = "textBoxOperation";
      this.textBoxOperation.Size = new Size(137, 23);
      this.textBoxOperation.TabIndex = 4;
      this.textBoxOperation.Text = "0.00";
      this.textBoxOperation.TextAlign = HorizontalAlignment.Center;
      this.buttonAdd.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonAdd.Location = new Point(155, 179);
      this.buttonAdd.Name = "buttonAdd";
      this.buttonAdd.Size = new Size(66, 23);
      this.buttonAdd.TabIndex = 5;
      this.buttonAdd.Text = "Add";
      this.buttonAdd.UseVisualStyleBackColor = true;
      this.buttonAdd.Click += new EventHandler(this.ButtonAddClick);
      this.buttonSubtract.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonSubtract.Location = new Point(227, 179);
      this.buttonSubtract.Name = "buttonSubtract";
      this.buttonSubtract.Size = new Size(90, 23);
      this.buttonSubtract.TabIndex = 6;
      this.buttonSubtract.Text = "Subtract";
      this.buttonSubtract.UseVisualStyleBackColor = true;
      this.buttonSubtract.Click += new EventHandler(this.ButtonSubtractClick);
      this.saveFileDialogReport.Filter = "CSV files (*.csv)|*.csv|All files| *.*";
      this.labelBalance.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.labelBalance.Location = new Point(12, 115);
      this.labelBalance.Name = "labelBalance";
      this.labelBalance.Size = new Size(83, 23);
      this.labelBalance.TabIndex = 7;
      this.labelBalance.Text = "Balance:";
      this.textBoxBalance.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.textBoxBalance.Location = new Point(101, 115);
      this.textBoxBalance.Name = "textBoxBalance";
      this.textBoxBalance.ReadOnly = true;
      this.textBoxBalance.Size = new Size(216, 26);
      this.textBoxBalance.TabIndex = 8;
      this.textBoxBalance.TextAlign = HorizontalAlignment.Right;
      this.buttonReport.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonReport.Location = new Point(117, 229);
      this.buttonReport.Name = "buttonReport";
      this.buttonReport.Size = new Size(104, 26);
      this.buttonReport.TabIndex = 9;
      this.buttonReport.Text = "Report...";
      this.buttonReport.UseVisualStyleBackColor = true;
      this.buttonReport.Click += new EventHandler(this.ButtonReportClick);
      this.buttonWalletNew.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonWalletNew.Location = new Point(74, 13);
      this.buttonWalletNew.Name = "buttonWalletNew";
      this.buttonWalletNew.Size = new Size(75, 28);
      this.buttonWalletNew.TabIndex = 10;
      this.buttonWalletNew.Text = "New...";
      this.buttonWalletNew.UseVisualStyleBackColor = true;
      this.buttonWalletNew.Click += new EventHandler(this.ButtonWalletNewClick);
      this.buttonSave.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonSave.Location = new Point(236, 14);
      this.buttonSave.Name = "buttonSave";
      this.buttonSave.Size = new Size(81, 28);
      this.buttonSave.TabIndex = 11;
      this.buttonSave.Text = "Save...";
      this.buttonSave.UseVisualStyleBackColor = true;
      this.buttonSave.Click += new EventHandler(this.Button1Click);
      this.buttonExit.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonExit.Location = new Point(227, 229);
      this.buttonExit.Name = "buttonExit";
      this.buttonExit.Size = new Size(90, 26);
      this.buttonExit.TabIndex = 12;
      this.buttonExit.Text = "Exit";
      this.buttonExit.UseVisualStyleBackColor = true;
      this.buttonExit.Click += new EventHandler(this.ButtonExitClick);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(329, 267);
      this.Controls.Add((Control) this.buttonExit);
      this.Controls.Add((Control) this.buttonSave);
      this.Controls.Add((Control) this.buttonWalletNew);
      this.Controls.Add((Control) this.buttonReport);
      this.Controls.Add((Control) this.textBoxBalance);
      this.Controls.Add((Control) this.labelBalance);
      this.Controls.Add((Control) this.buttonSubtract);
      this.Controls.Add((Control) this.buttonAdd);
      this.Controls.Add((Control) this.textBoxOperation);
      this.Controls.Add((Control) this.labelOperation);
      this.Controls.Add((Control) this.buttonWalletOpen);
      this.Controls.Add((Control) this.textBoxWallet);
      this.Controls.Add((Control) this.labelWallet);
      this.Icon = (Icon) resources.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MainForm";
      this.Text = "EasyBlockchain";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
