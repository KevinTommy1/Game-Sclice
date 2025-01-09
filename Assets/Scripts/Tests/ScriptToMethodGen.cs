//"aaa bbb ccc" as input creates following output

// "using UnityEngine;
// 
// public class aaa
// {
// 	internal string output = "";
// 	internal aaa(string input)
// 	{
// 		this.output = input;
// 	}
// 
// 	public aaa bbb()
// 	{
// 		output += "bbb";
// 		return this;
// 	}
// 
// 	public aaa ccc()
// 	{
// 		output += "ccc";
// 		return this;
// 	}
// }
// 
// public class CreateString : MonoBehaviour
// {
// 	private void Start()
// 	{
// 		aaa aaa = new aaa("aaa");
//		aaa.bbb().ccc();
// 		Debug.Log( aaa.output);
// 	}
// }"

using System;

public class ScriptToMethodGen
{
	private string finalString;
	// in case of the example funnyCommand would be "aaa.bbb().ccc()"
	private string funnyCommand;

	public ScriptToMethodGen()
	{
		throw new ArgumentException("no input parameter given");
	}

	public ScriptToMethodGen(string initialString)
	{
		string[] words = initialString.Split(' ');
		int currentWordIndex = 0;
		finalString += CreateBeginningBoilerplate(words[currentWordIndex]);

		// increment current word because it was used in CreateBeginningBoilerplate()
		for (currentWordIndex++; currentWordIndex < words.Length; currentWordIndex++ )
		{
			finalString += CreateMethod(words[0],words[currentWordIndex]);
		}
		
		// close of {words[0]} class
		finalString = finalString.Substring(0, finalString.Length - 1);
		finalString += "}\n\n";

		// create instance of {words[0]}, run funny command and output
		finalString += CreateCreateStringClass(words[0]);
		funnyCommand = CreateFunnyCommand(words);
		finalString += CreateCodeWithFunnyCommand(funnyCommand);
		finalString += CreateCodeOutputOfFunnyCommand(words[0]);
		
		// close of CreateString class
		finalString += "	}\n}";
	}

	private string CreateBeginningBoilerplate(string firstWord)
	{
		return "using UnityEngine;\n" +
		       "\n" +
		       "public class " + firstWord + "\n" +
		       "{\n" +
		       "	internal string output = \"\";\n" +
		       "	internal" + firstWord + "(string input)\n" +
		       "	{\n" +
		       "		this.output = input;\n" +
		       "	}\n\n"
		       ;
	}

	private string CreateMethod(string firstWord, string currentWord)
	{
		return "	public " + firstWord + " " + currentWord + "()\n" +
		       "	{\n" +
		       "		output += \"" + currentWord + "\";\n" +
		       "	}\n\n";
	}

	private string CreateCreateStringClass(string firstWord)
	{
		return "public class CreateString : MonoBehaviour\n" +
		       "{\n" +
		       $"	{firstWord} {firstWord} = new {firstWord}(\"{firstWord}\")\n"
		       ;
	}

	// in case of the example returnValue would be "aaa.bbb().ccc()"

	private string CreateFunnyCommand(string[] words)
	{
		string returnValue = $"{words[0]}.{words[1]}()";
		for (int i = 2; i < words.Length; i++)
		{
			returnValue += $".{words[i]}()";
		}
		return returnValue;
	}

	private string CreateCodeWithFunnyCommand(string funnyCommand)
	{
		return $"	{funnyCommand};\n";
	}

	private string CreateCodeOutputOfFunnyCommand(string firstWord)
	{
		return $"	Debug.Log(\"{firstWord}.output\");\n";
	}
}
