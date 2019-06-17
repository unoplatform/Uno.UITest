using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;

namespace Uno.UITest.Helpers.Queries
{
    public static class Helpers
    {
        private static IApp _app;

        private static int screenshotSuspensions = 0;

        private static bool? runningOnSimulator;

        public static IApp App
        {
            get
            {
				if(_app == null)
				{
					throw new InvalidOperationException("Helpers.App must be set before test code runs");
				}
                return Helpers._app;
            }
            set
            {
                Helpers._app = value;
            }
        }

        public static bool ScreenshotsSuspended => 0 < Helpers.screenshotSuspensions;

        public static QueryEx Any => QueryEx.Any;

        public static QueryEx Visible => QueryEx.Visible;

        public static QueryEx Button => QueryEx.Button;

        public static QueryEx Table => QueryEx.Table;

        public static QueryEx Cell => QueryEx.Cell;

        public static QueryEx Entry => QueryEx.Entry;

        public static QueryEx Switch => QueryEx.Switch;

        public static QueryEx Label => QueryEx.Label;

        public static BackdoorProxy Backdoor => new BackdoorProxy("Xamarin.UITest.Backdoor");

        private static void DoNothing()
        {
        }

		public static bool RunningOnSimulator
		{
			get
			{
				bool? flag = Helpers.runningOnSimulator;
				if(!flag.HasValue)
				{
					if(Helpers.App is Xamarin.XamarinApp xa)
					{
						string input = xa.Source.TestServer.Get("version");
						Helpers.runningOnSimulator = new bool?(Regex.IsMatch(input, "\"simulator_device\":\"(.+)\""));
					}
					else
					{
						Helpers.runningOnSimulator = false;
					}
				}
				return Helpers.runningOnSimulator.Value;
			}
		}

		public static Platform Platform
		{
			get
			{
				if(Helpers.App is Xamarin.XamarinApp xa)
				{
					if(xa.Source is global::Xamarin.UITest.iOS.iOSApp)
					{
						return Platform.iOS;
					}
					if(xa.Source is global::Xamarin.UITest.Android.AndroidApp)
					{
						return Platform.Android;
					}
				}
				else
				{
					if(Helpers.App is Selenium.SeleniumApp sa)
					{
						return Platform.Browser;
					}
				}
				throw new Exception("Current platform cannot be determined");
			}
		}

		public static class Android
		{
			public static void PressDefaultUserAction() => OnAndroid(delegate (global::Xamarin.UITest.Android.AndroidApp app)
			{
				app.PressUserAction(null);
			});

			public static void PressUserAction(global::Xamarin.UITest.Android.UserAction action) =>
				OnAndroid(delegate (global::Xamarin.UITest.Android.AndroidApp app)
				{
					app.PressUserAction(new global::Xamarin.UITest.Android.UserAction?(action));
				});
		}

		public static T On<T>(T iOS, T Android)
		{
			var platform = Platform;
			if(platform == Platform.Android)
			{
				return Android;
			}

			if(platform != Platform.iOS)
			{
				throw new ArgumentOutOfRangeException();
			}

			return iOS;
		}

		public static void OniOS(Action act) => Helpers.On<Action>(act, () => { })();

		public static void OnAndroid(Action act) => Helpers.On<Action>(() => { }, act)();

		public static void OniOS<T>(Func<T> f) => Helpers.OniOS<T>(() => f());

		public static void OnAndroid<T>(Func<T> f) => Helpers.OnAndroid<T>(() => f());

		public static void OniOS(Action<global::Xamarin.UITest.iOS.iOSApp> act) => Helpers.OniOS(() =>
		{
			act((Helpers.App as Xamarin.XamarinApp)?.Source as global::Xamarin.UITest.iOS.iOSApp);
		});

