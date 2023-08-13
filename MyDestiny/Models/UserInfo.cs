namespace MyDestiny.Models;

public record UserInfo
{
    public string HoVaTen { get; init; }

    public DateOnly NgayThangNamSinh { get; init; }

    public string HoVaTenAscii => Utils.ConvertToAscii(HoVaTen);

    public int TongCacSoCuaNgaySinh => Utils.KiemTraTapBasic(NgayThangNamSinh.Day) ? NgayThangNamSinh.Day : Utils.TongCacChuSo(NgayThangNamSinh.Day);

    public int TongCacSoCuaThangSinh => Utils.TongCacChuSo(NgayThangNamSinh.Month);

    public int TongCacSoCuaNamSinh => Utils.TongCacChuSo(NgayThangNamSinh.Year);
}