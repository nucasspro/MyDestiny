using MyDestiny.Models;

namespace MyDestiny.Extensions;

// TODO: longdang - Add logic to calculate here

public static class UserExtension
{
    /// <summary>
    /// ĐƯƠNG ĐỜI: TỔNG NGÀY THÁNG NĂM SINH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhDuongDoi(this UserInfo userInfo)
    {
        var ketQua = userInfo.TongCacSoCuaNgaySinh
                     + userInfo.TongCacSoCuaThangSinh
                     + userInfo.TongCacSoCuaNamSinh;

        while (ketQua >= 10)
            ketQua = Utils.TongCacChuSo(ketQua);

        return ketQua;
    }

    /// <summary>
    /// SỨ MỆNH: TỔNG HỌ VÀ TÊN
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhChiSoSuMenh(this UserInfo userInfo)
    {
        var danhSachCacChuAscii = userInfo.HoVaTenAscii.Split(" ").ToList();

        var tongDiemCuaChu = new List<(string chu, int tong)>();
        foreach (var chu in danhSachCacChuAscii)
        {
            var tong = chu.Sum(x => Utils.ChuyenChuCaiThanhSo(x.ToString()));

            while (tong >= 10 && !Utils.KiemTraTapBasic(tong))
                tong = Utils.TongCacChuSo(tong);

            tongDiemCuaChu.Add((chu, tong));
        }

        var ketQua = tongDiemCuaChu.Sum(item => item.tong);
        while (ketQua >= 10 && !Utils.KiemTraTapBasic(ketQua))
            ketQua = Utils.TongCacChuSo(ketQua);

        return ketQua;
    }

    /// <summary>
    /// LIÊN KẾT: HIỆU ĐƯỜNG ĐỜI VÀ SỨ MỆNH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="duongDoi"></param>
    /// <param name="suMenh"></param>
    /// <returns></returns>
    public static int TinhChiSoLienKetDuongDoiVaSuMenh(this UserInfo userInfo, int duongDoi, int suMenh)
    {
        return Math.Abs(suMenh - duongDoi);
    }

    /// <summary>
    /// TRƯỞNG THÀNH: TỔNG ĐƯỜNG ĐỜI VÀ SỨ MỆNH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="duongDoi"></param>
    /// <param name="suMenh"></param>
    /// <returns></returns>
    public static int TinhChiSoTruongThanh(this UserInfo userInfo, int duongDoi, int suMenh)
    {
        return Math.Abs(suMenh + duongDoi);
    }
    
    // 4. LINH HỒN: TỔNG NGUYÊN ÂM
    // 5. NHÂN CÁCH: TỔNG PHỤ ÂM
    
    
    /// <summary>
    /// LIÊN KẾT: HIỆU LINH HỒN VÀ NHÂN CÁCH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="linhHon"></param>
    /// <param name="nhanCach"></param>
    /// <returns></returns>
    public static int TinhChiSoLienKet(this UserInfo userInfo, int linhHon, int nhanCach)
    {
        return Math.Abs(linhHon - nhanCach);
    }

    /// <summary>
    /// CÂN BẰNG: TỔNG CÁC CHỮ CÁI ĐẦU TIÊN CỦA HỌ VÀ TÊN
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhChiSoCanBang(this UserInfo userInfo)
    {
        var danhSachChuCaiDau = userInfo.HoVaTenAscii
            .Split(" ")
            .Select(x => x[0].ToString())
            .ToList();

        var tong = danhSachChuCaiDau.Sum(Utils.ChuyenChuCaiThanhSo);
        while (tong >= 10 && !Utils.KiemTraTapBasic(tong))
            tong = Utils.TongCacChuSo(tong);

        return tong;
    }

    /// <summary>
    /// TƯ DUY LÝ TRÍ: TÊN + NGÀY SINH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhChiSoTuDuyLyTri(this UserInfo userInfo)
    {
        var ten = userInfo.HoVaTenAscii.Split(" ").Last();
        var tongTen = ten.Sum(x => Utils.ChuyenChuCaiThanhSo(x.ToString()));

        var tong = tongTen + userInfo.TongCacSoCuaNgaySinh;

        while (tong >= 10 && !Utils.KiemTraTapBasic(tong))
            tong = Utils.TongCacChuSo(tong);

        return tong;
    }
    
