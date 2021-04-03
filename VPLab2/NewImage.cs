using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace VPLab2
{
    static class ImageEdit
    { 
        public static void NewImage(PictureBox p)
        {
            ImageEdit.QSaveFile(p);
            Bitmap pic = new Bitmap(p.Width, p.Height);
            p.Image = pic;            
        }

        public static void SaveFile(PictureBox p)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*gif|PNG Image|*.png";
            saveFile.Title = "Savve an Image File";
            saveFile.FilterIndex = 4;
            saveFile.ShowDialog();

            if (saveFile.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFile.OpenFile();

                switch (saveFile.FilterIndex)
                {
                    case 1: p.Image.Save(fs, ImageFormat.Jpeg);
                        break;
                    case 2:
                        p.Image.Save(fs, ImageFormat.Bmp);
                        break;
                    case 3:
                        p.Image.Save(fs, ImageFormat.Gif);
                        break;
                    case 4:
                        p.Image.Save(fs, ImageFormat.Png);
                        break;
                }
                fs.Close();
            }
        }

        public static void OpenFile(PictureBox p)
        {
            ImageEdit.QSaveFile(p);
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*gif|PNG Image|*.png";
            openFile.Title = "Open an Image File";
            openFile.FilterIndex = 1;

            if (openFile.ShowDialog() != DialogResult.Cancel)
            {
                p.SizeMode = PictureBoxSizeMode.AutoSize;
                p.Load(openFile.FileName);
                p.AutoSize = true;
            }
        }

        public static void QSaveFile(PictureBox p)
        {
            if (p.Image != null)
            {
                var result = MessageBox.Show("Сохранить текущее изображение перед созданием нового?", "Предупреждение!", MessageBoxButtons.YesNoCancel);

                switch (result)
                {
                    case DialogResult.No: break;
                    case DialogResult.Yes: ImageEdit.SaveFile(p); break;
                    case DialogResult.Cancel: return;
                }
            }
        }
    }
}
