# CCSWE 500px API

A C# implementation of the 500px API using OAuth version 1.0.

Usage is straightforward. Windows Forms example:

    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using CCSWE.FiveHundredPx;

    namespace Test
    {
        public partial class Form1 : Form
        {
            public Form1()
            {
                InitializeComponent();

                _listener = new HttpListener();
                _listener.Prefixes.Add("http://localhost:1234/OAuth/");
                _listener.Start();

                var test2 = new Thread(Listen);
                test2.Start();

            }

            private static HttpListener _listener;

            private async void Listen()
            {
                while (true)
                {
                    var test = _listener.GetContext();
                    var response = test.Response;

                    var test2 = test.Request;
                    string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
                    byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                    response.ContentLength64 = buffer.Length;
                    Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();

                    foreach (var key in test2.QueryString.AllKeys)
                    {
                        switch (key)
                        {
                            case OAuthParameter.Token:
                            {
                                _requestToken.Token = test2.QueryString[key];
                                break;
                            }
                            case OAuthParameter.Verifier:
                            {
                                _requestToken.Verifier = test2.QueryString[key];
                                break;
                            }
                        }
                    }

                    var accessToken = await _service.AccessToken(_requestToken);
                    _service.Token = accessToken;
                    // The GetCurrentUser() method is an authenticated request and retrieves the details of the authenticated user
                    var response3 = await _service.GetCurrentUser();
                }
            }

            private OAuthToken _requestToken;
            private FiveHundredPxService _service = new FiveHundredPxService("<CONSUMER KEY>", "<CONSUMER SECRET>", "http://localhost:1234/OAuth/");

            private async void button1_Click(object sender, EventArgs e)
            {
                _requestToken = await _service.RequestToken();
                webBrowser1.Navigate(_service.AuthorizeToken(_requestToken));
            }
        }
    }
