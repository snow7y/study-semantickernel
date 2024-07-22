using System;
using DotNetEnv;

Console.WriteLine("Hello, World!");

// .envファイルを読み込む
Env.Load();

// .envファイルの内容を取得
string apiKey = Env.GetString("TEST_KEY");

Console.WriteLine("apikey : " + apiKey);
