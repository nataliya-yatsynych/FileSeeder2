using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;
using System.Globalization;


namespace eCrtSeederNS
{
    class Program
    {
        public static string EnvironmentIndicator { get; set; }
        public static string MSFAAFlag { get; set; }
        public static int NumberOfeCertRecords { get; set; }

        public static string Originator { get; set; }
        public static int SequenceNumbereCertInFileName { get; set; }
        public static int SequenceNumbereCertInHeader { get; set; }
        public static int MSFAASequenceNumberInHeader { get; set; }
        public static int MSFAASequenceNumberInFileName { get; set; }
        public static string AgreementNumber { get; set; }
        public static string RecordMSFAA { get; set; }
        public static string MSFAATrailer { get; set; }

        public static long SINHashTotal { get; set; }
        public static string MSFAAfileName { get; set; }
        //ecert
        public static int TotalDisbursement { get; set; }
        public static int TotalDisbursementNS { get; set; }
        public static int TotalDisbursementPE { get; set; }
        public static int TotalDisbursementYT { get; set; }
        public static int TotalDisbursementNL { get; set; }
        public static int TotalDisbursementNB { get; set; }
        public static int TotalDisbursementMB { get; set; }
        public static int TotalOfCanceledDisbursement { get; set; }
        public static int TotalOfCanceledDisbursementNS { get; set; }
        public static int TotalOfCanceledDisbursementNL { get; set; }
        public static int TotalOfCanceledDisbursementYT { get; set; }
        public static int TotalOfCanceledDisbursementPE { get; set; }
        public static int TotalOfCanceledDisbursementNB { get; set; }
        public static int TotalOfCanceledDisbursementMB { get; set; }

        public static int AwardTotal { get; set; }
        public static int CSGPTotalNBCanceled { get; set; }
        public static int CSGPTotalNB { get; set; }
        public static int NBProvintialGrant { get; set; }
        public static int NBProvintialGrantCanceled { get; set; }
        public static int AB_ecert_totalCSLamount { get; set; }
        public static int NB_ecert_totalCSGPgrants { get; set; }
        public static int NB_ecert_totalCSGPgrants_canceled { get; set; }
        public static int AB_ecert_totalCSGPamount { get; set; }
        public static string eCertRecordNS { get; set; }

        public static string eCertRecordNL { get; set; }
        public static string eCertRecordPE { get; set; }
        public static string eCertRecordYT { get; set; }
        public static string eCertRecordNB { get; set; }
        public static string eCertRecordMB { get; set; }
        public static string eCertRecordAB_section2 { get; set; }
        public static string eCertRecordAB_section3 { get; set; }
        public static string eCertRecordAB_section5 { get; set; }
        public static string eCertRecordAB_section6 { get; set; }
        public static string AB_ecert_Section3_total { get; set; }
        public static string AB_ecert_Section5_total { get; set; }
        public static string AB_ecert_Section6_total { get; set; }
        public static int AB_ecert_Section6_counter { get; set; }
        public static string eCertRecordSK_csl_header { get; set; }
        public static string eCertRecordSK_csl_detail { get; set; }
        public static string eCertRecordSK_csl_trailer { get; set; }
        public static string eCertRecordSK_ssl_header { get; set; }
        public static string eCertRecordSK_ssl_detail { get; set; }
        public static string eCertRecordSK_ssl_trailer { get; set; }
        public static string eCertRecordSK_csl_header_cancel { get; set; }
        public static string eCertRecordSK_csl_detail_cancel { get; set; }
        public static string eCertRecordSK_csl_trailer_cancel { get; set; }
        public static string eCertRecordSK_ssl_header_cancel { get; set; }
        public static string eCertRecordSK_ssl_detail_cancel { get; set; }
        public static string eCertRecordSK_ssl_trailer_cancel { get; set; }
        public static string eCertRecordSK_ssl_detail_total { get; set; }
        public static string eCertFileName { get; set; }

