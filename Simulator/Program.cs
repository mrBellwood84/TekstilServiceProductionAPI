
using Simulator.Lib;


var productionData = await SimulatorHttpClient.GetProductionData();
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
