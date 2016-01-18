using System;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace TwentyAI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            initCurrent();
            initStandardColor();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            registerHotKey();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            unregisterHotKey();
        }
        
        private string option = " ";
        static public int maxDepth = 250;
        private void myHotKeyEvent()
        {
            getOption();
            if (option == "AStar")
                moveBlock(0);
            else if (option == "BFS")
                moveBlock(1);
            else if (option == "DFS")
                moveBlock(2);
            else { }
            test1output();
        }
        private void getOption()
        {
            if (this.InvokeRequired)
            {
                getUI abc = new getUI(getOption);
                this.Invoke(abc);
            }
            else
            {
                option = comboBox1.Text;
            }
        }
        private delegate void getUI();
        private void finishOrNot()
        {
            if (this.InvokeRequired)
            {
                finish fff = new finish(finishOrNot);
                this.Invoke(fff);
            }
            else
            {
                finishLabel.Text = "Done";
            }
        }
        private delegate void finish();
        private void updateCurrentDepth(int depth)
        {
            if (this.InvokeRequired)
            {
                uCD ucd = new uCD(updateCurrentDepth);
                this.Invoke(ucd, depth);
            }
            else
            {
                currentDepth.Text = depth.ToString();
            }
        }
        private delegate void uCD(int depth);

        private int[] testScore = new int[maxDepth];
        private double[] testTime = new double[maxDepth];
        private void test1output()
        {
            // 設定儲存檔名，不用設定副檔名，系統自動判斷 excel 版本，產生 .xls 或 .xlsx 副檔名
            string pathFile = @"D:\test";

            Excel.Application excelApp;
            Excel._Workbook wBook;
            Excel._Worksheet wSheet;
            Excel.Range wRange;

            // 開啟一個新的應用程式
            excelApp = new Excel.Application();
            // 讓Excel文件可見
            excelApp.Visible = true;
            // 停用警告訊息
            excelApp.DisplayAlerts = false;
            // 加入新的活頁簿
            excelApp.Workbooks.Add(Type.Missing);
            // 引用第一個活頁簿
            wBook = excelApp.Workbooks[1];
            // 設定活頁簿焦點
            wBook.Activate();

            try
            {
                // 引用第一個工作表
                wSheet = (Excel._Worksheet)wBook.Worksheets[1];
                // 命名工作表的名稱
                wSheet.Name = "experiment1";
                // 設定工作表焦點
                wSheet.Activate();

                // 設定第1列資料
                excelApp.Cells[1, 1] = "Depth";
                excelApp.Cells[1, 2] = "Score";
                excelApp.Cells[1, 3] = "Time";

                for(int i = 1; i <= maxDepth; i++)
                {
                    excelApp.Cells[i+1, 1] = i;
                    excelApp.Cells[i+1, 2] = testScore[i-1].ToString();
                    excelApp.Cells[i+1, 3] = testTime[i-1].ToString();
                }

                // 自動調整欄寬
                wRange = wSheet.Range[wSheet.Cells[1, 1], wSheet.Cells[5, 2]];
                wRange.Select();
                wRange.Columns.AutoFit();

                try
                {
                    //另存活頁簿
                    wBook.SaveAs(pathFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    Console.WriteLine("Save excel files at: " + Environment.NewLine + pathFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("儲存檔案出錯，檔案可能正在使用" + Environment.NewLine + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("產生報表時出錯！" + Environment.NewLine + ex.Message);
            }

            //關閉活頁簿
            //wBook.Close(false, Type.Missing, Type.Missing);
            //關閉Excel
            //excelApp.Quit();
            //釋放Excel資源
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            wBook = null;
            wSheet = null;
            wRange = null;
            excelApp = null;
            GC.Collect();

            Console.Read();
        }
    }
}

/*

*/
