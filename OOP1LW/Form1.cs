using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace OOP1LW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            PictureBox expressionImg = new PictureBox();
            Console.WriteLine(Directory.GetCurrentDirectory());
            /*expressionImg.Image = Image.FromFile(Path.Combine(
                Environment.CurrentDirectory,
                "img\\expression.jpg"
            ));*/
            Controls.Add(expressionImg);
            
            
            DataGridView dataGridView = new DataGridView
            {
                Width = (int)(Width * 0.25),
                Height = (int)(Height * 0.55),
                Location = new Point(0, (int)(Height * 0.4)),
            };
            
            
            dataGridView.Columns.Add(new DataGridViewColumn()
                { HeaderText = "x", CellTemplate = new DataGridViewTextBoxCell() });
            dataGridView.Columns.Add(new DataGridViewColumn()
                { HeaderText = "y", CellTemplate = new DataGridViewTextBoxCell() });

            dataGridView.ReadOnly = true;
            
            Controls.Add(dataGridView);
            
            Chart chart = new Chart
            {
                Width = (int)(Width * 0.75),
                Height = (int)(Height * 0.55),
                Location = new Point((int)(Width * 0.25), (int)(Height * 0.4)),
            };
            Series series = new Series
            {
                Name = "Main",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line
            };
            series.Points.AddXY(20, 20);
            series.Points.AddXY(35, 35);
            series.Points.AddXY(45, 45);
            
            
            
            chart.Series.Add(series);
            ChartArea area = new ChartArea();
            chart.ChartAreas.Add(area);
            Controls.Add(chart);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}

