using NBomber.Contracts.Stats;
using NBomber.CSharp;
using NBomber.Http.CSharp;
using NBomber.Sinks;
using Xunit.Abstractions;

namespace NBomberHttpTest
{
    public class LoadTest
    {
        [Fact]
        public async Task SingleRequest()
        {
            using var httpClient = new HttpClient();

            var scenario = Scenario.Create(name: "SingleRequest", run: async scenarioContext =>
            {
                var httpRequest = Http.CreateRequest(method: "GET", url: "https://reqres.in/api/users/2")
                .WithHeader(name: "content-type", "application/json");

                var httpResponse = await Http.Send(httpClient, httpRequest);

                return httpResponse;

            });

            NBomberRunner.RegisterScenarios(scenario)
                    .WithReportFolder("my-reports")
                    .Run();
        }

        [Fact]
        public async Task Inject100Users()
        {
            // Create an HttpClient instance for sending HTTP requests
            using var httpClient = new HttpClient();

            // Define the scenario for the load test
            var scenario = Scenario.Create("Inject-100-Users", async context =>
            {
                // Define the HTTP request
                var request = Http.CreateRequest("GET", "https://reqres.in/api/users/2")
                    .WithHeader("Content-Type", "application/json");

                // Send the request and get the response
                var response = await Http.Send(httpClient, request);

                // Return the HTTP response (used for measuring performance)
                return response;
            })
            // Inject 100 users/second for 3 minutes
            .WithLoadSimulations(Simulation.Inject(rate: 100,interval: TimeSpan.FromSeconds(1),during: TimeSpan.FromMinutes(3)));

            // Run the test and get the result
            NBomberRunner.RegisterScenarios(scenario)
                    .WithReportFinalizer(data =>
                    {
                        var scnStats = data.ScenarioStats
                            .Where(x => x.ScenarioName != "hidden")
                            .ToArray();

                        return ReportData.Create(scnStats);
                    })
                .Run();
        }

        [Fact]
        public async Task ConcurrentUser50()
        {
            // Create an HttpClient instance for sending HTTP requests
            using var httpClient = new HttpClient();

            // Define the scenario for the load test
            var scenario = Scenario.Create("Constant-Throughput-Test", async context =>
            {
                // Define the HTTP request
                var request = Http.CreateRequest("GET", "https://reqres.in/api/users/2")
                    .WithHeader("Content-Type", "application/json");

                // Send the request and get the response
                var response = await Http.Send(httpClient, request);

                // Return the HTTP response (used for measuring performance)
                return response;
            })
            // 50 users running constantly for 1 minutes
            .WithLoadSimulations(Simulation.KeepConstant(50, TimeSpan.FromMinutes(1)));

            // Run the test and get the result
            NBomberRunner.RegisterScenarios(scenario)
                    .WithReportFinalizer(data =>
                    {
                        var scnStats = data.ScenarioStats
                            .Where(x => x.ScenarioName != "hidden")
                            .ToArray();

                        return ReportData.Create(scnStats);
                    })
                .Run();
        }
    }
}