        public static string ABProgramType { get; set; }
        static public void Main(string[] args)
        {

            Console.WriteLine("app can generate MSFAA and ecert files for NS/NL/AB/YT/PE provinces");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("****Files will be saved to the 'C:\\TestFiles' location. Please create this folder manually****");
            Console.WriteLine("****Also, please create folders within the TestFiles folder for each province called: eCert - NS, eCert - NL, etc.");
            Console.WriteLine("****Within each province's folder, create two folders called eCert Files and MSFAA Files");
            Console.WriteLine("****As an example, an eCert File for NL will be in C:\\TestFiles\\eCert - NL\\eCert Files" + Environment.NewLine);
            Console.WriteLine("****Enter string like: 1 NS S ");
            Console.WriteLine("****arg 1: Number Of Records ");
            Console.WriteLine("****arg 2: Province of Originator ");
            Console.WriteLine("****arg 3: S for SIT, D for DIT ");

            args = Console.ReadLine().Split(' ');

            NumberOfeCertRecords = Convert.ToInt32(args[0]);
            
            Originator = Convert.ToString(args[1]).ToUpper();
            EnvironmentIndicator = Convert.ToString(args[2]).ToUpper();

            Console.WriteLine("****Generate MSFAA? (Y/N) ");
            MSFAAFlag = Console.ReadLine().ToUpper();

            string[] MaritalStatus = new string[] { "M", "S", "O" };
            string[] Studenttype = new string[] { "1", "2", "3", "4" };
            string[] Gender = new string[] { "1", "2" };
            string[] GenderLetter = new string[] { "F", "M" };
            string[] DisabilityIndicator = new string[] { "Y", "N" };
            string[] ProvinceCode = new string[] { "ON", "AB", "BC", "NS", "QC", "NL", "YT", "SK", "MB", "NB", "NT", "NU", "PE" };
            string[] PostalCode = new string[] { "L4Z6S4", "l4z6m4" };
            string[] SemesterIndicator = new string[] { "S", "P" };
            string[] PTIndicator = new string[] { "F", "P" };
            string[] EnrollmentConfirmation = new string[] { /*"C",*/ "U" };
            string[] Status = new string[] { "N", "P", "P" };
            string[] MSFAaPTIndicator = new string[] { "FT", "PT" };
            string[] FirstNames = new string[] { "Gerhardt", "Stewart", "Lissi", "Kyle", "Juditha", "Brandice", "Juana", "Anneliese", "Thedrick", "Ruthy", "Joann", "Sampson", "Ivie", "Judy", "Noach", "Gaye", "Frankie", "Mariska", "Ondrea", "Merrill", "Stewart", "Helli", "Mariska", "Mariette", "Shadow", "Connor", "Penny", "Katerine", "Tandi", "Lorelei", "Pier", "Jackie", "Neda", "Derril", "Hale", "Cam", "Brockie", "Shelagh", "Jose", "Woodman", "Susann", "Evvy", "Marline", "Abigale", "Gottfried", "Jeremias", "Martie", "Adeline", "Mollee", "Filippa", "Ray", "Jami", "Dorothee", "Larry", "Wini", "Jasen", "Kearney", "Maudie", "Gradey", "Morgen", "Bronson", "Erda", "Vladimir", "Fayth", "Viviana", "Jemmy", "Randee", "Stanislaw", "Jodie", "Aurie", "Rosemaria", "Agnes", "Wilhelmina", "Deerdre", "Salomo", "Aubrie", "Sydney", "Jacintha", "Bondy", "Germaine", "Odilia", "Denny", "Asa", "Keri", "Anet", "Elysia", "Janeen", "Jessee", "Wayne", "Sherilyn", "Dolorita", "Alric", "Cindi", "Sybil", "Horatio", "Sybilla", "Olivier", "Jaymee", "Rene", "Brigg", "Harland", "Jilleen", "Amata", "Lizabeth", "Jourdain", "Peri", "Abigail", "Hastie", "Joanna", "Marjorie", "Laurel", "Tom", "Rosie", "Erich", "Kelley", "Bryant", "Perry", "Sisile", "Lolita", "Vick", "Cully", "Leda", "Kelila", "Pierre", "Idalina", "Cesare", "Stace", "Aldous", "Chanda", "Con", "Billy", "Christie", "Della", "Malinda", "Maje", "Phip", "Nobie", "Kati", "Timothea", "Addie", "Melina", "Steffane", "Georges", "Bondon", "Mavis", "Nolana", "Mortie", "Constantia", "Clio", "Rosie", "Josefina", "Davida", "Madlen", "Rorke", "Susette", "Beryl", "Bette", "Leta", "Rusty", "Skipton", "Lynea", "Bethanne", "Dmitri", "Chev", "Bobby", "Heddi", "Tina", "Aimee", "Worden", "Rebecca", "June", "Lesya", "Englebert", "Mareah", "Hale", "Scottie", "Frederigo", "Grazia", "Marty", "Bertha", "Jerrylee", "Marrilee", "Dan", "Olly", "Sabine", "Barty", "Michel", "Lea", "Paul", "Terrye", "Catrina", "Gale", "Lucian", "Yevette", "Wernher", "Salomo", "Janet", "Karlen", "Morty", "Othelia", "Penelope", "Saunders", "Ted", "Elena", "Natty", "Christian", "Mick", "Skell", "Vale", "Alphard", "Abbye", "Ronni", "Melanie", "Edmund", "Tuckie", "Hertha", "Hermann", "Gamaliel", "Alvin", "Marla", "Phyllida", "Ingra", "Alanah", "Natty", "Margret", "Cassi", "Caty", "Claudetta", "Auguste", "Meris", "Ranee", "Janela", "Randa", "Ced", "Annabel", "Sandor", "Rosalia", "Nanny", "Bondon", "Bennett", "Audrie", "Saidee", "Pansy", "Ignatius", "Dolorita", "Hillie", "Philis", "Catha", "Bronnie", "Mommy", "Neddie", "Bobbie", "Stace", "Shepperd", "Jennie", "Nikolaus", "Henrik", "Mead", "Kelli", "Elsbeth", "Halie", "Vittorio", "Danny", "Jesselyn", "Milton", "Lanie", "Estell", "Montgomery", "Sabra", "Sidnee", "Danice", "Arnoldo", "Dido", "Arlina", "Marleah", "Vitia", "Robin", "Heddie", "Freeman", "Kim", "Carlynne", "Dolly", "Lilith", "Tildie", "Vite", "Lyn", "Jordain", "Perren", "Remington", "Niki", "Lyda", "Dermot", "Phedra", "Sheilakathryn", "Rickert", "Helli", "Stella", "Johnathon", "Danielle", "Lynnea", "Stefano", "Millisent", "Ari", "Isis", "Charyl", "Tiff", "Dominique", "Porty", "Padraic", "Roseann", "Haydon", "Gaven", "Raddie", "Herrick", "Tobe", "Jarrod", "Paloma", "Carie", "Brunhilde", "Andeee", "Caryn", "Wallie", "Yancy", "Brittan", "Vi", "Farrel", "Gothart", "Sophronia", "Jeth", "Pepe", "Junia", "Bonni", "Wynn", "Giraud", "Dugald", "Rene", "Sascha", "Nicoli", "Arvin", "Rianon", "Abbie", "Karina", "Daile", "Marillin", "Elsie", "Rhodie", "Tyne", "Felicia", "Huntington", "Alyssa", "Caitrin", "Vinita", "Read", "Minnaminnie", "Elsie", "Wilmer", "Seymour", "Irene", "Saree", "Hyacinthie", "Darb", "Kathie", "Brunhilde", "Clemente", "Henderson", "Temple", "Eleanore", "Andreana", "Alfie", "Sid", "Morley", "Margalo", "Willey", "Jill", "Pauly", "Viv", "Forester", "Roberta", "Dollie", "Hyacinthia", "Joellen", "Quillan", "Man", "Gawain", "Ginevra", "Rudiger", "Terence", "Del", "Freda", "Rickie", "Harry", "Brennen", "Arleyne", "Cozmo", "Deb", "Revkah", "Colet", "Quill", "Melony", "Oralia", "Urbanus", "Laughton", "Ruttger", "Ber", "Worthy", "Dion", "Clotilda", "Car", "Elizabeth", "Nathalia", "Teddy", "Humfried", "Haleigh", "Randi", "Thedric", "Lee", "Elianora", "Charita", "Harvey", "Johanna", "Lauritz", "Joyan", "Desi", "Nola", "Cos", "Tadd", "Pepito", "Vaughan", "Riva", "Prudi", "Roanne", "Gram", "Cassey", "Andria", "Caresse", "Terrence", "Alric", "Walliw", "Harmonia", "Augustine", "Engracia", "Lorin", "Zebulen", "Irwinn", "Aubree", "Doug", "Giuditta", "Julie", "Stoddard", "Matti", "Corly", "Grantham", "Even", "Rhea", "Sybil", "Bink", "Penny", "Malvina", "Estele", "Sallyann", "Andre", "Estevan", "Hamlin", "Claudian", "Malia", "Chrysler", "Forest", "Cassandra", "Lesley", "Cordelie", "Otis", "Mill", "Eal", "Rayner", "Andra", "Jany", "Rudiger", "Meggy", "Douglas", "Matthieu", "Trescha", "Tildi", "Leeland", "Drusy", "Emlen", "Etta", "Philipa", "Christalle", "Rockwell", "Adelaida", "Faunie", "Enriqueta", "Steve", "Maurice", "Anthe", "Griffy", "Caria", "Nolan", "Darrelle", "Joell", "Jerrylee", "Travis", "Paxton", "Lucian", "Lyssa", "Adelind", "Alane", "Jareb", "Frederica", "Sybil", "Gisele", "Merrill", "Wilburt", "Dilan", "Jephthah", "Daisi", "Catlee", "Olivette", "Robin", "Briano", "Elfreda", "Friedrich", "Kaycee", "Corny", "Alisha", "Val", "Neda", "Salli", "Dimitri", "Tremayne", "Mylo", "Gallard", "Marlon", "Don", "Edy", "Rowan", "Georas", "Rosita", "Quinta", "Tab", "Leila", "Faber", "Corby", "Arlena", "Kym", "Hubert", "Caroline", "Carmel", "Truman", "Osborne", "Natala", "Evonne", "Randee", "Willamina", "Marie", "Lynnett", "Nikolaus", "Donn", "Hubert", "Maddie", "Neely", "Darsey", "Arnuad", "Cheri", "Davina", "Inger", "Kaile", "Veradis", "Horten", "Barbara-anne", "Davidde", "Lon", "Kacy", "Seline", "Isa", "Carolynn", "Jaye", "Decca", "Aimee", "Boothe", "Humphrey", "Jarret", "Benedicto", "Debra", "Elsi", "Welby", "Eugenia", "Harley", "Imogen", "Verna", "Bobbie", "Herrick", "Devin", "Linzy", "Perla", "Tristam", "Agace", "Tiebold", "Adrianne", "Tessi", "Jade", "Gerrard", "Cyndie", "Ede", "Alika", "Tanny", "Cirstoforo", "Ephrayim", "Hyacinthie", "Wolfgang", "Fiann", "Rose", "Fax", "Marianne", "Ania", "Lucy", "Celestina", "Ellswerth", "Zebedee", "Arlen", "Briant", "Robert", "Erwin", "Bordie", "Ibbie", "Ezekiel", "Baillie", "Eldridge", "Darla", "Johnathan", "Pieter", "Renee", "Patty", "Anthiathia", "Vernen", "Christen", "Norry", "Penny", "Nevsa", "Gino", "Spike", "Ferd", "Donella", "Jaymie", "Reggie", "Dyane", "Jessie", "Cornall", "Gabriele", "Halsy", "Delila", "Nicolis", "Marianne", "Sonja", "Viva", "Holly-anne", "Norman", "Brenn", "Rosene", "Jennie", "Fitzgerald", "Willi", "Cece", "Marylou", "Indira", "Joyce", "Krissy", "Fannie", "Bridie", "Eben", "Hershel", "Nicol", "Evaleen", "Carlina", "Tanitansy", "Zechariah", "Arlyn", "Dalila", "Porty", "Nerty", "Jessie", "Anneliese", "Dion", "Bertram", "Jimmie", "Krisha", "Marya", "Elyse", "Mart", "Angie", "Addy", "Ara", "Dorthy", "Emlynn", "Lindsy", "Adaline", "Winona", "Andonis", "Melissa", "Weidar", "Clemmy", "Gusella", "Bobinette", "Josy", "Nita", "Myrlene", "Morgan", "Corbet", "Crissie", "Edgardo", "Kennan", "Alden", "Colby", "Poul", "Catharine", "Lazarus", "Ulises", "Devy", "Daile", "Cacilie", "Eb", "Jenn", "Emlen", "Agnes", "Cacilie", "Belia", "Renae", "Leroy", "Claretta", "Allyn", "Ambrose", "Bucky", "Jackqueline", "Hillery", "Danny", "Fred", "Skipp", "Cully", "Hervey", "Brok", "Doreen", "Jermain", "Odetta", "Page", "Aubrette", "Hildy", "Melina", "Margaretha", "Alleen", "Rosabella", "Becka", "Leslie", "Kathlin", "Elsey", "Tammara", "Raffarty", "Catlee", "Aggi", "Elbertine", "Erv", "Court", "Hamish", "Kirstin", "Katlin", "Patrick", "Terese", "Elden", "Miran", "Byrle", "Maurita", "Paddie", "Tommie", "Madelle", "Rosmunda", "Cleveland", "Klemens", "Melisa", "Orlan", "Moore", "Aldrich", "Hilario", "Myrlene", "Leandra", "Perrine", "Ursuline", "Tasha", "Donovan", "Felipe", "Donielle", "Jobi", "Aurelia", "Haley", "Ivette", "Chic", "Cameron", "Emilio", "Lazare", "Alexandros", "Elwin", "Andre", "Quincey", "Christine", "Lorant", "Rafa", "Emmanuel", "Jelene", "Garrard", "Janeczka", "Chantalle", "Corry", "Brita", "Harp", "Jacklyn", "Bell", "Cullen", "Rosanne", "Hasheem", "Janna", "Annabel", "Kerstin", "Jacqueline", "Clio", "Lorne", "Meryl", "Georgina", "Haily", "Rosabella", "Cathryn", "Wainwright", "Isidor", "Klaus", "Pauly", "Gabi", "Toiboid", "Bellina", "Raul", "Meredith", "Josiah", "Mariquilla", "Charlie", "Reuven", "Miguelita", "Franky", "Shayne", "Nathalie", "Loraine", "Elwin", "Dolli", "Kaylil", "Osbert", "Sarita", "Victoir", "Bonni", "Jamil", "Lizbeth", "Alley", "Britteny", "Jasmina", "Bruis", "Amandie", "Rouvin", "Tammie", "Wilburt", "Domenic", "Garrot", "Orson", "Myriam", "Hervey", "Collie", "Leslie", "Berky", "Dacia", "Chantalle", "Jacky", "Leland", "Gusta", "Paulo", "Sammy", "Ward", "Cori", "Avis", "Benjamin", "Christian", "Aron", "Gae", "Veronica", "Maris", "Morey", "Margi", "Gail", "Dedie", "Tommie", "Adlai", "Cristabel", "Augustin", "Magdaia", "Gwendolen", "Arlyn", "Tiffie", "Mia", "Carina", "Flo", "Fee", "Saudra", "Hale", "Oberon", "Lyndsie", "Aurelea", "Danya", "Georgette", "Rutherford", "Cayla", "Richardo", "Rudd", "Sharai", "Maddie", "Lonee", "Carlin", "Lotte", "Dana", "Tresa", "Viv", "Kim", "Tull", "Camel", "Correy", "Tobi", "Lilas", "Alvis", "Kati", "Mona", "Darcee", "Maddie", "Bartel", "Ken", "Nealson", "Trista", "Alejandro", "Monroe", "Kahlil", "Lanni", "Elayne", "Zebulen", "Claribel", "Jessy", "Tova", "Rosalyn", "Rubina", "Christian", "Pepita", "Regan", "Kristen", "Quinn", "Emmie", "Ulrick", "Desmund", "Kirby", "Gabi", "Petronille", "Jaymie", "Garrek", "Faina", "Berna", "Isak", "Jenna", "Adrian", "Dur", "Jennilee", "Truda", "Ade", "Hilliard", "Lorri", "Johanna", "Terrel", "Lorette", "Link", "Winna", "Kermy", "Philippe", "Pru", "Johan", "Mireielle", "Roana", "Cristina", "Pauline", "Riki", "Adelina", "Hastie", "Moritz", "Allyce", "Kalil", "Elissa", "Allard", "Verina", "Delores", "Carmella", "Adriane", "Vasili", "Ambrosi", "Shurlocke" };
            string[] LastNames = new string[] { "Munkley", "Yorke", "Legonidec", "Brandenberg", "Owers", "Hawkswood", "Bittany", "Garrold", "Yesinov", "Eicheler", "Sambles", "Vasishchev", "Yarn", "Keenleyside", "Sawden", "Zanassi", "Jaffray", "Louisot", "Sherer", "Crates", "Mattiello", "Dyshart", "Lovejoy", "Disbrow", "Attwill", "Livick", "Fidgeon", "Giggs", "Kemell", "Eberst", "Passe", "Blacker", "Iozefovich", "Pamplin", "Beake", "Gerdes", "Blankhorn", "Simonds", "Kinkaid", "McKee", "Kellar", "Facher", "MacClancey", "Kadd", "Ties", "Gaiger", "Arnhold", "Lampen", "Throughton", "Ettles", "Savaage", "Connett", "Extill", "McCluin", "Greenham", "Flooks", "Huckerby", "Squire", "Arnatt", "Heis", "Schubuser", "Laybourne", "Moncaster", "Jacomb", "Knutton", "Rendbaek", "Faudrie", "Frogley", "McMickan", "Daspar", "Cauley", "Ligerton", "Cluney", "Viger", "McGairl", "Childes", "Adrien", "Beldan", "Dowe", "Hilary", "Wadesworth", "Windus", "Crighten", "Ebourne", "Carlozzi", "Duggon", "Hickenbottom", "Wroth", "Priddey", "Vecard", "Cottesford", "Quarrie", "Moverley", "Reasun", "Lorrimer", "Paulitschke", "Tesoe", "Van der Velde", "Simpole", "Moreman", "McCaughran", "Bridel", "Nulty", "Winspear", "Jockle", "Wycliff", "Tue", "Heintz", "Ounsworth", "Strewther", "Medlen", "Minard", "Goshawk", "Pinkett", "Wraxall", "Reskelly", "McAllaster", "Pellissier", "Longmate", "Vick", "Meggison", "Tippings", "French", "Calver", "Bravey", "Fidell", "Tudhope", "Cavey", "Mix", "Havill", "Udale", "Loseke", "Couchman", "Groven", "Raise", "Mourant", "Wallentin", "Boyack", "Rennebach", "Dene", "Panas", "Layman", "Rignall", "Callery", "Bing", "Vynehall", "Dartan", "Bellin", "Pigden", "Baudasso", "Calterone", "Irving", "Worlock", "Tremberth", "Joutapaitis", "Hambatch", "Swaton", "Ogborn", "Wildber", "Dempster", "Rantoul", "Switsur", "Orrick", "Glassopp", "Madsen", "Riddell", "Ruffey", "Bulcroft", "Hearnaman", "Skate", "MacLaughlin", "Bass", "Petera", "Botler", "Strafen", "Elmore", "Rottgers", "Booi", "Heinish", "Shimman", "Vasilchikov", "Kettlestringes", "Dartnall", "Hunnam", "Gosnay", "Flippelli", "Garlett", "Grivori", "Clague", "Saintpierre", "D'Adda", "Juza", "McPhee", "Boffey", "Quarrie", "Mickleborough", "Cobello", "Baybutt", "Beardsall", "Giddens", "Radin", "Meran", "O'Lynn", "Gall", "Bronger", "Aartsen", "Brownsall", "Boscott", "Izkovici", "Hedden", "Martynikhin", "Sponer", "Hammer", "Beartup", "Jeune", "Skillett", "Di Franceschi", "Noden", "Fausset", "Gabotti", "Strephan", "Caird", "Trenouth", "Endersby", "Edge", "Rameau", "Milesap", "Whitmore", "Auletta", "Proudlove", "Stampfer", "Kilroy", "Laxe", "Kenforth", "Duckham", "Ivakhnov", "Djorevic", "Dodding", "Viger", "Pead", "Cockburn", "Briant", "Kingswood", "Turbitt", "Jeeves", "Redferne", "Halliwell", "Peartree", "Baudichon", "Bertolaccini", "Raithby", "Wrassell", "Conachie", "Lavallie", "Kift", "Heimes", "Shelf", "Blemen", "Filippov", "Torre", "Megarrell", "Edmondson", "Carberry", "Eadmeads", "Feehily", "Maurice", "Fealty", "Worsfield", "Golden", "Steagall", "Greedier", "Stonebanks", "Poppleton", "Gallear", "Bawme", "Fratczak", "Magnus", "Hayworth", "Canete", "Beeden", "Gorgler", "Gaber", "Kytter", "Sawforde", "Lavell", "Berridge", "O'Shesnan", "Batting", "Djurkovic", "Hudleston", "Lamberteschi", "Deer", "Vicar", "Searjeant", "Tinsley", "Swansbury", "Addis", "Batram", "Byfford", "Baumer", "Barcroft", "Shead", "Crampton", "Figurski", "Sainsberry", "Whale", "Hirjak", "Cockren", "Lamasna", "Heggison", "Hampstead", "Bedding", "Ebbetts", "Kenchington", "Ingles", "Penhall", "Schwandermann", "Drayn", "Wearn", "Riddle", "Mcmanaman", "Stannard", "Maddock", "Poynser", "Witherspoon", "Atheis", "Mendenhall", "Aronoff", "Cowton", "D'Oyley", "Hensmans", "von Grollmann", "Lawlance", "Millthorpe", "Teliga", "Seven", "Cretney", "Huyhton", "Rubinfajn", "Halston", "Ridolfi", "Attiwill", "Blunsum", "Jedrasik", "Galea", "Davidovici", "Ohanessian", "Madgin", "Gisborne", "Hannah", "Fain", "Buss", "Inkpin", "Humpatch", "Latehouse", "Scholig", "Troyes", "Sharrem", "Puckham", "Gerrietz", "Wynrehame", "Leahey", "Mouton", "Haythornthwaite", "Peskett", "de Keyser", "Kemsley", "Baldock", "Habbijam", "Brandassi", "Dimmne", "Chatten", "Brunger", "Issacov", "More", "Pigne", "Bernette", "Ortler", "Akester", "Manolov", "Swan", "Pohls", "Southeran", "Rubee", "Griston", "Dacca", "Furneaux", "Riccio", "Aronovitz", "Oddie", "Sheepy", "Angus", "Dayly", "McGillreich", "Heitz", "Huggins", "Ghiotto", "Shorten", "Dwelly", "Joney", "Ailmer", "Mager", "Stapleford", "Sackur", "Fernant", "Bambrugh", "Goodright", "Stilliard", "Yele", "Lafford", "Gorries", "Gauche", "Cypler", "Calcut", "Grigson", "Garraway", "Snibson", "Hurton", "Speechly", "Stearnes", "McSharry", "McInally", "Spinige", "Wackley", "Bande", "Serrurier", "Janczewski", "Stigell", "Cardenoza", "Sollitt", "Pancast", "Karby", "Skowcraft", "Rizzetti", "Pietron", "Poyzer", "Duxbury", "Parkhouse", "Brandone", "Phippard", "Grigorini", "Hugonnet", "Cowwell", "Findon", "Quixley", "Shafto", "Shildrick", "Lyard", "Duignan", "Nutting", "Preene", "Kenchington", "Lademann", "Gilligan", "Hugo", "Eschalotte", "Gorvin", "Nevison", "Stanbury", "Sawden", "Di Frisco", "Wildman", "Bending", "Middler", "Powter", "Conville", "Orcas", "Kevern", "Tebbs", "Sawley", "Borg", "Firmager", "Frowd", "Ravillas", "Blurton", "D'Ugo", "MacCawley", "Wesker", "Dawley", "Kingdom", "Suermeier", "Chalice", "Petruskevich", "Randalston", "Gwyneth", "Hartgill", "Chagg", "Gundrey", "Bird", "Melton", "Montacute", "Deeney", "Collingham", "Torrese", "Hinkes", "Meaton", "Gumme", "Robbel", "Revey", "Nattriss", "Flawith", "Pechell", "Ridgeway", "Rosario", "Renwick", "Gabbett", "De Biaggi", "Solley", "Hakonsson", "Petters", "Tindle", "Snar", "Grabb", "Iddiens", "Slater", "Burtenshaw", "Eames", "Treat", "Lemarie", "Ruppelin", "Juden", "Dohr", "Whittet", "Sketcher", "Walklate", "Francklyn", "Micheu", "Catanheira", "MacConnechie", "Forryan", "Proudlove", "Faulkener", "Boakes", "Ludvigsen", "Laidler", "Ferens", "Kunkel", "Hollingshead", "Brightie", "Wotton", "Iggo", "Rathe", "Bratton", "Stolberger", "Paskell", "Bickerdike", "Bellay", "Balogh", "Creus", "Float", "Ollie", "Messier", "Penright", "Veltman", "Dowears", "Cusiter", "Eakly", "Seary", "Southwood", "Lowre", "Ewen", "Aldersea", "Dagless", "Dutnall", "Howson", "Godfree", "Fairbourn", "Gostage", "Frank", "Sarfas", "Thackham", "Geal", "Dodwell", "Hallbord", "Pardon", "Clayill", "Yousef", "Carty", "Hurn", "Graham", "Malone", "Trusty", "Masson", "Cudiff", "Bushen", "Hawsby", "Mapstone", "Cash", "Romayn", "Tomes", "Pickvance", "Finicj", "Bambridge", "Harrow", "Ives", "Truter", "Clohessy", "Butt", "Cankett", "Tomanek", "Bifield", "Merry", "Ashfull", "Todman", "Grindall", "Donisthorpe", "Berendsen", "Wincott", "Foresight", "Rennison", "Lyttle", "Crown", "Taw", "Hurne", "Gazey", "Havercroft", "Barends", "Dallimore", "Yakhin", "MacAllister", "Carlozzi", "Elliot", "Grimsell", "Newvell", "Rycraft", "Bronot", "Bister", "Ollerenshaw", "Steiner", "Robbel", "Demer", "Gaffney", "Bazoche", "Alu", "Flaws", "Friberg", "Bellelli", "Carslake", "Dillingston", "Cominetti", "Flook", "Olwen", "Ivanin", "Brushneen", "Ommundsen", "Pooly", "Bolduc", "Fassman", "Poulsum", "Bartosek", "De Freyne", "Sandells", "Ranger", "Sheekey", "Larway", "Cloutt", "Najara", "Beadon", "Crigin", "Kapiloff", "Fellnee", "Pitchford", "Goodwins", "Haydock", "Conibeer", "Waywell", "Coaten", "Purse", "Maplethorpe", "Gianneschi", "Petroulis", "Killock", "Damarell", "Plevin", "Galton", "Dugget", "McClymond", "Woodworth", "Lynes", "Palatini", "Turban", "Huntall", "Gendricke", "Satterly", "Domoney", "Samwyse", "Gatehouse", "Vasilyevski", "Terzo", "Stratley", "Flory", "Greenland", "Runnalls", "Snelling", "Gelardi", "Peres", "Crossan", "Florio", "Balnaves", "Ziehms", "Nasey", "Strathearn", "Jarret", "Reagan", "Huckstepp", "Luce", "Paddy", "Durrett", "Ellesworth", "Mattingson", "Bunson", "Cheese", "Seyfart", "Waywell", "Vasiljevic", "Gravatt", "Gosart", "Millberg", "Payn", "Axleby", "Mortimer", "Aimson", "Malim", "Catherall", "Lindfors", "Shorland", "Grishanov", "Karlolak", "Behnen", "Checo", "Wedge", "Farland", "Frayne", "Rodear", "Sleightholme", "Finci", "Barthot", "Annell", "Nowlan", "Pasticznyk", "Kerswell", "Deadman", "Yansons", "Achrameev", "Pinke", "Harmeston", "Cuming", "Brehaut", "Vearncomb", "Denes", "Pablo", "Berriman", "Gorden", "Vallender", "Dootson", "Habershon", "Ilchuk", "Beneze", "Menendez", "Kurth", "Feldharker", "Meharg", "Kenrick", "Lafay", "Temporal", "Fallow", "Allen", "Pickrell", "Norres", "Betjeman", "Ogilvy", "Elcom", "Hammant", "Lanaway", "Crowne", "Athowe", "Simmen", "Brownrigg", "Bente", "Decaze", "Guppey", "Upward", "Thame", "Lurriman", "Blundin", "Whinney", "Timmens", "Rozec", "Morena", "Rolles", "Shaul", "Durban", "Jojic", "Goodbourn", "Cornilleau", "Baddiley", "Jenson", "Sheardown", "Dowley", "Claessens", "Honeyghan", "York", "Hastewell", "Ryle", "Vicarey", "Rentalll", "Scoullar", "Rickardes", "Jikylls", "Bentzen", "Schouthede", "Laycock", "Erington", "Woodwin", "Byrom", "M'Barron", "MacGowing", "Beaby", "Neubigin", "Pringuer", "Pallesen", "Lethardy", "Raywood", "Veare", "Tchaikovsky", "Rosenstengel", "Deeney", "Salthouse", "Oldcroft", "Jefford", "Poupard", "Heckner", "Mundle", "Oki", "O'Siaghail", "Engall", "Luter", "Fealey", "Labrow", "Leece", "Gammage", "Rottenbury", "Hucks", "Birnie", "Rigby", "Drakeford", "Sumsion", "Wanjek", "Janman", "Rosenblath", "Knightsbridge", "Ballay", "Stovin", "Mousby", "Dawtry", "Tidbald", "Pauncefoot", "Janaszewski", "Paulazzi", "Lowy", "Boldry", "Skeldon", "Beartup", "Corthes", "Duran", "Beauly", "Benez", "Charle", "Merrgan", "Giffaut", "Faber", "Chaves", "Hurles", "Morritt", "Garnall", "Domelaw", "Glenister", "Gowling", "Bahls", "Cherm", "Cheevers", "Lodge", "Mordecai", "Garrood", "Quested", "Salmen", "Luetkemeyers", "Balm", "Iremonger", "Garvey", "Maccraw", "Reuther", "Shoulder", "Dering", "Abells", "Donner", "Jobern", "Screaton", "Elphinstone", "Kirkness", "Wallworke", "Cheltnam", "Shrieve", "Freake", "Boar", "Mingus", "Cianelli", "Olufsen", "Picheford", "Agate", "Gadie", "O'Nowlan", "Deeney", "Streeting", "Lyddon", "Paoloni", "Manwaring", "Rantoul", "Chestle", "Richten", "Coldman", "Gounard", "Daulby", "Shemmin", "Grzegorzewski", "Trevor", "Cruickshanks", "Fennell", "Brailey", "Engledow", "Clayworth", "Posten", "Chasmer", "Leadston", "Agge", "Blazdell", "Orrom", "Theriot", "Aslet", "Sallan", "Renfield", "Rayner", "Standley", "Canfer", "Warters", "Porteous", "Haydock", "Toon", "Lesper", "Bonus", "Gauche", "Mazillius", "Wickes", "Macartney", "Winger", "Orrick", "Roo", "Pidgley", "Dover", "Addison", "Griffiths", "Jersch", "Petyakov", "Matys", "Mattevi", "Obbard", "Plante", "Tebbutt", "Morit", "Canfield", "Battershall", "Bakhrushkin", "Langston", "Heitz", "Reddyhoff", "Coenraets", "Fawckner", "Runciman", "Ablitt", "Le Claire", "McKissack", "Leve", "Pepineaux", "Mickan", "Vegas", "Whitrod", "Massinger", "Aleso", "Durak", "Grimwad", "Gellion", "Stafford", "Newstead", "Hutchison", "Pennetti", "Kas", "Hasluck", "Kiraly", "Bayfield", "Iredell" };
            string[] City = new string[] { "Karak", "Stockholm", "Lenger", "Praia de Mira", "Angers", "Cumedak", "Lishan", "Shangqiu", "Qaxa", "Laborde", "Rosario de Lerma", "Kangalassy", "Devesa", "Xuyong", "Muli", "Verkhnya Rozhanka", "Guinsang-an", "Khrebtovaya", "Livadiya", "Rumbek", "Rodionovo-Nesvetayskaya", "Portland", "Mangli", "Abomsa", "Gizo", "Taipingying", "Tamra", "Deventer", "Cangyou", "Bijaepasu", "Chum Phuang", "Chengbei", "Navoiy", "Nanzhihui", "Cagmanaba", "Yantan", "Carleton Place", "Haradzyeya", "Xiaohebian" };
            string[] Address = new string[] { "14 Old Gate Place", "5 Schmedeman Alley", "4 Havey Park", "51290 Spohn Park", "7 Vermont Court", "992 Dapin Parkway", "3 Bultman Avenue", "9799 Stang Road", "90 Kropf Trail", "33 Pearson Avenue", "0 Twin Pines Place", "7860 Jackson Lane", "77909 Sunbrook Center", "41 1st Terrace", "8 Burning Wood Way", "785 Pearson Alley", "7 Bluejay Avenue", "3730 Johnson Parkway", "5013 Dottie Parkway", "0 Sherman Hill", "79 Springview Trail", "82 Stone Corner Parkway", "89118 Sugar Plaza", "28458 Mosinee Way", "79467 Swallow Way", "264 Shopko Street", "3517 Goodland Center", "92260 Mendota Alley", "888 Kedzie Road", "924 Westerfield Trail", "10 Elgar Terrace", "32 Pankratz Crossing", "97959 Brickson Park Park", "9 Birchwood Circle", "72598 Scoville Trail", "43878 Corscot Court", "89059 Heath Plaza", "17218 Northfield Street", "438 Lillian Park", "5093 Hauk Trail", "73464 Brickson Park Place", "37274 Autumn Leaf Road", "379 Sutherland Drive", "9 Lindbergh Drive", "3260 Surrey Hill", "6 Carey Parkway", "8585 Stephen Court", "19983 Warner Road", "9621 Pennsylvania Pass", "7 Mayfield Hill" };
            string[] EICode = new string[] { "HVAV", "HCAB", "HVAL" };
            string[] FieldOfStudy = new string[] { "30", "31", "32", "34" };
            string[] ProgramName = new string[] { "Animal Care", "Art Fundamentals", "Bachelor of Craft and Design", "Bachelor of Design (Honours)", "Bachelor of Early Childhood Leadership", "Bachelor of Film and Television", "Bachelor of Game Design", "Bachelor of Illustration", "Bachelor of Interaction Design", "Bachelor of Interior Design", "Bachelor of Music Theatre Performance", "Bachelor of Photography", "Business Process Management", "Computer Animation", "Computer Programmer", "Design (Bachelor of Design - Honours)", "Early Childhood Education - Intensive", "Educational Support", "Educational Support - Intensive", "Electrical Techniques" };
            string[] EIName = new string[] { "The Audio Recording Academy", "Canadian Mothercraft Society", "Trillium College-Kingston", "Great Lakes Bible College", "Heritage Baptist College", "Herzing Institutes of Canada", "Institute for Christian Studies", "Canada's National Ballet School", "Trillium College Inc.-St Catharines", "Career College Group Medix-Brampton", "Willis Business College-Smith Falls", "Trillium College Inc-Toronto", "Tyndale University College And Seminary", "RCC Institute of Technology-Concord", "WCI Westervelt College Inc.", "Willis Business College Limited", "West End Academy", "Canadian Memorial Chiropractic College", "Masters College And Seminary", "Emmanuel Bible College", "Royal Conservatory of Music", "Trios College -Scarborough", "Sunview College - Mississauga" };
            string[] CurrentProgramYear = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] LevelOfStudy = new string[] { "1", "2", "3", "4", "5" }; // " 1 - Certificate , 2 - Diploma, 3 - Bachelor, 4 - Masters, 5 - Doctorate"
            string[] InstituteType = new string[] { "1", "2", "3", "4", "5", "6", "7" }; //" 1 = University, 2 = Community College, 3 = Institute of Technology, 4 = CEGEP, 6 = Private, 7 = Diploma Sc of Nursing"
            // test NY 4

