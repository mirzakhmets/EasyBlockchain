
using System.Collections.Generic;
using System.IO;

namespace EasyBlockchain
{
  public class Wallet
  {
    public Blockchain Start = (Blockchain) null;
    public List<Blockchain> Chain = new List<Blockchain>();

    public Wallet() {
    	this.Start = new Blockchain();
    }

    public Wallet(StreamReader streamReader)
    {
      this.Start = new Blockchain(Blockchain.EncryptDecrypt(streamReader.ReadLine().Trim()));
      while (!streamReader.EndOfStream)
        this.Chain.Add(new Blockchain(Blockchain.EncryptDecrypt(streamReader.ReadLine().Trim())));
      streamReader.Close();
    }

    public void Save(StreamWriter streamWriter)
    {
      streamWriter.WriteLine(Blockchain.EncryptDecrypt(this.Start.Value));
      foreach (Blockchain blockchain in this.Chain)
        streamWriter.WriteLine(Blockchain.EncryptDecrypt(blockchain.Value));
      streamWriter.Close();
    }

    public float Balance()
    {
      return this.Chain.Count == 0 || this.Start == null ? 0.0f : this.Chain[checked (this.Chain.Count - 1)].Subtract(this.Start);
    }
  }
}
