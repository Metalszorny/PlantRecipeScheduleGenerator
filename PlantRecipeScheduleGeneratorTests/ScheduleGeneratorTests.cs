using Common;
using Common.Enums;
using Common.Model;

namespace PlantRecipeScheduleGeneratorTests
{
    public class Tests
    {
        [Test]
        public void Test_Should_ReturnNull_When_CalledWithNulls()
        {
            ICollection<Input> input = null;
            ICollection<Recipe> recipes = null;
            ICollection<Output> expectedResult = null;

            var actualResult = ScheduleGenerator.GenerateSchedule(input, recipes);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Test_Should_ReturnNull_When_CalledWithNullForInput()
        {
            ICollection<Input> input = null;
            var recipes = new List<Recipe>();
            ICollection<Output> expectedResult = null;

            var actualResult = ScheduleGenerator.GenerateSchedule(input, recipes);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Test_Should_ReturnNull_When_CalledWithNullForRecipes()
        {
            var input = new List<Input>();
            ICollection<Recipe> recipes = null;
            ICollection<Output> expectedResult = null;

            var actualResult = ScheduleGenerator.GenerateSchedule(input, recipes);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Test_Should_ReturnNull_When_CalledWithEmptyCollections()
        {
            var input = new List<Input>();
            var recipes = new List<Recipe>();
            ICollection<Output> expectedResult = null;

            var actualResult = ScheduleGenerator.GenerateSchedule(input, recipes);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Test_Should_ReturnNull_When_CalledWithEmptyCollectionForRecipes()
        {
            var input = new List<Input>()
            {
                new Input()
                {
                    TrayNumber = 1,
                    RecipeName = "Test",
                    StartDate = DateTime.Now,
                },
            };
            var recipes = new List<Recipe>();
            ICollection<Output> expectedResult = null;

            var actualResult = ScheduleGenerator.GenerateSchedule(input, recipes);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Test_Should_ReturnNull_When_CalledWithEmptyCollectionForInput()
        {
            var input = new List<Input>();
            var testRecipe = new Recipe("Test");
            testRecipe.WateringPhases.Add(new WateringPhase("Test", 0, 0, 0, 0, 0));
            var recipes = new List<Recipe>()
            {
                testRecipe,
            };
            ICollection<Output> expectedResult = null;

            var actualResult = ScheduleGenerator.GenerateSchedule(input, recipes);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Test_Should_ReturnEmptyCollection_When_CalledWithNoMatchingInputAndRecipes()
        {
            var input = new List<Input>()
            {
                new Input()
                {
                    TrayNumber = 1,
                    RecipeName = "Test1",
                    StartDate = DateTime.Now,
                },
            };
            var testRecipe = new Recipe("Test2");
            testRecipe.WateringPhases.Add(new WateringPhase("Test", 0, 0, 0, 0, 0));
            var recipes = new List<Recipe>()
            {
                testRecipe,
            };
            var expectedResult = new Output[0];

            var actualResult = ScheduleGenerator.GenerateSchedule(input, recipes);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Test_Should_ReturnEmptyCollection_When_CalledWithMatchingInputAndRecipeWithNoRepetition()
        {
            var input = new List<Input>()
            {
                new Input()
                {
                    TrayNumber = 1,
                    RecipeName = "Test",
                    StartDate = DateTime.Now,
                },
            };
            var testRecipe = new Recipe("Test");
            testRecipe.WateringPhases.Add(new WateringPhase("Test", 0, 0, 0, 0, 0));
            var recipes = new List<Recipe>()
            {
                testRecipe,
            };
            var expectedResult = new Output[0];

            var actualResult = ScheduleGenerator.GenerateSchedule(input, recipes);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Test_Should_ReturnEmptyCollection_When_CalledWithMatchingInputAndRecipeWithNoWaterAmount()
        {
            var startDate = DateTime.Now;
            var input = new List<Input>()
            {
                new Input()
                {
                    TrayNumber = 1,
                    RecipeName = "Test",
                    StartDate = startDate,
                },
            };
            var testRecipe = new Recipe("Test");
            testRecipe.WateringPhases.Add(new WateringPhase("Test", 0, 0, 0, 1, 0));
            var recipes = new List<Recipe>()
            {
                testRecipe,
            };
            var expectedResult = new Output[0];

            var actualResult = ScheduleGenerator.GenerateSchedule(input, recipes);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Test_Should_ReturnEmptyCollection_When_CalledWithMatchingInputAndRecipeWithNullAsLightingPhases()
        {
            var startDate = DateTime.Now;
            var input = new List<Input>()
            {
                new Input()
                {
                    TrayNumber = 1,
                    RecipeName = "Test",
                    StartDate = startDate,
                },
            };
            var testRecipe = new Recipe("Test");
            testRecipe.LightingPhases.Add(new LightingPhase("Test", 0, 0, 0, 1)
            {
                Operations = null,
            });
            var recipes = new List<Recipe>()
            {
                testRecipe,
            };
            var expectedResult = new Output[0];

            var actualResult = ScheduleGenerator.GenerateSchedule(input, recipes);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Test_Should_ReturnEmptyCollection_When_CalledWithMatchingInputAndRecipeWithEmptyCollectionAsLightingPhases()
        {
            var startDate = DateTime.Now;
            var input = new List<Input>()
            {
                new Input()
                {
                    TrayNumber = 1,
                    RecipeName = "Test",
                    StartDate = startDate,
                },
            };
            var testRecipe = new Recipe("Test");
            testRecipe.LightingPhases.Add(new LightingPhase("Test", 0, 0, 0, 1)
            {
                Operations = new List<LightingPhaseOperation>(),
            });
            var recipes = new List<Recipe>()
            {
                testRecipe,
            };
            var expectedResult = new Output[0];

            var actualResult = ScheduleGenerator.GenerateSchedule(input, recipes);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Test_Should_ReturnSingleItemOutput_When_CalledWithMatchingInputAndRecipeWithSingleLightingPhase()
        {
            var startDate = DateTime.Now;
            var input = new List<Input>()
            {
                new Input()
                {
                    TrayNumber = 1,
                    RecipeName = "Test",
                    StartDate = startDate,
                },
            };
            var testRecipe = new Recipe("Test");
            testRecipe.LightingPhases.Add(new LightingPhase("Test", 0, 0, 0, 1)
            {
                Operations = new List<LightingPhaseOperation>()
                {
                    new LightingPhaseOperation(0, 0, LightIntensity.Low)
                },
            });
            var recipes = new List<Recipe>()
            {
                testRecipe,
            };
            var expectedResult = new List<Output>()
            {
                new LightingOutput(1, startDate, LightIntensity.Low),
            };

            var actualResult = ScheduleGenerator.GenerateSchedule(input, recipes);

            Assert.That(actualResult, Is.Not.Null);
            Assert.That(actualResult, Is.TypeOf(typeof(List<Output>)));
            Assert.That(actualResult, Has.Count.EqualTo(expectedResult.Count));

            for (var i = 0; i < expectedResult.Count; i++)
            {
                Assert.That(actualResult.ElementAt(i).StartDate, Is.EqualTo(expectedResult.ElementAt(i).StartDate));
                Assert.That(actualResult.ElementAt(i).TrayNumber, Is.EqualTo(expectedResult.ElementAt(i).TrayNumber));

                if (actualResult.ElementAt(i).GetType() == typeof(WateringOutput))
                {
                    Assert.That(((WateringOutput)actualResult.ElementAt(i)).WaterAmount, Is.EqualTo(((WateringOutput)expectedResult.ElementAt(i)).WaterAmount));
                }
                else
                {
                    Assert.That(((LightingOutput)actualResult.ElementAt(i)).LightIntensity, Is.EqualTo(((LightingOutput)expectedResult.ElementAt(i)).LightIntensity));
                }
            }
        }

        [Test]
        public void Test_Should_ReturnSingleItemOutput_When_CalledWithMatchingInputAndRecipeWithSingleWateringPhase()
        {
            var startDate = DateTime.Now;
            var input = new List<Input>()
            {
                new Input()
                {
                    TrayNumber = 1,
                    RecipeName = "Test",
                    StartDate = startDate,
                },
            };
            var testRecipe = new Recipe("Test");
            testRecipe.WateringPhases.Add(new WateringPhase("Test", 0, 0, 0, 1, 1));
            var recipes = new List<Recipe>()
            {
                testRecipe,
            };
            var expectedResult = new List<Output>()
            {
                new WateringOutput(1, startDate, 1),
            };

            var actualResult = ScheduleGenerator.GenerateSchedule(input, recipes);

            Assert.That(actualResult, Is.Not.Null);
            Assert.That(actualResult, Is.TypeOf(typeof(List<Output>)));
            Assert.That(actualResult, Has.Count.EqualTo(expectedResult.Count));

            for (var i = 0; i < expectedResult.Count; i++)
            {
                Assert.That(actualResult.ElementAt(i).StartDate, Is.EqualTo(expectedResult.ElementAt(i).StartDate));
                Assert.That(actualResult.ElementAt(i).TrayNumber, Is.EqualTo(expectedResult.ElementAt(i).TrayNumber));

                if (actualResult.ElementAt(i).GetType() == typeof(WateringOutput))
                {
                    Assert.That(((WateringOutput)actualResult.ElementAt(i)).WaterAmount, Is.EqualTo(((WateringOutput)expectedResult.ElementAt(i)).WaterAmount));
                }
                else
                {
                    Assert.That(((LightingOutput)actualResult.ElementAt(i)).LightIntensity, Is.EqualTo(((LightingOutput)expectedResult.ElementAt(i)).LightIntensity));
                }
            }
        }

        [Test]
        public void Test_Should_ReturnExampleOutput_When_CalledWithExampleInputAndRecipes()
        {
            var input = new List<Input>()
            {
                new Input()
                {
                    TrayNumber = 1,
                    RecipeName = "Basil",
                    StartDate = DateTime.Parse("2022-01-24 12:30:00.000"),
                },
                new Input()
                {
                    TrayNumber = 2,
                    RecipeName = "Strawberries",
                    StartDate = DateTime.Parse("2021-12-08 17:33:00.000"),
                },
                new Input()
                {
                    TrayNumber = 3,
                    RecipeName = "Basil",
                    StartDate = DateTime.Parse("2030-01-01 23:45:00.000"),
                },
            };
            var basilRecipe = new Recipe("Basil");
            basilRecipe.LightingPhases.Add(new LightingPhase("LightingPhase 1", 0, 24, 0, 5)
            {
                Operations = new List<LightingPhaseOperation>()
                {
                    new LightingPhaseOperation(0, 0, LightIntensity.Low),
                    new LightingPhaseOperation(6, 0, LightIntensity.Medium),
                    new LightingPhaseOperation(12, 0, LightIntensity.High),
                    new LightingPhaseOperation(16, 0, LightIntensity.Off),
                },
            });
            basilRecipe.WateringPhases.Add(new WateringPhase("Watering Phase 1", 0, 24, 0, 5, 100));
            var strawberriesRecipe = new Recipe("Strawberries");
            strawberriesRecipe.LightingPhases.Add(new LightingPhase("Phase 3", 0, 24, 0, 5)
            {
                Operations = new List<LightingPhaseOperation>
                {
                    new LightingPhaseOperation(0, 0, LightIntensity.High),
                    new LightingPhaseOperation(20, 0, LightIntensity.Off),
                }
            });
            strawberriesRecipe.LightingPhases.Add(new LightingPhase("Phase 2", 1, 36, 30, 10)
            {
                Operations = new List<LightingPhaseOperation>
                {
                    new LightingPhaseOperation(0, 0, LightIntensity.Low),
                    new LightingPhaseOperation(6, 0, LightIntensity.Medium),
                    new LightingPhaseOperation(12, 0, LightIntensity.High),
                    new LightingPhaseOperation(16, 30, LightIntensity.Medium),
                    new LightingPhaseOperation(24, 30, LightIntensity.Low),
                    new LightingPhaseOperation(30, 0, LightIntensity.Off),
                }
            });
            strawberriesRecipe.LightingPhases.Add(new LightingPhase("Phase 3", 2, 24, 0, 2)
            {
                Operations = new List<LightingPhaseOperation>
                {
                    new LightingPhaseOperation(0, 0, LightIntensity.Low),
                    new LightingPhaseOperation(6, 0, LightIntensity.Medium),
                    new LightingPhaseOperation(12, 0, LightIntensity.Off),
                }
            });
            strawberriesRecipe.WateringPhases.Add(new WateringPhase("Phase 1", 0, 24, 0, 5, 0));
            strawberriesRecipe.WateringPhases.Add(new WateringPhase("Phase 2", 1, 24, 0, 6, 55));
            strawberriesRecipe.WateringPhases.Add(new WateringPhase("Phase 3", 3, 24, 0, 2, 30));
            strawberriesRecipe.WateringPhases.Add(new WateringPhase("Phase 4", 2, 12, 30, 4, 30));
            var recipes = new List<Recipe>()
            {
                basilRecipe,
                strawberriesRecipe,
            };
            var expectedResult = new List<Output>()
            {
                new LightingOutput(2, DateTime.Parse("2021-12-08T17:33:00.000"), LightIntensity.High),
                new LightingOutput(2, DateTime.Parse("2021-12-09T13:33:00.000"), LightIntensity.Off),
                new LightingOutput(2, DateTime.Parse("2021-12-09T17:33:00.000"), LightIntensity.High),
                new LightingOutput(2, DateTime.Parse("2021-12-10T13:33:00.000"), LightIntensity.Off),
                new LightingOutput(2, DateTime.Parse("2021-12-10T17:33:00.000"), LightIntensity.High),
                new LightingOutput(2, DateTime.Parse("2021-12-11T13:33:00.000"), LightIntensity.Off),
                new LightingOutput(2, DateTime.Parse("2021-12-11T17:33:00.000"), LightIntensity.High),
                new LightingOutput(2, DateTime.Parse("2021-12-12T13:33:00.000"), LightIntensity.Off),
                new LightingOutput(2, DateTime.Parse("2021-12-12T17:33:00.000"), LightIntensity.High),
                new LightingOutput(2, DateTime.Parse("2021-12-13T13:33:00.000"), LightIntensity.Off),
                new LightingOutput(2, DateTime.Parse("2021-12-13T17:33:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-13T23:33:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-14T05:33:00.000"), LightIntensity.High),
                new LightingOutput(2, DateTime.Parse("2021-12-14T10:03:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-14T18:03:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-14T23:33:00.000"), LightIntensity.Off),
                new LightingOutput(2, DateTime.Parse("2021-12-15T06:03:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-15T12:03:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-15T18:03:00.000"), LightIntensity.High),
                new LightingOutput(2, DateTime.Parse("2021-12-15T22:33:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-16T06:33:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-16T12:03:00.000"), LightIntensity.Off),
                new LightingOutput(2, DateTime.Parse("2021-12-16T18:33:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-17T00:33:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-17T06:33:00.000"), LightIntensity.High),
                new LightingOutput(2, DateTime.Parse("2021-12-17T11:03:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-17T19:03:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-18T00:33:00.000"), LightIntensity.Off),
                new LightingOutput(2, DateTime.Parse("2021-12-18T07:03:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-18T13:03:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-18T19:03:00.000"), LightIntensity.High),
                new LightingOutput(2, DateTime.Parse("2021-12-18T23:33:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-19T07:33:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-19T13:03:00.000"), LightIntensity.Off),
                new LightingOutput(2, DateTime.Parse("2021-12-19T19:33:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-20T01:33:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-20T07:33:00.000"), LightIntensity.High),
                new LightingOutput(2, DateTime.Parse("2021-12-20T12:03:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-20T20:03:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-21T01:33:00.000"), LightIntensity.Off),
                new LightingOutput(2, DateTime.Parse("2021-12-21T08:03:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-21T14:03:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-21T20:03:00.000"), LightIntensity.High),
                new LightingOutput(2, DateTime.Parse("2021-12-22T00:33:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-22T08:33:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-22T14:03:00.000"), LightIntensity.Off),
                new LightingOutput(2, DateTime.Parse("2021-12-22T20:33:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-23T02:33:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-23T08:33:00.000"), LightIntensity.High),
                new LightingOutput(2, DateTime.Parse("2021-12-23T13:03:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-23T21:03:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-24T02:33:00.000"), LightIntensity.Off),
                new LightingOutput(2, DateTime.Parse("2021-12-24T09:03:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-24T15:03:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-24T21:03:00.000"), LightIntensity.High),
                new LightingOutput(2, DateTime.Parse("2021-12-25T01:33:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-25T09:33:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-25T15:03:00.000"), LightIntensity.Off),
                new LightingOutput(2, DateTime.Parse("2021-12-25T21:33:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-26T03:33:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-26T09:33:00.000"), LightIntensity.High),
                new LightingOutput(2, DateTime.Parse("2021-12-26T14:03:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-26T22:03:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-27T03:33:00.000"), LightIntensity.Off),
                new LightingOutput(2, DateTime.Parse("2021-12-27T10:03:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-27T16:03:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-27T22:03:00.000"), LightIntensity.High),
                new LightingOutput(2, DateTime.Parse("2021-12-28T02:33:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-28T10:33:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-28T16:03:00.000"), LightIntensity.Off),
                new LightingOutput(2, DateTime.Parse("2021-12-28T22:33:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-29T04:33:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-29T10:33:00.000"), LightIntensity.Off),
                new LightingOutput(2, DateTime.Parse("2021-12-29T22:33:00.000"), LightIntensity.Low),
                new LightingOutput(2, DateTime.Parse("2021-12-30T04:33:00.000"), LightIntensity.Medium),
                new LightingOutput(2, DateTime.Parse("2021-12-30T10:33:00.000"), LightIntensity.Off),
                new WateringOutput(2, DateTime.Parse("2021-12-08T17:33:00.000"), 55),
                new WateringOutput(2, DateTime.Parse("2021-12-09T17:33:00.000"), 55),
                new WateringOutput(2, DateTime.Parse("2021-12-10T17:33:00.000"), 55),
                new WateringOutput(2, DateTime.Parse("2021-12-11T17:33:00.000"), 55),
                new WateringOutput(2, DateTime.Parse("2021-12-12T17:33:00.000"), 55),
                new WateringOutput(2, DateTime.Parse("2021-12-13T17:33:00.000"), 55),
                new WateringOutput(2, DateTime.Parse("2021-12-14T17:33:00.000"), 30),
                new WateringOutput(2, DateTime.Parse("2021-12-15T06:03:00.000"), 30),
                new WateringOutput(2, DateTime.Parse("2021-12-15T18:33:00.000"), 30),
                new WateringOutput(2, DateTime.Parse("2021-12-16T07:03:00.000"), 30),
                new WateringOutput(2, DateTime.Parse("2021-12-16T19:33:00.000"), 30),
                new WateringOutput(2, DateTime.Parse("2021-12-17T19:33:00.000"), 30),
                new LightingOutput(1, DateTime.Parse("2022-01-24T12:30:00.000"), LightIntensity.Low),
                new LightingOutput(1, DateTime.Parse("2022-01-24T18:30:00.000"), LightIntensity.Medium),
                new LightingOutput(1, DateTime.Parse("2022-01-25T00:30:00.000"), LightIntensity.High),
                new LightingOutput(1, DateTime.Parse("2022-01-25T04:30:00.000"), LightIntensity.Off),
                new LightingOutput(1, DateTime.Parse("2022-01-25T12:30:00.000"), LightIntensity.Low),
                new LightingOutput(1, DateTime.Parse("2022-01-25T18:30:00.000"), LightIntensity.Medium),
                new LightingOutput(1, DateTime.Parse("2022-01-26T00:30:00.000"), LightIntensity.High),
                new LightingOutput(1, DateTime.Parse("2022-01-26T04:30:00.000"), LightIntensity.Off),
                new LightingOutput(1, DateTime.Parse("2022-01-26T12:30:00.000"), LightIntensity.Low),
                new LightingOutput(1, DateTime.Parse("2022-01-26T18:30:00.000"), LightIntensity.Medium),
                new LightingOutput(1, DateTime.Parse("2022-01-27T00:30:00.000"), LightIntensity.High),
                new LightingOutput(1, DateTime.Parse("2022-01-27T04:30:00.000"), LightIntensity.Off),
                new LightingOutput(1, DateTime.Parse("2022-01-27T12:30:00.000"), LightIntensity.Low),
                new LightingOutput(1, DateTime.Parse("2022-01-27T18:30:00.000"), LightIntensity.Medium),
                new LightingOutput(1, DateTime.Parse("2022-01-28T00:30:00.000"), LightIntensity.High),
                new LightingOutput(1, DateTime.Parse("2022-01-28T04:30:00.000"), LightIntensity.Off),
                new LightingOutput(1, DateTime.Parse("2022-01-28T12:30:00.000"), LightIntensity.Low),
                new LightingOutput(1, DateTime.Parse("2022-01-28T18:30:00.000"), LightIntensity.Medium),
                new LightingOutput(1, DateTime.Parse("2022-01-29T00:30:00.000"), LightIntensity.High),
                new LightingOutput(1, DateTime.Parse("2022-01-29T04:30:00.000"), LightIntensity.Off),
                new WateringOutput(1, DateTime.Parse("2022-01-24T12:30:00.000"), 100),
                new WateringOutput(1, DateTime.Parse("2022-01-25T12:30:00.000"), 100),
                new WateringOutput(1, DateTime.Parse("2022-01-26T12:30:00.000"), 100),
                new WateringOutput(1, DateTime.Parse("2022-01-27T12:30:00.000"), 100),
                new WateringOutput(1, DateTime.Parse("2022-01-28T12:30:00.000"), 100),
                new LightingOutput(3, DateTime.Parse("2030-01-01T23:45:00.000"), LightIntensity.Low),
                new LightingOutput(3, DateTime.Parse("2030-01-02T05:45:00.000"), LightIntensity.Medium),
                new LightingOutput(3, DateTime.Parse("2030-01-02T11:45:00.000"), LightIntensity.High),
                new LightingOutput(3, DateTime.Parse("2030-01-02T15:45:00.000"), LightIntensity.Off),
                new LightingOutput(3, DateTime.Parse("2030-01-02T23:45:00.000"), LightIntensity.Low),
                new LightingOutput(3, DateTime.Parse("2030-01-03T05:45:00.000"), LightIntensity.Medium),
                new LightingOutput(3, DateTime.Parse("2030-01-03T11:45:00.000"), LightIntensity.High),
                new LightingOutput(3, DateTime.Parse("2030-01-03T15:45:00.000"), LightIntensity.Off),
                new LightingOutput(3, DateTime.Parse("2030-01-03T23:45:00.000"), LightIntensity.Low),
                new LightingOutput(3, DateTime.Parse("2030-01-04T05:45:00.000"), LightIntensity.Medium),
                new LightingOutput(3, DateTime.Parse("2030-01-04T11:45:00.000"), LightIntensity.High),
                new LightingOutput(3, DateTime.Parse("2030-01-04T15:45:00.000"), LightIntensity.Off),
                new LightingOutput(3, DateTime.Parse("2030-01-04T23:45:00.000"), LightIntensity.Low),
                new LightingOutput(3, DateTime.Parse("2030-01-05T05:45:00.000"), LightIntensity.Medium),
                new LightingOutput(3, DateTime.Parse("2030-01-05T11:45:00.000"), LightIntensity.High),
                new LightingOutput(3, DateTime.Parse("2030-01-05T15:45:00.000"), LightIntensity.Off),
                new LightingOutput(3, DateTime.Parse("2030-01-05T23:45:00.000"), LightIntensity.Low),
                new LightingOutput(3, DateTime.Parse("2030-01-06T05:45:00.000"), LightIntensity.Medium),
                new LightingOutput(3, DateTime.Parse("2030-01-06T11:45:00.000"), LightIntensity.High),
                new LightingOutput(3, DateTime.Parse("2030-01-06T15:45:00.000"), LightIntensity.Off),
                new WateringOutput(3, DateTime.Parse("2030-01-01T23:45:00.000"), 100),
                new WateringOutput(3, DateTime.Parse("2030-01-02T23:45:00.000"), 100),
                new WateringOutput(3, DateTime.Parse("2030-01-03T23:45:00.000"), 100),
                new WateringOutput(3, DateTime.Parse("2030-01-04T23:45:00.000"), 100),
                new WateringOutput(3, DateTime.Parse("2030-01-05T23:45:00.000"), 100),
            };

            var actualResult = ScheduleGenerator.GenerateSchedule(input, recipes);
            var jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(actualResult, new Newtonsoft.Json.JsonSerializerSettings()
            {
                DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffffffZ",
            });

            Assert.That(actualResult, Is.Not.Null);
            Assert.That(actualResult, Is.TypeOf(typeof(List<Output>)));
            Assert.That(actualResult, Has.Count.EqualTo(expectedResult.Count));

            for (var i = 0; i < expectedResult.Count; i++)
            {
                Assert.That(actualResult.ElementAt(i).StartDate, Is.EqualTo(expectedResult.ElementAt(i).StartDate));
                Assert.That(actualResult.ElementAt(i).TrayNumber, Is.EqualTo(expectedResult.ElementAt(i).TrayNumber));

                if (actualResult.ElementAt(i).GetType() == typeof(WateringOutput))
                {
                    Assert.That(((WateringOutput)actualResult.ElementAt(i)).WaterAmount, Is.EqualTo(((WateringOutput)expectedResult.ElementAt(i)).WaterAmount));
                }
                else
                {
                    Assert.That(((LightingOutput)actualResult.ElementAt(i)).LightIntensity, Is.EqualTo(((LightingOutput)expectedResult.ElementAt(i)).LightIntensity));
                }
            }
        }
    }
}