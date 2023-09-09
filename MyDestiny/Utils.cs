using System.Text;

namespace MyDestiny;

public static class Utils
{
    private static readonly List<int> TapSoNguyen = new()
    {
        1, 2, 3, 4, 5, 6, 7, 8, 9
    };

    private static readonly List<int> TapBasic = new()
    {
        1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 22, 33
    };

    public static bool KiemTraTapBasic(int source) => TapBasic.Contains(source);

    public static List<int> KhongTrongTapSoNguyen(IEnumerable<int> source) => TapSoNguyen.Except(source).ToList();

    private static readonly IReadOnlyDictionary<int, List<string>> NumberToCharacterMapping = new Dictionary<int, List<string>>
    {
        { 1, new List<string> { "A", "J", "S" } },
        { 2, new List<string> { "B", "K", "T" } },
        { 3, new List<string> { "C", "L", "U" } },
        { 4, new List<string> { "D", "M", "V" } },
        { 5, new List<string> { "E", "N", "W" } },
        { 6, new List<string> { "F", "O", "X" } },
        { 7, new List<string> { "G", "P", "Y" } },
        { 8, new List<string> { "H", "Q", "Z" } },
        { 9, new List<string> { "I", "R" } }
    };

    public static int TongCacChuSo(int source)
    {
        var tong = 0;
        while (source > 0)
        {
            tong += source % 10;
            source /= 10;
        }

        return tong;
    }


    public static int TinhTong(int tong, bool kiemTraTapBasic = false)
    {
        while (tong >= 10 && (!kiemTraTapBasic || !KiemTraTapBasic(tong)))
        {
            tong = TongCacChuSo(tong);
        }

        return tong;
    }


    public static string ChuyenSangAscii(string source)
    {
        var sb = new StringBuilder();
        foreach (var asciiCharacter in source.Select(character => character.ToString().Normalize(NormalizationForm.FormD)[0]))
        {
            sb.Append(asciiCharacter);
        }

        return sb.ToString();
    }

    public static int ChuyenChuCaiThanhSo(string chuCai)
    {
        var result = NumberToCharacterMapping
            .First(x => x.Value.Contains(chuCai.ToUpper()))
            .Key;

        return result;
    }


    private static readonly IReadOnlyList<string> DanhSachNguyenAm = new List<string>
    {
        "U", "E", "O", "A", "I"
    };

    private static bool KiemTraNguyenAm(string chuCai, bool isChuCaiTruocDoLaNguyenAm)
    {
        // Check if the character is a vowel (found in DanhSachNguyenAm)
        // or if it's 'Y' and the previous character was not a vowel.
        return DanhSachNguyenAm.Contains(chuCai) || (chuCai == "Y" && !isChuCaiTruocDoLaNguyenAm);
    }

    private static bool KiemTraPhuAm(string chuCai, bool isChuCaiTruocDoLaNguyenAm)
    {
        return !KiemTraNguyenAm(chuCai, isChuCaiTruocDoLaNguyenAm);
    }

    public static IEnumerable<string> LocCacChuNguyenAm(string chu)
    {
        var cacChuNguyenAm = new List<string>();
        var isChuCaiTruocDoLaNguyenAm = false;

        foreach (var chuCai in chu.ToUpper().Select(c => c.ToString()))
        {
            if (KiemTraNguyenAm(chuCai, isChuCaiTruocDoLaNguyenAm))
            {
                cacChuNguyenAm.Add(chuCai);
                isChuCaiTruocDoLaNguyenAm = true;
            }
            else
            {
                isChuCaiTruocDoLaNguyenAm = false;
            }
        }

        return cacChuNguyenAm;
    }

    public static IEnumerable<string> LocCacChuPhuAm(string chu)
    {
        var cacChuPhuAm = new List<string>();
        var isChuCaiTruocDoLaNguyenAm = false;

        foreach (var chuCai in chu.ToUpper().Select(c => c.ToString()))
        {
            if (KiemTraPhuAm(chuCai, isChuCaiTruocDoLaNguyenAm))
            {
                cacChuPhuAm.Add(chuCai);
                isChuCaiTruocDoLaNguyenAm = false;
            }
            else
            {
                isChuCaiTruocDoLaNguyenAm = true;
            }
        }

        return cacChuPhuAm;
    }
}