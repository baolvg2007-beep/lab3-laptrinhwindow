using System;
using System.Collections.Generic;
using System.Linq;

namespace lap3
{
    public class QuanLySinhVien
    {
        private readonly List<SinhVien> _danhSachSinhVien;

        public QuanLySinhVien()
        {
            _danhSachSinhVien = new List<SinhVien>();
        }

        public List<SinhVien> LayDanhSach()
        {
            return _danhSachSinhVien;
        }

        public bool Them(SinhVien sv)
        {
            if (sv == null) return false;

            if (_danhSachSinhVien.Any(s => s.MaSinhVien.Equals(sv.MaSinhVien, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            _danhSachSinhVien.Add(sv);
            return true;
        }

        public SinhVien TimTheoMa(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV)) return null;
            return _danhSachSinhVien.FirstOrDefault(s => s.MaSinhVien.Equals(maSV, StringComparison.OrdinalIgnoreCase));
        }

        public List<SinhVien> TimTheoTen(string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa)) return new List<SinhVien>();
            return _danhSachSinhVien
                .Where(s => s.HoTen.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        public bool SuaDiem(string maSV, double diemMoi)
        {
            var sv = TimTheoMa(maSV);
            if (sv == null) return false;

            sv.DiemTrungBinh = diemMoi;
            return true;
        }

        public bool Xoa(string maSV)
        {
            var sv = TimTheoMa(maSV);
            if (sv == null) return false;

            return _danhSachSinhVien.Remove(sv);
        }

        public List<SinhVien> SapXepTheoDiemGiamDan()
        {
            return _danhSachSinhVien.OrderByDescending(s => s.DiemTrungBinh).ToList();
        }

        public List<SinhVien> LocSinhVienDat()
        {
            return _danhSachSinhVien.Where(s => s.DiemTrungBinh >= 5.0).ToList();
        }
    }
}