    /// <summary>
    /// SỨC MẠNH TIỀM THỨC: 9- SỐ LƯỢNG SỐ THIẾU
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhChiSoSucManhTiemThuc(this UserInfo userInfo)
    {
        var numberFromCharacter = new HashSet<int>();
        
        var danhSachCacChuAscii = userInfo.HoVaTenAscii.Split(" ").ToList();
        foreach (var chu in danhSachCacChuAscii)
        {
            foreach (var number in chu.Select(chuCai => Utils.ChuyenChuCaiThanhSo(chuCai.ToString())))
            {
                numberFromCharacter.Add(number);
            }
        }
        
        var soLuongSoThieu = Utils.KhongTrongTapSoNguyen(numberFromCharacter);
        var sucManhTiemThuc = 9 - soLuongSoThieu.Count;
        return sucManhTiemThuc;
    }
    
    /// <summary>
    /// ĐAM MÊ: CÁC SỐ LẶP LẠI NHIỀU TRÊN HỌ VÀ TÊN
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static IEnumerable<int> TinhChiSoDamMe(this UserInfo userInfo)
    {
        var danhSachCacSo = new Dictionary<int, int>();
        
        var danhSachCacChuAscii = userInfo.HoVaTenAscii.Split(" ").ToList();
        foreach (var so in danhSachCacChuAscii.SelectMany(chu => chu.Select(chuCai => Utils.ChuyenChuCaiThanhSo(chuCai.ToString()))))
        {
            if (danhSachCacSo.ContainsKey(so))
                danhSachCacSo[so]++;
            else
                danhSachCacSo.Add(so, 1);
        }
        
        var giaTriLonNhat = danhSachCacSo.Values.Max();
        var danhSachSoLapLaiNhieuNhat = danhSachCacSo.Where(kv => kv.Value == giaTriLonNhat).Select(kv => kv.Key).ToList();
        return danhSachSoLapLaiNhieuNhat;
    }
    
    /// <summary>
    /// CHỈ SỐ THIẾU: LÀ CÁC SỐ KHÔNG XUẤT HIỆN TRÊN HỌ VÀ TÊN
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static IEnumerable<int> TinhChiSoThieu(this UserInfo userInfo)
    {
        var numberFromCharacter = new HashSet<int>();
        
        var danhSachCacChuAscii = userInfo.HoVaTenAscii.Split(" ").ToList();
        foreach (var chu in danhSachCacChuAscii)
        {
            foreach (var number in chu.Select(chuCai => Utils.ChuyenChuCaiThanhSo(chuCai.ToString())))
            {
                numberFromCharacter.Add(number);
            }
        }
        
        var soLuongSoThieu = Utils.KhongTrongTapSoNguyen(numberFromCharacter);
        return soLuongSoThieu;
    }

    /// <summary>
    /// NĂM CÁ NHÂN: TỔNG NGÀY THÁNG SINH VỚI NĂM HIỆN TẠI
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhNamCaNhan(this UserInfo userInfo)
    {
        var tongNgaySinh = userInfo.TongCacSoCuaNgaySinh;
        var tongThangSinh = userInfo.TongCacSoCuaThangSinh;
        var namVuTru = 2022;

        var tong = tongNgaySinh + tongThangSinh + namVuTru;
        while (tong >= 10 && !Utils.KiemTraTapBasic(tong))
            tong = Utils.TongCacChuSo(tong);

        return tong;
    }

    /// <summary>
    /// THÁNG CÁ NHÂN: TỔNG NĂM CÁ NHÂN VỚI THÁNG HIỆN TẠI
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="namCaNhan"></param>
    /// <returns></returns>
    public static int TinhThangCaNhan(this UserInfo userInfo, int namCaNhan)
    {
        var thangHienTai = DateTime.Now.Month;

        var tong = namCaNhan + thangHienTai;

        while (tong >= 10 && !Utils.KiemTraTapBasic(tong))
            tong = Utils.TongCacChuSo(tong);

        return tong;
    }

    /// <summary>
    /// NGÀY CÁ NHÂN: TỔNG THÁNG CÁ NHÂN VỚI NGÀY HIỆN TẠI
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="thangCaNhan"></param>
    /// <returns></returns>
    public static int TinhNgayCaNhan(this UserInfo userInfo, int thangCaNhan)
    {
        var ngayHienTai = DateTime.Now.Day;

        var tong = thangCaNhan + ngayHienTai;

        while (tong >= 10 && !Utils.KiemTraTapBasic(tong))
            tong = Utils.TongCacChuSo(tong);

        return tong;
    }

