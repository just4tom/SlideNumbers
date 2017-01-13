using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MainForm : Form
    {
        int nRowColLen = 3;  //设置游戏每行（列）的格子数
        Label emptyLabel;

        public MainForm()
        {
            InitializeComponent();
            InitializeLayOut(nRowColLen);            
        }

        private void InitializeLayOut(int nLenIn)
        {
            tableLayoutPanel1.Controls.Clear();
            //comboBox1.SelectedIndex = 0; //3X3
            int nLen = nLenIn;
            Single sPercent = 100 / nLen;

            this.tableLayoutPanel1.ColumnCount = nLen;
            this.tableLayoutPanel1.RowCount = nLen;
            for (int i = 0; i < nLen; i++)
            {
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, sPercent));
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, sPercent));
            }
            for (int i = 0; i < nLen; i++)
            {
                for (int j = 0; j < nLen; j++)
                {
                    System.Windows.Forms.Label theLabel = new Label();
                    theLabel.AutoSize = true;
                    theLabel.Dock = System.Windows.Forms.DockStyle.Fill;
                    theLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                    theLabel.Location = new System.Drawing.Point(1, 1);
                    theLabel.Margin = new System.Windows.Forms.Padding(1);
                    theLabel.Name = "label1";
                    theLabel.Size = new System.Drawing.Size(88, 79);
                    theLabel.TabIndex = 0;
                    theLabel.Text = (nLen * i + j + 1).ToString();
                    theLabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    theLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    theLabel.Tag = i.ToString() + "," + j.ToString();
                    theLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lable_MouseDown);
                    this.tableLayoutPanel1.Controls.Add(theLabel, j, i);
                }
            }

            Label lastLabel = (Label)tableLayoutPanel1.GetControlFromPosition(nLen - 1, nLen - 1);
            lastLabel.Text = "";
            emptyLabel = lastLabel;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //InitializeLayOut(comboBox1.SelectedIndex);
        }

        private void x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nRowColLen = 3;
            x3ToolStripMenuItem.Checked = true;
            x4ToolStripMenuItem.Checked = false;
            x5ToolStripMenuItem.Checked = false;
            InitializeLayOut(nRowColLen);
        }

        private void x4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nRowColLen = 4;
            x3ToolStripMenuItem.Checked = false;
            x5ToolStripMenuItem.Checked = false;
            InitializeLayOut(nRowColLen);
        }

        private void x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nRowColLen = 5;
            x3ToolStripMenuItem.Checked = false;
            x4ToolStripMenuItem.Checked = false;
            InitializeLayOut(nRowColLen);
        }

        private void lable_MouseDown(object sender, EventArgs e)
        {
            string[] strLabelPos = ((Label)sender).Tag.ToString().Split(',');
            string[] strEmptyLabelPos = emptyLabel.Tag.ToString().Split(',');
            int labelRow = Convert.ToInt32(strLabelPos[0]);
            int labelCol = Convert.ToInt32(strLabelPos[1]);
            int emptyLabelRow = Convert.ToInt32(strEmptyLabelPos[0]);
            int emptyLabelCol = Convert.ToInt32(strEmptyLabelPos[1]);

            if(labelRow == emptyLabelRow && labelCol != emptyLabelCol)
            {
                int colCount = emptyLabelCol;               
                do 
                {
                    if(labelCol > emptyLabelCol)
                    {
                        colCount++;
                    }
                    else
                    {
                        colCount--;
                    }
                    Label toMoveLabel = (Label)tableLayoutPanel1.GetControlFromPosition(colCount, emptyLabelRow);
                    emptyLabel.Text = toMoveLabel.Text;
                    toMoveLabel.Text = "";
                    emptyLabel = toMoveLabel;

                } while (colCount != labelCol);
            }
            else if(labelCol == emptyLabelCol && labelRow != emptyLabelRow)
            {
                int rowCount = emptyLabelRow;
                do 
                {
                    if(labelRow > emptyLabelRow)
                    {
                        rowCount++;
                    }
                    else
                    {
                        rowCount--;
                    }
                    Label toMoveLabel = (Label)tableLayoutPanel1.GetControlFromPosition(emptyLabelCol, rowCount);
                    emptyLabel.Text = toMoveLabel.Text;
                    toMoveLabel.Text = "";
                    emptyLabel = toMoveLabel;
                } while (rowCount != labelRow);
            }

            if(judgeSuccess())
            {
                MessageBox.Show("Congratulations! You Win!");
            }
        }

        private bool judgeSuccess()
        {
            int valueCount = 0;
            for (int i = 0; i < nRowColLen; i++)
            {
                for (int j = 0; j < nRowColLen; j++)
                {
                    if(i==nRowColLen-1 && j==nRowColLen-1)
                    {
                        continue;
                    }

                    valueCount++;
                    Label judgeLabel = (Label)tableLayoutPanel1.GetControlFromPosition(j, i);
                    if(judgeLabel.Text != "")
                    {
                        if (Convert.ToInt32(judgeLabel.Text) != valueCount)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if(i!=nRowColLen-1 || j!= nRowColLen-1)
                        {
                            return false;
                        }
                    }
                    
                }
            }
            return true;
        }

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            int valueCount = 0;
            for (int i = 0; i < nRowColLen; i++)
            {
                for (int j = 0; j < nRowColLen; j++)
                {
                    Label theLabel = (Label)tableLayoutPanel1.GetControlFromPosition(j, i);
                    theLabel.Text = (++valueCount).ToString();
                }
            }
            Label lastLabel = (Label)tableLayoutPanel1.GetControlFromPosition(nRowColLen - 1, nRowColLen-1);
            lastLabel.Text = "";
            emptyLabel = lastLabel;
        }

        // Number随机数个数
        // minNum随机数下限
        // maxNum随机数上限
        public int[] GetRandomArray(int Number, int minNum, int maxNum)
        {
            int j;
            int[] b = new int[Number];
            Random r = new Random();
            for (j = 0; j < Number; j++)
            {
                int i = r.Next(minNum, maxNum + 1);
                int num = 0;
                for (int k = 0; k < j; k++)
                {
                    if (b[k] == i)
                    {
                        num = num + 1;
                    }
                }
                if (num == 0)
                {
                    b[j] = i;
                }
                else
                {
                    j = j - 1;
                }
            }
            return b;
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            int[] randomArray = new int[nRowColLen * nRowColLen];
            randomArray = GetRandomArray(nRowColLen * nRowColLen, 1, nRowColLen * nRowColLen);
            for (int i = 0; i < nRowColLen; i++)
            {
                for (int j = 0; j < nRowColLen; j++)
                {
                    Label theLabel = (Label)tableLayoutPanel1.GetControlFromPosition(j, i);
                    theLabel.Text = randomArray[i * nRowColLen + j].ToString();
                    if(theLabel.Text == (nRowColLen * nRowColLen).ToString())
                    {
                        theLabel.Text = "";
                        emptyLabel = theLabel;
                    }
                }
            }
        }

    }
}
