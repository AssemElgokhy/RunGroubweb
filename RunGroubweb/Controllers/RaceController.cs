using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroubweb.Data;
using RunGroubweb.Interfaces;
using RunGroubweb.Models;
using RunGroubweb.Repository;
using RunGroubweb.Services;
using RunGroubweb.ViewModels;

namespace RunGroubweb.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IPhotoService _photoService;

        public RaceController(IRaceRepository raceRepository, IPhotoService photoService)
        {
            _raceRepository = raceRepository;
            _photoService = photoService;
        }
        public async Task <IActionResult> Index()
        {
            IEnumerable<Race> races = await _raceRepository.GetAll();
            return View(races);
        }

        public async Task <IActionResult> Detail(int id) 
        {
            Race race= await _raceRepository.GetByIdAsync(id);
            return View(race);
        
        }
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVm)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceVm.Image);
                var race = new Race
                {
                    Title = raceVm.Title,
                    Description = raceVm.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = raceVm.Address.Street,
                        City = raceVm.Address.City,
                        State = raceVm.Address.State,
                    }
                };

                _raceRepository.Add(race);

                return RedirectToAction("Index");


            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(raceVm);

        }
    }
}
