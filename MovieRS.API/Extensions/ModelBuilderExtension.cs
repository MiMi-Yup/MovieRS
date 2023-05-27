using Microsoft.EntityFrameworkCore;

namespace MovieRS.API.Models
{
    public static class CountrySeedData
    {
        public static Task OnModelCreatingPartial(this DbSet<Country> context)
        {
            return context.AddRangeAsync(
              new Country
              {
                  Name = "Ascension Island",
                  Code = "AC",
                  NameVi = null
              },
              new Country
              {
                  Name = "Andorra",
                  Code = "AD",
                  NameVi = null
              },
              new Country
              {
                  Name = "United Arab Emirates",
                  Code = "AE",
                  NameVi = "Các tiểu vương quốc Ả Rập Thống nhất"
              },
              new Country
              {
                  Name = "Afghanistan",
                  Code = "AF",
                  NameVi = null
              },
              new Country
              {
                  Name = "Antigua & Barbuda",
                  Code = "AG",
                  NameVi = null
              },
              new Country
              {
                  Name = "Anguilla",
                  Code = "AI",
                  NameVi = null
              },
              new Country
              {
                  Name = "Albania",
                  Code = "AL",
                  NameVi = null
              },
              new Country
              {
                  Name = "Armenia",
                  Code = "AM",
                  NameVi = null
              },
              new Country
              {
                  Name = "Angola",
                  Code = "AO",
                  NameVi = null
              },
              new Country
              {
                  Name = "Antarctica",
                  Code = "AQ",
                  NameVi = null
              },
              new Country
              {
                  Name = "Argentina",
                  Code = "AR",
                  NameVi = null
              },
              new Country
              {
                  Name = "American Samoa",
                  Code = "AS",
                  NameVi = "Samoa thuộc Mỹ"
              },
              new Country
              {
                  Name = "Austria",
                  Code = "AT",
                  NameVi = "Áo"
              },
              new Country
              {
                  Name = "Australia",
                  Code = "AU",
                  NameVi = "Úc"
              },
              new Country
              {
                  Name = "Aruba",
                  Code = "AW",
                  NameVi = null
              },
              new Country
              {
                  Name = "Åland Islands",
                  Code = "AX",
                  NameVi = null
              },
              new Country
              {
                  Name = "Azerbaijan",
                  Code = "AZ",
                  NameVi = null
              },
              new Country
              {
                  Name = "Bosnia & Herzegovina",
                  Code = "BA",
                  NameVi = null
              },
              new Country
              {
                  Name = "Barbados",
                  Code = "BB",
                  NameVi = null
              },
              new Country
              {
                  Name = "Bangladesh",
                  Code = "BD",
                  NameVi = null
              },
              new Country
              {
                  Name = "Belgium",
                  Code = "BE",
                  NameVi = "Bỉ"
              },
              new Country
              {
                  Name = "Burkina Faso",
                  Code = "BF",
                  NameVi = null
              },
              new Country
              {
                  Name = "Bulgaria",
                  Code = "BG",
                  NameVi = null
              },
              new Country
              {
                  Name = "Bahrain",
                  Code = "BH",
                  NameVi = null
              },
              new Country
              {
                  Name = "Burundi",
                  Code = "BI",
                  NameVi = null
              },
              new Country
              {
                  Name = "Benin",
                  Code = "BJ",
                  NameVi = null
              },
              new Country
              {
                  Name = "St. Barthélemy",
                  Code = "BL",
                  NameVi = null
              },
              new Country
              {
                  Name = "Bermuda",
                  Code = "BM",
                  NameVi = null
              },
              new Country
              {
                  Name = "Brunei",
                  Code = "BN",
                  NameVi = "Nhà nước Brunei Darussalam"
              },
              new Country
              {
                  Name = "Bolivia",
                  Code = "BO",
                  NameVi = null
              },
              new Country
              {
                  Name = "Caribbean Netherlands",
                  Code = "BQ",
                  NameVi = null
              },
              new Country
              {
                  Name = "Brazil",
                  Code = "BR",
                  NameVi = null
              },
              new Country
              {
                  Name = "Bahamas",
                  Code = "BS",
                  NameVi = null
              },
              new Country
              {
                  Name = "Bhutan",
                  Code = "BT",
                  NameVi = null
              },
              new Country
              {
                  Name = "Bouvet Island",
                  Code = "BV",
                  NameVi = "Đảo Bouvet"
              },
              new Country
              {
                  Name = "Botswana",
                  Code = "BW",
                  NameVi = null
              },
              new Country
              {
                  Name = "Belarus",
                  Code = "BY",
                  NameVi = null
              },
              new Country
              {
                  Name = "Belize",
                  Code = "BZ",
                  NameVi = null
              },
              new Country
              {
                  Name = "Canada",
                  Code = "CA",
                  NameVi = null
              },
              new Country
              {
                  Name = "Cocos (Keeling) Islands",
                  Code = "CC",
                  NameVi = "Quần đảo Cocos"
              },
              new Country
              {
                  Name = "Congo - Kinshasa",
                  Code = "CD",
                  NameVi = null
              },
              new Country
              {
                  Name = "Central African Republic",
                  Code = "CF",
                  NameVi = "Trung Phi"
              },
              new Country
              {
                  Name = "Congo - Brazzaville",
                  Code = "CG",
                  NameVi = null
              },
              new Country
              {
                  Name = "Switzerland",
                  Code = "CH",
                  NameVi = "Thụy Sĩ"
              },
              new Country
              {
                  Name = "Côte d’Ivoire",
                  Code = "CI",
                  NameVi = "Bờ Biển Ngà"
              },
              new Country
              {
                  Name = "Cook Islands",
                  Code = "CK",
                  NameVi = "Quần đảo Cook"
              },
              new Country
              {
                  Name = "Chile",
                  Code = "CL",
                  NameVi = null
              },
              new Country
              {
                  Name = "Cameroon",
                  Code = "CM",
                  NameVi = null
              },
              new Country
              {
                  Name = "China",
                  Code = "CN",
                  NameVi = "Trung Quốc"
              },
              new Country
              {
                  Name = "Colombia",
                  Code = "CO",
                  NameVi = null
              },
              new Country
              {
                  Name = "Clipperton Island",
                  Code = "CP",
                  NameVi = null
              },
              new Country
              {
                  Name = "Costa Rica",
                  Code = "CR",
                  NameVi = null
              },
              new Country
              {
                  Name = "Cuba",
                  Code = "CU",
                  NameVi = null
              },
              new Country
              {
                  Name = "Cape Verde",
                  Code = "CV",
                  NameVi = null
              },
              new Country
              {
                  Name = "Curaçao",
                  Code = "CW",
                  NameVi = null
              },
              new Country
              {
                  Name = "Christmas Island",
                  Code = "CX",
                  NameVi = "Đảo Giáng Sinh"
              },
              new Country
              {
                  Name = "Cyprus",
                  Code = "CY",
                  NameVi = "Síp"
              },
              new Country
              {
                  Name = "Czechia",
                  Code = "CZ",
                  NameVi = null
              },
              new Country
              {
                  Name = "Germany",
                  Code = "DE",
                  NameVi = "Đức"
              },
              new Country
              {
                  Name = "Diego Garcia",
                  Code = "DG",
                  NameVi = null
              },
              new Country
              {
                  Name = "Djibouti",
                  Code = "DJ",
                  NameVi = null
              },
              new Country
              {
                  Name = "Denmark",
                  Code = "DK",
                  NameVi = "Đan Mạch"
              },
              new Country
              {
                  Name = "Dominica",
                  Code = "DM",
                  NameVi = "Dominica"
              },
              new Country
              {
                  Name = "Dominican Republic",
                  Code = "DO",
                  NameVi = "Cộng hòa Dominica"
              },
              new Country
              {
                  Name = "Algeria",
                  Code = "DZ",
                  NameVi = null
              },
              new Country
              {
                  Name = "Ceuta & Melilla",
                  Code = "EA",
                  NameVi = null
              },
              new Country
              {
                  Name = "Ecuador",
                  Code = "EC",
                  NameVi = null
              },
              new Country
              {
                  Name = "Estonia",
                  Code = "EE",
                  NameVi = null
              },
              new Country
              {
                  Name = "Egypt",
                  Code = "EG",
                  NameVi = "Ai Cập"
              },
              new Country
              {
                  Name = "Western Sahara",
                  Code = "EH",
                  NameVi = "Tây Sahara"
              },
              new Country
              {
                  Name = "Eritrea",
                  Code = "ER",
                  NameVi = null
              },
              new Country
              {
                  Name = "Spain",
                  Code = "ES",
                  NameVi = "Tây Ban Nha"
              },
              new Country
              {
                  Name = "Ethiopia",
                  Code = "ET",
                  NameVi = null
              },
              new Country
              {
                  Name = "European Union",
                  Code = "EU",
                  NameVi = null
              },
              new Country
              {
                  Name = "Finland",
                  Code = "FI",
                  NameVi = "Phần Lan"
              },
              new Country
              {
                  Name = "Fiji",
                  Code = "FJ",
                  NameVi = null
              },
              new Country
              {
                  Name = "Falkland Islands",
                  Code = "FK",
                  NameVi = "Quần đảo Falkland"
              },
              new Country
              {
                  Name = "Micronesia",
                  Code = "FM",
                  NameVi = "Liên bang Micronesia"
              },
              new Country
              {
                  Name = "Faroe Islands",
                  Code = "FO",
                  NameVi = "Quần đảo Faroe"
              },
              new Country
              {
                  Name = "France",
                  Code = "FR",
                  NameVi = "Pháp"
              },
              new Country
              {
                  Name = "Gabon",
                  Code = "GA",
                  NameVi = null
              },
              new Country
              {
                  Name = "United Kingdom",
                  Code = "GB",
                  NameVi = "Anh"
              },
              new Country
              {
                  Name = "Grenada",
                  Code = "GD",
                  NameVi = null
              },
              new Country
              {
                  Name = "Georgia",
                  Code = "GE",
                  NameVi = "Nam Georgia và Quần đảo Nam Sandwich"
              },
              new Country
              {
                  Name = "French Guiana",
                  Code = "GF",
                  NameVi = "Guiana thuộc Pháp"
              },
              new Country
              {
                  Name = "Guernsey",
                  Code = "GG",
                  NameVi = null
              },
              new Country
              {
                  Name = "Ghana",
                  Code = "GH",
                  NameVi = null
              },
              new Country
              {
                  Name = "Gibraltar",
                  Code = "GI",
                  NameVi = null
              },
              new Country
              {
                  Name = "Greenland",
                  Code = "GL",
                  NameVi = null
              },
              new Country
              {
                  Name = "Gambia",
                  Code = "GM",
                  NameVi = null
              },
              new Country
              {
                  Name = "Guinea",
                  Code = "GN",
                  NameVi = "Guinea"
              },
              new Country
              {
                  Name = "Guadeloupe",
                  Code = "GP",
                  NameVi = null
              },
              new Country
              {
                  Name = "Equatorial Guinea",
                  Code = "GQ",
                  NameVi = "Guinea Xích Đạo"
              },
              new Country
              {
                  Name = "Greece",
                  Code = "GR",
                  NameVi = "Hy Lạp"
              },
              new Country
              {
                  Name = "South Georgia & South Sandwich Islands",
                  Code = "GS",
                  NameVi = null
              },
              new Country
              {
                  Name = "Guatemala",
                  Code = "GT",
                  NameVi = null
              },
              new Country
              {
                  Name = "Guam",
                  Code = "GU",
                  NameVi = "Đảo Guam"
              },
              new Country
              {
                  Name = "Guinea-Bissau",
                  Code = "GW",
                  NameVi = null
              },
              new Country
              {
                  Name = "Guyana",
                  Code = "GY",
                  NameVi = null
              },
              new Country
              {
                  Name = "Hong Kong SAR China",
                  Code = "HK",
                  NameVi = "Hồng Kông"
              },
              new Country
              {
                  Name = "Heard & McDonald Islands",
                  Code = "HM",
                  NameVi = null
              },
              new Country
              {
                  Name = "Honduras",
                  Code = "HN",
                  NameVi = null
              },
              new Country
              {
                  Name = "Croatia",
                  Code = "HR",
                  NameVi = null
              },
              new Country
              {
                  Name = "Haiti",
                  Code = "HT",
                  NameVi = null
              },
              new Country
              {
                  Name = "Hungary",
                  Code = "HU",
                  NameVi = null
              },
              new Country
              {
                  Name = "Canary Islands",
                  Code = "IC",
                  NameVi = null
              },
              new Country
              {
                  Name = "Indonesia",
                  Code = "ID",
                  NameVi = null
              },
              new Country
              {
                  Name = "Ireland",
                  Code = "IE",
                  NameVi = null
              },
              new Country
              {
                  Name = "Israel",
                  Code = "IL",
                  NameVi = null
              },
              new Country
              {
                  Name = "Isle of Man",
                  Code = "IM",
                  NameVi = "Đảo Man"
              },
              new Country
              {
                  Name = "India",
                  Code = "IN",
                  NameVi = "Ấn Độ"
              },
              new Country
              {
                  Name = "British Indian Ocean Territory",
                  Code = "IO",
                  NameVi = "Lãnh thổ Ấn Độ Dương thuộc Anh"
              },
              new Country
              {
                  Name = "Iraq",
                  Code = "IQ",
                  NameVi = null
              },
              new Country
              {
                  Name = "Iran",
                  Code = "IR",
                  NameVi = null
              },
              new Country
              {
                  Name = "Iceland",
                  Code = "IS",
                  NameVi = null
              },
              new Country
              {
                  Name = "Italy",
                  Code = "IT",
                  NameVi = "Ý"
              },
              new Country
              {
                  Name = "Jersey",
                  Code = "JE",
                  NameVi = null
              },
              new Country
              {
                  Name = "Jamaica",
                  Code = "JM",
                  NameVi = null
              },
              new Country
              {
                  Name = "Jordan",
                  Code = "JO",
                  NameVi = null
              },
              new Country
              {
                  Name = "Japan",
                  Code = "JP",
                  NameVi = "Nhật Bản"
              },
              new Country
              {
                  Name = "Kenya",
                  Code = "KE",
                  NameVi = null
              },
              new Country
              {
                  Name = "Kyrgyzstan",
                  Code = "KG",
                  NameVi = null
              },
              new Country
              {
                  Name = "Cambodia",
                  Code = "KH",
                  NameVi = "Campuchia"
              },
              new Country
              {
                  Name = "Kiribati",
                  Code = "KI",
                  NameVi = null
              },
              new Country
              {
                  Name = "Comoros",
                  Code = "KM",
                  NameVi = null
              },
              new Country
              {
                  Name = "St. Kitts & Nevis",
                  Code = "KN",
                  NameVi = null
              },
              new Country
              {
                  Name = "North Korea",
                  Code = "KP",
                  NameVi = "Triều tiên"
              },
              new Country
              {
                  Name = "South Korea",
                  Code = "KR",
                  NameVi = "Hàn Quốc"
              },
              new Country
              {
                  Name = "Kuwait",
                  Code = "KW",
                  NameVi = null
              },
              new Country
              {
                  Name = "Cayman Islands",
                  Code = "KY",
                  NameVi = "Quần đảo Cayman"
              },
              new Country
              {
                  Name = "Kazakhstan",
                  Code = "KZ",
                  NameVi = null
              },
              new Country
              {
                  Name = "Laos",
                  Code = "LA",
                  NameVi = "Lào"
              },
              new Country
              {
                  Name = "Lebanon",
                  Code = "LB",
                  NameVi = null
              },
              new Country
              {
                  Name = "St. Lucia",
                  Code = "LC",
                  NameVi = null
              },
              new Country
              {
                  Name = "Liechtenstein",
                  Code = "LI",
                  NameVi = null
              },
              new Country
              {
                  Name = "Sri Lanka",
                  Code = "LK",
                  NameVi = null
              },
              new Country
              {
                  Name = "Liberia",
                  Code = "LR",
                  NameVi = null
              },
              new Country
              {
                  Name = "Lesotho",
                  Code = "LS",
                  NameVi = null
              },
              new Country
              {
                  Name = "Lithuania",
                  Code = "LT",
                  NameVi = null
              },
              new Country
              {
                  Name = "Luxembourg",
                  Code = "LU",
                  NameVi = null
              },
              new Country
              {
                  Name = "Latvia",
                  Code = "LV",
                  NameVi = null
              },
              new Country
              {
                  Name = "Libya",
                  Code = "LY",
                  NameVi = null
              },
              new Country
              {
                  Name = "Morocco",
                  Code = "MA",
                  NameVi = "Ma Rốc (Maroc)"
              },
              new Country
              {
                  Name = "Monaco",
                  Code = "MC",
                  NameVi = null
              },
              new Country
              {
                  Name = "Moldova",
                  Code = "MD",
                  NameVi = null
              },
              new Country
              {
                  Name = "Montenegro",
                  Code = "ME",
                  NameVi = null
              },
              new Country
              {
                  Name = "St. Martin",
                  Code = "MF",
                  NameVi = null
              },
              new Country
              {
                  Name = "Madagascar",
                  Code = "MG",
                  NameVi = null
              },
              new Country
              {
                  Name = "Marshall Islands",
                  Code = "MH",
                  NameVi = "Quần đảo Marshall"
              },
              new Country
              {
                  Name = "North Macedonia",
                  Code = "MK",
                  NameVi = null
              },
              new Country
              {
                  Name = "Mali",
                  Code = "ML",
                  NameVi = null
              },
              new Country
              {
                  Name = "Myanmar (Burma)",
                  Code = "MM",
                  NameVi = null
              },
              new Country
              {
                  Name = "Mongolia",
                  Code = "MN",
                  NameVi = "Mông Cổ"
              },
              new Country
              {
                  Name = "Macao SAR China",
                  Code = "MO",
                  NameVi = "Ma cao"
              },
              new Country
              {
                  Name = "Northern Mariana Islands",
                  Code = "MP",
                  NameVi = "Quần đảo Bắc Mariana"
              },
              new Country
              {
                  Name = "Martinique",
                  Code = "MQ",
                  NameVi = null
              },
              new Country
              {
                  Name = "Mauritania",
                  Code = "MR",
                  NameVi = null
              },
              new Country
              {
                  Name = "Montserrat",
                  Code = "MS",
                  NameVi = null
              },
              new Country
              {
                  Name = "Malta",
                  Code = "MT",
                  NameVi = null
              },
              new Country
              {
                  Name = "Mauritius",
                  Code = "MU",
                  NameVi = null
              },
              new Country
              {
                  Name = "Maldives",
                  Code = "MV",
                  NameVi = null
              },
              new Country
              {
                  Name = "Malawi",
                  Code = "MW",
                  NameVi = null
              },
              new Country
              {
                  Name = "Mexico",
                  Code = "MX",
                  NameVi = null
              },
              new Country
              {
                  Name = "Malaysia",
                  Code = "MY",
                  NameVi = null
              },
              new Country
              {
                  Name = "Mozambique",
                  Code = "MZ",
                  NameVi = null
              },
              new Country
              {
                  Name = "Namibia",
                  Code = "NA",
                  NameVi = null
              },
              new Country
              {
                  Name = "New Caledonia",
                  Code = "NC",
                  NameVi = null
              },
              new Country
              {
                  Name = "Niger",
                  Code = "NE",
                  NameVi = null
              },
              new Country
              {
                  Name = "Norfolk Island",
                  Code = "NF",
                  NameVi = "Đảo Norfolk"
              },
              new Country
              {
                  Name = "Nigeria",
                  Code = "NG",
                  NameVi = null
              },
              new Country
              {
                  Name = "Nicaragua",
                  Code = "NI",
                  NameVi = null
              },
              new Country
              {
                  Name = "Netherlands",
                  Code = "NL",
                  NameVi = "Hà Lan"
              },
              new Country
              {
                  Name = "Norway",
                  Code = "NO",
                  NameVi = "Na Uy"
              },
              new Country
              {
                  Name = "Nepal",
                  Code = "NP",
                  NameVi = null
              },
              new Country
              {
                  Name = "Nauru",
                  Code = "NR",
                  NameVi = null
              },
              new Country
              {
                  Name = "Niue",
                  Code = "NU",
                  NameVi = null
              },
              new Country
              {
                  Name = "New Zealand",
                  Code = "NZ",
                  NameVi = null
              },
              new Country
              {
                  Name = "Oman",
                  Code = "OM",
                  NameVi = null
              },
              new Country
              {
                  Name = "Panama",
                  Code = "PA",
                  NameVi = null
              },
              new Country
              {
                  Name = "Peru",
                  Code = "PE",
                  NameVi = null
              },
              new Country
              {
                  Name = "French Polynesia",
                  Code = "PF",
                  NameVi = "Polynesia thuộc Pháp"
              },
              new Country
              {
                  Name = "Papua New Guinea",
                  Code = "PG",
                  NameVi = null
              },
              new Country
              {
                  Name = "Philippines",
                  Code = "PH",
                  NameVi = null
              },
              new Country
              {
                  Name = "Pakistan",
                  Code = "PK",
                  NameVi = null
              },
              new Country
              {
                  Name = "Poland",
                  Code = "PL",
                  NameVi = "Ba Lan"
              },
              new Country
              {
                  Name = "St. Pierre & Miquelon",
                  Code = "PM",
                  NameVi = null
              },
              new Country
              {
                  Name = "Pitcairn Islands",
                  Code = "PN",
                  NameVi = null
              },
              new Country
              {
                  Name = "Puerto Rico",
                  Code = "PR",
                  NameVi = null
              },
              new Country
              {
                  Name = "Palestinian Territories",
                  Code = "PS",
                  NameVi = null
              },
              new Country
              {
                  Name = "Portugal",
                  Code = "PT",
                  NameVi = "Bồ Đào Nha"
              },
              new Country
              {
                  Name = "Palau",
                  Code = "PW",
                  NameVi = null
              },
              new Country
              {
                  Name = "Paraguay",
                  Code = "PY",
                  NameVi = null
              },
              new Country
              {
                  Name = "Qatar",
                  Code = "QA",
                  NameVi = null
              },
              new Country
              {
                  Name = "Réunion",
                  Code = "RE",
                  NameVi = null
              },
              new Country
              {
                  Name = "Romania",
                  Code = "RO",
                  NameVi = null
              },
              new Country
              {
                  Name = "Serbia",
                  Code = "RS",
                  NameVi = null
              },
              new Country
              {
                  Name = "Russia",
                  Code = "RU",
                  NameVi = "Nga"
              },
              new Country
              {
                  Name = "Rwanda",
                  Code = "RW",
                  NameVi = null
              },
              new Country
              {
                  Name = "Saudi Arabia",
                  Code = "SA",
                  NameVi = "Ả Rập Xê Út"
              },
              new Country
              {
                  Name = "Solomon Islands",
                  Code = "SB",
                  NameVi = "Quần đảo Solomon"
              },
              new Country
              {
                  Name = "Seychelles",
                  Code = "SC",
                  NameVi = null
              },
              new Country
              {
                  Name = "Sudan",
                  Code = "SD",
                  NameVi = "Nam Sudan"
              },
              new Country
              {
                  Name = "Sweden",
                  Code = "SE",
                  NameVi = "Thụy Điển"
              },
              new Country
              {
                  Name = "Singapore",
                  Code = "SG",
                  NameVi = null
              },
              new Country
              {
                  Name = "St. Helena",
                  Code = "SH",
                  NameVi = null
              },
              new Country
              {
                  Name = "Slovenia",
                  Code = "SI",
                  NameVi = null
              },
              new Country
              {
                  Name = "Svalbard & Jan Mayen",
                  Code = "SJ",
                  NameVi = null
              },
              new Country
              {
                  Name = "Slovakia",
                  Code = "SK",
                  NameVi = null
              },
              new Country
              {
                  Name = "Sierra Leone",
                  Code = "SL",
                  NameVi = null
              },
              new Country
              {
                  Name = "San Marino",
                  Code = "SM",
                  NameVi = null
              },
              new Country
              {
                  Name = "Senegal",
                  Code = "SN",
                  NameVi = null
              },
              new Country
              {
                  Name = "Somalia",
                  Code = "SO",
                  NameVi = null
              },
              new Country
              {
                  Name = "Suriname",
                  Code = "SR",
                  NameVi = null
              },
              new Country
              {
                  Name = "South Sudan",
                  Code = "SS",
                  NameVi = "Nam Sudan"
              },
              new Country
              {
                  Name = "São Tomé & Príncipe",
                  Code = "ST",
                  NameVi = null
              },
              new Country
              {
                  Name = "El Salvador",
                  Code = "SV",
                  NameVi = null
              },
              new Country
              {
                  Name = "Sint Maarten",
                  Code = "SX",
                  NameVi = null
              },
              new Country
              {
                  Name = "Syria",
                  Code = "SY",
                  NameVi = null
              },
              new Country
              {
                  Name = "Eswatini",
                  Code = "SZ",
                  NameVi = null
              },
              new Country
              {
                  Name = "Tristan da Cunha",
                  Code = "TA",
                  NameVi = null
              },
              new Country
              {
                  Name = "Turks & Caicos Islands",
                  Code = "TC",
                  NameVi = null
              },
              new Country
              {
                  Name = "Chad",
                  Code = "TD",
                  NameVi = "Sát (Tchad)"
              },
              new Country
              {
                  Name = "French Southern Territories",
                  Code = "TF",
                  NameVi = "Vùng đất phía Nam và châu Nam Cực thuộc Pháp"
              },
              new Country
              {
                  Name = "Togo",
                  Code = "TG",
                  NameVi = null
              },
              new Country
              {
                  Name = "Thailand",
                  Code = "TH",
                  NameVi = "Thái Lan"
              },
              new Country
              {
                  Name = "Tajikistan",
                  Code = "TJ",
                  NameVi = null
              },
              new Country
              {
                  Name = "Tokelau",
                  Code = "TK",
                  NameVi = null
              },
              new Country
              {
                  Name = "Timor-Leste",
                  Code = "TL",
                  NameVi = "Đông Timor"
              },
              new Country
              {
                  Name = "Turkmenistan",
                  Code = "TM",
                  NameVi = null
              },
              new Country
              {
                  Name = "Tunisia",
                  Code = "TN",
                  NameVi = null
              },
              new Country
              {
                  Name = "Tonga",
                  Code = "TO",
                  NameVi = null
              },
              new Country
              {
                  Name = "Turkey",
                  Code = "TR",
                  NameVi = "Thổ Nhĩ Kỳ"
              },
              new Country
              {
                  Name = "Trinidad & Tobago",
                  Code = "TT",
                  NameVi = null
              },
              new Country
              {
                  Name = "Tuvalu",
                  Code = "TV",
                  NameVi = null
              },
              new Country
              {
                  Name = "Taiwan",
                  Code = "TW",
                  NameVi = "Đài Loan"
              },
              new Country
              {
                  Name = "Tanzania",
                  Code = "TZ",
                  NameVi = null
              },
              new Country
              {
                  Name = "Ukraine",
                  Code = "UA",
                  NameVi = null
              },
              new Country
              {
                  Name = "Uganda",
                  Code = "UG",
                  NameVi = null
              },
              new Country
              {
                  Name = "U.S. Outlying Islands",
                  Code = "UM",
                  NameVi = null
              },
              new Country
              {
                  Name = "United Nations",
                  Code = "UN",
                  NameVi = null
              },
              new Country
              {
                  Name = "United States",
                  Code = "US",
                  NameVi = "Mỹ"
              },
              new Country
              {
                  Name = "Uruguay",
                  Code = "UY",
                  NameVi = null
              },
              new Country
              {
                  Name = "Uzbekistan",
                  Code = "UZ",
                  NameVi = null
              },
              new Country
              {
                  Name = "Vatican City",
                  Code = "VA",
                  NameVi = "Tòa thánh Vatican"
              },
              new Country
              {
                  Name = "St. Vincent & Grenadines",
                  Code = "VC",
                  NameVi = null
              },
              new Country
              {
                  Name = "Venezuela",
                  Code = "VE",
                  NameVi = null
              },
              new Country
              {
                  Name = "British Virgin Islands",
                  Code = "VG",
                  NameVi = "Quần đảo Virgin thuộc Anh"
              },
              new Country
              {
                  Name = "U.S. Virgin Islands",
                  Code = "VI",
                  NameVi = null
              },
              new Country
              {
                  Name = "Vietnam",
                  Code = "VN",
                  NameVi = "Việt Nam"
              },
              new Country
              {
                  Name = "Vanuatu",
                  Code = "VU",
                  NameVi = null
              },
              new Country
              {
                  Name = "Wallis & Futuna",
                  Code = "WF",
                  NameVi = null
              },
              new Country
              {
                  Name = "Samoa",
                  Code = "WS",
                  NameVi = null
              },
              new Country
              {
                  Name = "Kosovo",
                  Code = "XK",
                  NameVi = null
              },
              new Country
              {
                  Name = "Yemen",
                  Code = "YE",
                  NameVi = null
              },
              new Country
              {
                  Name = "Mayotte",
                  Code = "YT",
                  NameVi = null
              },
              new Country
              {
                  Name = "South Africa",
                  Code = "ZA",
                  NameVi = "Nam Phi"
              },
              new Country
              {
                  Name = "Zambia",
                  Code = "ZM",
                  NameVi = null
              },
              new Country
              {
                  Name = "Zimbabwe",
                  Code = "ZW",
                  NameVi = null
              },
              new Country
              {
                  Name = "England",
                  Code = "ENGLAND",
                  NameVi = null
              },
              new Country
              {
                  Name = "Scotland",
                  Code = "SCOTLAND",
                  NameVi = null
              },
              new Country
              {
                  Name = "Wales",
                  Code = "WALES",
                  NameVi = null
              }
            );
        }
    }
}