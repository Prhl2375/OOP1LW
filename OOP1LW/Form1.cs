using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static OOP1LW.CalculationClass;

namespace OOP1LW
{
    public partial class Form1 : Form
    {
        private TextBox _textBox1;
        private TextBox _textBox2;
        private TextBox _textBox3;
        private TextBox _textBox4;
        private PictureBox _templateExpressionImg;
        private PictureBox _expressionImg;
        private DataGridView _dataGridView;
        private Chart _chart;

        private void InitializeControls()
        {
            SuspendLayout();
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Name = "Form1";
            Text = "Sergiy`s Form";
            Load += new System.EventHandler(Form1_Load);
            ResumeLayout(false);


            _textBox1 = new TextBox
            {
                Width = (int)(Width * 0.2),
                Height = (int)(Height * 0.05),
                Location = new Point((int)(Width * 0.05), 0)
            };
            _textBox2 = new TextBox
            {
                Width = (int)(Width * 0.2),
                Height = (int)(Height * 0.05),
                Location = new Point((int)(Width * 0.05), (int)(Height * 0.05))
            };
            _textBox3 = new TextBox
            {
                Width = (int)(Width * 0.2),
                Height = (int)(Height * 0.05),
                Location = new Point((int)(Width * 0.05), (int)(Height * 0.1))
            };
            _textBox4 = new TextBox
            {
                Width = (int)(Width * 0.2),
                Height = (int)(Height * 0.05),
                Location = new Point((int)(Width * 0.05), (int)(Height * 0.15))
            };

            Label textBox1Label = new Label {
                Width = (int)(Width * 0.03),
                Height = (int)(Height * 0.05),
                Location = new Point((int)(Width * 0.02), (int)(Height * 0))
            };
            Label textBox2Label = new Label {
                Width = (int)(Width * 0.03),
                Height = (int)(Height * 0.05),
                Location = new Point((int)(Width * 0.02), (int)(Height * 0.05))
            };
            Label textBox3Label = new Label {
                Width = (int)(Width * 0.03),
                Height = (int)(Height * 0.05),
                Location = new Point((int)(Width * 0.02), (int)(Height * 0.1))
            };
            Label textBox4Label = new Label {
                Width = (int)(Width * 0.03),
                Height = (int)(Height * 0.05),
                Location = new Point((int)(Width * 0.02), (int)(Height * 0.15))
            };
            textBox1Label.Text = "xn:";
            textBox2Label.Text = "xk:";
            textBox3Label.Text = "h:";
            textBox4Label.Text = "a:";

            _textBox1.Text = "-2.25";
            _textBox2.Text = "16.3";
            _textBox3.Text = "0.25";
            _textBox4.Text = "7.0";
            
            Controls.Add(_textBox1);
            Controls.Add(_textBox2);
            Controls.Add(_textBox3);
            Controls.Add(_textBox4);
            Controls.Add(textBox1Label);
            Controls.Add(textBox2Label);
            Controls.Add(textBox3Label);
            Controls.Add(textBox4Label);

            Button doCalculationsButton = new Button
            {
                Width = (int)(Width * 0.15),
                Height = (int)(Height * 0.1),
                Location = new Point((int)(Width * 0.075), (int)(Height * 0.25))
            };
            
            
            doCalculationsButton.Text = "calculate";
            doCalculationsButton.Click += (sender, e) =>
            {
                try
                {
                    double[,] data = Calculate(double.Parse(_textBox1.Text), double.Parse(_textBox2.Text), double.Parse(_textBox3.Text), double.Parse(_textBox4.Text));

                    
                    for (int i = 0; i < data.GetLength(0); i++)
                    {
                        for (int j = 0; j < data.GetLength(1); j++)
                        {
                            if (data[i, j] > 1E+5 || data[i, j] < -1E+5)
                            {
                                data[i, j] = double.NaN;
                            }
                        }
                    }

                    for (int i = 0; i < data.GetLength(0); i++)
                    {
                        for (int j = 0; j < data.GetLength(1); j++)
                        {
                            Console.Write(data[i, j] + "\t");
                        }
                        Console.WriteLine();
                    }
                    
                    Series updateSeries = new Series
                    {
                        Name = "update",
                        Color = System.Drawing.Color.Green,
                        IsVisibleInLegend = false,
                        IsXValueIndexed = true,
                        ChartType = SeriesChartType.Spline
                    };
                    for (int i = 0; i < data.GetLength(0); i++)
                    {
                        updateSeries.Points.AddXY(data[i, 0], data[i, 1]);
                    }

                    
                    _dataGridView.ColumnCount = data.GetLength(1);

                    // Add column headers
                    for (int i = 0; i < data.GetLength(1); i++)
                    {
                        _dataGridView.Columns[i].Name = $"Column{i + 1}";
                    }

                    // Add rows to DataGridView
                    for (int i = 0; i < data.GetLength(0); i++)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(_dataGridView);

                        for (int j = 0; j < data.GetLength(1); j++)
                        {
                            row.Cells[j].Value = data[i, j];
                        }

                        _dataGridView.Rows.Add(row);
                    }

                    
                    
                    
                    _chart.ChartAreas[0].AxisY.Minimum = -20;
                    _chart.ChartAreas[0].AxisY.Maximum = 20;
                    _chart.Series.Clear();
                    _chart.Series.Add(updateSeries);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            };
            
            Controls.Add(doCalculationsButton);
            
            _templateExpressionImg = new PictureBox{
                Width = (int)(Width * 0.7),
                Height = (int)(Height * 0.3),
                Location = new Point((int)(Width * 0.25), (int)(Height * 0)),
            };
            
            
            
            
            
            _templateExpressionImg.Image = Image.FromFile("img/templateExpression.png");
            _templateExpressionImg.SizeMode = PictureBoxSizeMode.Zoom;
            Controls.Add(_templateExpressionImg);
            
            
            _expressionImg = new PictureBox{
                Width = (int)(Width * 0.7),
                Height = (int)(Height * 0.1),
                Location = new Point((int)(Width * 0.25), (int)(Height * 0.3)),
            };
            
            
            
            
            _expressionImg.Image = Image.FromFile("img/expression.png");
            _expressionImg.SizeMode = PictureBoxSizeMode.Zoom;
            Controls.Add(_expressionImg);
            
            
            _dataGridView = new DataGridView
            {
                Width = (int)(Width * 0.25),
                Height = (int)(Height * 0.55),
                Location = new Point(0, (int)(Height * 0.4)),
            };
            
            
            _dataGridView.Columns.Add(new DataGridViewColumn()
                { HeaderText = "x", CellTemplate = new DataGridViewTextBoxCell() });
            _dataGridView.Columns.Add(new DataGridViewColumn()
                { HeaderText = "y", CellTemplate = new DataGridViewTextBoxCell() });

            _dataGridView.ReadOnly = true;
            
            Controls.Add(_dataGridView);
            
            _chart = new Chart
            {
                Width = (int)(Width * 0.75),
                Height = (int)(Height * 0.55),
                Location = new Point((int)(Width * 0.25), (int)(Height * 0.4)),
            };
            
            
            ChartArea area = new ChartArea();
            _chart.ChartAreas.Add(area);
            Controls.Add(_chart);
            
            SizeChanged += Form_SizeChanged;
        }
        
