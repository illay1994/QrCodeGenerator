using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using QrCodeGenerator.DataStorage;

namespace QrCodeGenerator
{
    public partial class QrCodes : Form
    {
        private readonly ISet<long> newData;
        private readonly Random random;

        public QrCodes()
        {
            random = new Random();
            newData = new HashSet<long>();
            InitializeComponent();
        }

        private async void LoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Item|*.csv";
            openFileDialog1.Title = "Select a Data File";

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            Regex regex = new Regex("^MQ(\\d{12})$");
            int lineCount = 0;
            using (StreamReader reader = new StreamReader(openFileDialog1.OpenFile()))
            {
                while (reader.ReadLine() != null)
                {
                    lineCount++;
                }
            }
            progressBar1.Maximum = lineCount;
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            panel1.Enabled = false;
            menuStrip1.Enabled = false;
            using (StreamReader reader = new StreamReader(openFileDialog1.OpenFile()))
            {
                string data;
                int index = 0;
                using (QrCodesDbContext p = new QrCodesDbContext())
                {
                    while ((data = await reader.ReadLineAsync()) != null)
                    {
                        index++;
                        progressBar1.Value = index;
                        string[] items = data.Split(';', ',');
                        foreach (var item in items)
                        {
                            Match match = regex.Match(item);
                            if (!match.Success) continue;
                            if (!long.TryParse(match.Groups[1].Value, out long longValue)) continue;
                            if (p.QrCodes.Select(qr => qr.Code).Any(c => c == longValue)) continue;

                            p.QrCodes.Add(
                                new QrCode()
                                {
                                    Code = longValue,
                                    CreatedAt = DateTime.Now
                                });

                            if (index % 1000 == 0) await p.SaveChangesAsync();
                        }
                    }
                    await p.SaveChangesAsync();

                }
            }
            panel1.Enabled = true;
            menuStrip1.Enabled = true;
            progressBar1.Visible = false;
        }

        private void GenerateNewButton_Click(object sender, EventArgs e)
        {
            int count = (int)QrCountNumber.Value;
            progressBar1.Maximum = count;
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            panel1.Enabled = false;
            menuStrip1.Enabled = false;

            using (QrCodesDbContext p = new QrCodesDbContext())
            {
                for (int index = 0; index < count; index++)
                {
                    progressBar1.Value = index;
                    long data = random.Next(1000000000000);
                    while (newData.Contains(data) || p.QrCodes.Select(t => t.Code).Contains(data))
                    {
                        data = random.Next(1000000000000);
                    }
                    newData.Add(data);
                }
            }
            NewQrCodesList.Items.Clear();
            foreach (var item in newData)
            {
                NewQrCodesList.Items.Add(item.ToQrCode());
            }
            panel1.Enabled = true;
            menuStrip1.Enabled = true;
            progressBar1.Visible = false;
            GenerateNewButton.Enabled = false;
            saveToolStripMenuItem.Enabled = true;
            SaveButton.Enabled = true;
            cleanToolStripMenuItem.Enabled = true;
            CleanButton.Enabled = true;
            CopyButton.Enabled = true;

        }

        private async void SaveNewButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Item|*.csv";
            saveFileDialog.Title = "Save a Data File";

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            progressBar1.Maximum = newData.Count;
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            panel1.Enabled = false;
            menuStrip1.Enabled = false;

            using (StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile()))
            {
                using (QrCodesDbContext p = new QrCodesDbContext())
                {
                    foreach (var item in newData)
                    {
                        await writer.WriteLineAsync($"{item.ToQrCode()}");
                        if (!p.QrCodes.Select(t => t.Code).Contains(item))
                        {
                            p.QrCodes.Add(
                                new QrCode()
                                {
                                    Code = item,
                                    CreatedAt = DateTime.Now
                                });
                        }
                        progressBar1.Value++;
                    }
                    await p.SaveChangesAsync();
                }
            }

            progressBar1.Visible = false;
            panel1.Enabled = true;
            menuStrip1.Enabled = true;
        }

        private void SaveAllButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Item|*.csv";
            saveFileDialog.Title = "Save a Data File";

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;


            progressBar1.Value = 0;
            progressBar1.Visible = true;
            panel1.Enabled = false;
            menuStrip1.Enabled = false;

            using (QrCodesDbContext p = new QrCodesDbContext())
            {
                progressBar1.Maximum = p.QrCodes.Count();
                using (StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile()))
                {
                    foreach (var item in p.QrCodes.OrderBy(t => t.CreatedAt))
                    {
                        writer.WriteLine($"{item.Code.ToQrCode()},{item.CreatedAt:G}");
                        progressBar1.Value++;
                    }
                }
            }
            progressBar1.Visible = false;
            panel1.Enabled = true;
            menuStrip1.Enabled = true;
        }

        private void CleanButton_Click(object sender, EventArgs e)
        {
            SaveButton.Enabled = false;
            saveToolStripMenuItem.Enabled = false;
            CleanButton.Enabled = false;
            cleanToolStripMenuItem.Enabled = false;
            newData.Clear();
            CopyButton.Enabled = false;
            NewQrCodesList.Items.Clear();
            GenerateNewButton.Enabled = true;
        }

        private async void CopyButton_Click(object sender, EventArgs e)
        {
            string s1 = "";
            using (QrCodesDbContext p = new QrCodesDbContext())
            {
                foreach (long item in newData)
                {
                    s1 += item.ToQrCode() + "\r\n";
                    if (!p.QrCodes.Select(t => t.Code).Contains(item))
                    {
                        p.QrCodes.Add(
                            new QrCode()
                            {
                                Code = item,
                                CreatedAt = DateTime.Now
                            });
                    }
                }
                await p.SaveChangesAsync();
            }

            Clipboard.SetText(s1);
        }

        private void StatisticButton_Click(object sender, EventArgs e)
        {
            using (QrCodesDbContext p = new QrCodesDbContext())
            {
                DateTime today = DateTime.Today;
                long totalCount = p.QrCodes.Count();
                long thisMonthCount = p.QrCodes.Count(qr => qr.CreatedAt.Month == today.Month);
                long todayCount = p.QrCodes.Count(qr => qr.CreatedAt >= today);
                MessageBox.Show($"Total:{totalCount}\r\nThis Month:{thisMonthCount}\r\nToday:{todayCount}", "Statistic", MessageBoxButtons.OK);
            }
        }
    }
}
