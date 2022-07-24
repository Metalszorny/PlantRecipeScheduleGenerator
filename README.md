# PlantRecipeScheduleGenerator

## Objective
Given the list of recipe names, start dates and tray numbers outlined below, create an application that queries the Recipe API (see info below) and generates a JSON file with a schedule.
The schedule should outline at exactly what time light and water commands should be sent to the Tower, and what the light intensity or amount to water should be.
The dates in the schedule should be in UTC.
```json
{
  input: [
    {
      trayNumber: 1,
      recipeName: "Basil",
      startDate: "2022-01-24T12:30:00.0000000Z"
    },
    {
      trayNumber: 2,
      recipeName: "Strawberries",
      startDate: "2021-13-08T17:33:00.0000000Z"
    },
    {
      trayNumber: 3,
      recipeName: "Basil",
      startDate: "2030-01-01T23:45:00.0000000Z"
    }
  ]
}
```

## Assumptions:
- The 2021-13-08T17:33:00.0000000Z timestamp in the example is assumed to be wrong an is corrected to be 2021-12-08T17:33:00.0000000Z in all test schenarios.
- It is assumed the the output records should only contain the tray's number, the start date, the water amount and the lighting setting values in them.
- It is assumed that separate processes are responnsible for setting lighting and watering for the plants and thus schedule records are separated into lighting schedule and watering schedule outputs only containing the respective settings.
- For the same reason as above records with matching timestamps are kept as separate output records, this also helps not disturb in progress processes with default or previous values if merged togeather.
- It is assumed that the repetition field in phases holds the number of times the phase needs to be run, meaning a value of N will result in N output records of the phase with adjusted start dates and it can hold 0 as a value, meanining that phase should not be included.
- It is assumed that watering amounts vith 0 as a value are left out from the output records as it holds no useful data for the processing units.
- It is assumed that lighting phases can have no operations, these are left out of the output records.

## Runing the application:
The application should be dockerized and a 'docker-compose up' should start it.
If not, then opening it in Visual Studio and running it IIS or IIS Express should start the application.