using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestMakerWebApp.ViewModels;

namespace TestMakerWebApp.Controllers
{
    [Route("api/[controller]")]
    public class QuizController: Controller
    {

        [HttpGet("latest/{numberOfElements}")]
        public IActionResult Latest(int numberOfElements = 10)
        {
            return new JsonResult(
                CreateMockQuizViewModels(numberOfElements), 
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        
        /// <summary>
        /// GET: api/quiz/byTitle
        /// Retrieves the {num} Quizzes sorted by title (A to Z)
        /// </summary>
        /// <param name="numberOfElements">The number of quizzes to be retrieve</param>
        /// <returns>{numberOfElements} Quizzes sorted by Title</returns>
        [HttpGet("byTitle/{numberOfElements:int?}")]
        public IActionResult ByTitle(int numberOfElements = 10)
        {
            var sampleQuizzesViewModels = CreateMockQuizViewModels(numberOfElements); 
            return new JsonResult(
                sampleQuizzesViewModels.OrderBy(t => t.Title), 
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// GET: api/quiz/random
        /// Retrieves the {numberOfElements} random Quizzes
        /// </summary>
        /// <param name="numberOfElements"></param>
        /// <returns>{numberOfElements} random QUizzes</returns>
        [HttpGet("random/{numberOfElements:int?}")]
        public IActionResult Random(int numberOfElements = 10)
        {
            var sampleQuizzes = CreateMockQuizViewModels(numberOfElements);
            
            return new JsonResult(
                sampleQuizzes.OrderBy(t => Guid.NewGuid()),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        
        
        public QuizController()
        {
            
        }

        private List<QuizViewModel> CreateMockQuizViewModels(int numberOfQuizzes)
        {
            var sampleQuizzes = new List<QuizViewModel>();
            
            sampleQuizzes.Add(new QuizViewModel()
            {
                Id = 1,
                Title = "Which shingeki no kyojin character are you?",
                Description = "Anime-related per sonality test",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });


            for (int index = 2; index <= numberOfQuizzes; index++)
            {
                sampleQuizzes.Add(new QuizViewModel()
                {
                    Id = 1,
                    Title = String.Format("Sample Quiz {0} ", index),
                    Description = String.Format("This is a sample quiz of number {0}", index),
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }
            
            return sampleQuizzes;
        }
    }
}