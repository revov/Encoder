using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCryptography
{
    public class PowerEncoding
    {
        public Encoding Converter { get; set; }

        #region OldStyleCharacterMap
        /*
        public static readonly char[] db1 =
                    {
                        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',

                        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
                        'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
                        's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I',
                        'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
                        'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',

                        'а', 'б', 'в', 'г', 'д', 'е', 'ж', 'з', 'и', 'й',
                        'к','л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у',
                        'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ь', 'ю', 'я',
                        'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ж', 'З', 'И', 'Й',
                        'К','Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У',
                        'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ь', 'Ю', 'Я',

                        '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*',
                        '+',',', '-', '.', '/', ':', ';', '<', '=', '>',
                        '?', '{', '|', '}', '~', '^', '_', '`', '@', '№',
                        '€', '§', ' '
                    };

        public static readonly string[] db2 = 
                    {
                        "0990", "0991", "0992", "0993", "0994", "0995", "0996", "0997", 
                        "0998", "0999", "1000", "1001", "1002", "1003", "1004", 
                        "1005", "1006", "1007", "1008", "1009", "1010", "1011", 
                        "1012", "1013", "1014", "1015", "1016", "1017", "1018", 
                        "1019", "1020", "1021", "1022", "1023", "1024", "1025", 
                        "1026", "1027", "1028", "1029", "1030", "1031", "1032", 
                        "1033", "1034", "1035", "1036", "1037", "1038", "1039", 
                        "1040", "1041", "1042", "1043", "1044", "1045", "1046", 
                        "1047", "1048", "1049", "1050", "1051", "1052", "1053", 
                        "1054", "1055", "1056", "1057", "1058", "1059", "1060", 
                        "1061", "1062", "1063", "1064", "1065", "1066", "1067", 
                        "1068", "1069", "1070", "1071", "1072", "1073", "1074", 
                        "1075", "1076", "1077", "1078", "1079", "1080", "1081", 
                        "1082", "1083", "1084", "1085", "1086", "1087", "1088", 
                        "1089", "1090", "1091", "1092", "1093", "1094", "1095", 
                        "1096", "1097", "1098", "1099", "1100", "1101", "1102", 
                        "1103", "1104", "1105", "1106", "1107", "1108", "1109", 
                        "1110", "1111", "1112", "1113", "1114", "1115", "1116", 
                        "1117", "1118", "1119", "1120", "1121", "1122", "1123", 
                        "1124", "1125", "1126", "1127", "1128", "1129", "1130", 
                        "1131", "1132", "1133", "1134", "1135", "1136", "1137", 
                        "1138", "1139", "1140", "1141", "1142", "1143", "1144"
                    };
        */
        #endregion

        public static readonly Dictionary<char, string> dictionary;

        static PowerEncoding()
        {
            dictionary = new Dictionary<char, string>();
            dictionary.Add('0', "0990");
            dictionary.Add('1', "0991");
            dictionary.Add('2', "0992");
            dictionary.Add('3', "0993");
            dictionary.Add('4', "0994");
            dictionary.Add('5', "0995");
            dictionary.Add('6', "0996");
            dictionary.Add('7', "0997");
            dictionary.Add('8', "0998");
            dictionary.Add('9', "0999");
            dictionary.Add('a', "1000");
            dictionary.Add('b', "1001");
            dictionary.Add('c', "1002");
            dictionary.Add('d', "1003");
            dictionary.Add('e', "1004");
            dictionary.Add('f', "1005");
            dictionary.Add('g', "1006");
            dictionary.Add('h', "1007");
            dictionary.Add('i', "1008");
            dictionary.Add('j', "1009");
            dictionary.Add('k', "1010");
            dictionary.Add('l', "1011");
            dictionary.Add('m', "1012");
            dictionary.Add('n', "1013");
            dictionary.Add('o', "1014");
            dictionary.Add('p', "1015");
            dictionary.Add('q', "1016");
            dictionary.Add('r', "1017");
            dictionary.Add('s', "1018");
            dictionary.Add('t', "1019");
            dictionary.Add('u', "1020");
            dictionary.Add('v', "1021");
            dictionary.Add('w', "1022");
            dictionary.Add('x', "1023");
            dictionary.Add('y', "1024");
            dictionary.Add('z', "1025");
            dictionary.Add('A', "1026");
            dictionary.Add('B', "1027");
            dictionary.Add('C', "1028");
            dictionary.Add('D', "1029");
            dictionary.Add('E', "1030");
            dictionary.Add('F', "1031");
            dictionary.Add('G', "1032");
            dictionary.Add('H', "1033");
            dictionary.Add('I', "1034");
            dictionary.Add('J', "1035");
            dictionary.Add('K', "1036");
            dictionary.Add('L', "1037");
            dictionary.Add('M', "1038");
            dictionary.Add('N', "1039");
            dictionary.Add('O', "1040");
            dictionary.Add('P', "1041");
            dictionary.Add('Q', "1042");
            dictionary.Add('R', "1043");
            dictionary.Add('S', "1044");
            dictionary.Add('T', "1045");
            dictionary.Add('U', "1046");
            dictionary.Add('V', "1047");
            dictionary.Add('W', "1048");
            dictionary.Add('X', "1049");
            dictionary.Add('Y', "1050");
            dictionary.Add('Z', "1051");
            dictionary.Add('а', "1052");
            dictionary.Add('б', "1053");
            dictionary.Add('в', "1054");
            dictionary.Add('г', "1055");
            dictionary.Add('д', "1056");
            dictionary.Add('е', "1057");
            dictionary.Add('ж', "1058");
            dictionary.Add('з', "1059");
            dictionary.Add('и', "1060");
            dictionary.Add('й', "1061");
            dictionary.Add('к', "1062");
            dictionary.Add('л', "1063");
            dictionary.Add('м', "1064");
            dictionary.Add('н', "1065");
            dictionary.Add('о', "1066");
            dictionary.Add('п', "1067");
            dictionary.Add('р', "1068");
            dictionary.Add('с', "1069");
            dictionary.Add('т', "1070");
            dictionary.Add('у', "1071");
            dictionary.Add('ф', "1072");
            dictionary.Add('х', "1073");
            dictionary.Add('ц', "1074");
            dictionary.Add('ч', "1075");
            dictionary.Add('ш', "1076");
            dictionary.Add('щ', "1077");
            dictionary.Add('ъ', "1078");
            dictionary.Add('ь', "1079");
            dictionary.Add('ю', "1080");
            dictionary.Add('я', "1081");
            dictionary.Add('А', "1082");
            dictionary.Add('Б', "1083");
            dictionary.Add('В', "1084");
            dictionary.Add('Г', "1085");
            dictionary.Add('Д', "1086");
            dictionary.Add('Е', "1087");
            dictionary.Add('Ж', "1088");
            dictionary.Add('З', "1089");
            dictionary.Add('И', "1090");
            dictionary.Add('Й', "1091");
            dictionary.Add('К', "1092");
            dictionary.Add('Л', "1093");
            dictionary.Add('М', "1094");
            dictionary.Add('Н', "1095");
            dictionary.Add('О', "1096");
            dictionary.Add('П', "1097");
            dictionary.Add('Р', "1098");
            dictionary.Add('С', "1099");
            dictionary.Add('Т', "1100");
            dictionary.Add('У', "1101");
            dictionary.Add('Ф', "1102");
            dictionary.Add('Х', "1103");
            dictionary.Add('Ц', "1104");
            dictionary.Add('Ч', "1105");
            dictionary.Add('Ш', "1106");
            dictionary.Add('Щ', "1107");
            dictionary.Add('Ъ', "1108");
            dictionary.Add('Ь', "1109");
            dictionary.Add('Ю', "1110");
            dictionary.Add('Я', "1111");
            dictionary.Add('!', "1112");
            dictionary.Add('"', "1113");
            dictionary.Add('#', "1114");
            dictionary.Add('$', "1115");
            dictionary.Add('%', "1116");
            dictionary.Add('&', "1117");
            dictionary.Add('\'', "1118");
            dictionary.Add('(', "1119");
            dictionary.Add(')', "1120");
            dictionary.Add('*', "1121");
            dictionary.Add('+', "1122");
            dictionary.Add(',', "1123");
            dictionary.Add('-', "1124");
            dictionary.Add('.', "1125");
            dictionary.Add('/', "1126");
            dictionary.Add(':', "1127");
            dictionary.Add(';', "1128");
            dictionary.Add('<', "1129");
            dictionary.Add('=', "1130");
            dictionary.Add('>', "1131");
            dictionary.Add('?', "1132");
            dictionary.Add('{', "1133");
            dictionary.Add('|', "1134");
            dictionary.Add('}', "1135");
            dictionary.Add('~', "1136");
            dictionary.Add('^', "1137");
            dictionary.Add('_', "1138");
            dictionary.Add('`', "1139");
            dictionary.Add('@', "1140");
            dictionary.Add('№', "1141");
            dictionary.Add('€', "1142");
            dictionary.Add('§', "1143");
            dictionary.Add(' ', "1144");
        }

        public PowerEncoding()
        {
            Converter = new UTF8Encoding();
        }

        public string GetString(byte[] x)
        {
            return Converter.GetString(x);
        }

        public byte[] GetBytes(string x)
        {
            return Converter.GetBytes(x);
        }

        public string Encode(string x)
        {
            x = x.Trim();
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < x.Length; i++)
            {
                if (dictionary.ContainsKey(x[i]))
                {
                    res.Append(dictionary[x[i]]);
                }
            }
            return res.ToString();
        }

        public string Decode(string x)
        {
            StringBuilder res = new StringBuilder();

            if (x.Length % 4 == 0)
            {
                for (int i = 0; i < x.Length; i += 4)
                {
                    string code = x.Substring(i, 4);
                    try
                    {
                        res.Append(dictionary.First(elem => elem.Value == code).Key);
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("The specified string contains invalid encoding symbols.");
                    }
                }
                return res.ToString();
            }
            else
            {
                throw new ArgumentException("The specified string is not in a valid format.");
            }
        }

    }
}
