# NBomber Http Load Testing

This repository contains a set of load testing scenarios implemented using the [NBomber](https://nbomber.com/) framework in C#. The tests focus on simulating different HTTP request loads and measuring the performance of an API endpoint.

## Prerequisites

To run these tests, make sure you have the following:

- [.NET SDK](https://dotnet.microsoft.com/download) installed.
- The following NuGet packages:
  - `NBomber`
  - `NBomber.Http.CSharp`
  - `XUnit` for testing framework integration
  - `NBomber.Sinks` for report generation

## Load Testing Scenarios

### 1. **Single Request Test**

This scenario sends a single HTTP GET request to `https://reqres.in/api/users/2` and logs performance metrics.

#### Scenario:
- **Name**: `SingleRequest`
- **HTTP Method**: `GET`
- **URL**: `https://reqres.in/api/users/2`
- **Load Simulation**: One request executed.

### 2. **Inject 100 Users**

This test simulates 100 users per second making GET requests to the same API endpoint for 3 minutes.

#### Scenario:
- **Name**: `Inject-100-Users`
- **HTTP Method**: `GET`
- **URL**: `https://reqres.in/api/users/2`
- **Load Simulation**: 100 users per second for 3 minutes.

### 3. **Concurrent 50 Users**

In this scenario, 50 concurrent users send GET requests to the API endpoint for 1 minute.

#### Scenario:
- **Name**: `Constant-Throughput-Test`
- **HTTP Method**: `GET`
- **URL**: `https://reqres.in/api/users/2`
- **Load Simulation**: 50 concurrent users for 1 minute.

## Report Generation

Each scenario generates performance reports which are stored in the `my-reports` directory. The `WithReportFolder` and `WithReportFinalizer` methods allow for custom report handling and finalization.

![Alt text](https://github.com/prghub/NBomberLoadTest/blob/master/Inject100Users.JPG)
![Alt text](https://github.com/prghub/NBomberLoadTest/blob/master/Throughput.JPG)


### Custom Report Finalizer
The `WithReportFinalizer` method processes the scenario data, filtering out scenarios marked as "hidden" and creating a summary report.

## Running the Tests

To execute the tests, follow these steps:

1. Clone the repository.
2. Open the project in Visual Studio or any other compatible IDE.
3. Run the tests using your preferred test runner (e.g., `XUnit` test runner).

```bash
dotnet test
