using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace Arenda
{
    class ImageClass
    {
        public ImageClass()
        {
        }

        public Image ByteArrayToImage(byte[] ImageBytes)
        {
            //Подготовим временный поток для чтения массива байтов
            MemoryStream ImageStream = new MemoryStream(ImageBytes);

            //Создадим изображение из потока
            Image Image = Image.FromStream(ImageStream);

            //Закроем поток
            ImageStream.Close();

            //Вернем изображение
            return Image;
        }

        public byte[] ImageToByteArray(Image Image)
        {
            //Проверим, что изображение не нулевое
            if (Image == null)
            {
                throw new ArgumentNullException("Image");
            }

            //Подготовим поток для сохранения в него изображения
            MemoryStream ImageStream = new MemoryStream();

            //Сохраним изображение в этот поток
            Image.Save(ImageStream, System.Drawing.Imaging.ImageFormat.Png);

            //Считаем из потока байты
            byte[] ImageBytes = ImageStream.ToArray();

            //Закроем временный поток
            ImageStream.Close();

            //Вернем массив байтов
            return ImageBytes;
        }

    }
}