            RandomValueFromList item = new RandomValueFromList();
            DBconnection obj = new DBconnection();


            string pathToFile = "\\\\VC01WFSS";  
            if (EnvironmentIndicator == "S")
            {
                pathToFile += "QA";
            } else if (EnvironmentIndicator == "D")
            {
                pathToFile += "DV";
            }

            pathToFile += "701.devservices.dh.com\\DES_DATA\\INBOUND\\";

            eCertFileName = "ECERT\\PP" + Originator.ToString() + ".EDU.CERTS.D" + CurrentDate.GenerateTodayDateJulian() + "." + obj.GetEcertSqnInFileName().ToString().PadLeft(3, '0');
            MSFAAfileName = "MSFAA\\TP" + Originator.ToString() + ".EDU.MSFA.SENT." + CurrentDate.GenerateTodayDate() + "." + obj.GetMCFAASqnInFileName().ToString().PadLeft(3, '0');

            //Write ecert Header part
            switch (Originator)
            {
                case "NS":
                    //Create eCert File header NS
                    File.WriteAllText(pathToFile + eCertFileName, Header.AddEcertHeaderNS() + Environment.NewLine);
                    break;
                case "NL":
                    //Create eCert File header NL
                    File.WriteAllText(pathToFile + eCertFileName, Header.AddEcertHeaderNL() + Environment.NewLine);
                    break;
                case "ON":
                    //Create eCert File header ON to do
                    File.WriteAllText(pathToFile + eCertFileName, Header.AddEcertHeaderNL() + Environment.NewLine);
                    break;
                case "AB":
                    //Create eCert File header AB 
                    File.WriteAllText(pathToFile + "CSL.CERT.SENT." + CurrentDate.GenerateTodayDate(), Header.AddEcertHeaderAB() + Environment.NewLine);
                    break;
                case "YT":
                    //Create eCert File header YT
                    File.WriteAllText(pathToFile + eCertFileName, Header.AddEcertHeaderYT() + Environment.NewLine);
                    break;
                case "PE":
                    //Create eCert File header PE
                    File.WriteAllText(pathToFile + eCertFileName, Header.AddEcertHeaderPE() + Environment.NewLine);
                    break;
                case "NB":
                    //Create eCert File header NB
                    File.WriteAllText(pathToFile + eCertFileName, Header.AddEcertHeaderNB() + Environment.NewLine);
                    break;
                case "MB":
                    //Create eCert File header MB
                    File.WriteAllText(pathToFile + eCertFileName, Header.AddEcertHeaderMB() + Environment.NewLine);
                    break;
                case "SK":
                    //Create eCert File header SK
                    File.WriteAllText(pathToFile + eCertFileName, Header.AddEcertHeaderSK() + Environment.NewLine);
                    break;
                default:
                    Console.WriteLine("please specify correct province code");
                    break;
            }




