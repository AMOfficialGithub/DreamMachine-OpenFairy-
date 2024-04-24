using System;
using System.Collections.Generic;

public class FantasyConsole
{
    private List<string> screen;
    private Dictionary<string, double> sound;
    private List<string> inputBuffer;

    public FantasyConsole()
    {
        screen = new List<string>();
        for (int i = 0; i < 20; i++)
        {
            screen.Add(new string(' ', 40));
        }

        sound = new Dictionary<string, double>
        {
            {"C", 261.63},
            {"D", 293.66},
            {"E", 329.63},
            {"F", 349.23},
            {"G", 392.00},
            {"A", 440.00},
            {"B", 493.88}
        };

        inputBuffer = new List<string>();
    }

    public void RunProgram(string program)
    {
        string[] lines = program.Split('\n');
        foreach (string line in lines)
        {
            ExecuteLine(line);
        }
    }

    private void ExecuteLine(string line)
    {
        string[] tokens = line.Split(' ');

        switch (tokens[0])
        {
            case "CLS":
                ClearScreen();
                break;
            case "PRINT":
                PrintText(string.Join(" ", tokens[1..]));
                break;
            case "INPUT":
                Input();
                break;
            case "PLAY":
                PlaySound(tokens[1]);
                break;
        }
    }

    private void ClearScreen()
    {
        for (int i = 0; i < screen.Count; i++)
        {
            screen[i] = new string(' ', 40);
        }
    }

    private void PrintText(string text)
    {
        int row = screen.Count / 2;
        int col = (40 - text.Length) / 2;
        screen[row] = screen[row].Substring(0, col) + text + screen[row].Substring(col + text.Length);
    }

    private void Input()
    {
        string userInput = Console.ReadLine();
        inputBuffer.Add(userInput);
    }

    private void PlaySound(string note)
    {
        if (sound.ContainsKey(note))
        {
            Console.WriteLine($"Playing note {note} ({sound[note]} Hz)");
            // Here you can add sound generation code using libraries like `NAudio`
        }
    }

    public void DisplayScreen()
    {
        foreach (string row in screen)
        {
            Console.WriteLine(row);
        }
    }

    public string GetInput()
    {
        if (inputBuffer.Count > 0)
        {
            string input = inputBuffer[0];
            inputBuffer.RemoveAt(0);
            return input;
        }
        return "";
    }
}

class Program
{
    static void Main()
    {
        FantasyConsole console = new FantasyConsole();
        string program = @"
CLS
PRINT Hello, Fantasy Console!
INPUT
PRINT You entered: 
PRINT [INPUT]
PLAY C
";
        console.RunProgram(program);
        console.DisplayScreen();
    }
}
