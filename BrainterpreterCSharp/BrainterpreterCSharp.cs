using System;
using System.IO;
using System.Collections.Generic;

public class BrainterpreterCSharp{
	private string m_sTape;
	private byte[] m_Mem;
	
	public BrainterpreterCSharp(string BFCode){
		m_sTape = BFCode;
		m_Mem = new byte[30000];
		for(int i = 0; i< 30000; ++i) m_Mem[i] = 0; // format memory
	}
	
	
	public void Interpret(){
		int ip = 0;		// instruction pointer
		int dp = 0;		// data pointer
		
		// Loop through instructions
		for(; ip < m_sTape.Length; ip++){
			
			// Reference:
			// http://en.wikipedia.org/wiki/Brainfuck
			switch(m_sTape[ip]){
				case '+':
					++m_Mem[dp];
					break;
				case '-':
					--m_Mem[dp];
					break;
				case '>':
					++dp;
					break;
				case '<':
					--dp;
					break;
				case '.':
					System.Console.Write(String.Format("{0}", (char) m_Mem[dp]));
					break;
				case ',':
					m_Mem[dp] = (byte) System.Console.Read();
					break;
				case '[':
					if (m_Mem[dp] == 0){
						int bc = 1;
						while(bc > 0){
							var a = m_sTape[++ip];
							if (a == '[')
								++bc;
							else if (a == ']')
								--bc;
						}
					}
					break;
				case ']':
					if (m_Mem[dp] != 0){
						int bc = 1;
						while(bc > 0){
							var a = m_sTape[--ip];
							if (a == '[')
								--bc;
							else if (a == ']')
								++bc;
						}
					}
					break;
				default:
					break;
				// Not proper token, ignore
			}
		}
	}
}



public class Program
{
	
	public static void Main(string[] args)
	{
		string hw = "++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.";
		string s = "";
		System.Console.WriteLine("Enter path of BrainF*** file: ");
		
		string path = System.Console.ReadLine();
		try{
			StreamReader sr = new StreamReader (path);
			s = sr.ReadToEnd();
		}
		catch (Exception e){
			System.Console.WriteLine(e.ToString());
			return;
		}
		
		BrainterpreterCSharp BCS = new BrainterpreterCSharp(s);
		BCS.Interpret();
		
	}
}