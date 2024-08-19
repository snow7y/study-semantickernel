// See https://aka.ms/new-console-template for more information
using chatConsoleApp.Plugins;
using DotNetEnv;

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

// .envファイルを読み込む
Env.Load();

// APIを利用するための設定
var builder = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion(
        deploymentName: Env.GetString("YOUR_MODEL_ID"),
        endpoint: Env.GetString("YOUR_ENDPOINT_NAME"),
        apiKey: Env.GetString("API_KEY")
        );



// カーネルをビルド
Kernel kernel = builder.Build();
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

// プラグインを追加
kernel.Plugins.AddFromType<DateTimePlugin>();



// Toolを指定
OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

// チャット履歴を作成
var history = new ChatHistory();



// ユーザーの入力を受け取り、それに対する応答を生成
string? userInput;
do
{
    Console.Write("User > ");
    // ユーザーの入力を受け取る
    userInput = Console.ReadLine();

    // ユーザーの入力が空の場合は、次の入力を待つ
    if (string.IsNullOrEmpty(userInput))
    {
        continue;
    }

    // チャット履歴にユーザーのメッセージを追加
    history.AddUserMessage(userInput);

    // チャット履歴を使用して、エージェントのメッセージを取得
    var result = await chatCompletionService.GetChatMessageContentAsync(
        history,
        executionSettings: openAIPromptExecutionSettings,
        kernel: kernel);

    // エージェントのメッセージを表示
    Console.WriteLine("Assistant > " + result);

    // チャット履歴にエージェントのメッセージを追加
    history.AddMessage(result.Role, result.Content ?? string.Empty);
} while (userInput is not null);
