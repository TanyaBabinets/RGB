using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Задание 8.3
//Создайте структуру «RGB цвет». Определите внутри неё необходимые поля и методы. Реализуйте следующую
//функциональность:
//■ Перевод в HEX формат;
//■ Перевод в HSL;
//■ Перевод в CMYK.
namespace RGB
{
    struct ColorRGB
    {
    public int Red { get; }
    public int Green { get; }
    public int Blue { get; }

    public ColorRGB(int red, int green, int blue)
    {
        Red = red;
        Green = green;
        Blue = blue;
    }

        public string ToHex()
    { 
        string hex = Red.ToString("X2") + Green.ToString("X2") + Blue.ToString("X2");
        return hex;
    }

    public void ToHSL(out double hue, out double saturation, out double lightness)
    {
        double r = (double)Red / 255;
        double g = (double)Green / 255;
        double b = (double)Blue / 255;

        double cmax = Math.Max(Math.Max(r, g), b);
        double cmin = Math.Min(Math.Min(r, g), b);
        double delta = cmax - cmin;

        if (delta == 0)
        {
            hue = 0;
        }
        else if (cmax == r)
        {
            hue = ((g - b) / delta) % 6;
        }
        else if (cmax == g)
        {
            hue = ((b - r) / delta) + 2;
        }
        else // cmax == b
        {
            hue = ((r - g) / delta) + 4;
        }

        hue = hue * 60;
        if (hue < 0)
        {
            hue += 360;
        }

        lightness = (cmax + cmin) / 2;

        if (delta == 0)
        {
            saturation = 0;
        }
        else
        {
            saturation = delta / (1 - Math.Abs(2 * lightness - 1));
        }
    }

    public void ToCMYK(out double cyan, out double magenta, out double yellow, out double key)
    {
        double r = (double)Red / 255;
        double g = (double)Green / 255;
        double b = (double)Blue / 255;

        double k = 1 - Math.Max(Math.Max(r, g), b);
        double c = (1 - r - k) / (1 - k);
        double m = (1 - g - k) / (1 - k);
        double y = (1 - b - k) / (1 - k);

        cyan = c;
        magenta = m;
        yellow = y;
        key = k;
    }

        public override string ToString()
        {
            return $"Color RGB = {Red}, {Green}, {Blue}";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        ColorRGB color = new ColorRGB(255, 0, 0);
            Console.WriteLine(color);
        string hex = color.ToHex(); 
            Console.WriteLine($"HEX = {hex}") ;
          
            color.ToHSL(out double hue, out double saturation, out double lightness); // hue = 0, saturation = 1, lightness = 0.5
            Console.WriteLine($"HSL = Hue = {hue}, saturation = {saturation}, lightness = {lightness} ");
            color.ToCMYK(out double cyan, out double magenta, out double yellow, out double key); // cyan = 0, magenta = 1, yellow = 1, key = 0
            Console.WriteLine($"CMYK = {cyan}, {magenta}, {yellow}, {key}");
        }
    }
}
