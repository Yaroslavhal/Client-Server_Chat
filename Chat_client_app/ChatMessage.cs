using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Chat_client_app
{
    public class ChatMessage
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public string Photo { get; set; }
        public byte[] Serialize()
        {
            using (var m = new MemoryStream()) // в оперативну пам'ять
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(UserId);
                    writer.Write(UserName);
                    writer.Write(Text);
                    writer.Write(Photo);
                }
                return m.ToArray();

            }
        }

        public static ChatMessage Desserialize(byte[] data)
        {
            ChatMessage message = new ChatMessage();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    message.UserId = reader.ReadString();
                    message.UserName = reader.ReadString();
                    message.Text = reader.ReadString();
                    message.Photo = reader.ReadString();
                }
            }
            return message;
        }
        public static BitmapImage ToBitmapImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {

                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;//CacheOption must be set after BeginInit()
                img.StreamSource = ms;
                img.EndInit();

                if (img.CanFreeze)
                {
                    img.Freeze();
                }
                return img;
            }
        }
    }
}