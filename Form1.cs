using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Notepad
{
    public partial class Form1 : Form
    {
        private string currentFile = string.Empty;  // Для хранения текущего пути файла
        private string currentFilePath = string.Empty; // Переменная для хранения полного пути к файлу

        private void exitPrompt(object sender, EventArgs e)
        {
        }


        public Form1()
        {
            InitializeComponent();
            richTextBox1.ContextMenuStrip = contextMenuStrip1;
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resalt = MessageBox.Show("Хотите сохранить файл,", "Notepad",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
            if (resalt == DialogResult.Yes)
            {
                сохранитькакToolStripMenuItem_Click(sender, e);
            }
            else
            {
                return;
            }
            // Очищаем текстовое поле
            

            richTextBox1.Clear();
           
            // Сбрасываем текущий файл
            currentFilePath = string.Empty;

            // Обновляем заголовок окна
            this.Text = "Без названия - Блокнот"; 
        }


        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                openFileDialog.Title = "Открыть файл";

                if (openFileDialog.ShowDialog() == DialogResult.OK) 
                {
                    currentFilePath = openFileDialog.FileName; 
                    richTextBox1.Text = File.ReadAllText(currentFilePath); 
                    this.Text = $"{Path.GetFileName(currentFilePath)} - {currentFilePath}"; 
                }
            }
        }


        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath)) 
            {
                сохранитькакToolStripMenuItem_Click(sender, e); 
            }
            else
            {
                File.WriteAllText(currentFilePath, richTextBox1.Text); 
                this.Text = $"{Path.GetFileName(currentFilePath)} - {currentFilePath}"; 
            }
        }

        private void сохранитькакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.Title = "Сохранить файл как";

                if (saveFileDialog.ShowDialog() == DialogResult.OK) 
                {
                    File.WriteAllText(saveFileDialog.FileName, richTextBox1.Text); 
                    currentFilePath = saveFileDialog.FileName; 
                    this.Text = $"{Path.GetFileName(currentFilePath)} - {currentFilePath}"; 
                }
            }
        }

        private void отменадействияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo)
            {
                richTextBox1.Undo();
            }
        }

        private void отменадействияToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanRedo)
            {
                richTextBox1.Redo();
            }
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void вставкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void выделитьвсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fontDialog.Font;
            }
        }

        private void цветФонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.BackColor = colorDialog.Color;
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void создатьToolStripButton_Click(object sender, EventArgs e)
        {   
            создатьToolStripMenuItem_Click(sender, e);
        }

        private void открытьToolStripButton_Click(object sender, EventArgs e)
        {
            открытьToolStripMenuItem_Click(sender, e);
        }

        private void сохранитьToolStripButton_Click(object sender, EventArgs e)
        {          
            сохранитьToolStripMenuItem_Click(sender, e);
        }

        private void вырезатьToolStripButton_Click(object sender, EventArgs e)
        {
           
            if (richTextBox1.SelectedText.Length > 0)
            {
                Clipboard.SetText(richTextBox1.SelectedText); 
                richTextBox1.SelectedText = string.Empty; 
            }
        }

        private void копироватьToolStripButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length > 0)
            {
                Clipboard.SetText(richTextBox1.SelectedText);
            }
        }

        private void вставкаToolStripButton_Click(object sender, EventArgs e)
        {
            
            if (Clipboard.ContainsText())
            {
                richTextBox1.Paste(); 
            }
        }

        

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            копироватьToolStripMenuItem.Enabled = !string.IsNullOrEmpty(richTextBox1.SelectedText);
            вырезатьToolStripMenuItem1.Enabled = !string.IsNullOrEmpty(richTextBox1.SelectedText);
        }

        private void rjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Копируем выделенный текст в буфер обмена
            if (!string.IsNullOrEmpty(richTextBox1.SelectedText))
            {
                Clipboard.SetText(richTextBox1.SelectedText);
            }
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (Clipboard.ContainsText())
            {
                richTextBox1.Paste();
            }
        }

        private void вырезатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
            if (!string.IsNullOrEmpty(richTextBox1.SelectedText))
            {
                Clipboard.SetText(richTextBox1.SelectedText);
                richTextBox1.SelectedText = string.Empty;
            }
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (richTextBox1.CanUndo)
            {
                richTextBox1.Undo();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
    }
}