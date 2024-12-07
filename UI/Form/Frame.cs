namespace Main_Practice.UI.Form;

using Configuration;

public class Frame
{
    private int _width;
    private int _height;
    private int _x;
    private int _y;

    private Frame () : this(Config.FormWidth, Config.FormHeight, Config.PosX, Config.PosY) { }
    
    public Frame(int width, int height, int x, int y)
    {
        _width = width;
        _height = height;
        _x = Config.PosX + x;
        _y = Config.PosY + y;
    }
    
}