    /// <summary>
    /// CHẶNG 1: TỔNG NGÀY SINH VỚI THÁNG SINH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhChang1(this UserInfo userInfo)
    {
        var tong = userInfo.TongCacSoCuaNgaySinh + userInfo.TongCacSoCuaThangSinh;

        while (tong >= 10 && !Utils.KiemTraTapBasic(tong))
            tong = Utils.TongCacChuSo(tong);

        return tong;
    }

    /// <summary>
    /// CHẶNG 2: TỔNG NGÀY SINH VỚI NĂM SINH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhChang2(this UserInfo userInfo)
    {
        var tong = userInfo.TongCacSoCuaNgaySinh + userInfo.TongCacSoCuaNamSinh;

        while (tong >= 10 && !Utils.KiemTraTapBasic(tong))
            tong = Utils.TongCacChuSo(tong);

        return tong;
    }

    /// <summary>
    /// CHĂNG 3: TỔNG CHẶNG 1 VÀ CHẶNG 2
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="chang1"></param>
    /// <param name="chang2"></param>
    /// <returns></returns>
    public static int TinhChang3(this UserInfo userInfo, int chang1, int chang2)
    {
        var tong = chang1 + chang2;

        while (tong >= 10 && !Utils.KiemTraTapBasic(tong))
            tong = Utils.TongCacChuSo(tong);

        return tong;
    }

    /// <summary>
    /// CHẶNG 4: TỔNG THÁNG SINH VỚI NĂM SINH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhChang4(this UserInfo userInfo)
    {
        var tong = userInfo.TongCacSoCuaThangSinh + userInfo.TongCacSoCuaNamSinh;

        while (tong >= 10 && !Utils.KiemTraTapBasic(tong))
            tong = Utils.TongCacChuSo(tong);

        return tong;
    }

    /// <summary>
    /// THÁCH THỨC 1: HIỆU NGÀY SINH VỚI THÁNG SINH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhThachThuc1(this UserInfo userInfo)
    {
        var hieu = Math.Abs(userInfo.TongCacSoCuaNgaySinh - userInfo.TongCacSoCuaThangSinh);

        while (hieu >= 10 && !Utils.KiemTraTapBasic(hieu))
            hieu = Utils.TongCacChuSo(hieu);

        return hieu;
    }

    /// <summary>
    /// THÁCH THỨC 2: HIỆU NGÀY SINH VỚI NĂM SINH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhThachThuc2(this UserInfo userInfo)
    {
        var hieu = Math.Abs(userInfo.TongCacSoCuaNgaySinh - userInfo.TongCacSoCuaNamSinh);

        while (hieu >= 10 && !Utils.KiemTraTapBasic(hieu))
            hieu = Utils.TongCacChuSo(hieu);

        return hieu;
    }

    /// <summary>
    /// THÁCH THỨC 3: HIỆU THÁCH THỨC 1 VỚI THÁCH THỨC 2
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="thachThuc1"></param>
    /// <param name="thachThuc2"></param>
    /// <returns></returns>
    public static int TinhThachThuc3(this UserInfo userInfo, int thachThuc1, int thachThuc2)
    {
        var hieu = Math.Abs(thachThuc1 - thachThuc2);

        while (hieu >= 10 && !Utils.KiemTraTapBasic(hieu))
            hieu = Utils.TongCacChuSo(hieu);

        return hieu;
    }

    /// <summary>
    /// THÁCH THỨC 4: HIỆU CỦA THÁNG SINH VỚI NĂM SINH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhThachThuc4(this UserInfo userInfo)
    {
        var hieu = Math.Abs(userInfo.TongCacSoCuaThangSinh - userInfo.TongCacSoCuaNamSinh);

        while (hieu >= 10 && !Utils.KiemTraTapBasic(hieu))
            hieu = Utils.TongCacChuSo(hieu);

        return hieu;
    }

    // 17. SỐ THIẾU: CÁC SỐ KHÔNG XUẤT HIỆN TRONG HỌ VÀ TÊN
}