		public static void OnAndroid(Action<global::Xamarin.UITest.Android.AndroidApp> act) => Helpers.OnAndroid(() =>
		{
			act((Helpers.App as Xamarin.XamarinApp)?.Source as global::Xamarin.UITest.Android.AndroidApp);
		});

		public static void OniOS<T>(Func<global::Xamarin.UITest.iOS.iOSApp, T> f) => Helpers.OniOS<T>((global::Xamarin.UITest.iOS.iOSApp app) => f(app));

		public static void OnAndroid<T>(Func<global::Xamarin.UITest.Android.AndroidApp, T> f) => Helpers.OnAndroid<T>((global::Xamarin.UITest.Android.AndroidApp app) => f(app));

		private static Action Void<T>(Func<T> f) => delegate
                                                              {
                                                                  f();
                                                              };

        public static void SuspendScreenshots() => Helpers.screenshotSuspensions++;

        public static void ResumeScreenshots() => Helpers.screenshotSuspensions--;

        public static void WithScreenshotsSuspended(Action act)
        {
            Helpers.SuspendScreenshots();
            act();
            Helpers.ResumeScreenshots();
        }

        public static T WithScreenshotsSuspended<T>(Func<T> f)
        {
            T t = default(T);
            Helpers.WithScreenshotsSuspended<T>(() => t = f());
            return t;
        }

        public static void Screenshot(string label)
        {
            if (Helpers.ScreenshotsSuspended)
            {
                return;
            }
            Helpers.App.Screenshot(label);
        }

        public static void Interact() => Helpers.App.Repl();

        public static void PressEnter() => Helpers.App.PressEnter();

        public static void DismissKeyboard() => Helpers.App.DismissKeyboard();

        public static QueryEx Raw(string s) => Helpers.Any.Raw(s);

        public static void Back() => Helpers.App.Back();

        public static void Wait(TimeSpan time) => Thread.Sleep(time);

        public static void Wait(int seconds) => Helpers.Wait(TimeSpan.FromSeconds((double)seconds));

        public static void Wait(float seconds) => Helpers.Wait(TimeSpan.FromSeconds((double)seconds));

        public static void WaitUntilExists(params QueryEx[] queries)
        {
            for (int i = 0; i < queries.Length; i++)
            {
                QueryEx query = queries[i];
                Helpers.App.WaitForElement(query, "Timed out waiting for element...", null, null, null);
            }
        }

        public static void First(string title, Action actions = null) => Helpers.Step(title, actions, "First");

        public static void First<T>(string title, Func<T> f) => Helpers.First(title, Helpers.Void<T>(f));

        public static void Then(string title, Action actions = null) => Helpers.Step(title, actions, "Then");

        public static void Then<T>(string title, Func<T> f) => Helpers.Then(title, Helpers.Void<T>(f));

        public static void Step(string title, Action actions = null, string prefix = "")
        {
            actions = actions ?? Helpers.DoNothing;
            actions();
            Helpers.Screenshot(string.Format("{0} {1}", prefix, title));
        }

        public static void ThenTap(string marked) => Helpers.ThenTap(marked, Helpers.Any.Marked(marked));

        public static void ThenTap(string label, QueryEx query) => Helpers.Then<QueryEx>(string.Format("I tap '{0}'", label), () => query.Tap());

        public static void Tap(QueryEx query) => query.Tap();

        public static QueryEx ScrollDownTo(QueryEx query) => query.ScrollDownTo();

        public static QueryEx ScrollUpTo(QueryEx query) => query.ScrollUpTo();
        public static void ShouldBeVisible(params QueryEx[] queries)
        {
            for (int i = 0; i < queries.Length; i++)
            {
                QueryEx query = queries[i];
                query.ShouldBeVisible(null);
            }
        }

        public static void ShouldNotBeVisible(params QueryEx[] queries)
        {
            for (int i = 0; i < queries.Length; i++)
            {
                QueryEx query = queries[i];
                query.ShouldNotBeVisible(null);
            }
        }
    }
}
