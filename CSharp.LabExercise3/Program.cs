using System;
using System.Text;
using System.Threading;

namespace CSharp.LabExercise3
{
    class AtmState
    {
        public string selectionValue { get; set; }
        
        public AtmState()
        {
            selectionValue = "0";
        }
    }
    class UserAccount
    {
        public int accountID { get; set; }
        public string accountName { get; set; }
        public decimal accountBalance { get; set; }

        public UserAccount()
        {
            accountID = 42069;
            accountName = "Joe Salido";
            accountBalance = 0;
        }

        public void Credit(decimal amount)
        {
            if (amount > 0)
            { 
                this.accountBalance += amount;
            }
        }
        public void Debit(decimal amount)
        {
            if (amount > 0)
            {
                this.accountBalance -= amount;
            }
        }
    }

    class progressBar
    {
        public void showProgressBar()
        {
            Console.Write("\tProcessing transaction... ");
            using (var progress = new ProgressBar())
            {
                for (int i = 0; i <= 100; i++)
                {
                    progress.Report((double)i / 100);
                    Thread.Sleep(20);
                }
            }
            Console.WriteLine("Done.");
        }
    }
    class AtmProcessor
    {
        InputSelector inputSelector;
        AtmState atmState;
        OutputRenderer outputRenderer;
        UserAccount userAccount;

        public AtmProcessor(InputSelector inputSelector, AtmState atmState, OutputRenderer outputRenderer, UserAccount userAccount)
        {
            this.inputSelector = inputSelector;
            this.atmState = atmState;
            this.outputRenderer = outputRenderer;
            this.userAccount = userAccount;
            
            atmProcessInput();
        }

        private void atmProcessInput()
        {
           atmState.selectionValue = this.inputSelector.DisplaySelector();
           processTransaction();
           this.outputRenderer.DisplayOutput();
        }

        private void processTransaction()
        {
            switch(atmState.selectionValue)
            {
                case "2":
                    processTransaction_withdrawCash();
                    break;
                case "3":
                    processTransaction_depositCash();
                    break;
            }
        }

        private void processTransaction_depositCash()
        {
            decimal depositAmount = 0;
            Console.SetCursorPosition(0, 13);
            Console.WriteLine("*\tEnter Amount to Deposit: \t\t\t*");
            Console.WriteLine("*                                                       ");
            Console.WriteLine("*                                                       ");
            Console.WriteLine("*                                                       ");
            Console.SetCursorPosition(34, 13);
            try
            {
                depositAmount = Convert.ToDecimal(Console.ReadLine()); Console.SetCursorPosition(0, 15);
                if (depositAmount > 0)
                {
                    this.userAccount.Credit(depositAmount);
                }
                else
                {
                    Console.WriteLine("*    Invalid amount. Amount should be positive value!");
                    Console.Write("*\t    Press any key to try again . . . ");
                    Console.ReadKey();
                    this.atmState.selectionValue = "reset";
                }
            }
            catch
            {
                Console.SetCursorPosition(0, 15);
                Console.WriteLine("*\tTransaction Cancelled due to Invalid Input!    ");
                Console.Write("*\tPress any key to try again . . . ");
                Console.ReadKey();
                this.atmState.selectionValue = "reset";
            }
        }

