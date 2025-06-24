using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Student_Management.Entities;
using Student_Management.Models;

namespace Student_Management.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }


        public static Dictionary<string, List<string>> NigerianStatesWithLGAs = new Dictionary<string, List<string>>
{
    { "Abia", new List<string> { "Aba North", "Aba South", "Arochukwu", "Bende", "Ikwuano", "Isiala Ngwa North", "Isiala Ngwa South", "Isuikwuato", "Obi Ngwa", "Ohafia", "Osisioma", "Ugwunagbo", "Ukwa East", "Ukwa West", "Umuahia North", "Umuahia South", "Umu Nneochi" } },
    { "Adamawa", new List<string> { "Demsa", "Fufore", "Ganye", "Girei", "Gombi", "Guyuk", "Hong", "Jada", "Lamurde", "Madagali", "Maiha", "Mayo-Belwa", "Michika", "Mubi North", "Mubi South", "Numan", "Shelleng", "Song", "Toungo", "Yola North", "Yola South" } },
    { "Akwa Ibom", new List<string> { "Abak", "Eastern Obolo", "Eket", "Esit Eket", "Essien Udim", "Etim Ekpo", "Etinan", "Ibeno", "Ibesikpo Asutan", "Ibiono Ibom", "Ika", "Ikono", "Ikot Abasi", "Ikot Ekpene", "Ini", "Itu", "Mbo", "Mkpat Enin", "Nsit Atai", "Nsit Ibom", "Nsit Ubium", "Obot Akara", "Okobo", "Onna", "Oron", "Oruk Anam", "Udung Uko", "Ukanafun", "Uruan", "Urue-Offong/Oruko", "Uyo" } },
    { "Anambra", new List<string> { "Aguata", "Anambra East", "Anambra West", "Anaocha", "Awka North", "Awka South", "Ayamelum", "Dunukofia", "Ekwusigo", "Idemili North", "Idemili South", "Ihiala", "Njikoka", "Nnewi North", "Nnewi South", "Ogbaru", "Onitsha North", "Onitsha South", "Orumba North", "Orumba South", "Oyi" } },
    { "Bauchi", new List<string> { "Alkaleri", "Bauchi", "Bogoro", "Damban", "Darazo", "Dass", "Gamawa", "Ganjuwa", "Giade", "Itas/Gadau", "Jama'are", "Katagum", "Kirfi", "Misau", "Ningi", "Shira", "Tafawa Balewa", "Toro", "Warji", "Zaki" } },
    { "Bayelsa", new List<string> { "Brass", "Ekeremor", "Kolokuma/Opokuma", "Nembe", "Ogbia", "Sagbama", "Southern Ijaw", "Yenagoa" } },
    { "Benue", new List<string> { "Ado", "Agatu", "Apa", "Buruku", "Gboko", "Guma", "Gwer East", "Gwer West", "Katsina-Ala", "Konshisha", "Kwande", "Logo", "Makurdi", "Obi", "Ogbadibo", "Ohimini", "Oju", "Okpokwu", "Otukpo", "Tarka", "Ukum", "Ushongo", "Vandeikya" } },
    { "Borno", new List<string> { "Abadam", "Askira/Uba", "Bama", "Bayo", "Biu", "Chibok", "Damboa", "Dikwa", "Gubio", "Guzamala", "Gwoza", "Hawul", "Jere", "Kaga", "Kala/Balge", "Konduga", "Kukawa", "Kwaya Kusar", "Mafa", "Magumeri", "Maiduguri", "Marte", "Mobbar", "Monguno", "Ngala", "Nganzai", "Shani" } },
    { "Cross River", new List<string> { "Abi", "Akamkpa", "Akpabuyo", "Bakassi", "Bekwarra", "Biase", "Boki", "Calabar Municipal", "Calabar South", "Etung", "Ikom", "Obanliku", "Obubra", "Obudu", "Odukpani", "Ogoja", "Yakurr", "Yala" } },
    { "Delta", new List<string> { "Aniocha North", "Aniocha South", "Bomadi", "Burutu", "Ethiope East", "Ethiope West", "Ika North East", "Ika South", "Isoko North", "Isoko South", "Ndokwa East", "Ndokwa West", "Okpe", "Oshimili North", "Oshimili South", "Patani", "Sapele", "Udu", "Ughelli North", "Ughelli South", "Ukwuani", "Uvwie", "Warri North", "Warri South", "Warri South West" } },
    { "Ebonyi", new List<string> { "Abakaliki", "Afikpo North", "Afikpo South", "Ebonyi", "Ezza North", "Ezza South", "Ikwo", "Ishielu", "Ivo", "Izzi", "Ohaozara", "Ohaukwu", "Onicha" } },
    { "Edo", new List<string> { "Akoko-Edo", "Egor", "Esan Central", "Esan North-East", "Esan South-East", "Esan West", "Etsako Central", "Etsako East", "Etsako West", "Igueben", "Ikpoba-Okha", "Oredo", "Orhionmwon", "Ovia North-East", "Ovia South-West", "Owan East", "Owan West", "Uhunmwonde" } },
    { "Ekiti", new List<string> { "Ado Ekiti", "Efon", "Ekiti East", "Ekiti South-West", "Ekiti West", "Emure", "Gbonyin", "Ido Osi", "Ijero", "Ikere", "Ikole", "Ilejemeje", "Irepodun/Ifelodun", "Ise/Orun", "Moba", "Oye" } },
    { "Enugu", new List<string> { "Aninri", "Awgu", "Enugu East", "Enugu North", "Enugu South", "Ezeagu", "Igbo Etiti", "Igbo Eze North", "Igbo Eze South", "Isi Uzo", "Nkanu East", "Nkanu West", "Nsukka", "Oji River", "Udenu", "Udi", "Uzo Uwani" } },
    { "Gombe", new List<string> { "Akko", "Balanga", "Billiri", "Dukku", "Funakaye", "Gombe", "Kaltungo", "Kwami", "Nafada", "Shongom", "Yamaltu/Deba" } },
    { "Imo", new List<string> { "Aboh Mbaise", "Ahiazu Mbaise", "Ehime Mbano", "Ezinihitte", "Ideato North", "Ideato South", "Ihitte/Uboma", "Ikeduru", "Isiala Mbano", "Isu", "Mbaitoli", "Ngor Okpala", "Njaba", "Nkwerre", "Nwangele", "Obowo", "Oguta", "Ohaji/Egbema", "Okigwe", "Onuimo", "Orlu", "Orsu", "Oru East", "Oru West", "Owerri Municipal", "Owerri North", "Owerri West" } },
    { "Jigawa", new List<string> { "Auyo", "Babura", "Biriniwa", "Birnin Kudu", "Buji", "Dutse", "Gagarawa", "Garki", "Gumel", "Guri", "Gwaram", "Gwiwa", "Hadejia", "Jahun", "Kafin Hausa", "Kaugama", "Kazaure", "Kiri Kasama", "Kiyawa", "Maigatari", "Malam Madori", "Miga", "Ringim", "Roni", "Sule Tankarkar", "Taura", "Yankwashi" } },
    { "Kaduna", new List<string> { "Birnin Gwari", "Chikun", "Giwa", "Igabi", "Ikara", "Jaba", "Jema'a", "Kachia", "Kaduna North", "Kaduna South", "Kagarko", "Kajuru", "Kaura", "Kauru", "Kubau", "Kudan", "Lere", "Makarfi", "Sabon Gari", "Sanga", "Soba", "Zangon Kataf", "Zaria" } },
    { "Kano", new List<string> { "Ajingi", "Albasu", "Bagwai", "Bebeji", "Bichi", "Bunkure", "Dala", "Dambatta", "Dawakin Kudu", "Dawakin Tofa", "Doguwa", "Fagge", "Gabasawa", "Garko", "Garun Mallam", "Gaya", "Gezawa", "Gwale", "Gwarzo", "Kabo", "Kano Municipal", "Karaye", "Kibiya", "Kiru", "Kumbotso", "Kunchi", "Kura", "Madobi", "Makoda", "Minjibir", "Nasarawa", "Rano", "Rimin Gado", "Rogo", "Shanono", "Sumaila", "Takai", "Tarauni", "Tofa", "Tsanyawa", "Tudun Wada", "Ungogo", "Warawa", "Wudil" } },
    { "FCT - Abuja", new List<string> { "Abaji", "Bwari", "Gwagwalada", "Kuje", "Kwali", "Municipal Area Council" } }
};


        public IActionResult Index()
        {
            return View();

        }


        [HttpGet]
        public JsonResult GetLgasByState(string state)
        {
            if (!string.IsNullOrEmpty(state) && NigerianStatesWithLGAs.ContainsKey(state))
            {
                var lgas = NigerianStatesWithLGAs[state];
                return Json(lgas);
            }

            return Json(new List<string>());
        }



        [HttpGet]
        public IActionResult Registration()
        {
            var model = new RegistrationViewModel();

            // Populate states
            model.StateList = StudentController.NigerianStatesWithLGAs.Keys
                .Select(x => new SelectListItem { Value = x, Text = x })
                .ToList();

            // If you want to preload LGA list for a specific state (e.g., in edit mode), do:
            if (!string.IsNullOrEmpty(model.State) &&
                StudentController.NigerianStatesWithLGAs.ContainsKey(model.State))
            {
                model.LGAList = StudentController.NigerianStatesWithLGAs[model.State]
                    .Select(lga => new SelectListItem { Value = lga, Text = lga })
                    .ToList();
            }
            else
            {
                model.LGAList = new List<SelectListItem>();
            }

            return View(model);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(RegistrationViewModel model)
        {

            model.StateList = StudentController.NigerianStatesWithLGAs.Keys
                    .Select(state => new SelectListItem { Value = state, Text = state })
                    .ToList();

             ModelState.Remove(nameof(model.StateList));

            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            var newStudent = new UserAccount
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                State = model.State,
                LGA = model.LGA,
                ApplicationNumber = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                AdmissionStatus = ApplicationStatus.Pending,
                DateOfApplication = DateTime.Now
            };

            _context.UserAccounts.Add(newStudent);
            _context.SaveChanges();

            ModelState.Clear();

            ViewBag.ApplicationNumber = newStudent.ApplicationNumber;
            return View("RegistrationSuccess");


        }


        [HttpGet]
        public ActionResult CheckAdmission()
        {
            return View(new LoginViewModel());
        }


        [HttpGet]
        public IActionResult CheckStatus()
        {
            return View("CheckAdmission", new LoginViewModel());
        }

        //POST: Student/CheckAdmissio
       [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckAdmission(LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.ApplicationNumber))
            {
                ViewBag.Message = "Please enter your application number.";
                return View("CheckAdmission", model);
            }

            var student = _context.UserAccounts.FirstOrDefault(x => x.ApplicationNumber == model.ApplicationNumber);

            if (student == null)
            {
                ViewBag.Message = "Invalid Application Number. Please try again.";
                return View("CheckAdmission", model);
            }

            return View("AdmissionStatus", student);
        }


        public IActionResult SecurePage()
        {
            return View();
        }
    }
}