        public static double RoundToNearestMultipleOf5(double num)
        {
            if (num >= 0)
            {
                return 5 * Math.Ceiling(num / 5.0);
            }
            else
            {
                return 5 * Math.Floor(num / 5.0);
            }
        }
        
        static double[,] ConvertScientificToDecimal(double[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            double[,] convertedArray = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    // Convert the number to a string and parse it back to a double
                    // This effectively removes the scientific notation part
                    convertedArray[i, j] = double.Parse(array[i, j].ToString());
                }
            }

            return convertedArray;
        }
        
        static double[,] RoundArray(double[,] array, int decimalPlaces)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            double[,] roundedArray = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    // Convert the number to a string to avoid scientific notation
                    string numberAsString = array[i, j].ToString("F15");

                    // Parse the string back to a double and round to the specified decimal places
                    roundedArray[i, j] = Math.Round(double.Parse(numberAsString), decimalPlaces);
                }
            }

            return roundedArray;
        }

        public Form1()
        {
            InitializeControls();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        
        private void Form_SizeChanged(object sender, EventArgs e)
        {
            // Update the PictureBox size and location
            _expressionImg.Width = (int)(Width * 0.7);
            _expressionImg.Height = (int)(Height * 0.1);
            _expressionImg.Location = new Point((int)(Width * 0.25), (int)(Height * 0.3));
            
            // Update the PictureBox size and location
            _templateExpressionImg.Width = (int)(Width * 0.7);
            _templateExpressionImg.Height = (int)(Height * 0.3);
            _templateExpressionImg.Location = new Point((int)(Width * 0.25), 0);
            
            // Update the DataGridView
            _dataGridView.Width = (int)(Width * 0.25);
            _dataGridView.Height = (int)(Height * 0.55);
            _dataGridView.Location = new Point(0, (int)(Height * 0.4));

            // Update the Chart
            _chart.Width = (int)(Width * 0.75);
            _chart.Height = (int)(Height * 0.55);
            _chart.Location = new Point((int)(Width * 0.25), (int)(Height * 0.4));
            
            Refresh();
        }
    }
}