        private void processTransaction_withdrawCash()
        {
            decimal withdrawlAmount = 0;
            Console.SetCursorPosition(0, 13);
            Console.WriteLine("*\tEnter Amount to Withdraw: \t\t\t*");
            Console.WriteLine("*                                                       ");
            Console.WriteLine("*                                                       ");
            Console.WriteLine("*                                                       ");
            Console.SetCursorPosition(34, 13);
            try
            {
                withdrawlAmount = Convert.ToDecimal(Console.ReadLine());

                Console.SetCursorPosition(0, 15);
                if (withdrawlAmount < 1)
                {
                    Console.WriteLine("*    Invalid amount. Amount should be positive value!");
                    Console.Write("*\t    Press any key to try again . . . ");
                    Console.ReadKey();
                    this.atmState.selectionValue = "reset";
                }
                else if (withdrawlAmount > this.userAccount.accountBalance)
                {
                    Console.WriteLine("*\t\t  Insufficient Funds!    ");
                    Console.Write("*\t    Press any key to try again . . . ");
                    Console.ReadKey();
                    this.atmState.selectionValue = "reset";
                }
                else if(withdrawlAmount % 100 != 0)
                {
                    Console.WriteLine("*\t    Amount must be divisible by 100!");
                    Console.Write("*\t    Press any key to try again . . . ");
                    Console.ReadKey();
                    this.atmState.selectionValue = "reset";
                }
                else
                {
                    this.userAccount.Debit(withdrawlAmount);
                }
            }
            catch
            {
                Console.SetCursorPosition(0, 15);
                Console.WriteLine("*\tTransaction Cancelled due to Invalid Input!    ");
                Console.Write("*\tPress any key to try again . . . ");
                Console.ReadKey();
                this.atmState.selectionValue = "reset";
            }

        }
    }
    class InputSelector
    { 
        public string DisplaySelector()
        {
            Console.SetCursorPosition(0, 7);
            Console.WriteLine("*********************************************************");
            Console.WriteLine("*\t\t\t\t\t\t\t*");
            Console.Write("*\t\tEnter Selection:\t\t\t*\n");
            Console.WriteLine("*\t\t\t\t\t\t\t*");
            Console.WriteLine("*********************************************************");
            Console.SetCursorPosition(33, 9);
            return Console.ReadLine();
        }
    }

    class OutputRenderer
    {
        AtmState atmState;
        UserAccount userAccount;
        progressBar progressBar = new progressBar();

        public OutputRenderer(AtmState atmState, UserAccount userAccount)
        {
            this.atmState = atmState;
            this.userAccount = userAccount;
        }

        public void DisplayAppHeader()
        {
            Console.WriteLine("********************* ATM Service! **********************");
            Console.WriteLine("*\t\t\t\t\t\t\t*");
            Console.WriteLine("*\t\t [1] - Check Balance\t\t\t*");
            Console.WriteLine("*\t\t [2] - Withdraw Cash\t\t\t*");
            Console.WriteLine("*\t\t [3] - Deposit Cash\t\t\t*");
            Console.WriteLine("*\t\t [4] - Quit\t\t\t\t*");
            Console.WriteLine("*\t\t\t\t\t\t\t*");
        }
        public void DisplayOutput()
        {
            Console.SetCursorPosition(0, 12);
            Console.WriteLine("*\t\t\t\t\t\t\t*");
            Console.WriteLine("*\t\t\t\t\t\t\t*");
            Console.WriteLine("*                                                       *");
            Console.WriteLine("*                                                       *");
            Console.SetCursorPosition(0, 14);

            if (atmState.selectionValue == "0")
            {
                Console.WriteLine("*\tWelcome to Joe's 3rd Lab Excercise!\t\t*");
                Console.WriteLine("*\t Please Select From Menu To Begin!\t\t*");
                Console.WriteLine("*\t\t\t\t\t\t\t*");
            }
            else if (atmState.selectionValue == "1")
            {
                Console.WriteLine($"*\tYOUR CURRENT BALANCE IS: P{String.Format("{0:N}", this.userAccount.accountBalance)}");
                Console.WriteLine("*\t\t\t\t\t\t\t*");
                Console.WriteLine("*\t\t\t\t\t\t\t*");
            }
            else if (atmState.selectionValue == "2")
            {
                Console.SetCursorPosition(0, 14);
                progressBar.showProgressBar();
                Console.WriteLine($"*\tYOUR CURRENT BALANCE IS: P{String.Format("{0:N}", this.userAccount.accountBalance)}");
                Console.Write("*\tPress any key to continue . . . ");
                Console.ReadKey();
                this.atmState.selectionValue = "reset";
            }
            else if (atmState.selectionValue == "3")
            {
                Console.SetCursorPosition(0, 14);
                progressBar.showProgressBar();
                Console.WriteLine($"*\tYOUR CURRENT BALANCE IS: P{String.Format("{0:N}", this.userAccount.accountBalance)}");
                Console.Write("*\tPress any key to continue . . . ");
                Console.ReadKey();
                this.atmState.selectionValue = "reset";
            }
            else if (atmState.selectionValue == "4")
            {
                Console.WriteLine("*\t    THANK YOU FOR USING OUR SYSTEM!\t\t*");
                Console.WriteLine("*\t\t   Come Again Later!\t\t\t*");
                Console.WriteLine("*\t\t\t\t\t\t\t*");
            }
            else if (atmState.selectionValue == "reset")
            {
                Console.WriteLine("*\t\tWelcome to ATM Services!\t\t*");
                Console.WriteLine("*\t    Please Select From Menu To Begin!\t\t*");
                Console.WriteLine("*\t\t\t\t\t\t\t*");
            }
            else
            {
                Console.WriteLine("*\t\t    Invalid Input!\t\t\t*");
                Console.WriteLine("*\t\t  Please Try Again!\t\t\t*");
                Console.WriteLine("*\t\t\t\t\t\t\t*");
            }

            Console.WriteLine("*\t\t\t\t\t\t\t*");
            Console.WriteLine("*********************************************************");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            UserAccount userAccount = new UserAccount();
            AtmState atmState = new AtmState();
            OutputRenderer ouputRenderer = new OutputRenderer(atmState, userAccount);
            InputSelector inputSelector = new InputSelector();
            while (true)
            {
                Console.Clear();
                ouputRenderer.DisplayAppHeader();
                ouputRenderer.DisplayOutput();

                AtmProcessor atmProcessor = new AtmProcessor(inputSelector, atmState, ouputRenderer, userAccount);

                if (atmState.selectionValue == "4")
                {
                    Environment.Exit(-1);
                }
            }
        }
    }

