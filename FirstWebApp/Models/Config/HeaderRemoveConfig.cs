namespace FirstWebApp.Models.Config;

public class HeaderRemoveConfig
{
    public bool Enabled { get; set; }
    public string[] HeaderKeys { get; set; } = new string[0];
}
