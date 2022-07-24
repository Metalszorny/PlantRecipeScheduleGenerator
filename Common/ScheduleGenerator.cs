using Common.Model;

namespace Common
{
    public static class ScheduleGenerator
    {
        /// <summary>
        /// Generates a schedule for the provided trays using the given recipes.
        /// </summary>
        /// <param name="input">The collection of input containing information about each tray, recipe and start date.</param>
        /// <param name="recipes">The collection of recipes for the plants containing information what lighting and watering phases they need.</param>
        /// <returns>The collection of output for each tray containing start dates for lighting settings and watering amounts.</returns>
        public static ICollection<Output> GenerateSchedule(ICollection<Input> input, ICollection<Recipe> recipes)
        {
            if (input == null || recipes == null || !input.Any() || !recipes.Any())
            {
                return null;
            }

            var result = new List<Output>();

            // Loop though the trays and process the recipe for each of them.
            foreach (var inputItem in input.OrderBy(tray => tray.StartDate))
            {
                // Get the recipe for the tray.
                var recipe = recipes.FirstOrDefault(recipe => recipe.Name == inputItem.RecipeName);

                if (recipe == null)
                {
                    continue;
                }

                // If any lighting phases are specified, create outputs from them and add them to the results.
                if (recipe.LightingPhases != null && recipe.LightingPhases.Any())
                {
                    var lightingOutputs = GenerateScheduleFromLightingPhases(recipe.LightingPhases, inputItem);
                    result.AddRange(lightingOutputs);
                }

                // If any watering phases are specified, create outputs from them and add them to the results.
                if (recipe.WateringPhases != null && recipe.WateringPhases.Any())
                {
                    var wateringOutputs = GenerateScheduleFromWateringPhases(recipe.WateringPhases, inputItem);
                    result.AddRange(wateringOutputs);
                }
            }

            return result;
        }

        /// <summary>
        /// Generates a schedule from the provided lighting phases for the tray.
        /// </summary>
        /// <param name="lightingPhases">The collection of lighting phases for the tray.</param>
        /// <param name="input">The input information of the tray containing the tray number and start date.</param>
        /// <returns>The collection of output for the tray containing start dates for lighting settings.</returns>
        private static ICollection<Output> GenerateScheduleFromLightingPhases(ICollection<LightingPhase> lightingPhases, Input input)
        {
            if (lightingPhases == null || lightingPhases.Count < 1)
            {
                return null;
            }

            var result = new List<Output>();
            var currentDate = input.StartDate;

            // Loop through the lighting phases in order and add them to the outputs in the number of the repetition if a operations are specified.
            foreach (var lightingPhase in lightingPhases.OrderBy(phase => phase.Order))
            {
                // The assumption was made that the operations can hold invalid values and in such schenarios the process should continue with the next phase.
                if (lightingPhase.Operations == null || lightingPhase.Operations.Count < 1)
                {
                    continue;
                }

                // The assumption was made that the repetition holds the value of how many times the phase is done, meaning if it hold 0 as a value then the phase in not done.
                for (var i = 0; i < lightingPhase.Repetitions; i++)
                {
                    var lightingPhaseOutputs = lightingPhase.Operations
                        .OrderBy(operation => operation.OffsetHours)
                        .Select(operation => new LightingOutput()
                    {
                        TrayNumber = input.TrayNumber,
                        LightIntensity = operation.LightIntensity,
                        StartDate = currentDate.AddHours(operation.OffsetHours).AddMinutes(operation.OffsetMinutes),
                    });
                    result.AddRange(lightingPhaseOutputs);
                    currentDate = currentDate.AddHours(lightingPhase.Hours).AddMinutes(lightingPhase.Minutes);
                }
            }

            return result;
        }

        /// <summary>
        /// Generates a schedule from the provided watering phases for the tray.
        /// </summary>
        /// <param name="wateringPhases">The collection of watering phases for the tray.</param>
        /// <param name="input">The input information of the tray containing the tray number and start date.</param>
        /// <returns>The collection of output for the tray containing start dates for watering amounts.</returns>
        private static ICollection<Output> GenerateScheduleFromWateringPhases(ICollection<WateringPhase> wateringPhases, Input input)
        {
            if (wateringPhases == null || wateringPhases.Count < 1)
            {
                return null;
            }

            var result = new List<Output>();
            var currentDate = input.StartDate;

            // Loop through the watering phases in order and add them to the outputs in the number of the repetition if an amount is specified.
            foreach (var wateringPhase in wateringPhases.OrderBy(phase => phase.Order))
            {
                // The assumption was made that if the water amount in a phase is yero or less, then there is no point in including it in the output as it would only take up processing time with no real use.
                if (wateringPhase.Amount <= 0)
                {
                    continue;
                }

                // The assumption was made that the repetition holds the value of how many times the phase is done, meaning if it hold 0 as a value then the phase in not done.
                for (var i = 0; i < wateringPhase.Repetitions; i++)
                {
                    var wateringPhaseOutput = new WateringOutput()
                    {
                        TrayNumber = input.TrayNumber,
                        WaterAmount = wateringPhase.Amount,
                        StartDate = currentDate,
                    };
                    result.Add(wateringPhaseOutput);
                    currentDate = currentDate.AddHours(wateringPhase.Hours).AddMinutes(wateringPhase.Minutes);
                }
            }

            return result;
        }
    }
}