    public class ProgressBar : IDisposable, IProgress<double>
    {
        private const int blockCount = 10;
        private readonly TimeSpan animationInterval = TimeSpan.FromSeconds(1.0 / 8);
        private const string animation = @"|/-\";

        private readonly Timer timer;

        private double currentProgress = 0;
        private string currentText = string.Empty;
        private bool disposed = false;
        private int animationIndex = 0;

        public ProgressBar()
        {
            timer = new Timer(TimerHandler);

            // A progress bar is only for temporary display in a console window.
            // If the console output is redirected to a file, draw nothing.
            // Otherwise, we'll end up with a lot of garbage in the target file.
            if (!Console.IsOutputRedirected)
            {
                ResetTimer();
            }
        }

        public void Report(double value)
        {
            // Make sure value is in [0..1] range
            value = Math.Max(0, Math.Min(1, value));
            Interlocked.Exchange(ref currentProgress, value);
        }

        private void TimerHandler(object state)
        {
            lock (timer)
            {
                if (disposed) return;

                int progressBlockCount = (int)(currentProgress * blockCount);
                int percent = (int)(currentProgress * 100);
                string text = string.Format("[{0}{1}] {2,3}% {3}",
                    new string('#', progressBlockCount), new string('-', blockCount - progressBlockCount),
                    percent,
                    animation[animationIndex++ % animation.Length]);
                UpdateText(text);

                ResetTimer();
            }
        }

        private void UpdateText(string text)
        {
            // Get length of common portion
            int commonPrefixLength = 0;
            int commonLength = Math.Min(currentText.Length, text.Length);
            while (commonPrefixLength < commonLength && text[commonPrefixLength] == currentText[commonPrefixLength])
            {
                commonPrefixLength++;
            }

            // Backtrack to the first differing character
            StringBuilder outputBuilder = new StringBuilder();
            outputBuilder.Append('\b', currentText.Length - commonPrefixLength);

            // Output new suffix
            outputBuilder.Append(text.Substring(commonPrefixLength));

            // If the new text is shorter than the old one: delete overlapping characters
            int overlapCount = currentText.Length - text.Length;
            if (overlapCount > 0)
            {
                outputBuilder.Append(' ', overlapCount);
                outputBuilder.Append('\b', overlapCount);
            }

            Console.Write(outputBuilder);
            currentText = text;
        }

        private void ResetTimer()
        {
            timer.Change(animationInterval, TimeSpan.FromMilliseconds(-1));
        }

        public void Dispose()
        {
            lock (timer)
            {
                disposed = true;
                UpdateText(string.Empty);
            }
        }

    }
}
