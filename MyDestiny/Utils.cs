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

    private static readonly IReadOnlyList<string> DanhSachNguyenAm = new List<string>
    {
        "U", "E", "O", "A", "I", "Y"
    };

    public static bool KiemTraTapBasic(int source) => TapBasic.Contains(source);

    public static List<int> KhongTrongTapSoNguyen(IReadOnlyCollection<int> source) => TapSoNguyen.Except(source).ToList();

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


    public static string ChuyenSangAscii(string source)
    {
        var sb = new StringBuilder();
        foreach (var asciiCharacter in source.Select(character => character.ToString().Normalize(NormalizationForm.FormD)[0]))
        {
            sb.Append(asciiCharacter);
        }

        return sb.ToString();
    }

    public static int ChuyenChuCaiThanhSo(string character)
    {
        var result = NumberToCharacterMapping
            .First(x => x.Value.Contains(character.ToUpper()))
            .Key;

        return result;
    }
}