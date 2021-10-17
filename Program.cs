using System;
using System.Diagnostics;
using LibGit2Sharp;

namespace GitTools
{
    class Program
    {
        static void cmd_DataReceived(object sender, DataReceivedEventArgs e)
        {
            // Console.WriteLine("Output from other process");
            Console.WriteLine(e.Data);
        }


        static void Main(string[] args)
        {
            string repo = "";
            var test = new Repository(repo);
            foreach(var branch in test.Branches) {
                // uh... probably go off of the remote name?
                // branch.RemoteName;
            }
        }
    }
}