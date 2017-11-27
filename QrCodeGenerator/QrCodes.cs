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

        private void LoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Item|*.csv";
            openFileDialog1.Title = "Select a Data File";

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            Regex regex = new Regex("^MQ(\\d{12})$");
            using (StreamReader reader = new StreamReader(openFileDialog1.OpenFile()))
            {
                string data;
                int index = 0;
                using (QrCodesDbContext p = new QrCodesDbContext())
                {

                    while ((data = reader.ReadLine()) != null)
                    {
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
                            index++;
                            if (index % 1000 == 0) p.SaveChanges();
                        }
                    }
                    p.SaveChanges();

                }
            }
        }

        private void GenerateNewButton_Click(object sender, EventArgs e)
        {
            decimal count = QrCountNumber.Value;
            using (QrCodesDbContext p = new QrCodesDbContext())
            {
                for (decimal index = 0; index < count; index++)
                {
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

            GenerateNewButton.Enabled = false;
            saveToolStripMenuItem.Enabled = true;
            SaveButton.Enabled = true;
            cleanToolStripMenuItem.Enabled = true;
            CleanButton.Enabled = true;
            CopyButton.Enabled = true;

        }

        private void SaveNewButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Item|*.csv";
            saveFileDialog.Title = "Save a Data File";

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            using (StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile()))
            {
                using (QrCodesDbContext p = new QrCodesDbContext())
                {
                    foreach (var item in newData)
                    {
                        writer.WriteLine($"{item.ToQrCode()}");
                        if (p.QrCodes.Select(qr => qr.Code).Any(c => c == item)) continue;

                        p.QrCodes.Add(
                            new QrCode()
                            {
                                Code = item,
                                CreatedAt = DateTime.Now
                            });
                        p.SaveChanges();
                    }
                }
            }
        }

        private void SaveAllButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Item|*.csv";
            saveFileDialog.Title = "Save a Data File";

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            using (StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile()))
            {
                using (QrCodesDbContext p = new QrCodesDbContext())
                {
                    foreach (var item in p.QrCodes.OrderBy(t=>t.CreatedAt))
                    {
                        writer.WriteLine($"{item.Code.ToQrCode()},{item.CreatedAt:G}");
                    }
                }
            }
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

        private void CopyButton_Click(object sender, EventArgs e)
        {
            string s1 = "";
            foreach (object item in NewQrCodesList.Items)
                s1 += item.ToString() + "\r\n";
            Clipboard.SetText(s1);
        }

        private void StatisticButton_Click(object sender, EventArgs e)
        {
            using (QrCodesDbContext p = new QrCodesDbContext())
            {
                DateTime today = DateTime.Today;;
                long totalCount = p.QrCodes.Count();
                long thisMonthCount = p.QrCodes.Count(qr => qr.CreatedAt.Month == today.Month);
                long todayCount = p.QrCodes.Count(qr => qr.CreatedAt>=today);
                MessageBox.Show($"Total:{totalCount}\r\nThis Month:{thisMonthCount}\r\nToday:{todayCount}", "Statistic", MessageBoxButtons.OK);
            }
        }
    }
}
