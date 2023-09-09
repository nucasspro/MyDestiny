using MyDestiny.Models;

namespace MyDestiny.Extensions;

public static class UserExtension
{
    /// <summary>
    /// ĐƯƠNG ĐỜI: TỔNG NGÀY THÁNG NĂM SINH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhDuongDoi(this UserInfo userInfo)
    {
        var ketQua = userInfo.TongCacSoCuaNgaySinh + userInfo.TongCacSoCuaThangSinh + userInfo.TongCacSoCuaNamSinh;

        ketQua = Utils.TinhTong(ketQua, false);

        return ketQua;
    }

    /// <summary>
    /// SỨ MỆNH: TỔNG HỌ VÀ TÊN
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhChiSoSuMenh(this UserInfo userInfo)
    {
        var danhSachCacChuAscii = userInfo.DanhSachCacChuTrongTenAscii;

        var tongDiemCuaChu = new List<(string chu, int tong)>();
        foreach (var chu in danhSachCacChuAscii)
        {
            var tong = chu.Sum(x => Utils.ChuyenChuCaiThanhSo(x.ToString()));

            tong = Utils.TinhTong(tong, true);

            tongDiemCuaChu.Add((chu, tong));
        }

        var ketQua = tongDiemCuaChu.Sum(item => item.tong);
        ketQua = Utils.TinhTong(ketQua, true);
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
        var tongDuongDoi = Utils.TongCacChuSo(duongDoi);
        var tongSuMenh = Utils.TongCacChuSo(suMenh);

        var hieu = Math.Abs(tongDuongDoi - tongSuMenh);

        hieu = Utils.TinhTong(hieu, false);

        return hieu;
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
        var tong = Math.Abs(suMenh + duongDoi);
        tong = Utils.TinhTong(tong, true);
        return tong;
    }

    /// <summary>
    /// LINH HỒN: TỔNG NGUYÊN ÂM
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhChiSoLinhHon(this UserInfo userInfo)
    {
        var tong = 0;
        var danhSachCacChuCaiNguyenAm = new List<string>();

        var danhSachCacChuAscii = userInfo.DanhSachCacChuTrongTenAscii;
        foreach (var cacChuCaiNguyenAm in danhSachCacChuAscii.Select(Utils.LocCacChuNguyenAm))
        {
            var chuCaiNguyenAm = cacChuCaiNguyenAm.ToList();

            danhSachCacChuCaiNguyenAm.AddRange(chuCaiNguyenAm);

            var tongCacChuCaiNguyenAm = chuCaiNguyenAm.Sum(Utils.ChuyenChuCaiThanhSo);
            tongCacChuCaiNguyenAm = Utils.TinhTong(tongCacChuCaiNguyenAm, true);

            tong += tongCacChuCaiNguyenAm;
        }

        tong = Utils.TinhTong(tong, true);

        return tong;
    }

    /// <summary>
    /// NHÂN CÁCH: TỔNG PHỤ ÂM
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhChiSoNhanCach(this UserInfo userInfo)
    {
        var tong = 0;
        var danhSachCacChuCaiPhuAm = new List<string>();

        var danhSachCacChuAscii = userInfo.DanhSachCacChuTrongTenAscii;
        foreach (var cacChuCaiPhuAm in danhSachCacChuAscii.Select(Utils.LocCacChuPhuAm))
        {
            var chuCaiPhuAm = cacChuCaiPhuAm.ToList();

            danhSachCacChuCaiPhuAm.AddRange(chuCaiPhuAm);

            var tongCacChuCaiPhuAm = chuCaiPhuAm.Sum(Utils.ChuyenChuCaiThanhSo);
            tongCacChuCaiPhuAm = Utils.TinhTong(tongCacChuCaiPhuAm, true);

            tong += tongCacChuCaiPhuAm;
        }

        tong = Utils.TinhTong(tong, true);

        return tong;
    }

    /// <summary>
    /// LIÊN KẾT: HIỆU LINH HỒN VÀ NHÂN CÁCH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="linhHon"></param>
    /// <param name="nhanCach"></param>
    /// <returns></returns>
    public static int TinhChiSoLienKetLinhHoVaNhanCach(this UserInfo userInfo, int linhHon, int nhanCach)
    {
        var hieu = Math.Abs(linhHon - nhanCach);
        hieu = Utils.TinhTong(hieu, false);
        return hieu;
    }

    /// <summary>
    /// CÂN BẰNG: TỔNG CÁC CHỮ CÁI ĐẦU TIÊN CỦA HỌ VÀ TÊN
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhChiSoCanBang(this UserInfo userInfo)
    {
        var danhSachChuCaiDau = userInfo.DanhSachCacChuTrongTenAscii
            .Select(x => x[0].ToString())
            .ToList();

        var tong = danhSachChuCaiDau.Sum(Utils.ChuyenChuCaiThanhSo);
        tong = Utils.TinhTong(tong, false);
        return tong;
    }

    /// <summary>
    /// TƯ DUY LÝ TRÍ: TÊN + NGÀY SINH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhChiSoTuDuyLyTri(this UserInfo userInfo)
    {
        var ten = userInfo.DanhSachCacChuTrongTenAscii.Last();
        var tongTen = ten.Sum(x => Utils.ChuyenChuCaiThanhSo(x.ToString()));

        var tong = tongTen + userInfo.TongCacSoCuaNgaySinh;
        tong = Utils.TinhTong(tong, true);
        return tong;
    }

