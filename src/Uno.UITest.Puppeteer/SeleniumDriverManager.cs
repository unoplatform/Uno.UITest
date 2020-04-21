using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace Uno.UITest.Selenium
{
	public class SeleniumDriverManager
	{
		public static class Chrome
		{
			// Chrome driver selection: http://chromedriver.chromium.org/downloads/version-selection

			public static ChromeDriver FromChromePath(string chromePath, ChromeOptions options)
				=> new ChromeDriver(
					new SeleniumDriverManager("chromedriver").GetOrDownloadLatestDriverForBin(
						chromePath ?? $@"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)}\Google\Chrome\Application\chrome.exe",
						GetDriverLatestVersion,
						GetDriverUri).FullName,
					options);

			public static ChromeDriver FromDriverPath(string driverPath, ChromeOptions options)
				=> new ChromeDriver(driverPath, options);

			private static Uri GetDriverLatestVersion(Version browserVersion) => new Uri($"https://chromedriver.storage.googleapis.com/LATEST_RELEASE_{browserVersion.Major}");
			private static Uri GetDriverUri(string driverVersion) => new Uri($"https://chromedriver.storage.googleapis.com/{driverVersion}/chromedriver_win32.zip");
		}

		public static class Edge
		{
			// Edge driver selection https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/#downloads
			// Edge driver documentation https://docs.microsoft.com/en-us/microsoft-edge/webdriver-chromium

			public static EdgeDriver FromEdgePath(string edgePath, EdgeOptions options)
			{
				edgePath = edgePath ?? $@"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)}\Microsoft\Edge Dev\Application\msedge.exe";
				options.BinaryLocation = edgePath;
				options.UseChromium = true;

				var manager = new SeleniumDriverManager("msedgedriver");
				var driverPath = manager.GetOrDownloadLatestDriverForBin(edgePath, null, /*GetDriverLatestVersion, */GetDriverUri);

				var svc = EdgeDriverService.CreateDefaultServiceFromOptions(driverPath.FullName, "msedgedriver.exe", options);
				svc.EnableVerboseLogging = true;

				var driver = new EdgeDriver(svc, options);

				return driver;
			}

			public static EdgeDriver FromDriverPath(string driverPath, EdgeOptions options)
				=> new EdgeDriver(driverPath, options);

			private static Uri GetDriverLatestVersion(Version browserVersion) => new Uri($"https://msedgedriver.azureedge.net/LATEST_RELEASE_{browserVersion.Major}");
			private static Uri GetDriverUri(string driverVersion) => new Uri($"https://msedgedriver.azureedge.net/{driverVersion}/edgedriver_win32.zip");
		}

		private SeleniumDriverManager(string driverName)
		{
			DriverName = driverName;
		}

		public string DriverName { get; }

		/// <summary>
		/// Gets the file version of the provided browser path
		/// </summary>
		/// <param name="browserPath">Path to the browser executable</param>
		protected Version GetVersion(string browserPath)
		{
			var process = new Process();
			process.StartInfo.FileName = "wmic.exe";
			process.StartInfo.Arguments = $@"datafile where name=""{browserPath.Replace("\\", "\\\\")}"" get Version /value";
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			process.Start();

			var wincOutput = process.StandardOutput.ReadToEnd();
			var browserRawVersion = wincOutput.Split('=').LastOrDefault()?.Trim();

			if(Version.TryParse(browserRawVersion, out var browserVersion))
			{
				return browserVersion;
			}
			else
			{
				throw new NotSupportedException($"Unable to determine the browser version. The used path was [{browserPath}], found raw [{browserRawVersion}] parsed [{browserVersion}].");
			}
		}

		/// <summary>
		/// Gets the target standard install path for the given version of this driver
		/// </summary>
		/// <param name="version">The version of the driver</param>
		protected FileInfo GetDriverInstallPath(Version version)
		{
			var driverLocalPath = Path.Combine(Path.GetTempPath(), "Uno.UITests", $"{DriverName}", version.ToString());
			Directory.CreateDirectory(driverLocalPath);

			return new FileInfo(driverLocalPath + $"\\{DriverName}.exe");
		}

		/// <summary>
		/// Download the zip package of the driver, and extract the driver executable to the target install path.
		/// </summary>
		/// <param name="driverSourceUri">The Uri of the package of the driver to download</param>
		/// <param name="driverInstallPath">The target install path of the driver</param>
		protected void Download(Uri driverSourceUri, FileInfo driverInstallPath)
		{
			var tempZipFileName = Path.GetTempFileName();
			try
			{
				Console.WriteLine($"Downloading {DriverName} from [{driverSourceUri.OriginalString}]");
				new WebClient().DownloadFile(driverSourceUri, tempZipFileName);

				using(var zipFile = ZipFile.OpenRead(tempZipFileName))
				{
					zipFile.GetEntry($"{DriverName}.exe").ExtractToFile(driverInstallPath.FullName, true);
				}
			}
			finally
			{
				if(File.Exists(tempZipFileName))
				{
					File.Delete(tempZipFileName);
				}
			}
		}

		protected delegate Uri GetDriverLatestVersion(Version browserVersion);
		protected delegate Uri GetDriverUri(string driverVersion);

		protected DirectoryInfo GetOrDownloadLatestDriverForBin(
			string binPath,
			GetDriverLatestVersion getDriverLatestVersion,
			GetDriverUri getDriverUri)
		{
			if(Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				var version = GetVersion(binPath);
				var driverFile = GetDriverInstallPath(version);

				if(!driverFile.Exists)
				{
					string driverVersion;
					if(getDriverLatestVersion == null)
					{
						driverVersion = version.ToString();
					}
					else
					{
						var driverLatestVersion = getDriverLatestVersion(version);

						Console.WriteLine($"Fetching driver version for {DriverName} [{version}]");
						driverVersion = new WebClient().DownloadString(driverLatestVersion).Trim();
					}

					var driverUri = getDriverUri(driverVersion);

					Download(driverUri, driverFile);
				}

				return driverFile.Directory;
			}
			else
			{
				throw new NotSupportedException($"Unable to determine the chrome driver location. Use the {SeleniumAppConfigurator.UNO_UITEST_DRIVER_PATH} environment variable.");
			}
		}
	}
}