            //Create MSFAA File (Header)
            if (MSFAAFlag == "Y")
            {
                File.WriteAllText(pathToFile + MSFAAfileName, Header.AddMSFAAHeader() + Environment.NewLine);
            }


            for (int i = 0; i < NumberOfeCertRecords; i++)
            {

                string firstName = item.SelectRandomValueFromList(FirstNames);
                string lastName = item.SelectRandomValueFromList(LastNames);
                string address = item.SelectRandomValueFromList(Address);
                string city = item.SelectRandomValueFromList(City);
                string eicode = item.SelectRandomValueFromList(EICode);
                string fieldofstudy = item.SelectRandomValueFromList(FieldOfStudy);
                string mSFAaPTIndicator = item.SelectRandomValueFromList(MSFAaPTIndicator);
                string maritalStatus = item.SelectRandomValueFromList(MaritalStatus);
                string studenttype = item.SelectRandomValueFromList(Studenttype);
                string gender = item.SelectRandomValueFromList(Gender);
                string disabilityIndicator = item.SelectRandomValueFromList(DisabilityIndicator);
                string postalcode = item.SelectRandomValueFromList(PostalCode);
                string provinceCode = item.SelectRandomValueFromList(ProvinceCode);
                string semesterIndicator = item.SelectRandomValueFromList(SemesterIndicator);
                string enrollmentConfirmation = item.SelectRandomValueFromList(EnrollmentConfirmation);
                string status = item.SelectRandomValueFromList(Status);
                string programName = item.SelectRandomValueFromList(ProgramName);
                string eiName = item.SelectRandomValueFromList(EIName);
                string currentProgramYear = item.SelectRandomValueFromList(CurrentProgramYear);
                string levelOfStudy = item.SelectRandomValueFromList(LevelOfStudy);
                string instituteType = item.SelectRandomValueFromList(InstituteType);



                string Birthdate = RandomDate.GenerateRandomDate(1950, 2000);
                string Phone = "4163125658";
                string ProgramDate = RandomDate.GenerateRandomDate(2015, 2017);
                string CSLAmount = RandomData.RandomDigits(4);
                string CertificateNumber = RandomData.RandomDigits(8);
                string NotBeforeDate = RandomDate.GenerateRandomDate(2016, 2018);
                string WeeksOfStudy = RandomData.RandomDigits(2);
                string EIConfirmDate = RandomDate.GenerateRandomDate(2016, 2017);
                string EIAmount = "0";
                string FirstPaymentAmount = RandomData.RandomDigits(4);
                string ClientFileNumber = RandomData.RandomDigits(7);
                string ControlNumber = RandomData.RandomDigits(4);


                Random rnd = StaticRandom.Instance;



                //Define ProgramStart and End date
                string ProgramStartDate = ProgramDate;
                DateTime ProgramStartDateHolderDateFormat = DateTime.ParseExact(ProgramStartDate, "yyyyMMdd", null);

                Random RandomDays = StaticRandom.Instance;
                int rDays = RandomDays.Next(90, 1000);
                string ProgramEndDate = ProgramStartDateHolderDateFormat.AddDays(rDays).ToString("yyyyMMdd");
                string ProgramEndDateShort = ProgramStartDateHolderDateFormat.AddDays(rDays).ToString("yyyyMM");
                string MidPointDate = ProgramStartDateHolderDateFormat.AddDays(rDays / 2).ToString("yyyyMMdd");



                //Assign random value to grant
                Grants g1 = new Grants();
                GenericRandomGrant g1value = new GenericRandomGrant();

                g1.cSGP_LI_at_NBD_or_cSGP_PT_at_NBD = 100;

                g1.cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD = g1value.GrantValue();

                g1.cSGP_PDSE_at_the_NBD = g1value.GrantValue();

                g1.cSGP_PD_at_NBD = g1value.GrantValue();

                g1.NLAmount = g1value.GrantValue();

                g1.NL_provintial_grant = g1value.GrantValue();

                g1.TransitionGrantYT = g1value.GrantValue();

                g1.NBLAmount = g1value.GrantValue();

                g1.NBBursary = g1value.GrantValue();

                g1.NB_Grant = g1value.GrantValue();

                //assign value if PT indicator is F
                if (mSFAaPTIndicator.Truncate(1) == "F")
                {
                    g1.cSGP_MI_at_NBD = g1value.GrantValue();
                    ABProgramType = "S";

                }
                else
                {
                    g1.cSGP_MI_at_NBD = 0;
                    ABProgramType = "P";
                }



                //to be refactored, should go to  Create MSFAA
                AgreementNumber = RandomData.RandomDigits(10);
                string CountryName = "CA"; //hardcoded country
                long SINCommonForMSFAAandEcert = ValidSIN.GenerateValidSIN();

                ////csv file for auto msfaa signing
                //string datasource = lastName + "," + firstName + "," + DateTime.ParseExact(Birthdate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd")+","+ SINCommonForMSFAAandEcert.ToString();
                //string datasourceName = "automsfaa" + SequenceNumber.ToString().PadLeft(3, '0') + ".csv";
                //File.AppendAllText(pathToFile + datasourceName, datasource + Environment.NewLine);

                RecordMSFAA =
                        "200" //MSFAA RecordType
                      + AgreementNumber
                      + SINCommonForMSFAAandEcert.ToString()
                      + "P" //MSFAA StatusCode
                      + "AABB" //MSFAA FederalInstitutionCode
                      + Birthdate
                      + RandomDate.GenerateRandomDate(2016, 2017)
                      + lastName.Truncate(25).PadRight(25)
                      + firstName.Truncate(15).PadRight(15)
                      + Filler.AddFiller(3)
                      + item.SelectRandomValueFromList(GenderLetter)//gender 
                      + "S"/*meritalstatus to be replaced*/
                      + address.Truncate(40).PadRight(40)
                      + address.Truncate(40).PadRight(40)
                      + city.Truncate(25).PadRight(25)
                      + provinceCode.PadRight(4)//ProvinceCode[RandomProvinceCode].PadRight(4)
                      + postalcode.PadRight(16) //PostalCode[RandomPostalCode].PadRight(16)
                      + CountryName.PadRight(20) + Phone.PadLeft(20, '0')
                      + (firstName + lastName + "@gmail.com").PadRight(70)
                      + address.Truncate(40).PadRight(40)
                      + address.Truncate(40).PadRight(40)
                      + city.Truncate(25).PadRight(25)
                      + provinceCode.PadRight(4) //ProvinceCode[RandomProvinceCode].PadRight(4) 
                      + postalcode.PadRight(16)//PostalCode[RandomPostalCode].PadRight(16)
                      + CountryName.PadRight(20)
                      + Phone.PadLeft(20, '0')
                      + item.SelectRandomValueFromList(MSFAaPTIndicator) //MSFAaPTIndicator[RandomMSFAaPTIndicator]
                      + Filler.AddFiller(110);


                //Total all grants

                int AwardTotal = g1.cSGP_LI_at_NBD_or_cSGP_PT_at_NBD + g1.cSGP_MI_at_NBD + g1.cSGP_PD_at_NBD + g1.cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD + g1.cSGP_PDSE_at_the_NBD + MidPoint.ValueAtMidPoint(g1.cSGP_LI_at_NBD_or_cSGP_PT_at_NBD) + MidPoint.ValueAtMidPoint(g1.cSGP_MI_at_NBD) + MidPoint.ValueAtMidPoint(g1.cSGP_PD_at_NBD) + MidPoint.ValueAtMidPoint(g1.cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD) + MidPoint.ValueAtMidPoint(g1.cSGP_PDSE_at_the_NBD);
                int AwardTotalYT = AwardTotal + g1.TransitionGrantYT;


                eCertRecordNS =
                   "D"   //1 RecordType 
                   + SINCommonForMSFAAandEcert //2
                   + RandomData.RandomDigits(12) //3 StudentID
                   + maritalStatus //4
                   + studenttype  //5
                   + gender   //6
                   + "1"   // language 7
                   + disabilityIndicator  //8
                   + Birthdate //9
                   + lastName.PadRight(50)  //10
                   + firstName.PadRight(25) //11
                   + address.PadRight(50)  //12
                   + address.PadRight(50)  //13
                   + city.PadRight(28)   //14
                   + provinceCode //15
                   + postalcode    //16
                   + Phone.PadRight(20)    //17
                   + Filler.AddFiller(55)  //18
                   + address.Truncate(50).PadRight(50)  //19
                   + address.Truncate(50).PadRight(50)  //20
                   + city.Truncate(28).PadRight(28)   //21
                   + provinceCode    //22 
                   + postalcode    //23
                   + eicode //24
                   + eiName.PadRight(40)   //25
                   + address.Truncate(20).PadRight(20)   //26
                   + address.Truncate(20).PadRight(20)   //27
                   + fieldofstudy.PadRight(2)   //28
                   + programName.Truncate(30).PadRight(30)  //29
                   + currentProgramYear    //30
                   + currentProgramYear  //31  ProgramYears
                   + ProgramStartDate  //32
                   + ProgramEndDate    //33
                   + semesterIndicator //34
                   + CSLAmount.PadLeft(6, '0')     //35
                   + Filler.AddFiller(6)   //36
                   + CSLAmount.PadLeft(6, '0') //(Convert.ToInt32(CSLAmount) + AwardTotal).ToString().PadLeft(6, '0') //37
                   + mSFAaPTIndicator.Truncate(1) //38 PT Indicator
                   + Filler.AddFiller(4)   //39
                   + CertificateNumber //40
                   + NotBeforeDate //41
                   + DateTime.Now.ToString("yyyyMMdd") //42
                   + enrollmentConfirmation    //43
                   + status  //44
                   + WeeksOfStudy  //45
                   + Filler.AddFiller(8) //46 EIConfirmDate
                   + EIAmount.PadRight(8, '0')  //47
                   + (firstName + lastName + "@gmail.com").PadRight(50)  //48
                   + Filler.AddFiller(41)  //49
                   + AwardTotal.ToString().PadLeft(5, '0')  //50
                   + g1.cSGP_LI_at_NBD_or_cSGP_PT_at_NBD.ToString().PadLeft(5, '0') //51
                   + g1.cSGP_MI_at_NBD.ToString().PadLeft(5, '0')   //52
                   + g1.cSGP_PD_at_NBD.ToString().PadLeft(5, '0')   //53
                   + g1.cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD.ToString().PadLeft(5, '0')   //54 
                   + g1.cSGP_PDSE_at_the_NBD.ToString().PadLeft(5, '0') //55
                   + Filler.AddFiller(20)  //56
                   + MidPointDate  //57
                   + MidPoint.ValueAtMidPoint(g1.cSGP_LI_at_NBD_or_cSGP_PT_at_NBD).ToString().PadLeft(5, '0') //58
                   + MidPoint.ValueAtMidPoint(g1.cSGP_MI_at_NBD).ToString().PadLeft(5, '0') //59
                   + MidPoint.ValueAtMidPoint(g1.cSGP_PD_at_NBD).ToString().PadLeft(5, '0')    //60
                   + MidPoint.ValueAtMidPoint(g1.cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD).ToString().PadLeft(5, '0') //61
                   + MidPoint.ValueAtMidPoint(g1.cSGP_PDSE_at_the_NBD).ToString().PadLeft(5, '0')  //62
                   + Filler.AddFiller(20); //63

                eCertRecordPE =
                    "D"   //1 RecordType 
                    + SINCommonForMSFAAandEcert //2
                    + RandomData.RandomDigits(12) //3 StudentID
                    + maritalStatus //4
                    + studenttype  //5
                    + gender   //6
                    + "1"   // language 7
                    + disabilityIndicator  //8
                    + Birthdate //9
                    + lastName.PadRight(50)  //10
                    + firstName.PadRight(25) //11
                    + address.PadRight(50)  //12
                    + address.PadRight(50)  //13
                    + city.PadRight(28)   //14
                    + provinceCode //15
                    + postalcode    //16
                    + Phone.PadRight(20)    //17
                    + CountryName.PadRight(20) //18
                    + CountryName.PadRight(20) //19  Alt Country
                    + Filler.AddFiller(15)  //20
                    + address.Truncate(50).PadRight(50)  //21
                    + address.Truncate(50).PadRight(50)  //22
                    + city.Truncate(28).PadRight(28)   //23
                    + provinceCode    //22 
                    + postalcode    //23
                    + eicode //24
                    + eiName.PadRight(40)   //25
                    + address.Truncate(20).PadRight(20)   //26
                    + address.Truncate(20).PadRight(20)   //27
                    + fieldofstudy.PadRight(2)   //28
                    + programName.Truncate(30).PadRight(30)  //29
                    + currentProgramYear    //30
                    + currentProgramYear  //31  ProgramYears
                    + ProgramStartDate  //32
                    + ProgramEndDate    //33
                    + semesterIndicator //34
                    + CSLAmount.PadLeft(6, '0')     //35
                    + Filler.AddFiller(6)   //36
                    + CSLAmount.PadLeft(6, '0') //37
                    + mSFAaPTIndicator.Truncate(1) //38 PT Indicator
                    + Filler.AddFiller(4)   //39
                    + CertificateNumber //40
                    + NotBeforeDate //41
                    + DateTime.Now.ToString("yyyyMMdd") //42
                    + enrollmentConfirmation    //43
                    + status  //44
                    + WeeksOfStudy  //45
                    + Filler.AddFiller(8) //46 EIConfirmDate
                    + EIAmount.PadLeft(8, '0')  //47
                    + (firstName + lastName + "@gmail.com").PadRight(50)  //48
                    + Filler.AddFiller(41)  //49
                    + AwardTotal.ToString().PadLeft(5, '0')  //50
                    + g1.cSGP_LI_at_NBD_or_cSGP_PT_at_NBD.ToString().PadLeft(5, '0') //51
                    + g1.cSGP_MI_at_NBD.ToString().PadLeft(5, '0')   //52
                    + g1.cSGP_PD_at_NBD.ToString().PadLeft(5, '0')   //53
                    + g1.cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD.ToString().PadLeft(5, '0')   //54 
                    + g1.cSGP_PDSE_at_the_NBD.ToString().PadLeft(5, '0') //55
                    + Filler.AddFiller(20)  //56
                    + MidPointDate  //57
                    + MidPoint.ValueAtMidPoint(g1.cSGP_LI_at_NBD_or_cSGP_PT_at_NBD).ToString().PadLeft(5, '0') //58
                    + MidPoint.ValueAtMidPoint(g1.cSGP_MI_at_NBD).ToString().PadLeft(5, '0') //59
                    + MidPoint.ValueAtMidPoint(g1.cSGP_PD_at_NBD).ToString().PadLeft(5, '0')    //60
                    + MidPoint.ValueAtMidPoint(g1.cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD).ToString().PadLeft(5, '0') //61
                    + MidPoint.ValueAtMidPoint(g1.cSGP_PDSE_at_the_NBD).ToString().PadLeft(5, '0')  //62
                    + Filler.AddFiller(20); //63

                //eCert record for NL
                eCertRecordNL =
                    "D"  //1
                    + SINCommonForMSFAAandEcert //2
                    + RandomData.RandomDigits(9) //StudentID //3
                    + maritalStatus //4
                    + studenttype  //5
                    + item.SelectRandomValueFromList(GenderLetter)  //6   Gender
                    + "1"    //7  Language
                    + disabilityIndicator  //8
                    + Birthdate //9
                    + lastName.Truncate(20).PadRight(20) //10
                    + firstName.Truncate(20).PadRight(20) //11
                    + address.Truncate(20).PadRight(20) //12    
                    + address.Truncate(20).PadRight(20) //13
                    + provinceCode   //14
                    + postalcode   //15
                    + city.Truncate(20).PadRight(20)   //16
                    + CountryName.PadRight(20)  //17
                    + address.Truncate(20).PadRight(20)  //18
                    + address.Truncate(20).PadRight(20)  //19
                    + provinceCode   //20
                    + postalcode   //21
                    + eicode //22
                    + eiName.Truncate(40).PadRight(40)   //23
                    + address.Truncate(20).PadRight(20)   //24
                    + address.Truncate(20).PadRight(20)   //25
                    + fieldofstudy.PadRight(2)   //26
                    + Filler.AddFiller(8)       //EIConfirmDate //27
                    + EIAmount.PadLeft(8, '0')  //28
                    + Filler.AddFiller(6)   //29
                    + ProgramStartDate  //30
                    + currentProgramYear    //31
                    + currentProgramYear  //32 ProgramYears
                    + ProgramStartDate  //33
                    + ProgramEndDate  //34
                    + semesterIndicator  //35
                    + CSLAmount.PadLeft(6, '0') //36
                    + g1.NLAmount.ToString().PadLeft(6, '0')  //37
                    + (Convert.ToInt32(CSLAmount) + g1.NLAmount).ToString().PadLeft(6, '0') //38
                    + Filler.AddFiller(1)   //39
                    + "I" + CertificateNumber.Truncate(6) //40
                    + NotBeforeDate //41
                    + DateTime.Now.ToString("yyyyMMdd") //42
                    + enrollmentConfirmation  //43
                    + status    //44
                    + WeeksOfStudy  //45
                    + g1.NL_provintial_grant.ToString().PadLeft(5, '0') //46
                    + Filler.AddFiller(5)   //47
                    + (firstName + lastName + "@gmail.com").PadRight(50)  //48
                    + Phone.PadRight(10)    //49
                    + mSFAaPTIndicator.Truncate(1) //PT Indicator 50
                    + CourseLoad.GenerateCourseLoad(44, 99)  //51
                    + AwardTotal.ToString().PadLeft(5, '0') //52
                    + g1.cSGP_LI_at_NBD_or_cSGP_PT_at_NBD.ToString().PadLeft(5, '0') //53
                    + g1.cSGP_MI_at_NBD.ToString().PadLeft(5, '0')  //54
                    + g1.cSGP_PD_at_NBD.ToString().PadLeft(5, '0')  //55
                    + g1.cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD.ToString().PadLeft(5, '0')  //56
                    + g1.cSGP_PDSE_at_the_NBD.ToString().PadLeft(5, '0') //57
                    + Filler.AddFiller(20)  //58
                    + MidPointDate  //59
                    + MidPoint.ValueAtMidPoint(g1.cSGP_LI_at_NBD_or_cSGP_PT_at_NBD).ToString().PadLeft(5, '0')  //60
                    + MidPoint.ValueAtMidPoint(g1.cSGP_MI_at_NBD).ToString().PadLeft(5, '0')    //61
                    + MidPoint.ValueAtMidPoint(g1.cSGP_PD_at_NBD).ToString().PadLeft(5, '0')    //62
                    + MidPoint.ValueAtMidPoint(g1.cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD).ToString().PadLeft(5, '0')    //63
                    + MidPoint.ValueAtMidPoint(g1.cSGP_PDSE_at_the_NBD).ToString().PadLeft(5, '0')  //64
                    + Filler.AddFiller(20)  //65
                    + programName.Truncate(30).PadRight(30)  //66
                    + Filler.AddFiller(70)  //67
                    ;


                //eCert record section 2 for AB
                eCertRecordAB_section2 =
                      "02" //2.1 Record Type
                    + SINCommonForMSFAAandEcert //2.2 SIN
                    + CertificateNumber.ToString().PadRight(9, '0') //2.3 Certificate number
                    + CurrentDate.GenerateTodayDate() //2.4 Date issued YYYYMMDD
                    + ProgramEndDate //2.5 Program end date YYYYMMDD
                    + ProgramStartDate //2.6 Program start date YYYYMMDD
                    + DateTime.Now.ToString("yyyyMMdd") //2.7 Disburse date YYYYMMDD
                    + CSLAmount.PadLeft(9, '0') //2.8 CSL amount
                    + AwardTotal.ToString().PadLeft(5, '0') //2.9 CSGP amount (Total Award Amount)
                    + g1.cSGP_LI_at_NBD_or_cSGP_PT_at_NBD.ToString().PadLeft(5, '0') //2.10
                    + g1.cSGP_MI_at_NBD.ToString().PadLeft(5, '0') //2.11
                    + g1.cSGP_PD_at_NBD.ToString().PadLeft(5, '0')  //2.12
                    + g1.cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD.ToString().PadLeft(5, '0')  //2.13
                    + g1.cSGP_PDSE_at_the_NBD.ToString().PadLeft(5, '0')  //2.14
                    + Filler.AddFiller(20) //2.15 Filler
                    + eicode //2.16 EI Code
                    + fieldofstudy.PadRight(2) //2.17 
                    + ABProgramType // 2.18 ProgramType or Semester Indicator for AB
                    + currentProgramYear   //2.19
                    + currentProgramYear.PadLeft(2, '0')  //2.20 ProgramYears
                    + WeeksOfStudy.PadLeft(3, '0') //2.21
                    + RandomData.RandomDigits(12) //2.22 Student Number
                    + lastName.Truncate(25).PadRight(25) //2.23 
                    + firstName.Truncate(16).PadRight(16) //2.24
                    + firstName.Truncate(10).PadRight(10) //2.25  Middle Name
                    + item.SelectRandomValueFromList(GenderLetter)//gender  //    2.26  Gender
                    + Birthdate //2.27
                    + "1" // 2.28 Language
                    + maritalStatus //2.29
                    + RandomData.RandomDigits(12) //2.30 Application ID
                    + RandomData.RandomDigits(9) //2.31 AB student number
                    + gender //2.32 Ent method 1-paper 2- electronic (e-cert) ; values defaults to gender list, that's why gender variable was reused. REDO!!!!
                    + enrollmentConfirmation //2.33 
                    + EIConfirmDate //2.34
                    + "0000000" //2.35 EI tuition amt; Optional - default to 0
                    + RandomData.RandomDigits(12) //2.36 Confirmation authorization id
                    + Filler.AddFiller(22)
                    ;


                //eCert record section 3 for AB
                eCertRecordAB_section3 =
                 "03" //3.1  Record Type
               + SINCommonForMSFAAandEcert //3.2 SIN
               + address.Truncate(40).PadRight(40) //3.3    
               + address.Truncate(40).PadRight(40) //3.4
               + city.PadRight(28)   //3.5
               + provinceCode    //3.6
               + CountryName.PadRight(20)  //3.7
               + postalcode.PadLeft(16)    //3.8
               + Phone // 3.9
               + Filler.AddFiller(103) //3.10
               + System.Environment.NewLine
               ;

                //eCert record section 5 for AB
                eCertRecordAB_section5 =
                 "05" //5.1  Record Type
              + SINCommonForMSFAAandEcert //5.2 SIN
              + address.Truncate(40).PadRight(40) //5.3    
              + address.Truncate(40).PadRight(40) //5.4
              + city.PadRight(28)   //5.5
              + provinceCode    //5.6
              + CountryName.PadRight(20)  //5.7
              + postalcode.PadLeft(16)    //5.8
              + Phone // 3.9
              + Filler.AddFiller(103) //5.10
              + System.Environment.NewLine
              ;

                //eCert record section 6 for AB
                eCertRecordAB_section6 =
                 "06" //6.1  Record Type
                + SINCommonForMSFAAandEcert //6.2 SIN
                + CertificateNumber.ToString().PadRight(9, '0') //6.3 Certificate number
                + CurrentDate.GenerateTodayDate() //6.4 Date issued YYYYMMDD
                + ProgramEndDate //6.5 Program end date YYYYMMDD
                + ProgramStartDate //6.6 Program start date YYYYMMDD
                + DateTime.Now.ToString("yyyyMMdd") //6.7 Disburse date YYYYMMDD
                + CSLAmount.PadLeft(9, '0') //6.8 CSL amount
                + eicode //6.9 EI Code
                + EIConfirmDate //6.10
                + Filler.AddFiller(197) //6.11
                + System.Environment.NewLine
                ;

                //Total CSL amount for AB ecert trailer
                AB_ecert_totalCSLamount = AB_ecert_totalCSLamount + Convert.ToInt32(CSLAmount);
                AB_ecert_totalCSGPamount = AB_ecert_totalCSGPamount + AwardTotal;


                //eCert record for YT
                eCertRecordYT =
                    "D" // 1 Record Type
                    + SINCommonForMSFAAandEcert //2 SIN
                    + RandomData.RandomDigits(12) //3 StudentID
                    + maritalStatus //4
                    + item.SelectRandomValueFromList(GenderLetter)//gender   5
                    + "1"   // language 6
                    + Birthdate //7
                    + lastName.PadRight(50)  //8
                    + firstName.PadRight(25) //9
                    + address.PadRight(50)  //10
                    + address.PadRight(50)  //11
                    + city.PadRight(28)   //12
                    + provinceCode //13
                    + Phone.PadRight(20)    //14
                    + postalcode //15
                    + CountryName.PadRight(20) //16
                    + (firstName + lastName + "@gmail.com").PadRight(50)  //17
                    + eicode // 18
                    + fieldofstudy.PadRight(2)   //19
                    + currentProgramYear    //20
                    + currentProgramYear  //21  ProgramYears
                    + ProgramStartDate  //22
                    + ProgramEndDate    //23
                    + CSLAmount.PadLeft(6, '0')     //24
                    + mSFAaPTIndicator.Truncate(1) //25 PT Indicator
                    + Filler.AddFiller(2)   //26
                    + CertificateNumber //27
                    + NotBeforeDate // 28
                    + DateTime.Now.ToString("yyyyMMdd") //29
                    + status  //30
                    + WeeksOfStudy  //31
                    + "000000" // 32 CAG
                    + "000000" // 33 CAG
                    + g1.TransitionGrantYT.ToString().PadLeft(6, '0') //34 Transition Grant
                    + AwardTotalYT.ToString().PadLeft(5, '0') //35
                    + g1.cSGP_LI_at_NBD_or_cSGP_PT_at_NBD.ToString().PadLeft(5, '0') //36
                    + g1.cSGP_MI_at_NBD.ToString().PadLeft(5, '0')   //37
                    + g1.cSGP_PD_at_NBD.ToString().PadLeft(5, '0')   //38
                    + g1.cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD.ToString().PadLeft(5, '0')   //39 
                    + g1.cSGP_PDSE_at_the_NBD.ToString().PadLeft(5, '0') //40
                    + Filler.AddFiller(20) //41
                    + MidPointDate  //42
                    + MidPoint.ValueAtMidPoint(g1.cSGP_LI_at_NBD_or_cSGP_PT_at_NBD).ToString().PadLeft(5, '0') //43
                    + MidPoint.ValueAtMidPoint(g1.cSGP_MI_at_NBD).ToString().PadLeft(5, '0') //44
                    + MidPoint.ValueAtMidPoint(g1.cSGP_PD_at_NBD).ToString().PadLeft(5, '0')    //45
                    + MidPoint.ValueAtMidPoint(g1.cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD).ToString().PadLeft(5, '0') //46
                    + MidPoint.ValueAtMidPoint(g1.cSGP_PDSE_at_the_NBD).ToString().PadLeft(5, '0')  //47
                    + Filler.AddFiller(20) //48
                    + enrollmentConfirmation    //49
                    + Filler.AddFiller(8) //50 EIConfirmDate
                    + EIAmount.PadRight(8)  //51
                    + programName.Truncate(50).PadRight(50)  //52
                    + disabilityIndicator //53
                    + CourseLoad.GenerateCourseLoad(10, 99) //54
                    + Filler.AddFiller(27) //55
                     ;

                eCertRecordNB =
                    "D" // 1 Record Type
                    + SINCommonForMSFAAandEcert //2 SIN
                    + RandomData.RandomDigits(12) //3 StudentID
                    + maritalStatus //4
                    + studenttype // 5
                    + item.SelectRandomValueFromList(GenderLetter)//gender  6
                    + "1"   // language 7
                    + disabilityIndicator // 8
                    + Birthdate //9
                    + lastName.Truncate(30).PadRight(30)  //10
                    + firstName.PadRight(30) //11
                    + address.PadRight(40)  //12
                    + address.PadRight(40)  //13
                    + provinceCode //14
                    + postalcode //15
                    + city.PadRight(28)   //16
                    + CountryName.PadRight(20) //17
                    + address.PadRight(40)  //18
                    + address.PadRight(40)  //19
                    + provinceCode //20
                    + postalcode //21
                    + city.PadRight(28)   //22
                    + CountryName.PadRight(20) //23
                    + eicode // 24
                    + eiName.Truncate(40).PadRight(40) //25
                    + address.PadRight(40)  //26
                    + address.PadRight(40)  //27
                    + fieldofstudy.PadRight(2)   //28
                    + programName.PadRight(50) //29
                    + Filler.AddFiller(8) // EIConfirmDate //30
                    + currentProgramYear    //31
                    + currentProgramYear  //32  ProgramYears
                    + ProgramStartDate  //33
                    + ProgramEndDate    //34
                    + semesterIndicator //35
                    + CSLAmount.PadLeft(6, '0')     //36
                    + g1.NBLAmount.ToString().PadLeft(6, '0') //37
                    + (Convert.ToInt32(CSLAmount) + g1.NBLAmount).ToString().PadLeft(6, '0') //38
                    + CertificateNumber //39
                    + NotBeforeDate // 40
                    + DateTime.Now.ToString("yyyyMMdd") //41 Date issued
                    + enrollmentConfirmation    //42
                    + status  //43
                    + WeeksOfStudy  //44
                    + levelOfStudy //45
                    + instituteType //46
                    + "A01   " //47 ProgramOfStudy Code
                    + "00000000" //48  EI Remittance Amount  V0.2 Should be zeros for NB  as they should be unconfirmed records
                    + mSFAaPTIndicator.Truncate(1) //49 PT Indicator
                    + CourseLoad.GenerateCourseLoad(10, 99) //50
                    + g1.NBBursary.ToString().PadLeft(5, '0') //51
                    + g1.NB_Grant.ToString().PadLeft(5, '0') //52
                    + AwardTotal.ToString().PadLeft(5, '0') //53
                    + g1.cSGP_LI_at_NBD_or_cSGP_PT_at_NBD.ToString().PadLeft(5, '0') //54
                    + g1.cSGP_MI_at_NBD.ToString().PadLeft(5, '0')   //55
                    + g1.cSGP_PD_at_NBD.ToString().PadLeft(5, '0')   //56
                    + g1.cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD.ToString().PadLeft(5, '0')   //57 
                    + g1.cSGP_PDSE_at_the_NBD.ToString().PadLeft(5, '0') //58
                    + Filler.AddFiller(20)   //59
                    + MidPointDate  //60
                    + MidPoint.ValueAtMidPoint(g1.cSGP_LI_at_NBD_or_cSGP_PT_at_NBD).ToString().PadLeft(5, '0') //61
                    + MidPoint.ValueAtMidPoint(g1.cSGP_MI_at_NBD).ToString().PadLeft(5, '0') //62
                    + MidPoint.ValueAtMidPoint(g1.cSGP_PD_at_NBD).ToString().PadLeft(5, '0')    //63
                    + MidPoint.ValueAtMidPoint(g1.cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD).ToString().PadLeft(5, '0') //64
                    + MidPoint.ValueAtMidPoint(g1.cSGP_PDSE_at_the_NBD).ToString().PadLeft(5, '0')  //65
                    + Filler.AddFiller(20) //66
                    + Phone.PadRight(20)    //67
                    + (firstName + lastName + "@gmail.com").PadRight(75)  //68
                    + Filler.AddFiller(65) //69
                     ;

                //eCert record for MB
                eCertRecordMB =
                     "D" // 1 Record Type
                    + SINCommonForMSFAAandEcert //2 SIN
                    + RandomData.RandomDigits(12) //3 StudentID
                    + maritalStatus //4
                    + studenttype // 5
                    + item.SelectRandomValueFromList(GenderLetter)//gender  6
                    + "1"   // language 7
                    + disabilityIndicator // 8
                    + Birthdate //9
                    + lastName.Truncate(20).PadRight(20)  //10
                    + firstName.Truncate(20).PadRight(20) //11
                    + address.Truncate(20).PadRight(20)  //12
                    + address.Truncate(20).PadRight(20)  //13
                    + provinceCode //14
                    + postalcode //15
                    + city.Truncate(20).PadRight(20)   //16
                    + CountryName.PadRight(20) //17
                    + address.Truncate(20).PadRight(20)  //18
                    + address.Truncate(20).PadRight(20)  //19
                    + provinceCode //20
                    + postalcode //21
                    + eicode // 22
                    + eiName.Truncate(40).PadRight(40) //23
                    + address.Truncate(20).PadRight(20)  //24
                    + address.Truncate(20).PadRight(20)  //25
                    + fieldofstudy.PadRight(2)   //26
                    + programName.Truncate(30).PadRight(30) //27
                    + currentProgramYear    //28
                    + currentProgramYear  //29  ProgramYears
                    + ProgramStartDate  //30
                    + ProgramEndDate    //31
                    + semesterIndicator //32
                    + CSLAmount.PadLeft(6, '0')     //33
                    + Filler.AddFiller(6)     //34
                    + CSLAmount.PadLeft(6, '0')     //35
                    + Filler.AddFiller(1)     //36
                    + CertificateNumber + "1234" //37
                    + NotBeforeDate // 38
                    + DateTime.Now.ToString("yyyyMMdd") //39 Date issued
                    + "C"           //enrollmentConfirmation    //40 (since MB is always confirmed)
                    + status  //41
                    + WeeksOfStudy  //42
                    + Filler.AddFiller(12)  //43 and 44
                    + (firstName + lastName + "@gmail.com").PadRight(50)   //45
                    + Phone    //46
                    + mSFAaPTIndicator.Truncate(1) //47 PT Indicator
                    + EIConfirmDate //48
                    + EIAmount.PadLeft(8, '0')  //49
                    + Filler.AddFiller(14)  //50
                    + AwardTotal.ToString().PadLeft(5, '0')  //51
                    + g1.cSGP_LI_at_NBD_or_cSGP_PT_at_NBD.ToString().PadLeft(5, '0') //52
                    + g1.cSGP_MI_at_NBD.ToString().PadLeft(5, '0')   //53
                    + g1.cSGP_PD_at_NBD.ToString().PadLeft(5, '0')   //54
                    + g1.cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD.ToString().PadLeft(5, '0')   //55 
                    + g1.cSGP_PDSE_at_the_NBD.ToString().PadLeft(5, '0') //56
                    + Filler.AddFiller(20)  //57
                    + MidPointDate  //58
                    + MidPoint.ValueAtMidPoint(g1.cSGP_LI_at_NBD_or_cSGP_PT_at_NBD).ToString().PadLeft(5, '0') //59
                    + MidPoint.ValueAtMidPoint(g1.cSGP_MI_at_NBD).ToString().PadLeft(5, '0') //60
                    + MidPoint.ValueAtMidPoint(g1.cSGP_PD_at_NBD).ToString().PadLeft(5, '0')    //61
                    + MidPoint.ValueAtMidPoint(g1.cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD).ToString().PadLeft(5, '0') //62
                    + MidPoint.ValueAtMidPoint(g1.cSGP_PDSE_at_the_NBD).ToString().PadLeft(5, '0')  //63
                    + Filler.AddFiller(20);  //64


                //eCert csl detail record for SK
                eCertRecordSK_csl_detail =
                    "15"   //1
                    + "SK"   //2
                    + SINCommonForMSFAAandEcert //3 SIN
                    + lastName.Truncate(30).PadRight(30)  //4
                    + firstName.Truncate(30).PadRight(30) //5
                    + Birthdate   //6
                    + gender   //7
                    + maritalStatus   //8
                    + eicode   //9
                    + fieldofstudy  //10
                    + currentProgramYear    //11
                    + currentProgramYear  //12 ProgramYears
                    + WeeksOfStudy  //13
                    + semesterIndicator  //14
                    + ProgramEndDateShort   //15
                    + ProgramDate   //16
                    + FirstPaymentAmount.PadLeft(5, '0')   //17
                    + RandomData.RandomDigits(12) //18
                    + Filler.AddFiller(1)  //19
                    + FirstPaymentAmount.PadLeft(5, '0')   //20 
                    + CurrentDate.GenerateTodayDate()    //21
                    + "U"  //22
                    + ClientFileNumber   //23
                    + ControlNumber   //24
                    + Filler.AddFiller(8)  //25
                    + "0000000"    //26
                    + Filler.AddFiller(1)  //27
                    + Filler.AddFiller(3)  //28
                    + AwardTotal.ToString().PadLeft(5, '0')  //29
                    + g1.cSGP_LI_at_NBD_or_cSGP_PT_at_NBD.ToString().PadLeft(5, '0') //30
                    + g1.cSGP_MI_at_NBD.ToString().PadLeft(5, '0')   //31
                    + g1.cSGP_PD_at_NBD.ToString().PadLeft(5, '0')   //32
                    + g1.cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD.ToString().PadLeft(5, '0')   //33
                    + g1.cSGP_PDSE_at_the_NBD.ToString().PadLeft(5, '0') //34
                    + Filler.AddFiller(20)  //35
                    + MidPointDate  //36
                    + MidPoint.ValueAtMidPoint(g1.cSGP_LI_at_NBD_or_cSGP_PT_at_NBD).ToString().PadLeft(5, '0') //37
                    + MidPoint.ValueAtMidPoint(g1.cSGP_MI_at_NBD).ToString().PadLeft(5, '0') //38
                    + MidPoint.ValueAtMidPoint(g1.cSGP_PD_at_NBD).ToString().PadLeft(5, '0')    //39
                    + MidPoint.ValueAtMidPoint(g1.cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD).ToString().PadLeft(5, '0') //40
                    + MidPoint.ValueAtMidPoint(g1.cSGP_PDSE_at_the_NBD).ToString().PadLeft(5, '0')  //41
                    + Filler.AddFiller(20)   //42
                    + mSFAaPTIndicator.Truncate(1)   //43
                    + RandomData.RandomDigits(2).PadLeft(2, '0')   //44
                    + programName.Truncate(50).PadRight(50)   //45
                    + ProgramEndDate   //46
                    + Filler.AddFiller(46);  //47

                eCertRecordSK_csl_trailer =
                    "99"  //1
                    + NumberOfeCertRecords.ToString().PadLeft(6, '0')  //2
                    + Filler.AddFiller(372)  //3
                    + System.Environment.NewLine;

                eCertRecordSK_ssl_header =
                    "00"   //1
                    + "SK"   //2
                    + Filler.AddFiller(1)   //3
                    + "15 "   //4
                    + CurrentDate.GenerateTodayDate()   //5
                    + "P"   //6
                    + Filler.AddFiller(363)
                    + System.Environment.NewLine;

                eCertRecordSK_ssl_detail =
                    eCertRecordSK_csl_detail.Truncate(108)   // Fields 1 to 16
                    + FirstPaymentAmount.PadLeft(5, '0')   //17
                    + "0000000000000" //18
                    + FirstPaymentAmount.PadLeft(5, '0')   //19
                    + CurrentDate.GenerateTodayDate()    //20
                    + "U"  //21
                    + ClientFileNumber   //22
                    + ControlNumber   //23
                    + Filler.AddFiller(229)
                    + System.Environment.NewLine;

                eCertRecordSK_ssl_trailer =
                    "99"  //1
                    + NumberOfeCertRecords.ToString().PadLeft(6, '0')  //2
                    + Filler.AddFiller(372)  //3
                    + System.Environment.NewLine;

                // Temporary as Cancelled Records not in scope
                eCertRecordSK_csl_header_cancel = "00SK 90 " + CurrentDate.GenerateTodayDate() + Filler.AddFiller(364) + System.Environment.NewLine;
                eCertRecordSK_csl_trailer_cancel = "99000000" + Filler.AddFiller(372) + System.Environment.NewLine;
                eCertRecordSK_ssl_header_cancel = "00SK 90 " + CurrentDate.GenerateTodayDate() +"P" + Filler.AddFiller(363) + System.Environment.NewLine;
                eCertRecordSK_ssl_trailer_cancel = "99000000" + Filler.AddFiller(372) + System.Environment.NewLine;



                if (status == "N")
                {
                    TotalOfCanceledDisbursement = TotalOfCanceledDisbursement + AwardTotal + Convert.ToInt32(CSLAmount);
                    TotalOfCanceledDisbursementYT = TotalOfCanceledDisbursementYT + Convert.ToInt32(CSLAmount);
                    TotalOfCanceledDisbursementNS = TotalOfCanceledDisbursementNS + Convert.ToInt32(CSLAmount);
                    // total of all disbursements for ecert NL trailer
                    TotalOfCanceledDisbursementNL = TotalOfCanceledDisbursementNL + Convert.ToInt32(CSLAmount) + Convert.ToInt32(g1.NLAmount);
                    TotalOfCanceledDisbursementPE = TotalOfCanceledDisbursementPE + Convert.ToInt32(CSLAmount);
                    TotalOfCanceledDisbursementNB = TotalOfCanceledDisbursementNB + Convert.ToInt32(CSLAmount) + g1.NBLAmount;
                    CSGPTotalNBCanceled = CSGPTotalNBCanceled + AwardTotal;
                    NBProvintialGrantCanceled += g1.NBBursary + g1.NB_Grant;
                    TotalOfCanceledDisbursementMB += Convert.ToInt32(CSLAmount);

                }
                else
                {
                    //Total of all Non Cancelled disbursements per file
                    TotalDisbursement = TotalDisbursement + AwardTotal + Convert.ToInt32(CSLAmount);
                    TotalDisbursementYT = TotalDisbursementYT + Convert.ToInt32(CSLAmount);
                    TotalDisbursementNS = TotalDisbursementNS + Convert.ToInt32(CSLAmount);
                    TotalDisbursementNL = TotalDisbursementNL + Convert.ToInt32(CSLAmount) + Convert.ToInt32(g1.NLAmount);
                    TotalDisbursementPE = TotalDisbursementPE + Convert.ToInt32(CSLAmount);
                    TotalDisbursementNB = TotalDisbursementNB + Convert.ToInt32(CSLAmount) + g1.NBLAmount;
                    CSGPTotalNB = CSGPTotalNB + AwardTotal;
                    NBProvintialGrant += g1.NBBursary + g1.NB_Grant;
                    TotalOfCanceledDisbursementMB += Convert.ToInt32(CSLAmount);
                }

                SINHashTotal = SINHashTotal + SINCommonForMSFAAandEcert;

                //Write ecert record
                switch (Originator)
                {
                    case "NS":
                        //Append eCert records
                        File.AppendAllText(pathToFile + eCertFileName, eCertRecordNS + Environment.NewLine);
                        break;
                    case "PE":
                        //Append eCert records
                        File.AppendAllText(pathToFile + eCertFileName, eCertRecordPE + Environment.NewLine);
                        break;
                    case "NL":
                        File.AppendAllText(pathToFile + eCertFileName, eCertRecordNL + Environment.NewLine);
                        break;
                    case "YT":
                        File.AppendAllText(pathToFile + eCertFileName, eCertRecordYT + Environment.NewLine);
                        break;
                    case "ON":
                        //Append eCert records
                        File.AppendAllText(pathToFile + eCertFileName, "not yet ready!!!" + Environment.NewLine); //to do
                        break;
                    case "AB":
                        //Append eCert section 2 records
                        File.AppendAllText(pathToFile + "CSL.CERT.SENT." + CurrentDate.GenerateTodayDate(), eCertRecordAB_section2 + Environment.NewLine);
                        break;
                    case "NB":
                        File.AppendAllText(pathToFile + eCertFileName, eCertRecordNB + Environment.NewLine);
                        break;
                    case "MB":
                        File.AppendAllText(pathToFile + eCertFileName, eCertRecordMB + Environment.NewLine);
                        break;
                    case "SK":
                        File.AppendAllText(pathToFile + eCertFileName, eCertRecordSK_csl_detail + Environment.NewLine);
                        break;
                }
                //Append MSFAA records
                if (MSFAAFlag == "Y")
                {
                    File.AppendAllText(pathToFile + MSFAAfileName, RecordMSFAA + Environment.NewLine);
                }

                AB_ecert_Section3_total += eCertRecordAB_section3;
                AB_ecert_Section5_total += eCertRecordAB_section5;

                eCertRecordSK_ssl_detail_total += eCertRecordSK_ssl_detail;


                if (status == "N")
                {
                    AB_ecert_Section6_total += eCertRecordAB_section6;
                    AB_ecert_Section6_counter++;
                }
            }


