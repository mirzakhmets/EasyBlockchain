
using System;

namespace EasyBlockchain
{
  public class Blockchain
  {
    public const int CHAIN_LENGTH_BYTES = 16;
    public const string CHAIN_KEY = "abcdef1020304060abcdef1020304060";
    public static Random random = new Random(DateTime.Now.Millisecond);
    public string Value = (string) null;

    public static int valueOf(char c)
    {
      if (c >= 'a' && c <= 'f')
        return checked (10 + (int) c - 97);
      if (c >= 'A' && c <= 'F')
        return checked (10 + (int) c - 65);
      return c >= '0' && c <= '9' ? checked ((int) c - 48) : -1;
    }

    public static string EncryptDecrypt(string s)
    {
      string str = "";
      int index1 = 0;
      while (index1 < s.Length)
      {
        int index2 = Blockchain.valueOf(s[index1]) ^ Blockchain.valueOf("abcdef1020304060abcdef1020304060"[index1]);
        str += "0123456789abcdef"[index2];
        checked { ++index1; }
      }
      return str;
    }

    public Blockchain()
    {
      this.Value = "";
      int num = 0;
      while (num < 32)
      {
        this.Value += "0123456789abcdef"[Blockchain.random.Next(16)];
        checked { ++num; }
      }
    }

    public Blockchain(string Value) {
    	this.Value = Value;
    }

    public Blockchain Operation(float f)
    {
      int num1 = checked ((int) unchecked ((double) f * 100.0));
      string str = "";
      int num2 = num1;
      int index1 = 0;
      while (index1 < this.Value.Length)
      {
        int index2 = checked (Blockchain.valueOf(this.Value[index1]) + num2);
        num2 = 0;
        while (index2 < 0)
        {
          checked { index2 += 16; }
          checked { --num2; }
        }
        while (index2 >= 16)
        {
          checked { index2 -= 16; }
          checked { ++num2; }
        }
        str += "0123456789abcdef"[index2];
        checked { ++index1; }
      }
      return new Blockchain(str);
    }

    public bool Less(Blockchain bc)
    {
      int index = checked (this.Value.Length - 1);
      while (index >= 0)
      {
        if (Blockchain.valueOf(this.Value[index]) != Blockchain.valueOf(bc.Value[index]))
          return Blockchain.valueOf(this.Value[index]) < Blockchain.valueOf(bc.Value[index]);
        checked { --index; }
      }
      return false;
    }

    public float Subtract(Blockchain bc)
    {
      float num1 = 0.0f;
      float num2 = 1f;
      int num3 = 0;
      if (this.Less(bc))
        return -bc.Subtract(this);
      int index = 0;
      while (index < this.Value.Length)
      {
        int num4 = checked (Blockchain.valueOf(this.Value[index]) - Blockchain.valueOf(bc.Value[index]) + num3);
        num3 = 0;
        while (num4 < 0)
        {
          checked { num4 += 16; }
          checked { --num3; }
        }
        while (num4 >= 16)
        {
          checked { num4 -= 16; }
          checked { ++num3; }
        }
        num1 += num2 * (float) num4;
        num2 *= 16f;
        checked { ++index; }
      }
      return num1 / 100f;
    }
  }
}
