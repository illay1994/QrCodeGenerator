using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
                List<long> batchCodes = new List<long>();
                string data;
                while ((data = await reader.ReadLineAsync()) != null)
                {
                    progressBar1.Value++;
                    batchCodes.AddRange(
                            data.Split(';', ',')
                                .AsParallel()
                                .Select(t => regex.Match(t))
                                .Where(t => t.Success)
                                .Select(t => t.Groups[1].Value)
                                .Select(str =>
                                {
                                    bool success = long.TryParse(str, out long value);
                                    return new { value, success };
                                })
                                .Where(pair => pair.success)
                                .Select(pair => pair.value)
                            .ToArray());
                    if (batchCodes.Count < 1000) continue;
                    await AddOrSkipItem(batchCodes);
                    batchCodes.Clear();
                }
                await AddOrSkipItem(batchCodes);

            }
            panel1.Enabled = true;
            menuStrip1.Enabled = true;
            progressBar1.Visible = false;
        }

        public async Task AddOrSkipItem(ICollection<long> batchCodes)
        {
            using (QrCodesDbContext p = new QrCodesDbContext())
            {
                var existing = p.QrCodes.Select(qr => qr.Code).Where(t => batchCodes.Contains(t));
                var insertedData = batchCodes
                    .Except(existing)
                    .Select(t => new QrCode()
                    {
                        Code = t,
                        CreatedAt = DateTime.Now
                    });
                p.QrCodes.AddRange(insertedData);
                await p.SaveChangesAsync();
            }
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
                while (newData.Count < count)
                {
                    var temp = GenerateNewElements(Math.Min(count - newData.Count, 1000), random, newData);
                    temp.ExceptWith(p.QrCodes.Select(t => t.Code).Where(t => temp.Contains(t)));
                    newData.UnionWith(temp);
                    progressBar1.Value = newData.Count;
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

        private static ISet<long> GenerateNewElements(long count, Random random, ISet<long> existSet)
        {
            var result = new HashSet<long>();
            for (int index = 0; index < count; index++)
            {
                long data = random.Next(1000000000000);
                while (existSet.Contains(data))
                {
                    data = random.Next(1000000000000);
                }
                result.Add(data);
            }
            return result;
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

        private async void CleanDbButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want clean DataBase?", "Comfirm", MessageBoxButtons.YesNo) !=
                    DialogResult.Yes)
            {
                return;
            }
            using (QrCodesDbContext p = new QrCodesDbContext())
            {
                progressBar1.Maximum = p.QrCodes.Count() / 1000;
                progressBar1.Value = 0;
                progressBar1.Visible = true;
                panel1.Enabled = false;
                menuStrip1.Enabled = false;
                do
                {
                    progressBar1.Value++;
                    p.QrCodes.RemoveRange(p.QrCodes.Take(1000));
                } while (await p.SaveChangesAsync() > 0);
                progressBar1.Visible = false;
                panel1.Enabled = true;
                menuStrip1.Enabled = true;
            }
        }
    }
}
