using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;

public sealed class Highscore
{
    public List<HighscoreEntry> highscoreEntries = new List<HighscoreEntry>();

    public Highscore()
    {
    }

    public static Highscore GetHighscore()
    {
        return LoadHighscoreFromXML();
    }

    public void AddNewHighscoreEntry(string name, float points)
    {
        highscoreEntries.Add(new HighscoreEntry() { entryName = name, totalPoints = points });
        highscoreEntries = highscoreEntries.OrderByDescending(element => element.totalPoints).ToList();
        SaveHighscoreToXML(this);
    }

    private static Highscore LoadHighscoreFromXML()
    {
        Highscore score = new Highscore();
        
        if (File.Exists("Temp/Highscore.xml"))
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(Highscore));
            TextReader reader = new StreamReader("Temp/Highscore.xml");
            score = deserializer.Deserialize(reader) as Highscore;
            reader.Close();
        }
        return score;
    }

    private static void SaveHighscoreToXML(Highscore highscore)
    {
        if (!Directory.Exists("Temp"))
            Directory.CreateDirectory("Temp");
        XmlSerializer serializer = new XmlSerializer(typeof(Highscore));
        TextWriter writer = new StreamWriter("Temp/Highscore.xml");
        serializer.Serialize(writer, highscore);
        writer.Close();
    }
}
