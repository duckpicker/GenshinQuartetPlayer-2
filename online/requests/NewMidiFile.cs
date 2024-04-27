namespace GenshinQuartetPlayer2.online.requests;

public class NewMidiFile : BaseRequest
{
    public byte[] FileBytes { get; set; }

    public NewMidiFile()
    {

    }

    public void ReadFile(string filePath)
    {
        FileBytes = File.ReadAllBytes(filePath);
    }
}