            //Write ecert record section 3 to AB
            switch (Originator)
            {
                case "AB":
                    File.AppendAllText(pathToFile + "CSL.CERT.SENT." + CurrentDate.GenerateTodayDate(), AB_ecert_Section3_total);
                    File.AppendAllText(pathToFile + "CSL.CERT.SENT." + CurrentDate.GenerateTodayDate(), AB_ecert_Section5_total);
                    File.AppendAllText(pathToFile + "CSL.CERT.SENT." + CurrentDate.GenerateTodayDate(), AB_ecert_Section6_total);

                    break;

                case "SK":
                    File.AppendAllText(pathToFile + eCertFileName, eCertRecordSK_csl_trailer);
                    File.AppendAllText(pathToFile + eCertFileName, eCertRecordSK_ssl_header);
                    File.AppendAllText(pathToFile + eCertFileName, eCertRecordSK_ssl_detail_total);
                    File.AppendAllText(pathToFile + eCertFileName, eCertRecordSK_ssl_trailer);
                    File.AppendAllText(pathToFile + eCertFileName, eCertRecordSK_csl_header_cancel);
                    File.AppendAllText(pathToFile + eCertFileName, eCertRecordSK_csl_trailer_cancel);
                    File.AppendAllText(pathToFile + eCertFileName, eCertRecordSK_ssl_header_cancel);
                    File.AppendAllText(pathToFile + eCertFileName, eCertRecordSK_ssl_trailer_cancel);
                    break;
                default:

                    break;
            }

