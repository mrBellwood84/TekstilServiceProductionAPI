
using Simulator.Lib;


Console.WriteLine("\nSimulator started...");

var productionData = await SimulatorHttpClient.GetProductionData();
if (productionData == null )
{
    Console.WriteLine("\nCould not get data from API\nCheck if API is running and try again...");
    goto EndOfProgram;
}

var workers = new List<Worker>();

foreach (var p in productionData)
{
    var worker = new Worker(p);
    workers.Add(worker);
}


foreach (var worker in workers)
{
    ThreadPool.QueueUserWorkItem(state =>
    {
        worker.Tick();
    });
};


Console.WriteLine("Machines are running async");
Console.WriteLine("Press any key to exit program\n");
Console.Read();

EndOfProgram:
    Console.WriteLine("Program has exited\n");