    /// <summary>
    /// SỨC MẠNH TIỀM THỨC: 9 - SỐ LƯỢNG SỐ THIẾU
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhChiSoSucManhTiemThuc(this UserInfo userInfo)
    {
        var numberFromCharacter = new HashSet<int>();

        var danhSachCacChuAscii = userInfo.DanhSachCacChuTrongTenAscii;
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

        var danhSachCacChuAscii = userInfo.DanhSachCacChuTrongTenAscii;
        foreach (var so in danhSachCacChuAscii.SelectMany(chu => chu.Select(chuCai => Utils.ChuyenChuCaiThanhSo(chuCai.ToString()))))
        {
            if (danhSachCacSo.ContainsKey(so))
                danhSachCacSo[so]++;
            else
                danhSachCacSo.Add(so, 1);
        }

        var giaTriLonNhat = danhSachCacSo.Values.Max();
        var danhSachSoLapLaiNhieuNhat = danhSachCacSo
            .Where(kv => kv.Value == giaTriLonNhat)
            .Select(kv => kv.Key)
            .ToList();

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

        var danhSachCacChuAscii = userInfo.DanhSachCacChuTrongTenAscii;
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
        tong = Utils.TinhTong(tong, false);
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
        tong = Utils.TinhTong(tong, false);
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
        tong = Utils.TinhTong(tong, false);
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
        tong = Utils.TinhTong(tong, false);
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
        tong = Utils.TinhTong(tong, false);
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
        tong = Utils.TinhTong(tong, false);
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
        tong = Utils.TinhTong(tong, true);
        return tong;
    }

    /// <summary>
    /// TUỔI CHẶNG 1: 36 TRỪ CHỈ SỐ ĐƯỜNG ĐỜI
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="duongDoi"></param>
    /// <returns></returns>
    public static int TinhTuoiChang1(this UserInfo userInfo, int duongDoi)
    {
        const int magicNumber = 36;
        var tongDuongDoi = Utils.TongCacChuSo(duongDoi);

        var hieu = Math.Abs(magicNumber - tongDuongDoi);
        return hieu;
    }

    /// <summary>
    /// TUỔI CHẶNG 2: 9 CỘNG TUỔI CHẶNG 1
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="tuoiChang1"></param>
    /// <returns></returns>
    public static int TinhTuoiChang2(this UserInfo userInfo, int tuoiChang1)
    {
        const int magicNumber = 9;
        var tong = tuoiChang1 + magicNumber;
        return tong;
    }

    /// <summary>
    /// TUỔI CHẶNG 3: 9 CỘNG TUỔI CHẶNG 2
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="tuoiChang2"></param>
    /// <returns></returns>
    public static int TinhTuoiChang3(this UserInfo userInfo, int tuoiChang2)
    {
        const int magicNumber = 9;
        var tong = tuoiChang2 + magicNumber;
        return tong;
    }

    /// <summary>
    /// TUỔI CHẶNG 4: 9 CỘNG TUỔI CHẶNG 3
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="tuoiChang3"></param>
    /// <returns></returns>
    public static int TinhTuoiChang4(this UserInfo userInfo, int tuoiChang3)
    {
        const int magicNumber = 9;
        var tong = tuoiChang3 + magicNumber;
        return tong;
    }

    /// <summary>
    /// NĂM CHẶNG 1: TUỔI CHẶNG 1 CỘNG NĂM SINH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="tuoiChang1"></param>
    /// <returns></returns>
    public static int TinhTuoiNamChang1(this UserInfo userInfo, int tuoiChang1)
    {
        var tong = userInfo.NgayThangNamSinh.Year + tuoiChang1;
        return tong;
    }

    /// <summary>
    /// NĂM CHẶNG 2: TUỔI CHẶNG 2 CỘNG NĂM SINH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="tuoiChang2"></param>
    /// <returns></returns>
    public static int TinhTuoiNamChang2(this UserInfo userInfo, int tuoiChang2)
    {
        var tong = userInfo.NgayThangNamSinh.Year + tuoiChang2;
        return tong;
    }

    /// <summary>
    /// NĂM CHẶNG 3: TUỔI CHẶNG 3 CỘNG NĂM SINH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="tuoiChang3"></param>
    /// <returns></returns>
    public static int TinhTuoiNamChang3(this UserInfo userInfo, int tuoiChang3)
    {
        var tong = userInfo.NgayThangNamSinh.Year + tuoiChang3;
        return tong;
    }

    /// <summary>
    /// NĂM CHẶNG 4: TUỔI CHẶNG 4 CỘNG NĂM SINH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="tuoiChang4"></param>
    /// <returns></returns>
    public static int TinhTuoiNamChang4(this UserInfo userInfo, int tuoiChang4)
    {
        var tong = userInfo.NgayThangNamSinh.Year + tuoiChang4;
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
        hieu = Utils.TinhTong(hieu, false);
        return hieu;
    }

    /// <summary>
    /// THÁCH THỨC 2: HIỆU NGÀY SINH VỚI NĂM SINH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhThachThuc2(this UserInfo userInfo)
    {
        var tongNgaySinh = userInfo.TongCacSoCuaNgaySinh;
        tongNgaySinh = Utils.TinhTong(tongNgaySinh, false);

        var tongNamSinh = userInfo.TongCacSoCuaNamSinh;
        tongNamSinh = Utils.TinhTong(tongNamSinh, false);

        var hieu = Math.Abs(tongNgaySinh - tongNamSinh);
        hieu = Utils.TinhTong(hieu, false);

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
        hieu = Utils.TinhTong(hieu, false);
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
        hieu = Utils.TinhTong(hieu, false);
        return hieu;
    }

    /// <summary>
    /// CHỈ SỐ NGÀY SINH: TỔNG NGÀY SINH
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static int TinhChiSoNgaySinh(this UserInfo userInfo)
    {
        var tong = userInfo.TongCacSoCuaNgaySinh;
        tong = Utils.TinhTong(tong, false);
        return tong;
    }
}