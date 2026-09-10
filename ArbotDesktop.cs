using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace Arbot__V_Console___V_FileData_;

public static class ArbotDesktop
{
    private const string LocalAppUrl = "http://127.0.0.1:5080";

    public static void Run(string[] args)
    {
        ApplicationConfiguration.Initialize();

        var host = ArbotWebHost.StartAsync(args).GetAwaiter().GetResult();
        using var window = new Form
        {
            Text = "Arbot",
            Width = 1240,
            Height = 820,
            MinimumSize = new System.Drawing.Size(920, 620),
            StartPosition = FormStartPosition.CenterScreen,
            BackColor = System.Drawing.Color.FromArgb(244, 245, 239)
        };

        using var browser = new WebView2 { Dock = DockStyle.Fill };
        window.Controls.Add(browser);
        window.Shown += async (_, _) =>
        {
            try
            {
                await browser.EnsureCoreWebView2Async();
                browser.CoreWebView2.Navigate(LocalAppUrl);
            }
            catch (Exception error)
            {
                MessageBox.Show(
                    $"Arbot could not open its local interface.\n\n{error.Message}",
                    "Arbot",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                window.Close();
            }
        };

        Application.Run(window);
        host.StopAsync().GetAwaiter().GetResult();
        host.DisposeAsync().AsTask().GetAwaiter().GetResult();
    }
}
