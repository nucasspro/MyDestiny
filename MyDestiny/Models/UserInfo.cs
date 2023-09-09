namespace MyDestiny.Models;

public record UserInfo
{
    public string HoVaTen { get; set; }

    public DateTime NgayThangNamSinh { get; set; }

    public string HoVaTenAscii { get; init; }

    public List<string> DanhSachCacChuTrongTenAscii { get; init; }

    public int TongCacSoCuaNgaySinh { get; init; }

    public int TongCacSoCuaThangSinh { get; init; }

    public int TongCacSoCuaNamSinh { get; init; }

    public UserDetails UserDetails { get; set; } = new();

    public UserInfo(string hoVaTen, DateTime ngayThangNamSinh)
    {
        HoVaTen = hoVaTen;
        NgayThangNamSinh = ngayThangNamSinh;

        HoVaTenAscii = Utils.ChuyenSangAscii(HoVaTen);
        DanhSachCacChuTrongTenAscii = HoVaTenAscii.Split(" ").ToList();
        TongCacSoCuaNgaySinh = Utils.TongCacChuSo(NgayThangNamSinh.Day);
        TongCacSoCuaThangSinh = Utils.TongCacChuSo(NgayThangNamSinh.Month);
        TongCacSoCuaNamSinh = Utils.TongCacChuSo(NgayThangNamSinh.Year);
    }
}

public record UserDetails
{
    public int? ChiSoDuongDoi { get; set; }
    public int? ChiSoSuMenh { get; set; }
    public int? ChiSoLienKetDuongDoiVaSuMenh { get; set; }
    public int? ChiSoTruongThanh { get; set; }

    public int? ChiSoLinhHon { get; set; }
    public int? ChiSoNhanCach { get; set; }
    public int? ChiSoLienKet { get; set; }
    public int? ChiSoCanBang { get; set; }

    public int? ChiSoTuDuyLyTri { get; set; }
    public int? ChiSoSucManhTiemThuc { get; set; }
    public string? ChiSoDamMe { get; set; }
    public string? ChiSoThieu { get; set; }

    public int? ChiSoNamCaNhan { get; set; }
    public int? ChiSoThangCaNhan { get; set; }
    public int? ChiSoNgayCaNhan { get; set; }

    public int? ChiSoChang1 { get; set; }
    public int? ChiSoChang2 { get; set; }
    public int? ChiSoChang3 { get; set; }
    public int? ChiSoChang4 { get; set; }

    public int? ChiSoTuoiChang1 { get; set; }
    public int? ChiSoTuoiChang2 { get; set; }
    public int? ChiSoTuoiChang3 { get; set; }
    public int? ChiSoTuoiChang4 { get; set; }

    public int? ChiSoNamChang1 { get; set; }
    public int? ChiSoNamChang2 { get; set; }
    public int? ChiSoNamChang3 { get; set; }
    public int? ChiSoNamChang4 { get; set; }

    public int? ChiSoThachThuc1 { get; set; }
    public int? ChiSoThachThuc2 { get; set; }
    public int? ChiSoThachThuc3 { get; set; }
    public int? ChiSoThachThuc4 { get; set; }

    public int? ChiSoNgaySinh { get; set; }
}