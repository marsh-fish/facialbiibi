using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using DlibDotNet;
using DlibDotNet.Extensions;
using Dlib = DlibDotNet.Dlib;
using Point = DlibDotNet.Point;

namespace facialbiibi
{
    public partial class Form1 : Form
    {
        public string inputFilePath = new string(new char[] { });
        List<Point> point = new List<Point>();
        List<Point> pointr = new List<Point>();
        int[] bili = new int[5];
        int[] bilir = new int[5];
        double p1_angle1;
        double p1_angle2;
        double p1_angle3;
        double p2_angle1;
        double p2_angle2;
        double p2_angle3;
        double p1_faceangle;
        double p2_faceangle;
        double p1_noseangle;
        double p2_noseangle;
        double p1_eyemouthangle;
        double p2_eyemouthangle;
        double p1_eyemouthangle_2;
        double p2_eyemouthangle_2;
        double p1_headangle;
        double p2_headangle;
        double p1_eyenoseangle;
        double p2_eyenoseangle;
        double p1_eyenoseangle_2;
        double p2_eyenoseangle_2;
        int flag=0;
        Vector line1 = new Vector(0, 0);
        Vector line2 = new Vector(0, 0);
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //open
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Open picture1";
            fdlg.Filter = "Not All Files (.jpg)|*.*|Whatever you want is not here.(*.*)|*.*";
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                point.Clear();
                label3.Text = "人臉比例:";
                label13.Text = null;
                picture1.Image = Image.FromFile(fdlg.FileName);                
                inputFilePath = fdlg.FileName;
                MessageBox.Show(inputFilePath);
                using (var fd = Dlib.GetFrontalFaceDetector())
                using (var sp = ShapePredictor.Deserialize("./shape_predictor_68_face_landmarks.dat"))
                {
                    var img = Dlib.LoadImage<RgbPixel>(inputFilePath);

                    var faces = fd.Operator(img);
                    if (faces.Length > 1)
                    {
                        MessageBox.Show("只能有一個臉啦，你個白癡");
                        flag = 1;
                        picture1.Image = null;
                    }
                    else
                    {
                        flag++;
                        foreach (var face in faces)
                        {
                            var shape = sp.Detect(img, face);
                            for (var i = 0; i < shape.Parts; i++)
                            {
                                point.Add(shape.GetPart((uint)i));
                                var rect = new DlibDotNet.Rectangle(point[i]);
                                //Dlib.DrawRectangle(img, rect, color: new RgbPixel(255, 255, 0), thickness: 2);
                            }
                        }
                        picture1.Image = img.ToBitmap<RgbPixel>();
                        bili[0] = Math.Abs(point[0].X - point[36].X);
                        bili[1] = Math.Abs(point[36].X - point[39].X);
                        bili[2] = Math.Abs(point[39].X - point[42].X);
                        bili[3] = Math.Abs(point[42].X - point[45].X);
                        bili[4] = Math.Abs(point[45].X - point[16].X);
                        label3.Text += ((double)bili[0] / bili.Min()).ToString("f2") + " : " + ((double)bili[1] / bili.Min()).ToString("f2") + " : " + ((double)bili[2] / bili.Min()).ToString("f2") + " : " + ((double)bili[3] / bili.Min()).ToString("f2") + " : " + ((double)bili[4] / bili.Min()).ToString("f2");

                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Open picture1";
            fdlg.Filter = "Not All Files (.jpg)|*.*|Whatever you want is not here.(*.*)|*.*";
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                pointr.Clear();
                label4.Text = "人臉比例:";
                label13.Text = null;
                picture2.Image = Image.FromFile(fdlg.FileName);
                inputFilePath = fdlg.FileName;
                MessageBox.Show(inputFilePath);
                using (var fd = Dlib.GetFrontalFaceDetector())
                using (var sp = ShapePredictor.Deserialize("./shape_predictor_68_face_landmarks.dat"))
                {
                    var img = Dlib.LoadImage<RgbPixel>(inputFilePath);

                    var faces = fd.Operator(img);
                    if (faces.Length > 1)
                    {
                        MessageBox.Show("只能有一個臉啦，你個白癡");
                        flag = 1;
                        picture2.Image = null;
                    }
                    else
                    {
                        flag++;
                        foreach (var face in faces)
                        {
                            var shape = sp.Detect(img, face);
                            for (var i = 0; i < shape.Parts; i++)
                            {
                                pointr.Add(shape.GetPart((uint)i));
                                var rect = new DlibDotNet.Rectangle(pointr[i]);
                                //Dlib.DrawRectangle(img, rect, color: new RgbPixel(255, 255, 0), thickness: 2);
                            }
                        }
                        picture2.Image = img.ToBitmap<RgbPixel>();
                        bilir[0] = Math.Abs(pointr[0].X - pointr[36].X);
                        bilir[1] = Math.Abs(pointr[36].X - pointr[39].X);
                        bilir[2] = Math.Abs(pointr[39].X - pointr[42].X);
                        bilir[3] = Math.Abs(pointr[42].X - pointr[45].X);
                        bilir[4] = Math.Abs(pointr[45].X - pointr[16].X);
                        label4.Text += ((double)bilir[0] / bilir.Min()).ToString("f2") + " : " + ((double)bilir[1] / bilir.Min()).ToString("f2") + " : " + ((double)bilir[2] / bilir.Min()).ToString("f2") + " : " + ((double)bilir[3] / bilir.Min()).ToString("f2") + " : " + ((double)bilir[4] / bilir.Min()).ToString("f2");
                    }
                }
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
        }

        private void compare_Click(object sender, EventArgs e)
        {
            if (flag >= 2)
            {
                double c = 115.00;
                line1.X = point[36].X - point[30].X;
                line1.Y = point[36].Y - point[30].Y;
                line2.X = point[45].X - point[30].X;
                line2.Y = point[45].Y - point[30].Y;
                p1_angle1 = Vector.AngleBetween(line1, line2);//眼睛外寬度
                line1.X = point[39].X - point[30].X;
                line1.Y = point[39].Y - point[30].Y;
                line2.X = point[42].X - point[30].X;
                line2.Y = point[42].Y - point[30].Y;
                p1_angle2 = Vector.AngleBetween(line1, line2);//眼睛內寬度
                line1.X = point[60].X - point[30].X;
                line1.Y = point[60].Y - point[30].Y;
                line2.X = point[64].X - point[30].X;
                line2.Y = point[64].Y - point[30].Y;
                p1_angle3 = Vector.AngleBetween(line2, line1);//嘴巴寬度
                line1.X = pointr[36].X - pointr[30].X;
                line1.Y = pointr[36].Y - pointr[30].Y;
                line2.X = pointr[45].X - pointr[30].X;
                line2.Y = pointr[45].Y - pointr[30].Y;
                p2_angle1 = Vector.AngleBetween(line1, line2);//眼睛外寬度
                line1.X = pointr[39].X - pointr[30].X;
                line1.Y = pointr[39].Y - pointr[30].Y;
                line2.X = pointr[42].X - pointr[30].X;
                line2.Y = pointr[42].Y - pointr[30].Y;
                p2_angle2 = Vector.AngleBetween(line1, line2);//眼睛內寬度
                line1.X = pointr[60].X - pointr[30].X;
                line1.Y = pointr[60].Y - pointr[30].Y;
                line2.X = pointr[64].X - pointr[30].X;
                line2.Y = pointr[64].Y - pointr[30].Y;
                p2_angle3 = Vector.AngleBetween(line2, line1);//嘴巴寬度
                double p1_eye1 = Math.Abs(point[36].X - point[39].X) / Math.Abs(point[38].Y - point[40].Y);
                double p1_eye2 = Math.Abs(point[42].X - point[45].X) / Math.Abs(point[44].Y - point[46].Y);
                double p1_eye = (p1_eye1 + p1_eye2) / 2;//眼睛比例
                double p2_eye1 = Math.Abs(pointr[36].X - pointr[39].X) / Math.Abs(pointr[38].Y - pointr[40].Y);
                double p2_eye2 = Math.Abs(pointr[42].X - pointr[45].X) / Math.Abs(pointr[44].Y - pointr[46].Y);
                double p2_eye = (p2_eye1 + p2_eye2) / 2;//眼睛比例
                line1.X = point[4].X - point[30].X;
                line1.Y = point[4].Y - point[30].Y;
                line2.X = point[12].X - point[30].X;
                line2.Y = point[12].Y - point[30].Y;
                p1_faceangle = Vector.AngleBetween(line2, line1);//臉頰寬度
                line1.X = pointr[4].X - pointr[30].X;
                line1.Y = pointr[4].Y - pointr[30].Y;
                line2.X = pointr[12].X - pointr[30].X;
                line2.Y = pointr[12].Y - pointr[30].Y;
                p2_faceangle = Vector.AngleBetween(line2, line1);//臉頰寬度
                line1.X = point[31].X - point[27].X;
                line1.Y = point[31].Y - point[27].Y;
                line2.X = point[35].X - point[27].X;
                line2.Y = point[35].Y - point[27].Y;
                p1_noseangle = Vector.AngleBetween(line2, line1);//鼻子寬度
                line1.X = pointr[31].X - pointr[27].X;
                line1.Y = pointr[31].Y - pointr[27].Y;
                line2.X = pointr[35].X - pointr[27].X;
                line2.Y = pointr[35].Y - pointr[27].Y;
                p2_noseangle = Vector.AngleBetween(line2, line1);//鼻子寬度
                line1.X = point[36].X - point[0].X;
                line1.Y = point[36].Y - point[0].Y;
                line2.X = point[48].X - point[0].X;
                line2.Y = point[48].Y - point[0].Y;
                p1_eyemouthangle = Vector.AngleBetween(line1, line2);//眼睛嘴巴高度差
                line1.X = pointr[36].X - pointr[0].X;
                line1.Y = pointr[36].Y - pointr[0].Y;
                line2.X = pointr[48].X - pointr[0].X;
                line2.Y = pointr[48].Y - pointr[0].Y;
                p2_eyemouthangle = Vector.AngleBetween(line1, line2);//眼睛嘴巴高度差
                line1.X = point[45].X - point[16].X;
                line1.Y = point[45].Y - point[16].Y;
                line2.X = point[54].X - point[16].X;
                line2.Y = point[54].Y - point[16].Y;
                p1_eyemouthangle_2 = Vector.AngleBetween(line1, line2);//眼睛嘴巴高度差
                line1.X = pointr[45].X - pointr[16].X;
                line1.Y = pointr[45].Y - pointr[16].Y;
                line2.X = pointr[54].X - pointr[16].X;
                line2.Y = pointr[54].Y - pointr[16].Y;
                p2_eyemouthangle_2 = Vector.AngleBetween(line1, line2);//眼睛嘴巴高度差
                p1_eyenoseangle = (Math.Abs(p1_eyemouthangle) + Math.Abs(p1_eyemouthangle_2)) / 2;
                p2_eyenoseangle = (Math.Abs(p2_eyemouthangle) + Math.Abs(p2_eyemouthangle_2)) / 2;
                line1.X = point[0].X - point[30].X;
                line1.Y = point[0].Y - point[30].Y;
                line2.X = point[16].X - point[30].X;
                line2.Y = point[16].Y - point[30].Y;
                p1_headangle = Vector.AngleBetween(line1, line2);//頭部寬度
                line1.X = pointr[0].X - pointr[30].X;
                line1.Y = pointr[0].Y - pointr[30].Y;
                line2.X = pointr[16].X - pointr[30].X;
                line2.Y = pointr[16].Y - pointr[30].Y;
                p2_headangle = Vector.AngleBetween(line1, line2);//頭部寬度
                line1.X = point[36].X - point[0].X;
                line1.Y = point[36].Y - point[0].Y;
                line2.X = point[31].X - point[0].X;
                line2.Y = point[31].Y - point[0].Y;
                p1_eyenoseangle = Vector.AngleBetween(line1, line2);//眼睛鼻子高度差
                line1.X = pointr[36].X - pointr[0].X;
                line1.Y = pointr[36].Y - pointr[0].Y;
                line2.X = pointr[31].X - pointr[0].X;
                line2.Y = pointr[31].Y - pointr[0].Y;
                p2_eyenoseangle = Vector.AngleBetween(line1, line2);//眼睛鼻子高度差
                line1.X = point[45].X - point[16].X;
                line1.Y = point[45].Y - point[16].Y;
                line2.X = point[35].X - point[16].X;
                line2.Y = point[35].Y - point[16].Y;
                p1_eyenoseangle_2 = Vector.AngleBetween(line1, line2);//眼睛鼻子高度差
                line1.X = pointr[45].X - pointr[16].X;
                line1.Y = pointr[45].Y - pointr[16].Y;
                line2.X = pointr[35].X - pointr[16].X;
                line2.Y = pointr[35].Y - pointr[16].Y;
                p2_eyenoseangle_2 = Vector.AngleBetween(line1, line2);//眼睛鼻子高度差
                p1_eyenoseangle = (Math.Abs(p1_eyenoseangle) + Math.Abs(p1_eyenoseangle_2)) / 2;
                p2_eyenoseangle = (Math.Abs(p2_eyenoseangle) + Math.Abs(p2_eyenoseangle_2)) / 2;
                c -= Math.Abs(p1_eye - p2_eye) * 5.2;
                c -= Math.Abs(p1_angle1 - p2_angle1) * 0.2;
                c -= Math.Abs(p1_angle2 - p2_angle2) * 0.31;
                c -= Math.Abs(p1_noseangle - p2_noseangle) * 0.99;
                c -= Math.Abs(p1_angle3 - p2_angle3) * 0.32;
                c -= Math.Abs(p1_faceangle - p2_faceangle) * 0.97;
                c -= Math.Abs(p1_headangle - p2_headangle) * 0.87;
                c -= Math.Abs(p1_eyenoseangle - p2_eyenoseangle) * 3.753;
                c -= Math.Abs(p1_eyemouthangle - p2_eyemouthangle) * 2.89;
                List<int> eyesX = new List<int>();
                List<int> eyesY = new List<int>();
                double sum = 0.00;
                double sum2 = 0.00;
                for (int i = 36; i <= 41; i++)
                {
                    eyesX.Add(point[i].X);
                    eyesY.Add(point[i].Y);
                }
                eyesX.Sort();
                eyesY.Sort();
                for (int i = 36; i <= 41; i++)
                {
                    sum += Math.Pow((point[i].X - eyesX[0] - (point[i].Y - eyesY[0])), 2);
                }
                eyesX.Clear();
                eyesY.Clear();
                for (int i = 36; i <= 41; i++)
                {
                    eyesX.Add(pointr[i].X);
                    eyesY.Add(pointr[i].Y);
                }
                eyesX.Sort();
                eyesY.Sort();
                for (int i = 36; i <= 41; i++)
                {
                    sum2 += Math.Pow((pointr[i].X - eyesX[0] - (pointr[i].Y - eyesY[0])), 2);
                }
                MessageBox.Show(Math.Sqrt(sum).ToString("f2") +"，"+ Math.Sqrt(sum2).ToString("f2"));
                if (c<=0)
                {
                    c = 0.00;
                }
                if (c >= 100)
                {
                    c = 100.00;
                    MessageBox.Show("根本是同一張圖片吧!");
                }                   
                label13.Text = c.ToString("f2");
            }
            else
            {
                MessageBox.Show("選完兩張圖片再來!");
            }
        }
    }
}