            string MSFAAFileTitle = "MSFAA SENT";
            //Create trailer for MSFAA file
            MSFAATrailer = "999" + MSFAAFileTitle.ToString().PadRight(40) + NumberOfeCertRecords.ToString().PadLeft(9, '0') + SINHashTotal.ToString().PadLeft(15, '0') + Filler.AddFiller(533);

            //Add trailer to eCert
            switch (Originator)
            {
                case "NS":
                    //add trailer to eCert NS
                    File.AppendAllText(pathToFile + eCertFileName, "T" + NumberOfeCertRecords.ToString().PadLeft(6, '0') + TotalDisbursementNS.ToString().PadLeft(9, '0') + TotalOfCanceledDisbursementNS.ToString().PadLeft(9, '0') + Filler.AddFiller(828) + Environment.NewLine);
                    break;
                case "PE":
                    //add trailer to eCert PE
                    File.AppendAllText(pathToFile + eCertFileName, "T" + NumberOfeCertRecords.ToString().PadLeft(6, '0') + TotalDisbursementPE.ToString().PadLeft(9, '0') + TotalOfCanceledDisbursementPE.ToString().PadLeft(9, '0') + Filler.AddFiller(828) + Environment.NewLine);
                    break;
                case "NL":
                    //add trailer to eCert NL
                    File.AppendAllText(pathToFile + eCertFileName, "T" + NumberOfeCertRecords.ToString().PadLeft(6, '0') + TotalDisbursementNL.ToString().PadLeft(9, '0') + TotalOfCanceledDisbursementNL.ToString().PadLeft(9, '0') + Filler.AddFiller(640) + Environment.NewLine);
                    break;
                case "ON":
                    //add trailer to eCert on  to do
                    File.AppendAllText(pathToFile + eCertFileName, "T" + NumberOfeCertRecords.ToString().PadLeft(6, '0') + TotalDisbursement.ToString().PadLeft(9, '0') + TotalOfCanceledDisbursement.ToString().PadLeft(9, '0') + Filler.AddFiller(640) + Environment.NewLine);
                    break;
                case "AB":
                    //add trailer to eCert AB 
                    File.AppendAllText(pathToFile + "CSL.CERT.SENT." + CurrentDate.GenerateTodayDate(), "99" + NumberOfeCertRecords.ToString().PadLeft(9, '0') + NumberOfeCertRecords.ToString().PadLeft(9, '0') + "000000000" + NumberOfeCertRecords.ToString().PadLeft(9, '0') + AB_ecert_Section6_counter.ToString().PadLeft(9, '0') + AB_ecert_totalCSLamount.ToString().PadLeft(15, '0') + AB_ecert_totalCSGPamount.ToString().PadLeft(15, '0') + Filler.AddFiller(193) + Environment.NewLine);
                    break;
                case "YT":
                    //add trailer to eCert YT
                    File.AppendAllText(pathToFile + eCertFileName, "T" + NumberOfeCertRecords.ToString().PadLeft(6, '0') + TotalDisbursementYT.ToString().PadLeft(9, '0') + TotalOfCanceledDisbursementYT.ToString().PadLeft(9, '0') + Filler.AddFiller(587) + Environment.NewLine);
                    break;
                case "NB":
                    //add trailer to eCert NB
                    File.AppendAllText(pathToFile + eCertFileName, "T" + NumberOfeCertRecords.ToString().PadLeft(6, '0') + TotalDisbursementNB.ToString().PadLeft(9, '0') + TotalOfCanceledDisbursementNB.ToString().PadLeft(9, '0') + CSGPTotalNB.ToString().PadLeft(9, '0')+ NBProvintialGrant.ToString().PadLeft(9, '0') + NBProvintialGrant.ToString().PadLeft(9, '0')+ Filler.AddFiller(828) + Environment.NewLine);
                    break;
                case "MB":
                    //add trailer to eCert MB
                    File.AppendAllText(pathToFile + eCertFileName, "T" + NumberOfeCertRecords.ToString().PadLeft(6, '0') + TotalDisbursementMB.ToString().PadLeft(9, '0') + TotalOfCanceledDisbursementMB.ToString().PadLeft(9, '0') + Filler.AddFiller(578) + Environment.NewLine);
                    break;
            }
            //add trailer to MSFAA
            if (MSFAAFlag == "Y")
            {
                File.AppendAllText(pathToFile + MSFAAfileName, MSFAATrailer + Environment.NewLine);
            }

        }
    }
}
