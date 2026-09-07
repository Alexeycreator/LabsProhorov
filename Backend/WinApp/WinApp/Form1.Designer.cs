namespace WinApp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.label_answer = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_a = new System.Windows.Forms.TextBox();
            this.button_calculate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label_formula_k0 = new System.Windows.Forms.Label();
            this.label_formula_V = new System.Windows.Forms.Label();
            this.label_n2 = new System.Windows.Forms.Label();
            this.textBox_n2 = new System.Windows.Forms.TextBox();
            this.label_n1 = new System.Windows.Forms.Label();
            this.textBox_n1 = new System.Windows.Forms.TextBox();
            this.label_lambda = new System.Windows.Forms.Label();
            this.textBox_lambda = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_Lab1 = new System.Windows.Forms.TabPage();
            this.tabPage_Lab2 = new System.Windows.Forms.TabPage();
            this.tabPage_Lab3 = new System.Windows.Forms.TabPage();
            this.tabPage_Lab4 = new System.Windows.Forms.TabPage();
            this.tabPage_Lab5 = new System.Windows.Forms.TabPage();
            this.tabPage_Lab6 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage_Lab1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(350, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 16);
            this.label3.TabIndex = 32;
            this.label3.Text = "мкм";
            // 
            // label_answer
            // 
            this.label_answer.AutoSize = true;
            this.label_answer.Location = new System.Drawing.Point(193, 242);
            this.label_answer.Name = "label_answer";
            this.label_answer.Size = new System.Drawing.Size(53, 16);
            this.label_answer.TabIndex = 31;
            this.label_answer.Text = "Ответ: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 16);
            this.label2.TabIndex = 30;
            this.label2.Text = "Радиус сердцевины а:";
            // 
            // textBox_a
            // 
            this.textBox_a.Location = new System.Drawing.Point(244, 189);
            this.textBox_a.Name = "textBox_a";
            this.textBox_a.Size = new System.Drawing.Size(100, 22);
            this.textBox_a.TabIndex = 28;
            // 
            // button_calculate
            // 
            this.button_calculate.Location = new System.Drawing.Point(18, 235);
            this.button_calculate.Name = "button_calculate";
            this.button_calculate.Size = new System.Drawing.Size(91, 23);
            this.button_calculate.TabIndex = 29;
            this.button_calculate.Text = "Расчитать";
            this.button_calculate.UseVisualStyleBackColor = true;
            this.button_calculate.Click += new System.EventHandler(this.button_calculate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(350, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 16);
            this.label1.TabIndex = 27;
            this.label1.Text = "мкм";
            // 
            // label_formula_k0
            // 
            this.label_formula_k0.AutoSize = true;
            this.label_formula_k0.Location = new System.Drawing.Point(444, 120);
            this.label_formula_k0.Name = "label_formula_k0";
            this.label_formula_k0.Size = new System.Drawing.Size(113, 16);
            this.label_formula_k0.TabIndex = 26;
            this.label_formula_k0.Text = "Формула для k0:";
            // 
            // label_formula_V
            // 
            this.label_formula_V.AutoSize = true;
            this.label_formula_V.Location = new System.Drawing.Point(444, 48);
            this.label_formula_V.Name = "label_formula_V";
            this.label_formula_V.Size = new System.Drawing.Size(70, 16);
            this.label_formula_V.TabIndex = 25;
            this.label_formula_V.Text = "Формула:";
            // 
            // label_n2
            // 
            this.label_n2.AutoSize = true;
            this.label_n2.Location = new System.Drawing.Point(15, 143);
            this.label_n2.Name = "label_n2";
            this.label_n2.Size = new System.Drawing.Size(197, 16);
            this.label_n2.TabIndex = 24;
            this.label_n2.Text = "Показатель преломления n2:";
            // 
            // textBox_n2
            // 
            this.textBox_n2.Location = new System.Drawing.Point(244, 143);
            this.textBox_n2.Name = "textBox_n2";
            this.textBox_n2.Size = new System.Drawing.Size(100, 22);
            this.textBox_n2.TabIndex = 23;
            // 
            // label_n1
            // 
            this.label_n1.AutoSize = true;
            this.label_n1.Location = new System.Drawing.Point(15, 98);
            this.label_n1.Name = "label_n1";
            this.label_n1.Size = new System.Drawing.Size(197, 16);
            this.label_n1.TabIndex = 22;
            this.label_n1.Text = "Показатель преломления n1:";
            // 
            // textBox_n1
            // 
            this.textBox_n1.Location = new System.Drawing.Point(244, 98);
            this.textBox_n1.Name = "textBox_n1";
            this.textBox_n1.Size = new System.Drawing.Size(100, 22);
            this.textBox_n1.TabIndex = 21;
            // 
            // label_lambda
            // 
            this.label_lambda.AutoSize = true;
            this.label_lambda.Location = new System.Drawing.Point(15, 45);
            this.label_lambda.Name = "label_lambda";
            this.label_lambda.Size = new System.Drawing.Size(153, 16);
            this.label_lambda.TabIndex = 20;
            this.label_lambda.Text = "Введите длину волны:";
            // 
            // textBox_lambda
            // 
            this.textBox_lambda.Location = new System.Drawing.Point(244, 45);
            this.textBox_lambda.Name = "textBox_lambda";
            this.textBox_lambda.Size = new System.Drawing.Size(100, 22);
            this.textBox_lambda.TabIndex = 19;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::WinApp.Properties.Resources.Lambda;
            this.pictureBox3.Location = new System.Drawing.Point(174, 41);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(22, 23);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 35;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::WinApp.Properties.Resources.Formula_k0;
            this.pictureBox2.Location = new System.Drawing.Point(573, 104);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(59, 55);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 34;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WinApp.Properties.Resources.Formula_V;
            this.pictureBox1.Location = new System.Drawing.Point(520, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(216, 55);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 33;
            this.pictureBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Lab1);
            this.tabControl1.Controls.Add(this.tabPage_Lab2);
            this.tabControl1.Controls.Add(this.tabPage_Lab3);
            this.tabControl1.Controls.Add(this.tabPage_Lab4);
            this.tabControl1.Controls.Add(this.tabPage_Lab5);
            this.tabControl1.Controls.Add(this.tabPage_Lab6);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1195, 782);
            this.tabControl1.TabIndex = 36;
            // 
            // tabPage_Lab1
            // 
            this.tabPage_Lab1.Controls.Add(this.pictureBox1);
            this.tabPage_Lab1.Controls.Add(this.pictureBox3);
            this.tabPage_Lab1.Controls.Add(this.textBox_lambda);
            this.tabPage_Lab1.Controls.Add(this.pictureBox2);
            this.tabPage_Lab1.Controls.Add(this.label_lambda);
            this.tabPage_Lab1.Controls.Add(this.textBox_n1);
            this.tabPage_Lab1.Controls.Add(this.label3);
            this.tabPage_Lab1.Controls.Add(this.label_n1);
            this.tabPage_Lab1.Controls.Add(this.label_answer);
            this.tabPage_Lab1.Controls.Add(this.textBox_n2);
            this.tabPage_Lab1.Controls.Add(this.label2);
            this.tabPage_Lab1.Controls.Add(this.label_n2);
            this.tabPage_Lab1.Controls.Add(this.textBox_a);
            this.tabPage_Lab1.Controls.Add(this.label_formula_V);
            this.tabPage_Lab1.Controls.Add(this.button_calculate);
            this.tabPage_Lab1.Controls.Add(this.label_formula_k0);
            this.tabPage_Lab1.Controls.Add(this.label1);
            this.tabPage_Lab1.Location = new System.Drawing.Point(4, 25);
            this.tabPage_Lab1.Name = "tabPage_Lab1";
            this.tabPage_Lab1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Lab1.Size = new System.Drawing.Size(1187, 753);
            this.tabPage_Lab1.TabIndex = 0;
            this.tabPage_Lab1.Text = "Лабораторная 1";
            this.tabPage_Lab1.UseVisualStyleBackColor = true;
            // 
            // tabPage_Lab2
            // 
            this.tabPage_Lab2.Location = new System.Drawing.Point(4, 25);
            this.tabPage_Lab2.Name = "tabPage_Lab2";
            this.tabPage_Lab2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Lab2.Size = new System.Drawing.Size(1187, 753);
            this.tabPage_Lab2.TabIndex = 1;
            this.tabPage_Lab2.Text = "Лабораторная 2";
            this.tabPage_Lab2.UseVisualStyleBackColor = true;
            // 
            // tabPage_Lab3
            // 
            this.tabPage_Lab3.Location = new System.Drawing.Point(4, 25);
            this.tabPage_Lab3.Name = "tabPage_Lab3";
            this.tabPage_Lab3.Size = new System.Drawing.Size(1187, 753);
            this.tabPage_Lab3.TabIndex = 2;
            this.tabPage_Lab3.Text = "Лабораторная 3";
            this.tabPage_Lab3.UseVisualStyleBackColor = true;
            // 
            // tabPage_Lab4
            // 
            this.tabPage_Lab4.Location = new System.Drawing.Point(4, 25);
            this.tabPage_Lab4.Name = "tabPage_Lab4";
            this.tabPage_Lab4.Size = new System.Drawing.Size(1187, 753);
            this.tabPage_Lab4.TabIndex = 3;
            this.tabPage_Lab4.Text = "Лабораторная 4";
            this.tabPage_Lab4.UseVisualStyleBackColor = true;
            // 
            // tabPage_Lab5
            // 
            this.tabPage_Lab5.Location = new System.Drawing.Point(4, 25);
            this.tabPage_Lab5.Name = "tabPage_Lab5";
            this.tabPage_Lab5.Size = new System.Drawing.Size(1187, 753);
            this.tabPage_Lab5.TabIndex = 4;
            this.tabPage_Lab5.Text = "Лабораторная 5";
            this.tabPage_Lab5.UseVisualStyleBackColor = true;
            // 
            // tabPage_Lab6
            // 
            this.tabPage_Lab6.Location = new System.Drawing.Point(4, 25);
            this.tabPage_Lab6.Name = "tabPage_Lab6";
            this.tabPage_Lab6.Size = new System.Drawing.Size(1187, 753);
            this.tabPage_Lab6.TabIndex = 5;
            this.tabPage_Lab6.Text = "Лабораторная 6";
            this.tabPage_Lab6.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 806);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Лабораторные работы";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Lab1.ResumeLayout(false);
            this.tabPage_Lab1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_answer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_a;
        private System.Windows.Forms.Button button_calculate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_formula_k0;
        private System.Windows.Forms.Label label_formula_V;
        private System.Windows.Forms.Label label_n2;
        private System.Windows.Forms.TextBox textBox_n2;
        private System.Windows.Forms.Label label_n1;
        private System.Windows.Forms.TextBox textBox_n1;
        private System.Windows.Forms.Label label_lambda;
        private System.Windows.Forms.TextBox textBox_lambda;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_Lab1;
        private System.Windows.Forms.TabPage tabPage_Lab2;
        private System.Windows.Forms.TabPage tabPage_Lab3;
        private System.Windows.Forms.TabPage tabPage_Lab4;
        private System.Windows.Forms.TabPage tabPage_Lab5;
        private System.Windows.Forms.TabPage tabPage_Lab6;
    }
}

