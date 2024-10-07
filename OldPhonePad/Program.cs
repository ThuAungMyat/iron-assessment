using System;
using System.Text;

public class OldPhonePadConverter
{
	private static readonly string[] keypadMapping =
	{
		" ", // 0
		"", // 1 (no letters assigned)
		"ABC", // 2
		"DEF", // 3
		"GHI", // 4
		"JKL", // 5
		"MNO", // 6
		"PQRS", // 7
		"TUV", // 8
		"WXYZ" // 9
	};
	public static string OldPhonePad(string input)
	{
		StringBuilder output = new StringBuilder();
		int currentButton = -1;
		int pressCount = 0;
		foreach (char ch in input)
		{
			switch (ch)
			{
				case ' ':
					if (currentButton != -1 && keypadMapping[currentButton].Length > 0)
					{
						int index = (pressCount - 1) % keypadMapping[currentButton].Length;
						output.Append(keypadMapping[currentButton][index]);
					}

					currentButton = -1;
					pressCount = 0;
					break;
				case '*':
					string val = ExtractValue(input);
					if (val.Length > 0)
					{
						return output + OldPhonePad(val + input[input.Length - 1]);
					}

					return output.Length > 0 ? output.ToString() : "";
				case '#':
					if (currentButton != -1 && keypadMapping[currentButton].Length > 0)
					{
						int index = (pressCount - 1) % keypadMapping[currentButton].Length;
						output.Append(keypadMapping[currentButton][index]);
					}

					return output.ToString();
				default:
					if (ch >= '0' && ch <= '9')
					{
						int button = ch - '0';
						if (button == currentButton)
						{
							pressCount++;
						}
						else
						{
							if (currentButton != -1 && keypadMapping[currentButton].Length > 0)
							{
								int index = (pressCount - 1) % keypadMapping[currentButton].Length;
								output.Append(keypadMapping[currentButton][index]);
							}

							currentButton = button;
							pressCount = 1;
						}
					}

					break;
			}
		}

		return output.ToString();
	}

	static string ExtractValue(string input)
	{
		int startIndex = input.IndexOf('*');
		int endIndex = input.IndexOf('#');
		if (startIndex != -1 && endIndex != -1 && startIndex < endIndex)
		{
			string result = input.Substring(startIndex + 1, endIndex - startIndex - 1);
			return result;
		}

		return string.Empty;
	}

	public static void Main(string[] args)
	{
		// Example test cases
		Console.WriteLine(OldPhonePad("33#")); // Expected Output: E
		Console.WriteLine(OldPhonePad("227*#")); // Expected Output: B
		Console.WriteLine(OldPhonePad("4433555 555666 #")); // Expected Output: HELLO
		Console.WriteLine(OldPhonePad("9 666 777 5553#")); // Expected Output: WORLD
		Console.WriteLine(OldPhonePad("222 2 22#")); // Output: CAB
		Console.WriteLine(OldPhonePad("8 88777444666*664#")); // Expected Output: TURING
		Console.WriteLine(OldPhonePad("664#")); // Expected Output: NG
		Console.WriteLine(OldPhonePad("8877744466#")); // Expected Output: URIN
	}
}