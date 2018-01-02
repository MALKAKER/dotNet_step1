using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BE
{
    //bank names need to fill numbers
    public enum Bank {
        [Description("Hapoalim")] Hapoalim = 12,
        [Description("Yahav")] Yahav,
        [Description("Leumi")] Leumi,
        [Description("Mizrahi Tefahot")] Mizrahi_Tefahot,
        [Description("Discount")] Discount,
        [Description("HaBinleumi")] HaBinleumi };
    //which gender
    public enum Gender
    {
        [Description("Male")]male,
        [Description("Female")] female };
    //what kind of employee 
    public enum Specialization
    {
        [Description("Nanny")]Nanny,
        [Description("Babysitter")] Babysitter };
    //nannies skills
    public enum SKILLS {
        [Description("Special Needs")]Special_Needs,
        [Description("Warm")] Warm,
        [Description("Orgenized")] Orgenized,
        [Description("Creative")] Creative,
        [Description("Patient")] Patient,
        [Description("Calm")] Calm,
        [Description("Tidy")] Tidy,
        [Description("Happy")] Happy }
    //the language the employee speaks
    public enum Language {
        [Description("Hebrew")] Hebrew,
        [Description("English")] English,
        [Description("Yiddish")] Yiddish,
        [Description("Arabic")] Arabic,
        [Description("French")] French };//(add more languages)
    //important information about the child
    public enum ChildInfo {[Description("Allergies")] Allergies,
        [Description("Special Needs")] SpecialNeeds,
        [Description("Desease")] Desease };// maybe there are more details to fill in
    //the child's HMO
    public enum HMO {[Description("Clalit")] Clalit,
        [Description("Meuhedet")] Meuhedet,
        [Description("Maccabi")] Maccabi,
        [Description("Leumit")] Leumit };
    //countries
    enum Countries
    {
        Afghanistan, Albania, Algeria, American_Samoa, Andorra, Angola, Anguilla, Antarctica, Antigua_and_Barbuda, Argentina, Armenia,
        Aruba, Australia, Austria, Azerbaijan, Bahamas, Bahrain, Bangladesh, Barbados, Belarus, Belgium, Belize, Benin, Bermuda, Bhutan, Bolivia,
        Bosnia_and_Herzegovina, Botswana, Bouvet_Island, Brazil, British_Indian_Ocean_Territory, Brunei_Darussalam, Bulgaria, Burkina_Faso, Burundi,
        Cambodia, Cameroon, Canada, Cape_Verde, Cayman_Islands, Central_African_Republic, Chad, Chile, China, Christmas_Island, Cocos_Keeling_Islands,
        Colombia, Comoros, Congo, The_Democratic_Republic_Of_The_Cook_Islands, Costa_Rica, Croatia, Cuba, Cyprus, Czech_Republic,
        Denmark, Djibouti, Dominica, Dominican_Republic, East_Timor, Ecuador, Egypt, El_Salvador, Equatorial_Guinea, Eritrea, Estonia, Ethiopia,
        Falkland_Islands_Malvinas, Faroe_Islands, Fiji, Finland, France, French_Guiana, French_Polynesia, French_Southern_Territories, Gabon, Gambia,
        Georgia, Germany, Ghana, Gibraltar, Greece, Greenland, Grenada, Guadeloupe, Guam, Guatemala, Guinea, Guinea_Bissau, Guyana, Haiti,
        Heard_Island_and_Mcdonald_Islands, Holy_See_Vatican_City_State, Honduras, Hong_Kong, Hungary, Iceland, India, Indonesia, Iran, Islamic_Republic_of_Iraq,
        Ireland,[Description("Israel")] Israel, Italy, Ivory_Coast, Jamaica, Japan, Jordan, Kazakstan, Kenya, Kiribati, Korea, Democratic_Republic_of_Korea, Republic_of_Kosovo, Kuwait,
        Kyrgyzstan, Lao_Democratic_Republic, Latvia, Lebanon, Lesotho, Liberia, Libyan_Arab_Jamahiriya, Liechtenstein, Lithuania, Luxembourg, Macau,
        Macedonia, The_Former_Yugoslav_Republic_of_Madagascar, Malawi, Malaysia, Maldives, Mali, Malta, Marshall_Islands, Martinique, Mauritania,
        Mauritius, Mayotte, Mexico, Micronesia, Federated_States_of_Moldova, Republic_of_Monaco, Mongolia, Montserrat, Montenegro, Morocco, Mozambique,
        Myanmar, Namibia, Nauru, Nepal, Netherlands, Netherlands_Antilles, New_Caledonia, New_Zealand, Nicaragua, Niger, Nigeria, Niue, Norfolk_Island,
        Northern_Mariana_Islands, Norway, Oman, Pakistan, Palau, Panama, Papua_New_Guinea, Paraguay, Peru, Philippines, Pitcairn, Poland, Portugal,
        Puerto_Rico, Qatar, Reunion, Romania, Russian_Federation, Rwanda, Saint_Helena, Saint_Kitts_and_Nevis, Saint_Lucia, Saint_Pierre_and_Miquelon,
        Saint_Vincent_and_The_Grenadines, Samoa, San_Marino, Sao_Tome_and_Principe, Saudi_Arabia, Senegal, Serbia, Seychelles, Sierra_Leone, Singapore,
        Slovakia, Slovenia, Solomon_Islands, Somalia, South_Africa, South_Georgia_and_The_South_Sandwich_Islands, Spain, Sri_Lanka, Sudan, Suriname,
        Svalbard_and_Jan_Mayen, Swaziland, Sweden, Switzerland, Syrian_Arab_Republic, Taiwan, Province_Of_China, Tajikistan, Tanzania,
        United_Republic_of_Thailand, Togo, Tokelau, Tonga, Trinidad_and_Tobago, Tunisia, Turkey, Turkmenistan, Turks_and_Caicos_Islands, Tuvalu, Uganda,
        Ukraine, United_Arab_Emirates, United_Kingdom, United_States, United_States_Minor_Outlying_Islands, Uruguay, Uzbekistan, Vanuatu, Venezuela,
        Vietnam, Virgin_Islands, British_Virgin_Islands, United_States_Of_America, Wallis_and_Futuna, Western_Sahara, Yemen, Zambia, Zimbabwe
    }
}
