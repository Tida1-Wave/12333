using System;
using System.Drawing;

namespace Captcha
{
    class Captcha
    {

        public Captcha()
        {
            //SetRandomizedCode();
            // SetBitmap();
        }

        public string CaptchaText;

        public Bitmap CreateImage(int Width, int Height)
        {
            Random rnd = new Random();

            //Создадим изображение
            Bitmap result = new Bitmap(Width, Height);

            //Вычислим позицию текста
            int Xpos = rnd.Next(50, Width - 100);
            int Ypos = rnd.Next(50, Height - 50);

            //Добавим различные цвета
            Brush[] colors = { Brushes.Black,
                     Brushes.Red,
                     Brushes.RoyalBlue,
                     Brushes.Green,
                     Brushes.Purple,
                     Brushes.Red,
            Brushes.Blue};

            Pen[] pens = { new Pen(Color.AliceBlue, 1),
                     new Pen(Color.Black, 2),
                     new Pen(Color.Azure, 2),
                     new Pen(Color.Green, 1),
                     new Pen(Color.Red, 1),
            new Pen(Color.Blue, 3),
            new Pen(Color.Yellow, 1),
            new Pen(Color.Pink, 3)
            };

            //Укажем где рисовать
            Graphics g = Graphics.FromImage((System.Drawing.Image)result);

            //Пусть фон картинки будет серым
            g.Clear(Color.LightGray);

            //Сгенерируем текст
            string text = String.Empty;
            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm@!-+<>#$%^&*()";
            for (int i = 0; i < 6; ++i)
                text += ALF[rnd.Next(ALF.Length)];

            CaptchaText = text;

            //Нарисуем сгенирируемый текст
            int x = 0;
            for (int i = 0; i < 6; ++i)
            {

                g.DrawString(text[i].ToString(),
                             new Font("Mont", 48),
                             colors[rnd.Next(colors.Length)],
                             new PointF(rnd.Next(x, x + 10), rnd.Next(30, Height - 160)));
                x += 60;
            }

            //Добавим немного помех
            /////Линии из углов
            g.DrawLine(pens[rnd.Next(pens.Length)],
                       new System.Drawing.Point(rnd.Next(-10, 150), rnd.Next(-10, 150)),
                       new System.Drawing.Point(Width - 1, Height - 1));
            g.DrawLine(pens[rnd.Next(pens.Length)],
                       new System.Drawing.Point(rnd.Next(-10, 150), rnd.Next(-10, 150)),
                       new System.Drawing.Point(rnd.Next(-100, 150), rnd.Next(-100, 150)));
            g.DrawLine(pens[rnd.Next(pens.Length)],
                       new System.Drawing.Point(rnd.Next(-10, 150), rnd.Next(-10, 150)),
                       new System.Drawing.Point(rnd.Next(-100, 150), rnd.Next(-100, 150)));
            g.DrawLine(pens[rnd.Next(pens.Length)],
                       new System.Drawing.Point(rnd.Next(-10, 150), rnd.Next(-10, 150)),
                       new System.Drawing.Point(rnd.Next(-100, 150), rnd.Next(-100, 150)));
            g.DrawLine(pens[rnd.Next(pens.Length)],
                       new System.Drawing.Point(rnd.Next(-10, 150), rnd.Next(-10, 150)),
                       new System.Drawing.Point(rnd.Next(-100, 150), rnd.Next(-100, 150)));
            g.DrawLine(pens[rnd.Next(pens.Length)],
                       new System.Drawing.Point(rnd.Next(-10, 150), rnd.Next(-10, 150)),
                       new System.Drawing.Point(rnd.Next(-100, 150), rnd.Next(-100, 150)));
            g.DrawLine(pens[rnd.Next(pens.Length)],
                      new System.Drawing.Point(rnd.Next(-100, 150), rnd.Next(-100, 150)),
                      new System.Drawing.Point(rnd.Next(-100, 150), rnd.Next(-100, 150)));
            g.DrawLine(pens[rnd.Next(pens.Length)],
                      new System.Drawing.Point(rnd.Next(-100, 150), rnd.Next(-100, 150)),
                      new System.Drawing.Point(rnd.Next(-100, 150), rnd.Next(-100, 150)));
            g.DrawLine(pens[rnd.Next(pens.Length)],
                      new System.Drawing.Point(rnd.Next(-100, 150), rnd.Next(-100, 150)),
                      new System.Drawing.Point(rnd.Next(-100, 150), rnd.Next(-100, 150)));
            g.DrawLine(pens[rnd.Next(pens.Length)],
                       new System.Drawing.Point(0, Height - 1),
                       new System.Drawing.Point(Width - 1, 0));
            ////Белые точки
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.DeepPink);
            return result;
        }
    }
}
