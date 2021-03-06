﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace WindowsFormsApplication1
{
    public partial class Main : Form
    {
        // 문서/프린터 설정/페이지 설정에 관한 변수를 선언합니다
        private PrintDocument printDoc1 = new PrintDocument();
        private PageSettings printDocPageSet1 = new PageSettings();
        private PrinterSettings printDocSet1 = new PrinterSettings();

        // 인쇄 전에 다이얼로그를 보이게 할 것인지를 설정하는 변수를 선언합니다
        new bool ShowDialog = false;
        int printTextSize, printTextByteSize, printFontSize; // 본문 글자수, 바이트수, 인쇄 폰트 크기
        string MemoTitle;

        public Main()
        {
            printDocSet1.PrinterName = "POS-58";
            printDocPageSet1.Margins = new Margins(0, 0, 0, 0);
            printDocPageSet1.PaperSize = new PaperSize("감열지 58mm", 185, 1169);
            printDocPageSet1.PaperSize.Width = 185;
            printDocPageSet1.PaperSize.Height = 1169;
            InitializeComponent();
            printFontSize = Decimal.ToInt16(FontSize.Value);
        }

        private void Print_Click(object sender, EventArgs e)
        {
            printDoc1.PrintController = new StandardPrintController(); // 인쇄 시 인쇄중 창이 뜨지 않도록 합니다
            printDoc1.DefaultPageSettings = printDocPageSet1;
            printDoc1.PrinterSettings = printDocSet1;
            printDoc1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            if (ShowDialog)
            {
                PrintPreviewDialog Dialog = new PrintPreviewDialog();
                Dialog.Document = printDoc1;
                Dialog.Show();
                /*
                if (Dialog.ShowDialog() == DialogResult.OK)
                {
                    printDoc1.Print();
                }
                */
            }else { printDoc1.Print(); }
            
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font Gulim = new Font("Gulim", 8);
            //Font Gulim_B = new Font("Gulim", 8, FontStyle.Bold);
            Font Gulim_t1 = new Font("Gulim", 12, FontStyle.Bold);
            Font Gulim_p = new Font("Gulim", printFontSize);
            Font Gulim_time = new Font("Gulim", 8);
            
            int uppermargin = 0; // 위쪽 여백을 0으로 정의합니다
            if (MemoTitle != null) { // 제목이 입력된 경우 제목을 입력할 여백을 추가하고 제목을 그립니다.
                e.Graphics.DrawString(PrintTitle.Text, Gulim_t1, Brushes.Black, 0, 0);
                uppermargin = 15;
            }

            //출력시간과 구분선을 그립니다
            e.Graphics.DrawString(DateTime.Now.ToString("yy/MM/dd (ddd) HH:mm"), Gulim_time, Brushes.Black, 70, uppermargin);
            e.Graphics.DrawLine(Pens.Black, 0, uppermargin + 11, 185, uppermargin + 11);

            // WordWrap을 위한 글상자를 만듭니다
            Rectangle mojibako = new Rectangle(0,uppermargin + 13, printDocPageSet1.PaperSize.Width, printDocPageSet1.PaperSize.Height);

            // 본문을 그립니다.
            e.Graphics.DrawString(printTextBox.Text, Gulim_p, Brushes.Black, mojibako);
            //e.Graphics.DrawString(printTextBox.Text, Gulim_p, Brushes.Black, 0, uppermargin + 13);

            /* 이미지를 추가합니다.
            Image img = Image.FromFile(@"..\..\test.png");
            e.Graphics.DrawImage(img, 0, 0, 500, 500);
            */
        }
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ShowDialog = checkBox1.Checked;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void printTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void PrintTitle_TextChanged(object sender, EventArgs e)
        {
            MemoTitle = PrintTitle.Text;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FontSize_ValueChanged(object sender, EventArgs e)
        {
            printFontSize = Decimal.ToInt16(FontSize.Value);
            printTextBox.Font = new Font("Gulim",printFontSize);
        }
    }
}
