using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using KSP_Chroma_Control.ColorSchemes;
using Newtonsoft.Json;
using Corale.Colore.Razer.Keyboard;

namespace ChromaServer
{
    class Server
    {
        private static Dictionary<string, Key> translate = new Dictionary<string, Key>()
        {
            {"esc", Key.Escape },
            { "f1", Key.F1 },
            { "f2", Key.F2 },
            { "f3", Key.F3 },
            { "f4", Key.F4 },
            { "f5", Key.F5 },
            { "f6", Key.F6 },
            { "f7", Key.F7 },
            { "f8", Key.F8 },
            { "f9", Key.F9 },
            { "f10", Key.F10 },
            { "f11", Key.F11 },
            { "f12", Key.F12 },
            { "prtsc", Key.PrintScreen },
            { "scrlk", Key.Scroll },
            { "break", Key.Pause },
            {"`", Key.OemTilde },
            { "1", Key.D1 },
            { "2", Key.D2 },
            { "3", Key.D3 },
            { "4", Key.D4 },
            { "5", Key.D5 },
            { "6", Key.D6 },
            { "7", Key.D7 },
            { "8", Key.D8 },
            { "9", Key.D9 },
            { "0", Key.D0 },
            { "-", Key.OemMinus },
            { "=", Key.OemEquals },
            { "backspace", Key.Backspace },
            { "ins", Key.Insert },
            { "home", Key.Home },
            { "pageup", Key.PageUp },
            {"tab", Key.Tab },
            { "q", Key.Q },
            { "w", Key.W },
            { "e", Key.E },
            { "r", Key.R },
            { "t", Key.T },
            { "y", Key.Y },
            { "u", Key.U },
            { "i", Key.I },
            { "o", Key.O },
            { "p", Key.P },
            { "[", Key.OemLeftBracket },
            { "]", Key.OemRightBracket },
            { "enter", Key.Enter },
            { "del", Key.Delete },
            { "end", Key.End },
            { "pagedown", Key.PageDown },
            {"capslock", Key.CapsLock },
            { "a", Key.A },
            { "s", Key.S },
            {"d", Key.D },
            {"f", Key.F },
            {"g", Key.G },
            {"h", Key.H },
            {"j", Key.J },
            {"k", Key.K },
            {"l", Key.L },
            {";", Key.OemSemicolon },
            {"'", Key.OemApostrophe },
            {"hash", Key.EurPound },
            {"leftshift", Key.LeftShift },
            {"backslash", Key.EurBackslash },
            {"z", Key.Z },
            {"x", Key.X },
            {"c", Key.C },
            {"v", Key.V },
            {"b", Key.B },
            {"n", Key.N },
            {"m", Key.M },
            {",", Key.OemComma },
            {".", Key.OemPeriod },
            {"slash", Key.OemSlash },
            {"rightshift", Key.RightShift },
            {"up", Key.Up },
            {"leftctrl", Key.LeftControl },
            {"windows", Key.LeftWindows },
            {"alt", Key.LeftAlt },
            {"space", Key.Space },
            {"altgr", Key.RightAlt },
            {"fn", Key.Function },
            {"contextmenu", Key.RightMenu },
            {"rightctrl", Key.RightControl },
            {"left", Key.Left },
            {"down", Key.Down },
            {"right", Key.Right },

            /// Numpad
            {"numlk", Key.NumLock },
            {"num/", Key.NumDivide },
            {"num*", Key.NumMultiply },
            {"num-", Key.NumSubtract },
            {"num7", Key.Num7 },
            {"num8", Key.Num8 },
            {"num9", Key.Num9 },
            {"num+", Key.NumAdd },
            {"num4", Key.Num4 },
            {"num5", Key.Num5 },
            {"num6", Key.Num6 },
            {"num1", Key.Num1 },
            {"num2", Key.Num2 },
            {"num3", Key.Num3 },
            {"numenter", Key.NumEnter },
            {"num0", Key.Num0 },
            {"num.", Key.NumDecimal },

            /// Macro keys
            {"m1", Key.Macro1 },
            {"m2", Key.Macro2 },
            {"m3", Key.Macro3 },
            {"m4", Key.Macro4 },
            {"m5", Key.Macro5 }
        };

        private static System.Drawing.Color ConvertToSystemColor(Color32 color)
        {
            return System.Drawing.ColorTranslator.FromHtml("#" + color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2"));
        }

        UdpClient UdpServer = new UdpClient(8888);
        IPEndPoint RemoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
        private Boolean quit;

        public void Start()
        {
            this.quit = false;

            while (!quit)
            {
                var data = UdpServer.Receive(ref this.RemoteEP);
                string json = Encoding.UTF8.GetString(data);

                try
                {
                    Dictionary<string, string> colors = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                    ColorScheme colorScheme = ColorScheme.createFromDictionary(colors);
                    applyToKeyboard(colorScheme);

                }
                catch (JsonReaderException jre)
                {
                    Console.Error.WriteLine(jre.Message);
                    Console.Error.WriteLine(jre.StackTrace);
                    Console.Error.WriteLine(json);
                }
            }
        }

        public void Stop()
        {
            this.quit = true;
        }

        private void applyToKeyboard(ColorScheme colorScheme)
        {
            foreach (KeyValuePair<string, Color> entry in colorScheme)
            {
                if (translate.ContainsKey(entry.Key))
                {
                    System.Drawing.Color keyColor = ConvertToSystemColor(entry.Value);
                    Corale.Colore.Core.Keyboard.Instance.SetKey(translate[entry.Key], Corale.Colore.Core.Color.FromSystemColor(keyColor));
                } else
                {
                    Console.Error.WriteLine("Unknown Key: " + entry.Key);
                }
            }
        }
    }
}
