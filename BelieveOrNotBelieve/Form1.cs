using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//1. а) Создать приложение, показанное на уроке, добавив в него защиту от возможных ошибок(не создана база данных, обращение к несуществующему вопросу, открытие слишком большого файла и т.д.).
//б) Изменить интерфейс программы, увеличив шрифт, поменяв цвет элементов и добавив другие «косметические» улучшения на свое усмотрение.
//в) Добавить в приложение меню «О программе» с информацией о программе(автор, версия, авторские права и др.).
//г) Добавить в приложение сообщение с предупреждением при попытке удалить вопрос.
//д) Добавить пункт меню Save As, в котором можно выбрать имя для сохранения базы данных(элемент SaveFileDialog).
//Владимр Гаврилов

namespace BelieveOrNotBelieve
{
    public partial class Form1 : Form
    {
        // База данных с вопросами
        TrueFalse database;

        Stack<decimal> numbers = new Stack<decimal>();

        public Form1()
        {
            InitializeComponent
                (
                );
        }

        private void FileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // Обработчик кнопки Добавить
        private void BtDob_Click(object sender, EventArgs e)
        {
            if (database == null)
            {
                MessageBox.Show("Создайте новую базу данных", "Сообщение");
                return;
            }
            database.Add((database.Count + 1).ToString(), true);
            nudNumber.Maximum = database.Count;
            nudNumber.Value = database.Count;
        }

        // Обработчик кнопки Удалить
        private void BtUdal_Click(object sender, EventArgs e)
        {

            if (nudNumber.Maximum == 1 || database == null) return;
            database.Remove((int)nudNumber.Value);
            nudNumber.Maximum--;
            if (nudNumber.Value > 1) nudNumber.Value = nudNumber.Value;
            MessageBox.Show("Вопрос удален", "Сообщение");
        }

        // Обработчик кнопки Сохранить (вопрос)
        private void BtSave_Click(object sender, EventArgs e)
        {
            try //защита от ошибки сохранения
            {
                database[(int)nudNumber.Value - 1].text = tboxQuestion.Text;
                database[(int)nudNumber.Value - 1].trueFalse = cboxTrue.Checked;
            }
            catch { }
        }

        // Обработчик события изменения значения numericUpDown
        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //Защита от обращения к несуществующему вопросу.
            try
            {
                tboxQuestion.Text = database[(int)nudNumber.Value - 1].text;
                cboxTrue.Checked = database[(int)nudNumber.Value - 1].trueFalse;
            }
            catch
            {
            }

        }

        private void ChB_true_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Обработчик пункта меню New
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                database = new TrueFalse(sfd.FileName);
                database.Add("123", true);
                database.Save();
                nudNumber.Minimum = 1;
                nudNumber.Maximum = 1;
                nudNumber.Value = 1;
            }
        }

        // Обработчик пункта меню Open
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                try //Защита от загрузки файлов отличных от базы данных игры
                {

                    database = new TrueFalse(ofd.FileName);
                    database.Load();
                    nudNumber.Minimum = 1;
                    nudNumber.Maximum = database.Count;
                    nudNumber.Value = 1;
                }
                catch
                {
                    MessageBox.Show("Загрузите базу данных для игры или создайте новую", "Сообщение");
                }

            }

        }

        // Обработчик пункта меню Save
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (database != null) database.Save();
            else MessageBox.Show("База данных не создана");
        }

        // Обработчик пункта меню Exit
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TBPlayPole_TextChanged(object sender, EventArgs e)
        {

        }

        private void ОПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Автор: Гаврилов Владимир  @GeekBrains", "Версия 1.1");
        }

        //д) Добавить пункт меню Save As, в котором можно выбрать имя для сохранения базы данных(элемент SaveFileDialog).
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog ass = new SaveFileDialog();
            ass.ShowDialog();
            database = new TrueFalse(ass.FileName);
            database.Add("123", true);
            database.Save();
            nudNumber.Minimum = 1;
            nudNumber.Maximum = 1;
            nudNumber.Value = 1;
        }
    }
}
