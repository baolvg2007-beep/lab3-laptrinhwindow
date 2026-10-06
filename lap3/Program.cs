using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace lap3
{
    internal class Program
    {
        private static readonly QuanLySinhVien qlsv = new QuanLySinhVien();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool isRunning = true;
            while (isRunning)
            {
                HienThiMenu();
                Console.Write("Chon chuc nang: ");
                string chon = Console.ReadLine();
                Console.WriteLine();

                switch (chon?.Trim())
                {
                    case "1":
                        ThucHienThemSinhVien();
                        break;
                    case "2":
                        ThucHienXuatDanhSach();
                        break;
                    case "3":
                        ThucHienTimTheoMa();
                        break;
                    case "4":
                        ThucHienTimTheoTen();
                        break;
                    case "5":
                        ThucHienSuaDiem();
                        break;
                    case "6":
                        ThucHienXoaSinhVien();
                        break;
                    case "7":
                        ThucHienSapXepTheoDiem();
                        break;
                    case "8":
                        ThucHienLocSinhVienDat();
                        break;
                    case "0":
                        isRunning = false;
                        Console.WriteLine("Cam on ban da su dung chuong trinh. Tam biet!");
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ! Vui lòng chọn từ 0 đến 8.");
                        break;
                }

                if (isRunning)
                {
                    Console.WriteLine("\nNhan phim bat ky de tiep tuc...");
                    if (!Console.IsInputRedirected)
                    {
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
        }

        private static void HienThiMenu()
        {
            Console.WriteLine("===== QUAN LY SINH VIEN =====");
            Console.WriteLine("1. Them sinh vien");
            Console.WriteLine("2. Xuat danh sach");
            Console.WriteLine("3. Tim sinh vien theo ma");
            Console.WriteLine("4. Tim sinh vien theo ten");
            Console.WriteLine("5. Sua diem trung binh");
            Console.WriteLine("6. Xoa sinh vien");
            Console.WriteLine("7. Sap xep theo diem giam dan");
            Console.WriteLine("8. Loc sinh vien dat");
            Console.WriteLine("0. Thoat");
        }

        private static void ThucHienThemSinhVien()
        {
            Console.WriteLine("--- THÊM SINH VIÊN MỚI ---");
            
            string maSV;
            while (true)
            {
                Console.Write("Nhập mã sinh viên: ");
                maSV = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(maSV))
                {
                    Console.WriteLine("Mã sinh viên không được để trống!");
                    continue;
                }

                if (qlsv.TimTheoMa(maSV) != null)
                {
                    Console.WriteLine($"Chương trình báo: Mã sinh viên '{maSV}' đã tồn tại! Vui lòng nhập mã khác.");
                    continue;
                }
                break;
            }

            string hoTen = NhapChuoiKhongRong("Nhập họ và tên: ");
            DateTime ngaySinh = NhapNgaySinh("Nhập ngày sinh (dd/MM/yyyy): ");
            string maLop = NhapChuoiKhongRong("Nhập mã lớp: ");
            double diemTB = NhapDiemTrungBinh("Nhập điểm trung bình (0 - 10): ");

            try
            {
                SinhVien sv = new SinhVien(maSV, hoTen, ngaySinh, maLop, diemTB);
                if (qlsv.Them(sv))
                {
                    Console.WriteLine("Thêm sinh viên thành công!");
                }
                else
                {
                    Console.WriteLine("Thêm sinh viên thất bại (Mã sinh viên đã tồn tại).");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm sinh viên: {ex.Message}");
            }
        }

        private static void ThucHienXuatDanhSach()
        {
            Console.WriteLine("--- DANH SÁCH SINH VIÊN ---");
            InDanhSach(qlsv.LayDanhSach());
        }

        private static void ThucHienTimTheoMa()
        {
            Console.WriteLine("--- TÌM SINH VIÊN THEO MÃ ---");
            Console.Write("Nhập mã sinh viên cần tìm: ");
            string maSV = Console.ReadLine()?.Trim();

            SinhVien sv = qlsv.TimTheoMa(maSV);
            if (sv != null)
            {
                Console.WriteLine("Kết quả tìm kiếm:");
                Console.WriteLine(sv.LayThongTin());
            }
            else
            {
                Console.WriteLine($"Không tìm thấy sinh viên có mã '{maSV}'.");
            }
        }

        private static void ThucHienTimTheoTen()
        {
            Console.WriteLine("--- TÌM SINH VIÊN THEO TÊN ---");
            Console.Write("Nhập từ khóa họ tên cần tìm: ");
            string tuKhoa = Console.ReadLine()?.Trim();

            List<SinhVien> ketQua = qlsv.TimTheoTen(tuKhoa);
            Console.WriteLine($"Tìm thấy {ketQua.Count} sinh viên phù hợp với từ khóa '{tuKhoa}':");
            InDanhSach(ketQua);
        }

        private static void ThucHienSuaDiem()
        {
            Console.WriteLine("--- SỬA ĐIỂM TRUNG BÌNH ---");
            Console.Write("Nhập mã sinh viên cần sửa điểm: ");
            string maSV = Console.ReadLine()?.Trim();

            SinhVien sv = qlsv.TimTheoMa(maSV);
            if (sv == null)
            {
                Console.WriteLine($"Không tìm thấy sinh viên có mã '{maSV}'.");
                return;
            }

            Console.WriteLine($"Thông tin hiện tại: {sv.LayThongTin()}");
            double diemMoi = NhapDiemTrungBinh("Nhập điểm trung bình mới (0 - 10): ");

            if (qlsv.SuaDiem(maSV, diemMoi))
            {
                Console.WriteLine("Cập nhật điểm trung bình thành công!");
                Console.WriteLine($"Thông tin sau cập nhật: {sv.LayThongTin()}");
            }
            else
            {
                Console.WriteLine("Cập nhật điểm thất bại!");
            }
        }

        private static void ThucHienXoaSinhVien()
        {
            Console.WriteLine("--- XÓA SINH VIÊN ---");
            Console.Write("Nhập mã sinh viên cần xóa: ");
            string maSV = Console.ReadLine()?.Trim();

            if (qlsv.Xoa(maSV))
            {
                Console.WriteLine($"Xóa sinh viên có mã '{maSV}' thành công!");
            }
            else
            {
                Console.WriteLine($"Không tìm thấy sinh viên có mã '{maSV}' để xóa.");
            }
        }

        private static void ThucHienSapXepTheoDiem()
        {
            Console.WriteLine("--- SẮP XẾP DANH SÁCH THEO ĐIỂM GIẢM DẦN ---");
            List<SinhVien> dsSapXep = qlsv.SapXepTheoDiemGiamDan();
            InDanhSach(dsSapXep);
        }

        private static void ThucHienLocSinhVienDat()
        {
            Console.WriteLine("--- DANH SÁCH SINH VIÊN ĐẠT (ĐTB >= 5.0) ---");
            List<SinhVien> dsDat = qlsv.LocSinhVienDat();
            InDanhSach(dsDat);
        }

        private static void InDanhSach(List<SinhVien> danhSach)
        {
            if (danhSach == null || danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách sinh viên trống.");
                return;
            }

            Console.WriteLine(new string('-', 95));
            foreach (var sv in danhSach)
            {
                Console.WriteLine(sv.LayThongTin());
            }
            Console.WriteLine(new string('-', 95));
            Console.WriteLine($"Tổng số sinh viên: {danhSach.Count}");
        }

        private static string NhapChuoiKhongRong(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                Console.WriteLine("Dữ liệu nhập không được để trống! Vui lòng nhập lại.");
            }
        }

        private static DateTime NhapNgaySinh(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim();
                if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngaySinh))
                {
                    return ngaySinh;
                }
                if (DateTime.TryParse(input, out ngaySinh))
                {
                    return ngaySinh;
                }
                Console.WriteLine("Ngày sinh không đúng định dạng (dd/MM/yyyy)! Vui lòng nhập lại.");
            }
        }

        private static double NhapDiemTrungBinh(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim();
                if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double diem) ||
                    double.TryParse(input, NumberStyles.Any, CultureInfo.CurrentCulture, out diem))
                {
                    if (diem >= 0.0 && diem <= 10.0)
                    {
                        return diem;
                    }
                }
                Console.WriteLine("Chương trình báo: Điểm không hợp lệ! Điểm phải là số từ 0 đến 10. Vui lòng nhập lại.");
            }
        }
    }
}
