using System;

namespace lap3
{
    public class SinhVien : Nguoi
    {
        public string MaSinhVien { get; set; }
        public string MaLop { get; set; }

        private double _diemTrungBinh;
        public double DiemTrungBinh
        {
            get => _diemTrungBinh;
            set
            {
                if (value < 0.0 || value > 10.0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Điểm trung bình phải nằm trong khoảng từ 0 đến 10.");
                }
                _diemTrungBinh = value;
            }
        }

        public SinhVien() : base()
        {
            MaSinhVien = string.Empty;
            MaLop = string.Empty;
            _diemTrungBinh = 0;
        }

        public SinhVien(string maSinhVien, string hoTen, DateTime ngaySinh, string maLop, double diemTrungBinh)
            : base(hoTen, ngaySinh)
        {
            MaSinhVien = maSinhVien;
            MaLop = maLop;
            DiemTrungBinh = diemTrungBinh;
        }

        public string XepLoai()
        {
            if (DiemTrungBinh >= 8.5) return "Xuất sắc";
            if (DiemTrungBinh >= 8.0) return "Giỏi";
            if (DiemTrungBinh >= 6.5) return "Khá";
            if (DiemTrungBinh >= 5.0) return "Trung bình";
            return "Yếu";
        }

        public override string LayThongTin()
        {
            return $"Mã SV: {MaSinhVien,-8} | Họ tên: {HoTen,-20} | Ngày sinh: {NgaySinh:dd/MM/yyyy} | Lớp: {MaLop,-8} | ĐTB: {DiemTrungBinh,5:F1} | Xếp loại: {XepLoai()}";
        }
    }
}
